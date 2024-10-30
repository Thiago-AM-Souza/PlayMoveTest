namespace Fornecedores.Application.Dtos
{
    public record EnderecoDto(string Logradouro,
                              string Numero,
                              string Cep,
                              string Cidade,
                              string Estado);
}
