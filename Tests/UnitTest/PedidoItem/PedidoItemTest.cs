using FluentAssertions;
using PedidoItemNameSpace = PedidoItem.Domain.Entity.PedidoItem;
using PedidoItem.Tests.FactoryTest.PedidoItemFactory;

namespace UnitTest;

public partial class PedidoItemTest
{
    PedidoItemNameSpace.PedidoItem _pedidoItem;

    [SetUp]
    public void SetUp()
    {
        _pedidoItem = PedidoItemFactory.Valid();
    }

    [Test]
    public void criara_PedidoItem_Sucesso()
    {
        var pedidoItem = new PedidoItemNameSpace.PedidoItem(
            _pedidoItem.Produto,
            _pedidoItem.Quantidade
        );

        pedidoItem.Should().NotBeNull();
        pedidoItem.Produto.Should().NotBeNull();
        pedidoItem.Quantidade.Should().BeGreaterThan(0);
    }

    [Test]
    public void FalhaAoCadastrar_PedidoItem_Produto()
    {
        _pedidoItem = PedidoItemFactory.InvalidProduto();

        Action act = () => new PedidoItemNameSpace.PedidoItem(
            _pedidoItem.Produto,
            _pedidoItem.Quantidade
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*produto*");
    }

    [Test]
    public void FalhaAoCadastrar_PedidoItem_Quantidade()
    {
        _pedidoItem = PedidoItemFactory.InvalidQuantidade();

        Action act = () => new PedidoItemNameSpace.PedidoItem(
            _pedidoItem.Produto,
            _pedidoItem.Quantidade
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*quantidade*");
    }
}