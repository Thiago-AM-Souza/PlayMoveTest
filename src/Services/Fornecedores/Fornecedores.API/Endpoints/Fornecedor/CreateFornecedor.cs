using BuildingBlocks.Extensions;
using BuildingBlocks.Responses;
using Fornecedores.Application.Dtos;
using Fornecedores.Application.Fornecedores.Commands.CreateFornecedor;

namespace Fornecedores.API.Endpoints.Fornecedor
{
    public record CreateFornecedorRequest(CreateFornecedorDto Fornecedor);

    public record CreateFornecedorResponse(Guid Id);

    public class CreateFornecedor : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/fornecedores", async (CreateFornecedorRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateFornecedorCommand>();

                var result = await sender.Send(command);

                if (result.IsSuccess())
                {
                    var response = result.GetSuccessResult().Adapt<CreateFornecedorResponse>();

                    return Results.Created($"/fornecedor/{response.Id}", response);
                }

                return result.GetErrorResult().Response();

            })
            .RequireAuthorization("Fornecedor")
            .WithGroupName("Fornecedor")
            .WithSummary("Inserir Fornecedor")
            .WithDescription("Endpoint para inserir um novo fornecedor.")
            .Produces<CreateFornecedorResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
        }
    }
}
