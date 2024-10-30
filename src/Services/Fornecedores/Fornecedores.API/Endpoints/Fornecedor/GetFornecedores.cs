
using BuildingBlocks.Pagination;
using Fornecedores.Application.Dtos;
using Fornecedores.Application.Fornecedores.Queries.GetFornecedores;

namespace Fornecedores.API.Endpoints.Fornecedor
{
    public record GetFornecedoresResponse(PaginatedResult<FornecedorDto> Fornecedores);

    public class GetFornecedores : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/fornecedores", async ([AsParameters] PaginationRequest request, bool? desativado, ISender sender) =>
            {
                var result = await sender.Send(new GetFornecedoresQuery(request, desativado));

                var response = result.Adapt<GetFornecedoresResponse>();

                if (response.Fornecedores.Data.Count() > 0)
                {
                    return Results.Ok(response);
                }

                return Results.NoContent();
            })
            .RequireAuthorization("Fornecedor")
            .WithGroupName("Fornecedor")
            .WithSummary("Obter todos os Fornecedores")
            .WithDescription("Obter todos os fornecedores, é possivel passar um parâmetro 'desativado' para retornar apenas fornecedores ativos.")
            .Produces<GetFornecedoresResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
        }
    }
}
