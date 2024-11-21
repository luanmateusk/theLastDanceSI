using Bogus;
using PedidoItem.Domain.Entity.Cliente;

namespace PedidoItem.Tests.FactoryTest.ClienteFactory
{
    public static class ClienteFactory
    {
        private static Faker<Cliente> ValidCliente()
        {
            return new Faker<Cliente>()
                .RuleFor(e => e.Nome, f => f.Name.FullName())
                .RuleFor(e => e.Email, f => f.Internet.Email())
                .RuleFor(e => e.Endereco, f => f.Address.FullAddress());
        }

        public static Cliente Valid()
        {
            return ValidCliente().Generate();
        }

        public static Cliente InvalidNome()
        {
            var faker = ValidCliente();
            faker.RuleFor(e => e.Nome, string.Empty);
            return faker.Generate();
        }

        public static Cliente InvalidEmail()
        {
            var faker = ValidCliente();
            faker.RuleFor(e => e.Email, string.Empty);
            return faker.Generate();
        }

        public static Cliente InvalidEndereco()
        {
            var faker = ValidCliente();
            faker.RuleFor(e => e.Endereco, string.Empty);
            return faker.Generate();
        }
    }
}