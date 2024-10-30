using Fornecedores.Application.Fornecedores.Commands.AtualizarFornecedor;

namespace Fornecedores.API.Endpoints.Fornecedor
{
    public record AtualizarFornecedorRequest(AtualizarFornecedorDto Fornecedor);

    public record AtualizarFornecedorResponse(bool IsSuccess);

    public class AtualizarFornecedor : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/fornecedores/{id}", async (Guid id, AtualizarFornecedorRequest request, ISender sender) =>
            {
                var command = request.Adapt<AtualizarFornecedorCommand>();

                var result = await sender.Send(command);

                if (result.IsSuccess())
                {
                    var response = result.GetSuccessResult().Adapt<AtualizarFornecedorResponse>();

                    return Results.Ok(response);
                }

                return result.GetErrorResult().Response();
            })
            .RequireAuthorization("Fornecedor")
            .WithGroupName("Fornecedor")
            .WithSummary("Atualizar Fornecedor")
            .WithDescription("Atualizar Fornecedor")
            .Produces<AtualizarFornecedorResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);
        }
    }
}
