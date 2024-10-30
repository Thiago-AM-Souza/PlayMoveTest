using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Fornecedores.Application.Data;
using Fornecedores.Application.Dtos;
using Fornecedores.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fornecedores.Application.Fornecedores.Queries.GetFornecedores
{
    internal class GetFornecedoresHandler(IFornecedorRepository repository)
        : IQueryHandler<GetFornecedoresQuery, GetFornecedoresResult>
    {
        public async Task<GetFornecedoresResult> Handle(GetFornecedoresQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var fornecedores = await repository.GetAll(query.desativado);

            var totalCount = fornecedores.LongCount();

            fornecedores = fornecedores.Skip(pageSize * pageIndex)
                                       .Take(pageSize)
                                       .ToList();

            return new GetFornecedoresResult(
                new PaginatedResult<FornecedorDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    fornecedores.ToDtoList()));
        }
    }
}
