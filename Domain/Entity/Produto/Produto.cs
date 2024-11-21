using Microsoft.Toolkit.Diagnostics;

namespace PedidoItem.Domain.Entity.Produto;
public partial class Produto
    {
        public Produto(string nome, decimal preco, string descricao)
        {
            Guard.IsNotNullOrWhiteSpace(nome, nameof(nome));
            Guard.IsGreaterThan(preco, 0, nameof(preco));
            Guard.IsNotNullOrWhiteSpace(descricao, nameof(descricao));

            Nome = nome;
            Preco = preco;
            Descricao = descricao;
        }

        public Produto()
        {
            // EntityFramework
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public string Descricao { get; private set; }
    }