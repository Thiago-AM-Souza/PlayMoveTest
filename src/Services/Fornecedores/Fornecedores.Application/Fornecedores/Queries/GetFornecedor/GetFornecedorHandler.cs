using BuildingBlocks.CQRS;
using Fornecedores.Application.Data;
using Fornecedores.Application.Extensions;

namespace Fornecedores.Application.Fornecedores.Queries.GetFornecedor
{
    internal class GetFornecedorHandler(IFornecedorRepository repository) : IQueryHandler<GetFornecedorQuery, GetFornecedorResult>
    {
        public async Task<GetFornecedorResult> Handle(GetFornecedorQuery query, CancellationToken cancellationToken)
        {
            var fornecedor = await repository.GetById(query.Id);

            if (fornecedor != null)
            {
                return new GetFornecedorResult(fornecedor.ToDto());
            }

            // AMANHA FOCAR NA PARTE DE TRATAMENTO DE ERROS

            return new GetFornecedorResult(fornecedor.ToDto());
        }
    }
}
