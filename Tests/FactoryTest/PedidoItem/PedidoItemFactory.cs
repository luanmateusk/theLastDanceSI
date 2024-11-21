using Bogus;
using PedidoItemNameSpace = PedidoItem.Domain.Entity.PedidoItem;

namespace PedidoItem.Tests.FactoryTest.PedidoItemFactory
{
    public static class PedidoItemFactory
    {
        private static Faker<PedidoItemNameSpace.PedidoItem> ValidPedidoItem()
        {
            // Criar um produto válido para o item
            var produto = ProdutoFactory.ProdutoFactory.Valid();

            return new Faker<PedidoItemNameSpace.PedidoItem>()
                .RuleFor(e => e.Produto, produto)
                .RuleFor(e => e.Quantidade, f => f.Random.Int(1, 10));
        }

        public static PedidoItemNameSpace.PedidoItem Valid()
        {
            return ValidPedidoItem().Generate();
        }

        public static PedidoItemNameSpace.PedidoItem InvalidProduto()
        {
            var faker = ValidPedidoItem();
            faker.RuleFor(e => e.Produto, e => null);
            return faker.Generate();
        }

        public static PedidoItemNameSpace.PedidoItem InvalidQuantidade()
        {
            var faker = ValidPedidoItem();
            faker.RuleFor(e => e.Quantidade, 0);
            return faker.Generate();
        }
    }
}