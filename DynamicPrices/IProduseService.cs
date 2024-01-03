using DynamicPrices.Models;

namespace DynamicPrices
{
    public interface IProduseService
    {
        Dictionary<string, int> GetTipProduseElectronice();
        List<Dictionary<string, object>> GetProduseDupaTip(string tipProdus);
        List<string> GetProduseElectronice(string tipProdus);
    }
}
