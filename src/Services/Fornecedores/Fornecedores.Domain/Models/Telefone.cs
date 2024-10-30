using BuildingBlocks.DomainObjects;
using Fornecedores.Domain.ValueObjects;
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
            Numero = numero;
            Fornecedor = fornecedor;
            FornecedorId = fornecedor.Id;
        }

        private Telefone(string numero)
        {
            Numero = numero;
        }

        public void Alterar(string numero)
        {
            Numero = numero;
        }

        public static bool Verificar(string telefone, out Telefone tel)
        {
            tel = null;

            if (Validar(telefone))
            {
                tel = new Telefone(telefone);
                return true;
            }

            return false;
        }

        private static bool Validar(string telefone)
        {
            return Regex.IsMatch(telefone, @"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$");
        }
    }
}
