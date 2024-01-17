using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicPrices.Models
{
    public class Produse_Electronice
    {
        [Key]
        public int IdProdus { get; set; }

        [Required(ErrorMessage = "Numele produsului este obligatioriu!")]
        [MaxLength(150)]
        public string NumeProdus { get; set; }

        [Required(ErrorMessage = "Tipul produsului este obligatioriu!")]
        [MaxLength(100)]
        public string TipProdus { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CostProducere { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PretRecomandat {  get; set; }

        [Required(ErrorMessage = "Descrierea produsului este obligatorie!")]
        [Column(TypeName = "text")]
        public string Descriere { get; set; }
    }
}
