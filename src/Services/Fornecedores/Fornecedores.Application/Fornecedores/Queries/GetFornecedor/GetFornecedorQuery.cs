namespace Fornecedores.Application.Fornecedores.Queries.GetFornecedor
{
    public record GetFornecedorQuery(Guid Id) : IQuery<GetFornecedorResult>;

    public record GetFornecedorResult(FornecedorDto? Fornecedor);
}
