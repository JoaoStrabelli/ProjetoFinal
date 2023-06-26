using Microsoft.EntityFrameworkCore;
using JRJ.Modas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.db
{
    public class Context: DbContext
    {
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<PedidoProdutoModel> PedidosProdutos { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseMySql("server=localhost;database=BancodeDados;user=root;password=",
                                    new MySqlServerVersion("10.4.28-MariaDB")); // versão do servidor MySQL
        }

    }
}
