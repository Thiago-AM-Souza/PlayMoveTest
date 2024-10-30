namespace BuildingBlocks.Errors
{
    public abstract record AppError(string Details, ErrorType ErrorType);

    public enum ErrorType
    {
        BusinessRule,
        Validation,
        NotFound
    }
}
