namespace Fornecedores.Application.Extensions
{
    public static class FornecedoresExtensions
    {
        public static IEnumerable<FornecedorDto> ToDtoList(this IEnumerable<Fornecedor> fornecedores)
        {
            return fornecedores
                    .Select(fornecedor =>
                        new FornecedorDto(
                            fornecedor.Id,
                            fornecedor.NomeFantasia,
                            fornecedor.RazaoSocial,
                            fornecedor.Email,
                            fornecedor.Cnpj.Numero,
                            new EnderecoDto(fornecedor.Endereco.Logradouro,
                                            fornecedor.Endereco.Numero,
                                            fornecedor.Endereco.Cep,
                                            fornecedor.Endereco.Cidade,
                                            fornecedor.Endereco.Estado),
                            fornecedor.Telefones.Select(x => 
                                                    new TelefoneDto(x.Id, x.Numero))
                                                .ToList()));
        }

        public static FornecedorDto ToDto(this Fornecedor fornecedor)
        {
            return new FornecedorDto(
                        fornecedor.Id,
                        fornecedor.NomeFantasia,
                        fornecedor.RazaoSocial,
                        fornecedor.Email,
                        fornecedor.Cnpj.Numero,
                        new EnderecoDto(fornecedor.Endereco.Logradouro,
                                        fornecedor.Endereco.Numero,
                                        fornecedor.Endereco.Cep,
                                        fornecedor.Endereco.Cidade,
                                        fornecedor.Endereco.Estado),
                        fornecedor.Telefones.Select(x => 
                                                new TelefoneDto(x.Id,
                                                                x.Numero))
                                            .ToList());
        }
    }
}
