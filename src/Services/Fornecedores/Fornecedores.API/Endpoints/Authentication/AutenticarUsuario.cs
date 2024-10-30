using BuildingBlocks.Extensions;
using BuildingBlocks.Responses;
using Usuario.Application.Commands;
using Usuario.Application.Dtos;

namespace Fornecedores.API.Endpoints.Authentication
{
    public record AutenticacaoRequest(UserDto User);

    public record AutenticacaoResponse(string Token);

    public class AutenticarUsuario : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/autenticar", async (AutenticacaoRequest request, ISender sender) =>
            {
                var command = request.Adapt<AutenticarUsuarioCommand>();

                var result = await sender.Send(command);

                if (result.IsSuccess())
                {
                    var response = result.GetSuccessResult().Adapt<AutenticacaoResponse>();

                    return Results.Content(response.Token);
                }

                return result.GetErrorResult().Response();
            })
            .WithGroupName("Autenticar")
            .WithSummary("Autenticar")
            .WithDescription("Este endpoint permite que um usuário faça login na aplicação, retornando um token JWT. \n\n" + description)
            .Produces<AutenticacaoResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }

        const string description = @"{""user"":{""name"": ""admin"",""password"": ""admin""}}";
    }
}
