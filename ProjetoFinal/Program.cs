using JRJ.Modas;

bool continuar = true;
do
{
    Console.Clear();
    Console.WriteLine("1 - Gerenciar produtos");
    Console.WriteLine("2 - Gerenciar categorias");
    Console.WriteLine("3 - Gerenciar clientes");
    Console.WriteLine("4 - Gerenciar pedido");
    Console.WriteLine("5 - Mostrar relatório de pedidos");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            ProdutoUI produtoUI = new();
            produtoUI.Menu();
            break;
        case "2":
            CategoriaUI categoriaUI = new();
            categoriaUI.Menu();
            break;
        case "3":
            ClientesUI clienteUI = new();
            clienteUI.Menu();
            break;
        case "4":
            PedidoUI vendaUI = new();
            vendaUI.Menu();
            break;
        case "5":
            RelatorioUI relatorioUI = new();
            relatorioUI.MostrarRelatorioPedidos();
            break;
        case "0":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

} while (continuar);