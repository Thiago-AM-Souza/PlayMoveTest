using Fornecedores.Application.Fornecedores.Commands.CadastrarFornecedor;

namespace Fornecedores.API.Endpoints.Fornecedor
{
    public record CadastrarFornecedorRequest(CadastrarFornecedorDto Fornecedor);

    public record CadastrarFornecedorResponse(Guid Id);

    public class CadastrarFornecedor : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/fornecedores", async (CadastrarFornecedorRequest request, ISender sender) =>
            {
                var command = request.Adapt<CadastrarFornecedorCommand>();

                var result = await sender.Send(command);

                if (result.IsSuccess())
                {
                    var response = result.GetSuccessResult().Adapt<CadastrarFornecedorResponse>();

                    return Results.Created($"/fornecedor/{response.Id}", response);
                }

                return result.GetErrorResult().Response();

            })
            .RequireAuthorization("Fornecedor")
            .WithGroupName("Fornecedor")
            .WithSummary("Cadastrar Fornecedor")
            .WithDescription("Endpoint para cadastrar um novo fornecedor.")
            .Produces<CadastrarFornecedorResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);
        }
    }
}
