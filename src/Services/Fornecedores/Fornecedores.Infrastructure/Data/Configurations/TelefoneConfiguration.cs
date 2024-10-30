namespace Fornecedores.Infrastructure.Data.Configurations
{
    public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("Telefone", "Fornecedores");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.Numero)
                   .HasMaxLength(11)
                   .IsRequired();
        }
    }
}
