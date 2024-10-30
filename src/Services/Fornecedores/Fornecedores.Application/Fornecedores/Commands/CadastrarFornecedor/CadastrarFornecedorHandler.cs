namespace Fornecedores.Application.Fornecedores.Commands.CadastrarFornecedor
{
    internal class CadastrarFornecedorHandler
        : ICommandHandler<CadastrarFornecedorCommand, OneOf<CadastrarFornecedorResult, AppError>>
    {
        private readonly IFornecedorRepository _repository;

        public CadastrarFornecedorHandler(IFornecedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<OneOf<CadastrarFornecedorResult, AppError>> Handle(CadastrarFornecedorCommand command, CancellationToken cancellationToken)
        {
            var dtoValido = await Validacoes(command.Fornecedor);

            if (dtoValido.IsError())
            {
                return dtoValido.GetErrorResult();
            }

            var fornecedor = CriarFornecedor(command.Fornecedor);

            await _repository.Create(fornecedor);

            await _repository.UnitOfWork.Commit();

            return new CadastrarFornecedorResult(fornecedor.Id);
        }

        private async Task<OneOf<bool, AppError>> Validacoes(CadastrarFornecedorDto fornecedorDto)
        {
            var cnpj = fornecedorDto.Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            var validarCnpj = Cnpj.Validacoes(cnpj);

            if (validarCnpj.IsError())
            {
                return validarCnpj.GetErrorResult();
            }

            foreach (var telefone in fornecedorDto.Telefones)
            {
                var validarTelefone = Telefone.ValidarTelefone(telefone.Numero);

                if (validarTelefone.IsError())
                    return validarTelefone.GetErrorResult();
            }

            var validarEstado = Endereco.ValidarEstado(fornecedorDto.Endereco.Estado);

            if (validarEstado.IsError())
            {
                return validarEstado.GetErrorResult();
            }

            var validarCep = Endereco.ValidarCep(fornecedorDto.Endereco.Cep);

            if (validarCep.IsError())
            {
                return validarCep.GetErrorResult();
            }

            var fornecedorExistente = await _repository.GetByCnpj(cnpj);

            if (fornecedorExistente != null)
            {
                return new ErroFornecedorExistente("Fornecedor já está cadastrado no sistema.", ErrorType.BusinessRule);
            }

            return true;
        }

        private Fornecedor CriarFornecedor(CadastrarFornecedorDto fornecedorDto)
        {
            var endereco = new Endereco(fornecedorDto.Endereco.Logradouro,
                                        fornecedorDto.Endereco.Numero,
                                        fornecedorDto.Endereco.Cep,
                                        fornecedorDto.Endereco.Cidade,
                                        fornecedorDto.Endereco.Estado);            

            var fornecedor = new Fornecedor(fornecedorDto.NomeFantasia,
                                            fornecedorDto.RazaoSocial,
                                            fornecedorDto.Email,
                                            new Cnpj(fornecedorDto.Cnpj),
                                            endereco);

            foreach (var telefoneDto in fornecedorDto.Telefones)
            {
                var telefone = new Telefone(telefoneDto.Numero,
                                            fornecedor);

                fornecedor.AdicionarTelefone(telefone);
            }

            return fornecedor;

        }
    }
}
