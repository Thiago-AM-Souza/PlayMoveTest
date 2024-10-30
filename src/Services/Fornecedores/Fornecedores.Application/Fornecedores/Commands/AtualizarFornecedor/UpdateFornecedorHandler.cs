using Fornecedores.Domain.Errors.Fornecedor;

namespace Fornecedores.Application.Fornecedores.Commands.AtualizarFornecedor
{
    internal class UpdateFornecedorHandler(IFornecedorRepository repository)
        : ICommandHandler<UpdateFornecedorCommand, OneOf<UpdateFornecedorResult, AppError>>
    {
        public async Task<OneOf<UpdateFornecedorResult, AppError>> Handle(UpdateFornecedorCommand command, CancellationToken cancellationToken)
        {
            var cnpj = command.Fornecedor.Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            var validarCnpj = Cnpj.Validacoes(cnpj);

            if (validarCnpj.IsError())
            {
                return validarCnpj.GetErrorResult();
            }

            var fornecedor = await repository.GetById(command.Fornecedor.Id);

            if (fornecedor == null)
            {
                return new ErroFornecedorExistente("Não foi encontrado fornecedor com o Id informado.", ErrorType.NotFound);
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

            return new UpdateFornecedorResult(true);
        }
    }
}
