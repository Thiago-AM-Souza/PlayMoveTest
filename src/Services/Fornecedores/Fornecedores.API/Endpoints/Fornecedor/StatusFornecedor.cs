
using BuildingBlocks.Extensions;
using BuildingBlocks.Responses;
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
            .WithDescription("Ativar Fornecedor")
            .Produces<StatusFornecedorResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);

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
            .Produces<StatusFornecedorResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Desativar Fornecedor")
            .WithDescription("Desativar Fornecedor");
        }
    }
}
