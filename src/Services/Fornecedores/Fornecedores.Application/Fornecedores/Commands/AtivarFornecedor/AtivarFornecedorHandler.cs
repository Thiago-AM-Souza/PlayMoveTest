namespace Fornecedores.Application.Fornecedores.Commands.AtivarFornecedor
{
    internal class AtivarFornecedorHandler(IFornecedorRepository repository)
        : ICommandHandler<AtivarFornecedorCommand, OneOf<AtivarFornecedorResult, AppError>>
    {
        public async Task<OneOf<AtivarFornecedorResult, AppError>> Handle(AtivarFornecedorCommand command, CancellationToken cancellationToken)
        {
            var fornecedor = await repository.GetById(command.Id);

            if (fornecedor == null)
            {
                return new ErroNaoEncontrado("Não foi encontrado fornecedor com o Id informado", ErrorType.NotFound);
            }

            fornecedor.Ativar();            

            repository.Update(fornecedor);

            await repository.UnitOfWork.Commit();

            return new AtivarFornecedorResult(true);
        }
    }
}
