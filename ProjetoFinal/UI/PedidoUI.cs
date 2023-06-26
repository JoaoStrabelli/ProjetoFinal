using Gamificação_01___Final.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjetoFinal.db;



namespace GerenciadorVendas
{
    public class PedidoUI: IUserInterface
    {
        public void Menu()
        {
            bool continuar = true;
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Listar Pedidos");
                Console.WriteLine("2 - Realizar Pedido");
                Console.WriteLine("3 - Alterar Pedido");
                Console.WriteLine("4 - Confirmar Pedido");
                Console.WriteLine("5 - Cancelar Pedido");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Listar();
                        break;
                    case "2":
                        Cadastrar();
                        break;
                    case "3":
                        Alterar();
                        break;
                    case "4":
                        ConfirmarPedido();
                        break;
                    case "5":
                        CancelarPedido();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (continuar);
        }

        public void Listar()
        {
            
            Console.Clear();

            try
            {
                if (PedidoModel.pedidos.Count == 0)
                {
                    throw new Exception("Não há nenhum pedido cadastrado!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("1 - Listar todos os pedidos");
            Console.WriteLine("2 - Listar pedidos em processamento");
            Console.WriteLine("3 - Listar pedidos concluidos");
            Console.WriteLine("4 - Listar pedidos cancelados");
            Console.WriteLine("0 - Voltar");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TodosPedidos();
                    break;

                case 2:
                    PedidosProcessando();
                    break;

                case 3:
                    PedidosConfirmados();
                    break;

                case 4:
                    PedidosCancelados();
                    break;

                case 0:
                    return;
                    break;

                default:
                    Console.WriteLine("Opção Inválida!");
                    break;
            }
        }

        private void TodosPedidos()
        {
            Console.WriteLine("Lista de todos os pedidos:");
            foreach (var pedido in PedidoModel.pedidos)
            {
                Console.WriteLine(
                    $"ID: {pedido.PedidoID} " +
                    $"| Cliente: {pedido.Cliente.NomeCompleto} " +
                    $"| Status: {pedido.Status} "
                );

                foreach (var itemPedido in pedido.ProdutosPedido)
                {
                    Console.WriteLine($"   - Produto: {itemPedido.Produto.Nome} | Quantidade: {itemPedido.Quantidade} | Preço unitário: {itemPedido.Produto.Preco:C2} | Subtotal: {itemPedido.Subtotal:C2}");
                }
            }
        }

        private void PedidosProcessando()
        {
            Console.WriteLine("Lista de pedidos em processamento:");
            foreach (var pedido in PedidoModel.pedidos.Where(p => p.Status == StatusPedido.Processando))
            {
                Console.WriteLine(
                    $"ID: {pedido.PedidoID} " +
                    $"| Cliente: {pedido.Cliente.NomeCompleto} " +
                    $"| Status: {pedido.Status} "
                );

                foreach (var itemPedido in pedido.ProdutosPedido)
                {
                    Console.WriteLine($"   - Produto: {itemPedido.Produto.Nome} | Quantidade: {itemPedido.Quantidade} | Preço unitário: {itemPedido.Produto.Preco:C2} | Subtotal: {itemPedido.Subtotal:C2}");
                }
            }
        }

        private void PedidosConfirmados()
        {
            Console.WriteLine("Lista de pedidos confirmados:");
            foreach (var pedido in PedidoModel.pedidos.Where(p => p.Status == StatusPedido.Confirmado))
            {
                Console.WriteLine(
                    $"ID: {pedido.PedidoID} " +
                    $"| Cliente: {pedido.Cliente.NomeCompleto} " +
                    $"| Status: {pedido.Status} "
                );

                foreach (var itemPedido in pedido.ProdutosPedido)
                {
                    Console.WriteLine($"   - Produto: {itemPedido.Produto.Nome} | Quantidade: {itemPedido.Quantidade} | Preço unitário: {itemPedido.Produto.Preco:C2} | Subtotal: {itemPedido.Subtotal:C2}");
                }
            }
        }

        private void PedidosCancelados()
        {
            Console.WriteLine("Lista de pedidos cancelados:");
            foreach (var pedido in PedidoModel.pedidos.Where(p => p.Status == StatusPedido.Cancelado))
            {
                Console.WriteLine(
                    $"ID: {pedido.PedidoID} " +
                    $"| Cliente: {pedido.Cliente.NomeCompleto} " +
                    $"| Status: {pedido.Status} "
                );

                foreach (var itemPedido in pedido.ProdutosPedido)
                {
                    Console.WriteLine($"   - Produto: {itemPedido.Produto.Nome} | Quantidade: {itemPedido.Quantidade} | Preço unitário: {itemPedido.Produto.Preco:C2} | Subtotal: {itemPedido.Subtotal:C2}");
                }
            }
        }

        public void Cadastrar() 
        { 
        
            Console.Clear();

            try
            {
                if (ProdutoModel.produtos.Count == 0)
                {
                    throw new Exception("Não há nenhum produto cadastrado!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Realizar venda:");

            ClienteModel cliente = SelecionarCliente();

            if(cliente == null)
            {
                return;
            }

            
            PedidoModel pedido = new PedidoModel(cliente);

            bool continuar = true;
            do
            {
                Console.Clear();
                new ProdutoUI().Listar();

                Console.WriteLine();
                Console.WriteLine("Digite o ID do produto que deseja comprar (0 para finalizar):");
                int idProduto = int.Parse(Console.ReadLine());

                if (idProduto == 0)
                {
                    // Finaliza o pedido
                    PedidoModel.pedidos.Add(pedido);
                    Console.WriteLine($"Pedido finalizado. Total: R${pedido.Total}");
                    continuar = false;
                }
                else
                {
                    // Seleciona o produto a ser comprado
                    ProdutoModel produto = ProdutoModel.produtos.Find(p => p.ProdutoID == idProduto);
                    if (produto == null)
                    {
                        Console.WriteLine("Produto não encontrado!");
                    }
                    else
                    {
                        // Pede a quantidade desejada
                        Console.WriteLine($"Digite a quantidade de {produto.Nome} que deseja pedir:");
                        int quantidade = int.Parse(Console.ReadLine());

                        while(quantidade <= 0 || quantidade > produto.Estoque)
                        {
                            Console.Write("Valor Inválido! Digite novamente a quantidade: ");
                            quantidade = int.Parse(Console.ReadLine());
                        }

                        produto.Estoque -= quantidade;

                        // Adiciona o item à venda
                        PedidoProdutoModel item = new PedidoProdutoModel(produto, quantidade);                    
                        pedido.ProdutosPedido.Add(item);

                        Console.WriteLine($"{quantidade}X {produto.Nome} adicionado(s) ao pedido!");

                    }
                }

                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (continuar);
        }

        public void Alterar()
        {
            if(PedidoModel.pedidos == null)
            {
                Console.WriteLine("Não há pedidos cadastrados!");
                return;
            }

            Console.Clear();
            Console.WriteLine("Alteração do pedido");
            Console.WriteLine();

            Console.Write("ID do pedido: ");
            long id = long.Parse(Console.ReadLine());

            PedidoModel pedido = PedidoModel.pedidos.Find(p => p.PedidoID == id);

            while(pedido == null || pedido.Status == StatusPedido.Cancelado || pedido.Status == StatusPedido.Confirmado)
            {
                Console.WriteLine("Pedido não encontrado ou inválido!");
                Console.WriteLine("Informe novamente o ID: ");
                id = long.Parse(Console.ReadLine());
                pedido = PedidoModel.pedidos.Find(p => p.PedidoID == id);
            }

            bool continuar = true;
            do
            {
                Console.Clear();

                Console.WriteLine("1 - Alterar Cliente");
                Console.WriteLine("2 - Adicionar Produto");
                Console.WriteLine("3 - Excluir Produto");
                Console.WriteLine("0 - Voltar");
                Console.WriteLine("Escolha uma opção: ");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        AlterarCliente(pedido);
                        break;
                    case 2:
                        AdicionarProduto(pedido);
                        break;
                    case 3:
                        ExcluirProduto(pedido);
                        break;
                    case 0:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                }
            } while (continuar);

        }

        private void AlterarCliente(PedidoModel pedido)
        {
            Console.Clear();
            Console.WriteLine("Alterando cliente do pedido: ");
            Console.WriteLine();

            foreach (var c in ClienteModel.clientes)
            {
                Console.WriteLine($"ID: {c.ClienteID} | Nome Completo: {c.NomeCompleto} | Endereço: {c.Endereco} | Telefone: {c.Telefone}");
            }

            Console.Write($"Informe o ID do novo cliente ({pedido.Cliente.Nome}): ");
            long id = long.Parse(Console.ReadLine());
            ClienteModel cliente = ClienteModel.clientes.Find(c => c.ClienteID == id);

            while( cliente == null )
            {
                Console.Write($"Cliente não encontrado! Informe o ID novamente: ");
                id = long.Parse(Console.ReadLine());
                cliente = ClienteModel.clientes.Find(c => c.ClienteID == id);
            }

            pedido.Cliente = cliente;

            Console.WriteLine("Cliente alterado com sucesso!");
            
        }

        private void AdicionarProduto(PedidoModel pedido)
        {
            Console.Clear();
            Console.WriteLine("Adicionando Produto: ");
            Console.WriteLine();

            bool continuar = true;
            do
            {
                Console.Clear();
                new ProdutoUI().Listar();

                Console.WriteLine();
                Console.WriteLine("Digite o ID do produto que deseja adicionar (0 para finalizar):");
                int idProduto = int.Parse(Console.ReadLine());

                if (idProduto == 0)
                {
                    continuar = false;
                }
                else
                {
                    // Seleciona o produto a ser pedido
                    ProdutoModel produto = ProdutoModel.produtos.Find(p => p.ProdutoID == idProduto);
                    if (produto == null)
                    {
                        Console.WriteLine("Produto não encontrado!");
                    }
                    else
                    {
                        // Pede a quantidade desejada
                        Console.WriteLine($"Digite a quantidade de {produto.Nome} que deseja comprar:");
                        int quantidade = int.Parse(Console.ReadLine());

                        while (quantidade <= 0 || quantidade > produto.Estoque)
                        {
                            Console.Write("Valor Inválido! Digite novamente a quantidade: ");
                            quantidade = int.Parse(Console.ReadLine());
                        }

                        produto.Estoque -= quantidade;

                        // Adiciona o item à venda
                        PedidoProdutoModel item = new PedidoProdutoModel(produto, quantidade);
                        pedido.ProdutosPedido.Add(item);

                        Console.WriteLine($"{quantidade}X {produto.Nome} adicionado(s) ao pedido!");

                    }
                }

                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (continuar);

        }

        private void ExcluirProduto(PedidoModel pedido)
        {
            Console.Clear();
            Console.WriteLine("Excluir Produto: ");
            Console.WriteLine();

            bool continuar = true;
            do
            {
                Console.Clear();
                foreach(var item in pedido.ProdutosPedido)
                {
                    Console.WriteLine(
                        $"ID: {item.Produto.ProdutoID} " +
                        $"| Nome: {item.Produto.Nome} " +
                        $"| Preço: {item.Produto.Preco} " +
                        $"| Quantidade: {item.Quantidade}");
                }

                Console.WriteLine();
                Console.WriteLine("Digite o ID do produto que deseja excluir (0 para finalizar):");
                int idProduto = int.Parse(Console.ReadLine());

                if (idProduto == 0)
                {
                    continuar = false;
                }
                else
                {
                    // Seleciona o produto a ser pedido
                    PedidoProdutoModel produto = pedido.ProdutosPedido.Find(p => p.Produto.ProdutoID == idProduto);
                    if (produto == null)
                    {
                        Console.WriteLine("Produto não encontrado!");
                    }
                    else
                    {
                        pedido.ProdutosPedido.Remove(produto);

                        produto.Produto.Estoque += produto.Quantidade;

                    }
                }

                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            } while (continuar);
        }

        private void ConfirmarPedido()
        {
            Console.Clear();
            Console.Write("Confirmação do pedido:");
            Console.WriteLine();
            
            Console.Write("ID do pedido: ");
            long id = long.Parse(Console.ReadLine());

            PedidoModel pedido = PedidoModel.pedidos.Find(p => p.PedidoID == id);

            while (pedido == null || pedido.Status != StatusPedido.Processando)
            {
                Console.WriteLine("Pedido não existe ou inválido!");
                Console.Write("Informe novamente o ID: ");
                id = long.Parse(Console.ReadLine());

                if (id == 0)
                {
                    break;
                }

                pedido = PedidoModel.pedidos.Find(p => p.PedidoID == id);
            }

            if (id == 0)
            {
                return;
            }

            pedido.Status = (StatusPedido) 1;
            Console.WriteLine("Pedido confirmado com sucesso!");
        }

        private void CancelarPedido()
        {
            Console.Clear();
            Console.WriteLine("Cancelamento de Pedido:");
            Console.WriteLine();

            Console.Write("ID do pedido: ");
            long id = long.Parse(Console.ReadLine());

            PedidoModel pedido = PedidoModel.pedidos.Find(p => p.PedidoID == id);

            while (pedido == null || pedido.Status != StatusPedido.Processando)
            {
                Console.WriteLine("Pedido não existe ou inválido!");
                Console.Write("Informe novamente o ID (0 para voltar): ");
                id = long.Parse(Console.ReadLine());

                if(id == 0)
                {
                    break;
                }

                pedido = PedidoModel.pedidos.Find(p => p.PedidoID == id);
            }

            if(id == 0)
            {
                return;
            }

            foreach (var prod in pedido.ProdutosPedido)
            {
                ProdutoModel produto = ProdutoModel.produtos.Find(p => p.ProdutoID == prod.Produto.ProdutoID);
                produto.Estoque += prod.Quantidade;
            }

            pedido.Status = (StatusPedido)2;

            Console.WriteLine("Pedido cancelado com sucesso!");

        }

        private ClienteModel SelecionarCliente()
        {
            Console.Clear();
            Console.WriteLine("Selecione o cliente:");

            foreach (var cliente in ClienteModel.clientes)
            {
                Console.WriteLine($"ID: {cliente.ClienteID} | Nome: {cliente.Nome} | Endereço: {cliente.Endereco}");
            }

            Console.Write("Digite o ID do cliente ou 0 para voltar: ");
            int idCliente;
            while (!int.TryParse(Console.ReadLine(), out idCliente) || (idCliente != 0 && !ClienteModel.clientes.Exists(c => c.ClienteID == idCliente)))
            {
                Console.WriteLine("ID inválido! Digite novamente...");
                Console.Write("Digite o ID do cliente ou 0 para voltar: ");
            }
            if (idCliente == 0)
            {
                return null;
            }
            else
            {
                return ClienteModel.clientes.Find(c => c.ClienteID == idCliente);
            }
        }

        public void Excluir()
        {
            throw new NotImplementedException("");
        }

    }

}