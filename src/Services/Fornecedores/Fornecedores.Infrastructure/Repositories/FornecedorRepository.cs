using BuildingBlocks.Abstractions;
using Fornecedores.Application.Data;
using Fornecedores.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Fornecedores.Infrastructure.Repositories
{
    internal class FornecedorRepository : IFornecedorRepository
    {
        #region Props
        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;
        #endregion

        #region Ctor
        public FornecedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Fornecedores
        public async Task<IEnumerable<Fornecedor>> GetAll(bool? desativado)
        {
            var query = _context.Fornecedores
                                .Include(x => x.Telefones)
                                .AsNoTracking()
                                .AsQueryable();

            if (desativado.HasValue)
            {
                query = query.Where(x => x.Desativado == desativado);
            }

            return await query.ToListAsync();
        }

        public async Task<Fornecedor> GetById(Guid id)
        {
            var fornecedor = await _context.Fornecedores
                                           .Include(f => f.Telefones)
                                           .FirstOrDefaultAsync(f => f.Id == id);

            return fornecedor;
        }

        public async Task<Fornecedor> GetByCnpj(string cnpj)
        {
            var fornecedor = await _context.Fornecedores
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(f => f.Cnpj.Numero == cnpj);

            return fornecedor;
        }

        public async Task Create(Fornecedor fornecedor)
        {
            await _context.Fornecedores.AddAsync(fornecedor);
        }

        public void Update(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);
        }
        #endregion

        public async Task AdicionarTelefone(Telefone telefone)
        {
            await _context.Telefones.AddAsync(telefone);
        }

        #region Dispose
        public void Dispose()
        {
            _context?.Dispose();
        }        
        #endregion
    }
}
