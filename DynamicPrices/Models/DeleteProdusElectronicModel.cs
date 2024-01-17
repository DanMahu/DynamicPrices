namespace DynamicPrices.Models
{
    public class DeleteProdusElectronicModel
    {
        public int IdProdus { get; set; }
        public virtual ICollection<Stoc_Electronice> Stoc { get; set; }
        public virtual ICollection<Preturi_Electronice> pretCurent { get; set; }             
        public virtual ICollection<Istoric_Preturi_Electronice> IstoricPreturi { get; set; }
    }
}
