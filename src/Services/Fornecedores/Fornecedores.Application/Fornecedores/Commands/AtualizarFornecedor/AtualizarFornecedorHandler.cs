namespace Fornecedores.Application.Fornecedores.Commands.AtualizarFornecedor
{
    internal class AtualizarFornecedorHandler(IFornecedorRepository repository)
        : ICommandHandler<AtualizarFornecedorCommand, OneOf<AtualizarFornecedorResult, AppError>>
    {
        public async Task<OneOf<AtualizarFornecedorResult, AppError>> Handle(AtualizarFornecedorCommand command, CancellationToken cancellationToken)
        {
            var dtoValido = await Validacoes(command.Fornecedor);

            if (dtoValido.IsError())
            {
                return dtoValido.GetErrorResult();
            }

            var fornecedor = await repository.GetById(command.Fornecedor.Id);

            if (fornecedor == null)
            {
                return new ErroFornecedorExistente("Não foi encontrado fornecedor com o Id informado.", ErrorType.NotFound);
            }

            var fornecedorCnpjExiste = await repository.GetByCnpj(command.Fornecedor.Cnpj);

            if (fornecedorCnpjExiste.Id != fornecedor.Id)
            {
                return new ErroFornecedorExistente("Já existe um outro fornecedor cadastrado com esse cnpj.", ErrorType.BusinessRule);
            }

            fornecedor.Alterar(command.Fornecedor.NomeFantasia,
                               command.Fornecedor.RazaoSocial,
                               command.Fornecedor.Email,
                               command.Fornecedor.Cnpj);

            var telefonesRemover = fornecedor.Telefones.Where(tel =>
                                                !command.Fornecedor.Telefones
                                                    .Any(telDto => telDto.Id == tel.Id))
                                                    .ToList();

            telefonesRemover.ForEach(x => fornecedor.RemoverTelefone(x.Id));

            foreach (var telDto in command.Fornecedor.Telefones)
            {
                var telefoneExistente = fornecedor.Telefones.FirstOrDefault(t => t.Id == telDto.Id);

                if (telefoneExistente == null)
                {
                    var telefone = new Telefone(telDto.Numero, fornecedor);
                    fornecedor.AdicionarTelefone(telefone);
                    await repository.AdicionarTelefone(telefone);
                }
                else
                {
                    if (telefoneExistente.Numero != telDto.Numero)
                        telefoneExistente.Alterar(telDto.Numero);
                }                
            }

            repository.Update(fornecedor);

            await repository.UnitOfWork.Commit();

            return new AtualizarFornecedorResult(true);
        }

        private async Task<OneOf<bool, AppError>> Validacoes(AtualizarFornecedorDto fornecedorDto)
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

            return true;
        }
    }
}
