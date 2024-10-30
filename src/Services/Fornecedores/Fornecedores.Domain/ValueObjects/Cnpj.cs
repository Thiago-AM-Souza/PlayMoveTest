using BuildingBlocks.Extensions;
using Fornecedores.Domain.Errors.Cnpj;

namespace Fornecedores.Domain.ValueObjects
{
    public record Cnpj
    {
        public string Numero { get; private set; }

        public Cnpj(string numero)
        {
            numero = numero.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
            Numero = numero;
        }

        public void Alterar(string cnpj)
        {
            Numero = cnpj;
        }

        public static OneOf<bool, AppError> Validacoes(string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();

            var validacoes = ValidarFormato(cnpj);

            if (validacoes.IsError())
            {
                return validacoes.GetErrorResult();
            }

            validacoes = ValidarNumero(cnpj);

            if (validacoes.IsError())
            {
                return validacoes.GetErrorResult();
            }

            return true;
        }

        private static OneOf<bool, AppError> ValidarFormato(string cnpj)
        {
            if (cnpj.Count() != 14)
                return new ErroFormatoIncorreto("O CNPJ deve conter 14 digitos", ErrorType.Validation);

            foreach(char c in cnpj)
            {
                if (!char.IsDigit(c))
                    return new ErroFormatoIncorreto("O CNPJ deve conter apenas números", ErrorType.Validation);
            }

            return true;
        }

        private static OneOf<bool, AppError> ValidarNumero(string cnpj)
        {
            int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            if (cnpj.EndsWith(digito))
            {
                return true;
            }

            return new ErroCnpjNumeroInvalido("Por favor verifique o CNPJ", ErrorType.Validation);
        }
    }
}
