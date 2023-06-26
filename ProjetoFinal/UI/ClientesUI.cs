using Gamificação_01___Final.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoFinal.db;



namespace GerenciadorVendas
{

    public class ClientesUI: IUserInterface
    {
        public void Menu()
        {
            bool continuar = true;
            do
            { 
                Console.Clear();
                Console.WriteLine("1 - Listar clientes");
                Console.WriteLine("2 - Cadastrar cliente");
                Console.WriteLine("3 - Alterar cliente");
                Console.WriteLine("4 - Excluir cliente");
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
                        Excluir();
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

            using (var db = new Context())
            {
                try
                {
                    if (db.Clientes.Count() == 0)
                    {
                        throw new Exception("Não há nenhum cliente cadastrado!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Lista de clientes:");
                foreach (var cliente in db.Clientes)
                {
                    Console.WriteLine($"ID: {cliente.ClienteID} | Nome Completo: {cliente.NomeCompleto} | Endereço: {cliente.Endereco} | Telefone: {cliente.Telefone}");
                }
            }
        }

        public void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("Cadastro de cliente:");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Sobrenome: ");
            string sobrenome = Console.ReadLine();

            Console.Write("Endereço: ");
            string endereco = Console.ReadLine();

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();

            using (var db = new Context())
            {
                ClienteModel cliente = new ClienteModel
                {
                    ClienteIDGUID = Guid.NewGuid(),
                    Nome = nome,
                    Sobrenome = sobrenome,
                    Endereco = endereco,
                    Telefone = telefone
                };

                db.Clientes.Add(cliente);
                db.SaveChanges();
            }

            Console.WriteLine();
            Console.WriteLine("Cliente cadastrado com sucesso!");
        }

        public void Alterar()
        {
            Console.Clear();

            using (var db = new Context())
            {
                try
                {
                    if (db.Clientes.Count() == 0)
                    {
                        throw new Exception("Não há nenhum cliente cadastrado!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Alterar cliente");

                long id;

                while (true)
                {
                    Console.Write("ID do cliente: ");
                    try
                    {
                        id = long.Parse(Console.ReadLine());
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("ID inválido. Digite um valor numérico válido.");
                    }
                }

                var cliente = db.Clientes.FirstOrDefault(c => c.ClienteID == id);

                while (cliente == null)
                {
                    Console.Write("\nCliente não encontrada!\nDigite novamente o código do cliente: ");
                    id = long.Parse(Console.ReadLine());
                    cliente = db.Clientes.FirstOrDefault(c => c.ClienteID == id);
                }

                Console.Write($"\nDigite o novo nome ({cliente.Nome}): ");
                string nome = Console.ReadLine();

                Console.Write($"\nDigite o novo sobrenome ({cliente.Sobrenome}): ");
                string sobrenome = Console.ReadLine();

                Console.Write($"\nDigite o novo endereço({cliente.Endereco}): ");
                string endereco = Console.ReadLine();

                Console.Write($"\nDigite o novo telefone ({cliente.Telefone}): ");
                string telefone = Console.ReadLine();

                cliente.Nome = nome;
                cliente.Sobrenome = sobrenome;
                cliente.Endereco = endereco;
                cliente.Telefone = telefone;

                db.SaveChanges();
            }

            Console.WriteLine("Cliente alterado com sucesso!");
        }

        public void Excluir()
        {
            Console.Clear();

            using (var db = new Context())
            {
                try
                {
                    if (db.Clientes.Count() == 0)
                    {
                        throw new Exception("Não há nenhum cliente cadastrado!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Excluir cliente:\n");

                long id;

                while (true)
                {
                    Console.Write("ID do cliente: ");
                    try
                    {
                        id = long.Parse(Console.ReadLine());
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("ID inválido. Digite um valor numérico válido.");
                    }
                }

                var cliente = db.Clientes.FirstOrDefault(c => c.ClienteID == id);

                while (cliente == null)
                {
                    Console.Write("\nCliente não encontrado!\nDigite novamente o código do cliente: ");
                    id = long.Parse(Console.ReadLine());
                    cliente = db.Clientes.FirstOrDefault(c => c.ClienteID == id);
                }

                db.Clientes.Remove(cliente);
                db.SaveChanges();

                Console.WriteLine("Cliente excluido com sucesso!");
            }
        }
    }
}