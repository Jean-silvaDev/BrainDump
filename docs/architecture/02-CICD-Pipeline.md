# Architecture Decision Record (ADR 02): Estratégia de CI/CD e Workflows no GitHub Actions

## Contexto
O repositório possuía apenas um modelo genérico e incompleto para aplicações desktop WPF (`dotnet-desktop.yml`), que não refletia a arquitetura real do projeto (Clean Architecture .NET 10 + Web API + .NET MAUI Cross-Platform + Testes Unitários xUnit).

## Decisão
Substituir a automação antiga por um ecossistema modular de GitHub Actions divididos por responsabilidades e alvos de execução:

1. **`ci-backend.yml` (CI Backend & Web)**:
   - **Gatilhos**: Pushes e PRs para `main` e `develop`.
   - **Ambiente**: `ubuntu-latest` (rápido e de baixo custo de consumo de minutos).
   - **Ações**: Restore com cache NuGet, build da solução e execução de todos os testes unitários (`BrainDump.Domain.Tests`, `BrainDump.Application.Tests`, `BrainDump.Infrastructure.Tests`).
   - **Artefatos**: Publica relatórios de testes `.trx`.

2. **`ci-maui.yml` (CI .NET MAUI)**:
   - **Gatilhos**: Pushes e PRs afetando `src/BrainDump.Maui/**`.
   - **Ambiente**: `windows-latest` (necessário para compilação Windows e workloads MAUI).
   - **Ações**: Instalação do workload MAUI, validação de compilação para Windows e Android.

3. **`code-quality.yml` (Qualidade de Código e Segurança)**:
   - **Gatilhos**: Pushes e PRs para `main` e `develop`.
   - **Ambiente**: `ubuntu-latest`.
   - **Ações**: Validação de formatação de código baseada no `.editorconfig` (`dotnet format`) e auditoria de vulnerabilidades em dependências NuGet (`dotnet list package --vulnerable`).

## Padrão de Branching Recomendado
- `main`: Branch de código estável/produção.
- `develop`: Branch de integração contínua.
- `feature/<nome>`: Desenvolvimento de novas funcionalidades (ex: `feature/github-actions-setup`).
- `bugfix/<nome>` / `hotfix/<nome>`: Correção de falhas.

## Consequências
- Aumento na velocidade e isolamento de feedback do CI.
- Prevenção de regressões na Web API e no app MAUI.
- Nenhum push automático foi realizado conforme diretriz de segurança.
