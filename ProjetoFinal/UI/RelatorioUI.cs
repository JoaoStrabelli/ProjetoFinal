using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoFinal.db;



namespace GerenciadorVendas
{
    public class RelatorioUI
    {
        public void MostrarRelatorioPedidos()
        {
            Console.Clear();
            Console.WriteLine("Relatório de Vendas:");

            try
            {
                if (PedidoModel.pedidos.Count == 0)
                {
                    throw new Exception("Não há nenhuma venda registrada");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            double totalPedidos = 0;

            foreach (var pedido in PedidoModel.pedidos)
            {
                Console.WriteLine(
                    $"ID do Pedido: {pedido.PedidoID} " +
                    $"| Data: {pedido.Data} " +
                    $"| Cliente: {pedido.Cliente.Nome} " +
                    $"| Status: {pedido.Status}"
                );

                foreach (var itemVenda in pedido.ProdutosPedido)
                {
                    Console.WriteLine($"   - Produto: {itemVenda.Produto.Nome} | Quantidade: {itemVenda.Quantidade} | Preço unitário: {itemVenda.Produto.Preco:C2} | Subtotal: {itemVenda.Subtotal:C2}");
                }

                Console.WriteLine($"   Total do pedido: {pedido.Total:C2}");
                Console.WriteLine();

                totalPedidos += pedido.Total;
            }

            Console.WriteLine($"Total de vendas: {PedidoModel.pedidos.Count} | Valor total das vendas: {totalPedidos:C2}");

        }
    }

}