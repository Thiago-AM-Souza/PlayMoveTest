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

    public record CreateFornecedorDto(
        string NomeFantasia,
        string RazaoSocial,
        string Email,
        string Cnpj,
        EnderecoDto Endereco,
        List<CreateTelDto> Telefones);

    public record CreateTelDto(string Numero);

    public record UpdateFornecedorDto(
        Guid Id,
        string NomeFantasia,
        string RazaoSocial,
        string Email,
        string Cnpj,
        EnderecoDto Endereco,
        List<TelefoneDto> Telefones);
}
