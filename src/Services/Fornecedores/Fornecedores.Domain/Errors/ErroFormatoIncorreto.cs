namespace Fornecedores.Domain.Errors
{
    public record ErroFormatoIncorreto : AppError
    {
        public ErroFormatoIncorreto(string Details, ErrorType ErrorType) : base(Details, ErrorType)
        {
        }
    }
}
