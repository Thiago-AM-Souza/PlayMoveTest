using BuildingBlocks.Errors;
using MediatR;
using OneOf;

namespace BuildingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
