using System.Globalization;
using Newtonsoft.Json;

namespace PullDataForGoDaddy.Models
{
    public class WCFSector
    {
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "WCFXferDate")]
        public string WCFXferDate { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "OneDayPriceChgPerCent")]
        public decimal OneDayPriceChgPerCent { get; set; }
        [JsonProperty(PropertyName = "MarketCap")]
        public string MarketCap { get; set; }
        [JsonProperty(PropertyName = "PriceToEarnings")]
        public decimal PriceToEarnings { get; set; }
        [JsonProperty(PropertyName = "ROEPerCent")]
        public decimal ROEPerCent { get; set; }
        [JsonProperty(PropertyName = "DivYieldPerCent")]
        public decimal DivYieldPerCent { get; set; }
        [JsonProperty(PropertyName = "DebtToEquity")]
        public decimal DebtToEquity { get; set; }
        [JsonProperty(PropertyName = "PriceToBook")]
        public decimal PriceToBook { get; set; }
        [JsonProperty(PropertyName = "NetProfitMarginMrq")]
        public decimal NetProfitMarginMrq { get; set; }
        [JsonProperty(PropertyName = "PriceToFreeCashFlowMrq")]
        public decimal PriceToFreeCashFlowMrq { get; set; }

        public WCFSector(){}

        public WCFSector(Sector sector)
        {
            Id = sector.Id;
            WCFXferDate = sector.Date.ToString(CultureInfo.InvariantCulture);
            Name = sector.Name;
            OneDayPriceChgPerCent = sector.OneDayPriceChgPerCent;
            MarketCap = sector.MarketCap;
            PriceToEarnings = sector.PriceToEarnings;
            ROEPerCent = sector.ROEPerCent;
            DivYieldPerCent = sector.DivYieldPerCent;
            DebtToEquity = sector.DebtToEquity;
            PriceToBook = sector.PriceToBook;
            NetProfitMarginMrq = sector.NetProfitMarginMrq;
            PriceToFreeCashFlowMrq = sector.PriceToFreeCashFlowMrq;
        }
    }
}
