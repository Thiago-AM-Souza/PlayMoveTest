using BuildingBlocks.CQRS;
using BuildingBlocks.Errors;
using OneOf;
using Usuario.Application.Dtos;

namespace Usuario.Application.Commands
{
    public record AutenticarUsuarioCommand(UserDto User) 
        : ICommand<OneOf<AutenticarUsuarioResult, AppError>>;

    public record AutenticarUsuarioResult(string Token);

}
