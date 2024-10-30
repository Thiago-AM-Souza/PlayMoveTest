namespace Fornecedores.Application.Fornecedores.Commands.AtualizarFornecedor
{
    public record AtualizarFornecedorCommand(AtualizarFornecedorDto Fornecedor)
        : ICommand<OneOf<AtualizarFornecedorResult, AppError>>;

    public record AtualizarFornecedorResult(bool IsSuccess);
}
