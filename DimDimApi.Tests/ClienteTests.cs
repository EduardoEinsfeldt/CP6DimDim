using DimDimApi.Models;
using Xunit;

namespace DimDimApi.Tests;

public class ClienteTests
{
    [Fact]
    public void Cliente_DeveTerNomeEEmailValidos()
    {
        var cliente = new Cliente
        {
            Nome = "João Silva",
            Email = "joao@email.com"
        };

        Assert.Equal("João Silva", cliente.Nome);
        Assert.Equal("joao@email.com", cliente.Email);
        Assert.NotNull(cliente.Contas);
    }

    [Fact]
    public void Conta_DeveTerSaldoInicialZero()
    {
        var conta = new Conta
        {
            ClienteId = 1,
            NumeroConta = "12345-6",
            Saldo = 0
        };

        Assert.Equal(0, conta.Saldo);
        Assert.Equal("12345-6", conta.NumeroConta);
    }

    [Fact]
    public void Conta_DevePermitirSaldoPositivo()
    {
        var conta = new Conta { Saldo = 1500.50m };
        Assert.True(conta.Saldo > 0);
    }
}
