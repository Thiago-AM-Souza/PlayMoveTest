namespace Fornecedores.Domain.Errors.Cnpj
{
    public record ErroCnpjNumeroInvalido : AppError
    {
        public ErroCnpjNumeroInvalido(string Details, ErrorType ErrorType) : base(Details, ErrorType)
        {
        }
    }
}
