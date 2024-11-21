using Microsoft.Toolkit.Diagnostics;

namespace PedidoItem.Domain.Entity.Cliente
{
    public partial class Cliente
    {
        public Cliente(string nome, string email, string endereco)
        {
            Guard.IsNotNullOrWhiteSpace(nome, nameof(nome));
            Guard.IsNotNullOrWhiteSpace(email, nameof(email));
            Guard.IsNotNullOrWhiteSpace(endereco, nameof(endereco));

            Nome = nome;
            Email = email;
            Endereco = endereco;
        }

        public Cliente()
        {
            // EntityFramework
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Endereco { get; private set; }
    }
}