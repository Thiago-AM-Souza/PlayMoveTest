using BuildingBlocks.DomainObjects;

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
            Cep = cep;
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
    }
}
