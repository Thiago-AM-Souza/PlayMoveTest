using BuildingBlocks.Abstractions;

namespace Fornecedores.Application.Data
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<IEnumerable<Fornecedor>> GetAll(bool? desativado);
        Task<Fornecedor> GetById(Guid id);
        Task<Fornecedor> GetByCnpj(string cnpj);
        Task Create(Fornecedor fornecedor);
        void Update(Fornecedor fornecedor);
        Task AdicionarTelefone(Telefone telefone);
    }
}
