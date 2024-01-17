using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicPrices.Models
{
    public class Clienti
    {
        [Key]
        public int IdClient { get; set; }

        [Column(TypeName = "varchar(60)")]
        public string Nume { get; set; }

        [Column(TypeName = "varchar(60)")]
        public string Prenume { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        public int Telefon {  get; set; }
    }
}
