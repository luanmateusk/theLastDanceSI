using FluentAssertions;
using PedidoItem.Domain.Entity.Pedido;
using PedidoItem.Tests.FactoryTest.PedidoFactory;
using PedidoItem.Tests.FactoryTest.ProdutoFactory;
using PedidoItemNameSpace = PedidoItem.Domain.Entity.PedidoItem;

namespace UnitTest;
public partial class PedidoTest
{
    Pedido _pedido;

    [SetUp]
    public void SetUp()
    {
        _pedido = PedidoFactory.Valid();
    }

    [Test]
    public void Deve_Criar_Pedido_Sucesso()
    {
        var pedido = new Pedido(
            _pedido.Cliente
        );

        pedido.Should().NotBeNull();
        pedido.Cliente.Should().NotBeNull();
        pedido.Itens.Should().BeEmpty();
    }

    [Test]
    public void Deve_Falhar_Pedido_Cliente()
    {
        _pedido = PedidoFactory.InvalidCliente();

        Action act = () => new Pedido(
            _pedido.Cliente
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*cliente*");
    }
}