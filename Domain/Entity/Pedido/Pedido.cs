using Microsoft.Toolkit.Diagnostics;
using PedidoItemNamespace = PedidoItem.Domain.Entity.PedidoItem;

namespace PedidoItem.Domain.Entity.Pedido;
public partial class Pedido
{
    public Pedido(Cliente.Cliente cliente)
    {
        Guard.IsNotNull(cliente, nameof(cliente));

        Cliente = cliente;
        Data = DateTime.Now;
        Itens = new List<PedidoItemNamespace.PedidoItem>();
    }

    public Pedido()
    {
        // EntityFramework
    }

    public int Id { get; private set; }
    public Cliente.Cliente Cliente { get; private set; }
    public DateTime Data { get; private set; }
    public ICollection<PedidoItemNamespace.PedidoItem> Itens { get; private set; }

    public void AdicionarItem(PedidoItemNamespace.PedidoItem item)
    {
        Guard.IsNotNull(item, nameof(item));
        Itens.Add(item);

        ValidarItens();
    }

    private void ValidarItens()
    {
        if (Itens == null || Itens.Count == 0)
        {
            throw new ArgumentException("O pedido deve ter pelo menos um item.", nameof(Itens));
        }

        foreach (var item in Itens)
        {
            if (item.Produto == null || item.Quantidade <= 0)
            {
                throw new ArgumentException("Cada item do pedido deve ter um produto válido e quantidade maior que zero.", nameof(Itens));
            }
        }
    }

    public decimal Total()
    {
        decimal total = 0;
        foreach (var item in Itens)
        {
            total += item.Produto.Preco * item.Quantidade;
        }
        return total;
    }
}