using System;
using System.Globalization;
using Newtonsoft.Json;

namespace PullDataForGoDaddy.Models
{
    public class WCFIndustry
    {
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "SectorId")]
        public int SectorId { get; set; }

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

        public WCFIndustry()
        {
        }

        public WCFIndustry(Industry industry)
        {
            Id = industry.Id;
            SectorId = industry.SectorId;
            WCFXferDate = industry.Date.ToString(CultureInfo.InvariantCulture);
            Name = industry.Name;
            OneDayPriceChgPerCent = industry.OneDayPriceChgPerCent;
            MarketCap = industry.MarketCap;
            PriceToEarnings = industry.PriceToEarnings;
            ROEPerCent = industry.ROEPerCent;
            DivYieldPerCent = industry.DivYieldPerCent;
            DebtToEquity = industry.DebtToEquity;
            PriceToBook = industry.PriceToBook;
            NetProfitMarginMrq = industry.NetProfitMarginMrq;
            PriceToFreeCashFlowMrq = industry.PriceToFreeCashFlowMrq;
        }

        public Industry ConvertToIndustry()
        {
            return new Industry
                {
                    Id = this.Id,
                    SectorId = this.SectorId,
                    Date = Convert.ToDateTime(this.WCFXferDate),
                    Name = this.Name,
                    OneDayPriceChgPerCent = this.OneDayPriceChgPerCent,
                    MarketCap = this.MarketCap,
                    PriceToEarnings = this.PriceToEarnings,
                    ROEPerCent = this.ROEPerCent,
                    DivYieldPerCent = this.DivYieldPerCent,
                    DebtToEquity = this.DebtToEquity,
                    PriceToBook = this.PriceToBook,
                    NetProfitMarginMrq = this.NetProfitMarginMrq,
                    PriceToFreeCashFlowMrq = this.PriceToFreeCashFlowMrq
                };
        }
    }
}