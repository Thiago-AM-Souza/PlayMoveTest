# PlayMove Teste

A aplicação foi desenvolvida utilizando .NET 8, seguindo o padrão de Clean Architecture com CQRS para separar operações de leitura e gravação. A construção da API foi facilitada pela biblioteca Carter, que simplifica a manipulação das requisições. O PostgreSQL foi a minha opção como banco de dados.

Para autenticação e autorização, foi implementado JWT de maneira simplificada, garantindo que apenas usuários autenticados possam acessar as funcionalidades. 

A aplicação segue os princípios SOLID e adota o DDD para refletir as regras de negócio diretamente nas entidades. A biblioteca OneOf foi utilizada para tratar respostas e erros de domínio de forma mais assertiva. O xUnit foi utilizado para uma implementação basica de testes unitários.

## Observações

Seguindo as orientações de boas praticas explicitadas no teste, optei por 2 alterações:

- Id (int) para (Guid)
- HttpDelete para HttpPatch(Ativar e Desativar) dando preferencia para deleção lógica.

## Autenticação
Body para usar na autenticação.
```json
{
    "user": {
        "name": "admin",
        "password": "admin"
    }
}
```

### Tecnologias Utilizadas

- .NET 8
- Clean Architecture
- MediatR
- Carter
- JWT
- OneOf
- PostgreSQL
- xUnit




