using FluentAssertions;
using PedidoItem.Domain.Entity.Produto;
using PedidoItem.Tests.FactoryTest.ProdutoFactory;

namespace UnitTest;

public partial class ProdutoTest
{
    Produto _produto;

    [SetUp]
    public void SetUp()
    {
        _produto = ProdutoFactory.Valid();
    }

    [Test]
    public void criara_Produto_Sucesso()
    {
        var produto = new Produto(
            _produto.Nome,
            _produto.Preco,
            _produto.Descricao
        );

        produto.Should().NotBeNull();
        produto.Nome.Should().NotBeNullOrWhiteSpace();
        produto.Preco.Should().BeGreaterThan(0);
        produto.Descricao.Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public void FalhaAoCadastrar__Produto_Nome()
    {
        _produto = ProdutoFactory.InvalidNome();

        Action act = () => new Produto(
            _produto.Nome,
            _produto.Preco,
            _produto.Descricao
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*nome*");
    }

    [Test]
    public void FalhaAoCadastrar__Produto_Preco()
    {
        _produto = ProdutoFactory.InvalidPreco();

        Action act = () => new Produto(
            _produto.Nome,
            _produto.Preco,
            _produto.Descricao
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*preco*");
    }

    [Test]
    public void FalhaAoCadastrar_Produto_Descricao()
    {
        _produto = ProdutoFactory.InvalidDescricao();

        Action act = () => new Produto(
            _produto.Nome,
            _produto.Preco,
            _produto.Descricao
        );

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"*descricao*");
    }
}