using FluentAssertions;
using PedidoItem.Domain.Entity.Cliente;
using PedidoItem.Tests.FactoryTest.ClienteFactory;

namespace UnitTest;

public partial class ClienteTest
{
    Cliente _cliente;

    [SetUp]
    public void SetUp()
    {
        _cliente = ClienteFactory.Valid();
    }

    [Test]
    public void Deve_Criar_Cliente_Sucesso()
    {
        var cliente = new Cliente(
            _cliente.Nome,
            _cliente.Email,
            _cliente.Endereco
        );

        cliente.Should().NotBeNull();
        cliente.Nome.Should().NotBeNullOrWhiteSpace();
        cliente.Email.Should().NotBeNullOrWhiteSpace().And.Contain("@");
        cliente.Endereco.Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public void Deve_Falhar_Cliente_Nome()
    {
        _cliente = ClienteFactory.InvalidNome();

        Action act = () => new Cliente(
            _cliente.Nome,
            _cliente.Email,
            _cliente.Endereco
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*nome*");
    }

    [Test]
    public void Deve_Falhar_Cliente_Email()
    {
        _cliente = ClienteFactory.InvalidEmail();

        Action act = () => new Cliente(
            _cliente.Nome,
            _cliente.Email,
            _cliente.Endereco
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*email*");
    }

    [Test]
    public void Deve_Falhar_Cliente_Endereco()
    {
        _cliente = ClienteFactory.InvalidEndereco();

        Action act = () => new Cliente(
            _cliente.Nome,
            _cliente.Email,
            _cliente.Endereco
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*endereco*");
    }
}