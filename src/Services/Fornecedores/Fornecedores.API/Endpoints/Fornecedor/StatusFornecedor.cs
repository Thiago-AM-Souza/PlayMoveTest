using Fornecedores.Application.Fornecedores.Commands.AtivarFornecedor;
using Fornecedores.Application.Fornecedores.Commands.DesativarFornecedor;

namespace Fornecedores.API.Endpoints.Fornecedor
{
    public record StatusFornecedorResponse(bool IsSuccess);

    public class StatusFornecedor : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPatch("/fornecedores/ativar/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new AtivarFornecedorCommand(id));

                if (result.IsSuccess())
                {
                    var response = result.GetSuccessResult().Adapt<StatusFornecedorResponse>();

                    return Results.Ok(response);
                }

                return result.GetErrorResult().Response();
            })
            .RequireAuthorization("Fornecedor")
            .WithGroupName("Fornecedor")
            .WithSummary("Ativar Fornecedor")
            .WithDescription("Ativa um fornecedor novamente no sistema.")
            .Produces<StatusFornecedorResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

            app.MapPatch("/fornecedores/desativar/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DesativarFornecedorCommand(id));

                if (result.IsSuccess())
                {
                    var response = result.GetSuccessResult().Adapt<StatusFornecedorResponse>();

                    return Results.Ok(response);
                }

                return result.GetErrorResult().Response();
            })
            .RequireAuthorization("Fornecedor")
            .WithGroupName("Fornecedor")
            .WithSummary("Desativar Fornecedor")
            .WithDescription("Desativar fornecedor colocando-o em um estado de exclusão.")
            .Produces<StatusFornecedorResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);
        }
    }
}
