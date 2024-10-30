
namespace Fornecedores.Application.Fornecedores.Commands.CreateFornecedor
{
    public record CreateFornecedorCommand(CreateFornecedorDto Fornecedor) 
        : ICommand<OneOf<CreateFornecedorResult, AppError>>;

    public record CreateFornecedorResult(Guid Id);
}
