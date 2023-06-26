using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorVendas
{
    public class CategoriaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? CategoriaID { get; set; }
        public Guid CategoriaIDGUID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public static List<CategoriaModel> categorias = new List<CategoriaModel>();
    }
}
