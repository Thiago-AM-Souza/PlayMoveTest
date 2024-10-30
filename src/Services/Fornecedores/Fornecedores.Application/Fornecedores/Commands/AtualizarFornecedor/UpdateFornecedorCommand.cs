namespace Fornecedores.Application.Fornecedores.Commands.AtualizarFornecedor
{
    public record UpdateFornecedorCommand(UpdateFornecedorDto Fornecedor)
        : ICommand<OneOf<UpdateFornecedorResult, AppError>>;

    public record UpdateFornecedorResult(bool IsSuccess);
}
