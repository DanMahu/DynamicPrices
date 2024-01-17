using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicPrices.Models
{
    public class Vanzari_Electronice
    {
        [Key]
        public int IdTranzactie { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DataTranzactie { get; set; }

        [ForeignKey("Clienti")]
        public int IdClient { get; set; }
        public virtual Clienti Clienti { get; set; }

        [ForeignKey("Produse_Electronice")]
        public int IdProdus { get; set; }
        public virtual Produse_Electronice Produse_Electronice { get; set; }

        public int Cantitate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PretTotal { get; set; }
    }
}
