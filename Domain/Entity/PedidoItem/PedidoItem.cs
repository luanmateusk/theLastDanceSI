using Microsoft.Toolkit.Diagnostics;
using ProdutoNamespace = PedidoItem.Domain.Entity.Produto;

namespace PedidoItem.Domain.Entity.PedidoItem;

public partial class PedidoItem
{
    public PedidoItem(ProdutoNamespace.Produto produto, int quantidade)
    {
        Guard.IsNotNull(produto, nameof(produto));
        Guard.IsGreaterThan(quantidade, 0, nameof(quantidade));

        Produto = produto;
        Quantidade = quantidade;
    }

    public PedidoItem()
    {
        // EntityFramework
    }

    public int Id { get; private set; }
    public ProdutoNamespace.Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
}