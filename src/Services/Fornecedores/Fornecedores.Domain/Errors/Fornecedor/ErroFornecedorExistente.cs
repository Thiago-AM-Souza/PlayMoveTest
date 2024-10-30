namespace Fornecedores.Domain.Errors.Fornecedor
{
    public record ErroFornecedorExistente : AppError
    {
        public ErroFornecedorExistente(string Details, ErrorType ErrorType) : base(Details, ErrorType)
        {
        }
    }
}
