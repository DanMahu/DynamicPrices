using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DynamicPrices.Models
{
    public class Istoric_Preturi_Electronice
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Produse_Electronice")]
        public int IdProdus { get; set; }
        public virtual Produse_Electronice Produse_Electronice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PretVechi { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PretNou { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataModificare { get; set; }
    }
}
