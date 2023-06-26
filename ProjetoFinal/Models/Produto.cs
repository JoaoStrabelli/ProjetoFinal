using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorVendas
{
    public class ProdutoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? ProdutoID { get; set; }
        public Guid ProdutoIDGUID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public double Preco { get; set; }
        public long Estoque { get; set; }
        public CategoriaModel Categoria { get; set; }
        public static readonly List<ProdutoModel> produtos = new List<ProdutoModel>();


        // Default constructor required by Entity Framework Core
        public ProdutoModel()
        {
        }

        // Additional constructor with parameters for Nome, Preco, Estoque, and Categoria
        public ProdutoModel(string nome, double preco, long estoque, CategoriaModel categoria)
        {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
            Categoria = categoria;
            ProdutoIDGUID = Guid.NewGuid();
        }
    }

}
