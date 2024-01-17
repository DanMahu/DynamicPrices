using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicPrices.Models
{
    public class Stoc_Electronice
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Produse_Electronice")]
        public int IdProdus { get; set; }
        public virtual Produse_Electronice Produse_Electronice { get; set; }

        public int InStoc { get; set; }
        public int StocMinim { get; set; }
        public int StocMaxim { get; set; }
    }
}
