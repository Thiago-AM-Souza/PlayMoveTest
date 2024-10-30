using System.Text.RegularExpressions;

namespace Fornecedores.Domain.Models
{
    public class Telefone : Entity
    {
        public string Numero { get; private set; } = default!;
        public Guid FornecedorId { get; private set; }
        public Fornecedor Fornecedor { get; private set; }

        protected Telefone() {}

        public Telefone(string numero,
                        Fornecedor fornecedor)
        {
            Numero = numero.Replace("(","").Replace(")", "").Replace("-", "").Trim();
            Fornecedor = fornecedor;
            FornecedorId = fornecedor.Id;
        }

        public void Alterar(string numero)
        {
            Numero = numero;
        }

        public static OneOf<bool, AppError> ValidarTelefone(string telefone)
        {
            var validar = Regex.IsMatch(telefone, @"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$");

            if (!validar)
            {
                return new ErroFormatoIncorreto("Por favor verifique o formato do telefone.", ErrorType.Validation);
            }

            return true;
        }
    }
}
