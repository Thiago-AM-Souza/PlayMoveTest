using Bogus;
using Bogus.Extensions.Brazil;
using Fornecedores.Domain.Models;
using Fornecedores.Domain.ValueObjects;

public class FornecedorTests
{
    private readonly Faker _faker;

    public FornecedorTests()
    {
        _faker = new Faker("pt_BR");
    }

    private Fornecedor CriarFornecedorFake()
    {
        var cnpj = new Cnpj(_faker.Company.Cnpj());
        var endereco = new Endereco(_faker.Address.StreetAddress(),
                                    _faker.Address.Random.AlphaNumeric(2),
                                    _faker.Address.City(), 
                                    _faker.Address.State(), 
                                    _faker.Address.ZipCode());

        return new Fornecedor(
            _faker.Company.CompanyName(),
            _faker.Company.CompanySuffix(),
            _faker.Internet.Email(),
            cnpj,
            endereco
        );
    }

    [Fact]
    public void Construtor_Completo_DeveInicializarPropriedadesCorretamente()
    {        
        var fornecedor = CriarFornecedorFake();        
        
        Assert.NotNull(fornecedor.NomeFantasia);
        Assert.NotNull(fornecedor.RazaoSocial);
        Assert.NotNull(fornecedor.Email);
        Assert.NotNull(fornecedor.Cnpj);
        Assert.NotNull(fornecedor.Endereco);
        Assert.False(fornecedor.Desativado);
    }

    [Fact]
    public void Ativar_DeveDefinirDesativadoComoFalse()
    {        
        var fornecedor = CriarFornecedorFake();
        fornecedor.Desativar();
        
        fornecedor.Ativar();
        
        Assert.False(fornecedor.Desativado);
    }

    [Fact]
    public void Desativar_DeveDefinirDesativadoComoTrue()
    {        
        var fornecedor = CriarFornecedorFake();
        
        fornecedor.Desativar();
        
        Assert.True(fornecedor.Desativado);
    }

    [Fact]
    public void Alterar_DeveModificarPropriedadesCorretamente()
    {        
        var fornecedor = CriarFornecedorFake();

        var novoNome = _faker.Company.CompanyName();
        var novaRazao = _faker.Company.CompanySuffix();
        var novoEmail = _faker.Internet.Email();
        var novoCnpj = _faker.Company.Cnpj();
        
        fornecedor.Alterar(novoNome, novaRazao, novoEmail, novoCnpj);
        
        Assert.Equal(novoNome, fornecedor.NomeFantasia);
        Assert.Equal(novaRazao, fornecedor.RazaoSocial);
        Assert.Equal(novoEmail, fornecedor.Email);
        Assert.Equal(novoCnpj, fornecedor.Cnpj.Numero);
    }

    [Fact]
    public void AdicionarTelefone_DeveAdicionarTelefoneNaLista()
    {        
        var fornecedor = CriarFornecedorFake();
        var telefone = new Telefone(_faker.Phone.PhoneNumber("## ####-####"), fornecedor); // Exemplo de telefone
        
        fornecedor.AdicionarTelefone(telefone);

        
        Assert.Contains(telefone, fornecedor.Telefones);
    }

    [Fact]
    public void RemoverTelefone_DeveRemoverTelefoneDaLista()
    {        
        var fornecedor = CriarFornecedorFake();
        var telefone = new Telefone(_faker.Phone.PhoneNumber("## ####-####"), fornecedor);
        fornecedor.AdicionarTelefone(telefone);

        
        fornecedor.RemoverTelefone(telefone.Id);
        
        Assert.DoesNotContain(telefone, fornecedor.Telefones);
    }

    [Fact]
    public void Telefones_DeveRetornarIReadOnlyList()
    {        
        var fornecedor = CriarFornecedorFake();
        
        var telefones = fornecedor.Telefones;
        
        Assert.IsAssignableFrom<IReadOnlyList<Telefone>>(telefones);
    }
}
