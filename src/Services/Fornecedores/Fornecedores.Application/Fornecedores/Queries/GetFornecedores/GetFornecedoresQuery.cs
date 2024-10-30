using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Fornecedores.Application.Dtos;

namespace Fornecedores.Application.Fornecedores.Queries.GetFornecedores
{


    public record GetFornecedoresQuery(PaginationRequest PaginationRequest, bool? desativado) 
        : IQuery<GetFornecedoresResult>;

    public record GetFornecedoresResult(PaginatedResult<FornecedorDto> Fornecedores);

}
