using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GerenciadorVendas
{
    public class PedidoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? PedidoID { get; set; }

        public Guid PedidoIDGUID { get; set; }

        public DateTime Data { get; set; }

        public long ClienteModelID { get; set; } // Foreign key property

        [ForeignKey("ClienteModelID")]
        public ClienteModel Cliente { get; set; } // Navigation property

        public StatusPedido Status { get; set; } = StatusPedido.Processando;

        public List<PedidoProdutoModel> ProdutosPedido { get; set; } = new List<PedidoProdutoModel>();

        public static readonly List<PedidoModel> pedidos = new List<PedidoModel>();

        // Default constructor required by Entity Framework Core
        public PedidoModel()
        {
            PedidoIDGUID = Guid.NewGuid();
            Data = DateTime.Now;
        }

        // Additional constructor with ClienteModel parameter
        public PedidoModel(ClienteModel cliente) : this()
        {
            Cliente = cliente;
        }

        public double Total
        {
            get
            {
                try
                {
                    return ProdutosPedido!.Sum(p => p.Subtotal);
                }
                catch (NullReferenceException nrfe)
                {
                    throw new Exception($"Nota sem venda: {nrfe.Message}");
                }
            }
        }
    }
}
