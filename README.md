# DimDim API - Checkpoint CI/CD (FIAP)

Projeto mínimo em .NET 8 para o 3º Checkpoint de Pipelines CI/CD.

**RM:** 556460  
**Região:** francecentral

## Recursos Azure (nomes fixos)
- Resource Group: `rg-dimdim-556460`
- SQL Server: `sql-dimdim-556460`
- Web App: `app-dimdim-556460`
- App Service Plan: `plan-dimdim-556460`

## Requisitos atendidos
- 2 tabelas com relacionamento (Clientes ↔ Contas)
- Banco em nuvem (Azure SQL - PaaS)
- API REST com CRUD completo
- Testes unitários (xUnit) que passam
- Pipeline com 3 stages: Infraestrutura → Build/Testes → Deploy
- Scripts de infraestrutura (Azure CLI) executados pela pipeline
- Variáveis de ambiente / connection string
- `script.sql` com DDL

## Como rodar localmente

```bash
dotnet restore
dotnet build
dotnet test

cd DimDimApi
dotnet run
```

## Pipeline
A pipeline (`azure-pipelines.yml`) segue o mesmo padrão do exemplo do professor:
1. **Infraestrutura** – executa `infra/criar-infra.sh`
2. **Build e testes** – restore, build, testes e publicação do artefato
3. **Deploy** – configura variáveis e faz deploy no App Service

**Antes de rodar a pipeline:**
1. Crie uma Service Connection no Azure DevOps chamada `MyAzureSubscription` (ou altere o nome no YAML).
2. Garanta que a Service Connection tenha permissão na subscription.