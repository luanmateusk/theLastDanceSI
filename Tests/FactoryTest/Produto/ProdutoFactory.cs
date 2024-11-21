using Bogus;
using PedidoItem.Domain.Entity.Produto;

namespace PedidoItem.Tests.FactoryTest.ProdutoFactory
{
    public static class ProdutoFactory
    {
        private static Faker<Produto> ValidProduto()
        {
            return new Faker<Produto>()
                .RuleFor(e => e.Nome, f => f.Commerce.ProductName())
                .RuleFor(e => e.Preco, f => f.Finance.Amount())
                .RuleFor(e => e.Descricao, f => f.Lorem.Sentence());
        }

        public static Produto Valid()
        {
            return ValidProduto().Generate();
        }

        public static Produto InvalidNome()
        {
            var faker = ValidProduto();
            faker.RuleFor(e => e.Nome, string.Empty);
            return faker.Generate();
        }

        public static Produto InvalidPreco()
        {
            var faker = ValidProduto();
            faker.RuleFor(e => e.Preco, -50);
            return faker.Generate();
        }

        public static Produto InvalidDescricao()
        {
            var faker = ValidProduto();
            faker.RuleFor(e => e.Descricao, string.Empty);
            return faker.Generate();
        }
    }
}