# Estratégia Docker

Para suportar o desenvolvimento local, testes consistentes e deploy de produção simplificado, o backend do BrainDump será conteinerizado utilizando Docker.

## Visão Geral da Arquitetura

- **Backend (Web API e Blazor Web):** Container baseado no `.NET 9`. A API expõe os endpoints REST e o Blazor serve a UI web.
- **Banco de Dados:** Container `Microsoft SQL Server 2022`. A escolha pelo SQL Server garante compatibilidade total com o ecossistema .NET e segurança nativa via EF Core.
- **Rede Local:** O `docker-compose.yml` expõe o serviço `sqlserver` na porta `1433`.

## Variáveis de Ambiente e Segredos

Nenhuma chave será commitada no repositório. O Docker utilizará o arquivo `.env` para credenciais.

```env
MSSQL_SA_PASSWORD=BrainDump@2026Pass
ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=BrainDumpDb;User Id=sa;Password=BrainDump@2026Pass;TrustServerCertificate=True;MultipleActiveResultSets=true
```

## Desenvolvimento Local (MAUI conectando no Docker)

Quando executando o emulador Android localmente, a aplicação MAUI precisará acessar a API rodando no host:

- No Emulador Android, o "localhost" do host é acessado via IP especial `10.0.2.2`.
- O endpoint da API no `appsettings.json` do MAUI (para debug Android) será: `http://10.0.2.2:5095`.

## Configuração do `docker-compose.yml`

```yaml
version: '3.8'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: braindump-sqlserver
    restart: always
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=${MSSQL_SA_PASSWORD:-BrainDump@2026Pass}
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql/data

volumes:
  sqlserver_data:
```
