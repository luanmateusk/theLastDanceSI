using Infrastructure;

using (var context = new BancoContext())
{
    var pedidoService = new PedidoService(context);

    pedidoService.CreateClient("Luan", "kusma@mail", "Street One, 123");

    pedidoService.CreateProduct("T1", 499.99m, "Cipó do Tarzan");
    pedidoService.CreateProduct("T2", 17.98m, "Orelha de Cobra");

    var clients = pedidoService.GetAllClients();
    foreach (var cc in clients)
    {
        Console.WriteLine($"Cliente: {cc.Nome} - {cc.Email} - {cc.Endereco}");
    }

    var products = pedidoService.GetAllProducts();
    foreach (var product in products)
    {
        Console.WriteLine($"Produto: {product.Nome} - Preço: {product.Preco} - Descrição: {product.Descricao}");
    }

    var client = context.Clientes.FirstOrDefault(c => c.Nome == "Luan");
    if (client != null)
    {
        pedidoService.CreateOrder(client.Id);
    }

    var order = pedidoService.GetAllOrders().FirstOrDefault(p => p.Cliente.Nome == "Luan");
    if (order != null)
    {
        Console.WriteLine($"Pedido ID: {order.Id} - Cliente: {order.Cliente.Nome} - Data: {order.Data}");

        var productA = context.Produtos.FirstOrDefault(p => p.Nome == "T1");
        var productB = context.Produtos.FirstOrDefault(p => p.Nome == "T2");

        if (productA != null)
        {
            pedidoService.AddItemToOrder(order.Id, productA.Id, 2);
        }

        if (productB != null)
        {
            pedidoService.AddItemToOrder(order.Id, productB.Id, 1);
        }

        var items = pedidoService.GetItemsFromOrder(order.Id);
        foreach (var item in items)
        {
            Console.WriteLine($"Produto: {item.Produto.Nome} - Quantidade: {item.Quantidade} - Preço: {item.Produto.Preco}");
        }

        var total = order.Total();
        Console.WriteLine($"Total do Pedido: {total:C}");
    }
}