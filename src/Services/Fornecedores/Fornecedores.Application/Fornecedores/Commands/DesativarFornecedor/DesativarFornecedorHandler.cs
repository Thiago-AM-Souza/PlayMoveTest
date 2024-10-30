namespace Fornecedores.Application.Fornecedores.Commands.DesativarFornecedor
{
    internal class DesativarFornecedorHandler(IFornecedorRepository repository)
        : ICommandHandler<DesativarFornecedorCommand, OneOf<DesativarFornecedorResult, AppError>>
    {
        public async Task<OneOf<DesativarFornecedorResult, AppError>> Handle(DesativarFornecedorCommand command, CancellationToken cancellationToken)
        {
            var fornecedor = await repository.GetById(command.Id);

            if (fornecedor == null)
            {
                return new ErroNaoEncontrado("Não foi encontrado fornecedor com o Id informado", ErrorType.NotFound);
            }

            fornecedor.Desativar();
            
            repository.Update(fornecedor);

            await repository.UnitOfWork.Commit();

            return new DesativarFornecedorResult(true);
        }
    }
}
