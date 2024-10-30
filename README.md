# PlayMove Teste

A aplicação foi desenvolvida utilizando .NET 8, seguindo o padrão de Clean Architecture com o padrão CQRS para separar operações de leitura e gravação. A construção da API foi facilitada pela biblioteca Carter, que simplifica o roteamento e a manipulação das requisições. Para autenticação e autorização, utilizei JWT de maneira simplificada, mas garantindo que apenas usuários autenticados possam acessar funcionalidades. Também utilizei a biblioteca OneOf para tratar respostas e erros de forma mais assertiva. Implementação básica de testes unitarios utilizando xUnit.

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
- CQRS
- Carter
- JWT
- OneOf
- xUnit
