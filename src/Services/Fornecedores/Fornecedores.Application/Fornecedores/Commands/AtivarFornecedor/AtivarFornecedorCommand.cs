namespace Fornecedores.Application.Fornecedores.Commands.AtivarFornecedor
{
    public record AtivarFornecedorCommand(Guid Id) : ICommand<OneOf<AtivarFornecedorResult, AppError>>;

    public record AtivarFornecedorResult(bool IsSuccess);
}
