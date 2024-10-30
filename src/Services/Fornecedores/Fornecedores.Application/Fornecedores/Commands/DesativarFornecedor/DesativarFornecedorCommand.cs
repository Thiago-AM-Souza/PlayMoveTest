namespace Fornecedores.Application.Fornecedores.Commands.DesativarFornecedor
{
    public record DesativarFornecedorCommand(Guid Id) 
        : ICommand<OneOf<DesativarFornecedorResult, AppError>>;

    public record DesativarFornecedorResult(bool IsSuccess);
}
