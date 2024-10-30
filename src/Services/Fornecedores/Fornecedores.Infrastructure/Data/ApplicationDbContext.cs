using BuildingBlocks.Abstractions;
using Fornecedores.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fornecedores.Application.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Fornecedor> Fornecedores { get; set; }        
        public DbSet<Telefone> Telefones { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
