namespace BuildingBlocks.Errors
{
    public record ErroNaoEncontrado : AppError
    {
        public ErroNaoEncontrado(string Details, ErrorType ErrorType) : base(Details, ErrorType)
        {
        }
    }
}
