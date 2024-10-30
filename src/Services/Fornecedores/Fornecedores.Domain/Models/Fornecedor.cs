using BuildingBlocks.DomainObjects;
using Fornecedores.Domain.ValueObjects;

namespace Fornecedores.Domain.Models
{
    public class Fornecedor : Entity, IAggregateRoot
    {
        public string NomeFantasia { get; private set; } = default!;
        public string RazaoSocial { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public Cnpj Cnpj { get; private set; }
        public Endereco Endereco { get; private set; }
        public bool Desativado { get; private set; }

        private readonly List<Telefone> _telefones;
        public IReadOnlyList<Telefone> Telefones => _telefones;

        protected Fornecedor()
        {
            _telefones = new();
        }

        public Fornecedor(string nomeFantasia,
                          string razaoSocial,
                          string email,
                          Cnpj cnpj,
                          Endereco endereco)
        {
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            Email = email;
            Cnpj = cnpj;
            Endereco = endereco;
            _telefones = new();
        }

        public void Ativar() => Desativado = false;

        public void Desativar() => Desativado = true;

        public void Alterar(string nomeFantasia,
                            string razaoSocial,
                            string email,
                            string cnpj)
        {
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            Email = email;
            Cnpj.Alterar(cnpj);
        }

        public void AdicionarTelefone(Telefone telefone)
        {
            if (!_telefones.Contains(telefone))
                _telefones.Add(telefone);
        }

        public void RemoverTelefone(Guid telefoneId)
        {
            var telefone = _telefones.Find(x => x.Id == telefoneId);

            if (telefone != null)
            {
                _telefones.Remove(telefone);
            }
        }
    }
}
