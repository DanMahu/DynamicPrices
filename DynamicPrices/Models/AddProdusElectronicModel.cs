using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DynamicPrices.Models
{
    public class AddProdusElectronicModel
    {
        [Required(ErrorMessage = "Numele produsului este obligatoriu!")]
        [DisplayName("Nume Produs")]
        [MaxLength(100)]
        public string NumeProdus { get; set; }

        [Required(ErrorMessage = "Tipul produsului este obligatoriu!")]
        [DisplayName("Tip Produs")]
        [MaxLength(55)]
        public string TipProdus { get; set; }

        [DisplayName("Cost Producere")]
        [Range(0.00, 99999999.99, ErrorMessage = "Cost invalid!")]
        public decimal CostProducere { get; set; }
        
        [DisplayName("Preț Recomandat")]
        [Range(0.00, 99999999.99, ErrorMessage = "Preț invalid!")]
        public decimal PretRecomandat { get; set; }
        
        [Required(ErrorMessage = "Prețul produsului este obligatoriu!")]
        [DisplayName("Preț Curent")]
        [Range(0.00, 99999999.99, ErrorMessage = "Preț invalid!")]
        public decimal PretCurent { get; set; }

        [Required(ErrorMessage = "Descrierea produsului este obligatorie!")]
        public string Descriere { get; set; }
    }
}
