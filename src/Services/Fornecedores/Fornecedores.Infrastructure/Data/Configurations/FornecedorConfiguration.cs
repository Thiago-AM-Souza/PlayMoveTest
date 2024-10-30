namespace Fornecedores.Infrastructure.Data.Configurations
{
    public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor", "Fornecedores");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.NomeFantasia)
                   .IsRequired();

            builder.Property(f => f.RazaoSocial)
                   .IsRequired();

            builder.Property(f => f.Desativado)
                   .HasDefaultValue(false)
                   .HasColumnType("boolean");

            builder.HasMany(f => f.Telefones)
                   .WithOne(t => t.Fornecedor)
                   .HasForeignKey(t => t.FornecedorId);

            builder.OwnsOne(f => f.Cnpj)                
                    .Property(c => c.Numero)
                    .HasColumnName("Cnpj");

            builder.ComplexProperty(
                f => f.Endereco, endereco =>
                {
                    endereco.Property(e => e.Logradouro)
                            .HasColumnName("Logradouro")
                            .HasMaxLength(50)
                            .IsRequired();

                    endereco.Property(e => e.Numero)
                            .HasColumnName("Numero")
                            .IsRequired();

                    endereco.Property(e => e.Cep)
                            .HasColumnName("Cep")
                            .HasMaxLength(8)
                            .IsRequired();

                    endereco.Property(e => e.Cidade)
                            .HasColumnName("Cidade")
                            .HasMaxLength(100)
                            .IsRequired();

                    endereco.Property(e => e.Estado)
                            .HasColumnName("Estado")
                            .HasMaxLength(2)
                            .IsRequired();
                });
        }
    }
}
