---
tags: [feature, documentacao, api]
status: done
camadas: [Web]
---

# Feature 9 — Documentação da API com Scalar

Como desenvolvedor, quero uma interface visual moderna para a documentação da Web API, para poder testar os endpoints interativamente durante o desenvolvimento.

## Tarefas

- [x] Adicionar o pacote `Scalar.AspNetCore` no projeto `BrainDump.Web` #infrastructure
- [x] Configurar o middleware do Scalar no pipeline HTTP (`Program.cs`) #web

## Critérios de aceite

- A interface do Scalar deve estar acessível via navegador.
- O Scalar deve ler a especificação OpenAPI gerada nativamente pelo .NET 9 (`Microsoft.AspNetCore.OpenApi`).

## Notas de implementação

> - **Ferramenta escolhida:** Optamos pelo Scalar no lugar do tradicional Swagger UI (Swashbuckle) por ter uma interface mais moderna, suporte excelente a OpenAPI 3.1 e por se integrar perfeitamente às novas APIs nativas do .NET 9.
