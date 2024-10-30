namespace Fornecedores.Application.Dtos
{
    public record FornecedorDto(
        Guid Id,
        string NomeFantasia,
        string RazaoSocial,
        string Email,
        string Cnpj,
        EnderecoDto Endereco,
        List<TelefoneDto> Telefones);

    public record CadastrarFornecedorDto(
        string NomeFantasia,
        string RazaoSocial,
        string Email,
        string Cnpj,
        EnderecoDto Endereco,
        List<CreateTelDto> Telefones);

    public record CreateTelDto(string Numero);

    public record AtualizarFornecedorDto(
        Guid Id,
        string NomeFantasia,
        string RazaoSocial,
        string Email,
        string Cnpj,
        EnderecoDto Endereco,
        List<TelefoneDto> Telefones);
}
