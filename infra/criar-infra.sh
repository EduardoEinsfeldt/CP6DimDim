#!/bin/bash
# Script de infraestrutura Azure CLI
# Região: francecentral
# RM: 556460
# Senha vem da variável secreta SQL_PASSWORD

RESOURCE_GROUP="rg-dimdim-556460"
LOCATION="francecentral"
SQL_SERVER="sql-dimdim-556460"
SQL_DB="DimDimDb"
SQL_ADMIN="dimdimadmin"
APP_PLAN="plan-dimdim-556460"
WEB_APP="app-dimdim-556460"

# Senha recebida como argumento ou variável de ambiente
SQL_PASSWORD="${1:-$SQL_PASSWORD}"

if [ -z "$SQL_PASSWORD" ]; then
  echo "ERRO: SQL_PASSWORD não informada."
  exit 1
fi

echo "=== Criando Resource Group ==="
az group create --name $RESOURCE_GROUP --location $LOCATION

echo "=== Criando SQL Server ==="
az sql server create \
  --name $SQL_SERVER \
  --resource-group $RESOURCE_GROUP \
  --location $LOCATION \
  --admin-user $SQL_ADMIN \
  --admin-password "$SQL_PASSWORD"

echo "=== Criando firewall (permite serviços Azure) ==="
az sql server firewall-rule create \
  --resource-group $RESOURCE_GROUP \
  --server $SQL_SERVER \
  --name AllowAzureServices \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 0.0.0.0

echo "=== Criando Database ==="
az sql db create \
  --resource-group $RESOURCE_GROUP \
  --server $SQL_SERVER \
  --name $SQL_DB \
  --service-objective Basic

echo "=== Criando App Service Plan ==="
az appservice plan create \
  --name $APP_PLAN \
  --resource-group $RESOURCE_GROUP \
  --sku B1 \
  --is-linux

echo "=== Criando Web App ==="
az webapp create \
  --resource-group $RESOURCE_GROUP \
  --plan $APP_PLAN \
  --name $WEB_APP \
  --runtime "DOTNETCORE:8.0"

echo "=== Configurando Connection String ==="
CONNECTION_STRING="Server=tcp:${SQL_SERVER}.database.windows.net,1433;Initial Catalog=${SQL_DB};Persist Security Info=False;User ID=${SQL_ADMIN};Password=${SQL_PASSWORD};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

az webapp config connection-string set \
  --resource-group $RESOURCE_GROUP \
  --name $WEB_APP \
  --settings DefaultConnection="$CONNECTION_STRING" \
  --connection-string-type SQLAzure

echo ""
echo "=== Recursos criados ==="
echo "Resource Group: $RESOURCE_GROUP"
echo "SQL Server: $SQL_SERVER.database.windows.net"
echo "Web App: https://$WEB_APP.azurewebsites.net"