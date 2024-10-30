using BuildingBlocks.Pagination;

namespace Fornecedores.Application.Fornecedores.Queries.GetFornecedores
{


    public record GetFornecedoresQuery(PaginationRequest PaginationRequest, bool? desativado) 
        : IQuery<GetFornecedoresResult>;

    public record GetFornecedoresResult(PaginatedResult<FornecedorDto> Fornecedores);

}
