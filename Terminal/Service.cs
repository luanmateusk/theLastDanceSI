using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using PedidoItem.Domain.Entity.Cliente;
using PedidoItem.Domain.Entity.Pedido;
using PedidoItem.Domain.Entity.Produto;
using PedidoItemNameSpace = PedidoItem.Domain.Entity.PedidoItem;

public class PedidoService
{
    private readonly BancoContext _context;

    public PedidoService(BancoContext context)
    {
        _context = context;
    }

    #region Cliente

    public void CreateClient(string nome, string email, string endereco)
    {
        var cliente = new Cliente(nome, email, endereco);
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
    }

    public Cliente GetClientById(int clientId)
    {
        return _context.Clientes
            .FirstOrDefault(c => c.Id == clientId);
    }
    public List<Cliente> GetAllClients()
    {
        return _context.Clientes.ToList();
    }

    #endregion

    #region Produto

    public void CreateProduct(string nome, decimal preco, string descricao)
    {
        var produto = new Produto(nome, preco, descricao);
        _context.Produtos.Add(produto);
        _context.SaveChanges();
    }

    public Produto GetProductById(int productId)
    {
        return _context.Produtos
            .FirstOrDefault(p => p.Id == productId);
    }

    public List<Produto> GetAllProducts()
    {
        return _context.Produtos.ToList();
    }

    #endregion

    #region Pedido

    public void CreateOrder(int clientId)
    {
        var cliente = _context.Clientes.Find(clientId);

        if (cliente != null)
        {
            var pedido = new Pedido(cliente);
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }
    }
    public Pedido GetOrderById(int orderId)
    {
        return _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefault(p => p.Id == orderId);
    }
    public List<Pedido> GetAllOrders()
    {
        return _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .ToList();
    }

    #endregion

    #region PedidoItem

    public void AddItemToOrder(int orderId, int productId, int quantity)
    {
        var pedido = _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefault(p => p.Id == orderId);
        var produto = _context.Produtos.Find(productId);

        if (pedido != null && produto != null)
        {
            var pedidoItem = new PedidoItemNameSpace.PedidoItem(produto, quantity);
            pedido.AdicionarItem(pedidoItem);
            _context.SaveChanges();
        }
    }

    public List<PedidoItemNameSpace.PedidoItem> GetItemsFromOrder(int orderId)
    {
        return _context.Pedidos
            .Where(p => p.Id == orderId)
            .SelectMany(p => p.Itens)
            .ToList();
    }

    #endregion
}