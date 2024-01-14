using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicPrices.Models
{
    public class Preturi_Electronice
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Produse_Electronice")]
        public int IdProdus { get; set; }
        public virtual Produse_Electronice Produse_Electronice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PretCurent { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataActualizare { get; set; }
    }
}
