using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorVendas
{
    public class PedidoProdutoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? PedidoProdutoID { get; set; }

        public ProdutoModel Produto { get; set; } // Navigation property

        public int Quantidade { get; set; }

        public double Subtotal => Produto.Preco * Quantidade;

        // Default constructor required by Entity Framework Core
        public PedidoProdutoModel()
        {
        }

        // Additional constructor with ProdutoModel and Quantidade parameters
        public PedidoProdutoModel(ProdutoModel produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }
    }

}
