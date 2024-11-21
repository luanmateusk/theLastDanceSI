using Bogus;
using PedidoItem.Domain.Entity.Pedido;
using PedidoItemNameSpace = PedidoItem.Domain.Entity.PedidoItem;

namespace PedidoItem.Tests.FactoryTest.PedidoFactory
{
    public static class PedidoFactory
    {
        private static Faker<Pedido> ValidPedido()
        {
            var cliente = ClienteFactory.ClienteFactory.Valid();

            var itens = new List<PedidoItemNameSpace.PedidoItem>();
            for (int i = 0; i < Faker.RandomNumber.Next(1, 5); i++)
            {
                itens.Add(PedidoItemFactory.PedidoItemFactory.Valid());
            }

            return new Faker<Pedido>()
                .RuleFor(e => e.Cliente, cliente)
                .RuleFor(e => e.Data, f => f.Date.Past(1))
                .RuleFor(e => e.Itens, itens);
        }

        public static Pedido Valid()
        {
            return ValidPedido().Generate();
        }

        public static Pedido InvalidCliente()
        {
            var faker = ValidPedido();
            faker.RuleFor(e => e.Cliente, e => null);
            return faker.Generate();
        }
    }
}