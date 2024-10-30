
using BuildingBlocks.Extensions;
using BuildingBlocks.Responses;
using Fornecedores.Application.Dtos;
using Fornecedores.Application.Fornecedores.Commands.AtualizarFornecedor;
using Microsoft.AspNetCore.Mvc;

namespace Fornecedores.API.Endpoints.Fornecedor
{
    public record UpdateFornecedorRequest(UpdateFornecedorDto Fornecedor);

    public record UpdateFornecedorResponse(bool IsSuccess);

    public class UpdateFornecedor : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/fornecedores/{id}", async (Guid id, UpdateFornecedorRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateFornecedorCommand>();

                var result = await sender.Send(command);

                if (result.IsSuccess())
                {
                    var response = result.GetSuccessResult().Adapt<UpdateFornecedorResponse>();

                    return Results.Ok(response);
                }

                return result.GetErrorResult().Response();
            })
            .RequireAuthorization("Fornecedor")
            .WithGroupName("Fornecedor")
            .WithSummary("Update Fornecedor")
            .WithDescription("Update Fornecedor")
            .Produces<UpdateFornecedorResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
