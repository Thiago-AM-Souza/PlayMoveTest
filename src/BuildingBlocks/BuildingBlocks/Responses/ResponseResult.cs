using BuildingBlocks.Errors;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Responses
{
    public static class ResponseResult
    {
        public static IResult Response(this AppError appError)
        {
            return appError.ErrorType switch
            {
                ErrorType.BusinessRule => Results.Conflict(new { error = appError.ErrorType.ToString(), details = appError.Details }),
                ErrorType.Validation => Results.BadRequest(new { error = appError.ErrorType.ToString(), details = appError.Details }),
                ErrorType.NotFound => Results.NotFound(new { error = appError.ErrorType.ToString(), details = appError.Details }),
                _ => Results.Problem("Erro interno no servidor")
            };
        }
    }
}
