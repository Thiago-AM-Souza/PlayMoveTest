using System.Text.RegularExpressions;

namespace Fornecedores.Domain.ValueObjects
{
    public class Endereco
    {
        public string Logradouro { get; private set; } = default!;
        public string Numero { get; private set; } = default!;
        public string Cep { get; private set; } = default!;
        public string Cidade { get; private set; } = default!;
        public string Estado { get; private set; } = default!;

        public Endereco(string logradouro,
                        string numero,
                        string cep,
                        string cidade,
                        string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            Cep = cep.Replace("-", "").Trim();
            Cidade = cidade;
            Estado = estado;
        }

        public void Alterar(string logradouro,
                            string numero,
                            string cep,
                            string cidade,
                            string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }

        public static OneOf<bool, AppError> ValidarEstado(string estado)
        {
            var validar = Regex.IsMatch(estado, @"^[A-Z]{2}$");

            if (!validar)
            {
                return new ErroFormatoIncorreto("O estado deve estar no formato de duas letras, Ex: 'SP'.", ErrorType.Validation);
            }

            return true;
        }

        public static OneOf<bool, AppError> ValidarCep(string cep)
        {
            var validar = Regex.IsMatch(cep, @"^\d{5}-?\d{3}$");

            if (!validar)
            {
                return new ErroFormatoIncorreto("O CEP deve estar no formato 'XXXXXXXX' ou 'XXXXX-XXX'.", ErrorType.Validation);
            }

            return true;
        }

    }
}
