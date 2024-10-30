
namespace Fornecedores.Application.Fornecedores.Commands.CadastrarFornecedor
{
    public record CadastrarFornecedorCommand(CadastrarFornecedorDto Fornecedor) 
        : ICommand<OneOf<CadastrarFornecedorResult, AppError>>;

    public record CadastrarFornecedorResult(Guid Id);
}
