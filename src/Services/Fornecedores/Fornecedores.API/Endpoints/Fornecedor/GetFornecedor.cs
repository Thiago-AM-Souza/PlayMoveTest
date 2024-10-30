using Fornecedores.Application.Dtos;
using Fornecedores.Application.Fornecedores.Queries.GetFornecedor;

namespace Fornecedores.API.Endpoints.Fornecedor
{
    public record GetFornecedorResponse(FornecedorDto Fornecedor);

    public class GetFornecedor : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/fornecedores/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetFornecedorQuery(id));

                var response = result.Adapt<GetFornecedorResponse>();

                return Results.Ok(response);
            })
            .RequireAuthorization("Fornecedor")
            .WithGroupName("Fornecedor")
            .WithSummary("Obter Fornecedor por Id")
            .WithDescription("Endpoint")
            .Produces<GetFornecedorResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
