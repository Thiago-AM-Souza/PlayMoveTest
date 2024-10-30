
namespace Fornecedores.Application.Fornecedores.Commands.CreateFornecedor
{
    internal class CreateFornecedorHandler(IFornecedorRepository repository)
        : ICommandHandler<CreateFornecedorCommand, OneOf<CreateFornecedorResult, AppError>>
    {
        public async Task<OneOf<CreateFornecedorResult, AppError>> Handle(CreateFornecedorCommand command, CancellationToken cancellationToken)
        {
            var cnpj = command.Fornecedor.Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            var validarCnpj = Cnpj.Validacoes(cnpj);

            if (validarCnpj.IsError())
            {
                return validarCnpj.GetErrorResult();
            }

            var fornecedorExistente = await repository.GetByCnpj(cnpj);

            if (fornecedorExistente != null) 
            {
                return new ErroFornecedorExistente("Fornecedor já está cadastrado no sistema.", ErrorType.BusinessRule);
            }

            var fornecedor = CreateFornecedor(command.Fornecedor);

            await repository.Create(fornecedor);

            await repository.UnitOfWork.Commit();

            return new CreateFornecedorResult(fornecedor.Id);
        }

        private Fornecedor CreateFornecedor(CreateFornecedorDto fornecedorDto)
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
