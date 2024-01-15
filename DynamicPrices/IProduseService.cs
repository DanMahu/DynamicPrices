using DynamicPrices.Models;

namespace DynamicPrices
{
    public interface IProduseService
    {
        Dictionary<string, int> GetTipProduseElectronice();
        List<Dictionary<string, object>> GetProduseDupaTip(string tipProdus);
        Dictionary<int, string> GetProduseElectronice(string tipProdus);
        List<object> GetPriceHistoryByProduct(int product_id);
        int GetStocActualDupaProdus(int idProdus);
        decimal GetPretCurentDupaProdus(int idProdus);
        List<int> MinMaxStoc(int idProdus);
        List<Dictionary<string, object>> GetAllProduseElectronice();
    }
}
