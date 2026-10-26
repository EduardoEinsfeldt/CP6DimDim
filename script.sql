-- script.sql - DDL das tabelas (mínimo exigido pelo professor)
-- Banco: Azure SQL Database

CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL
);

CREATE TABLE Contas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    NumeroConta NVARCHAR(20) NOT NULL,
    Saldo DECIMAL(18,2) NOT NULL DEFAULT 0,
    CONSTRAINT FK_Contas_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id) ON DELETE CASCADE
);

-- Índices simples
CREATE INDEX IX_Contas_ClienteId ON Contas(ClienteId);
