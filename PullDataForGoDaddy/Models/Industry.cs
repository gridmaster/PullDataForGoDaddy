using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PullDataForGoDaddy.Models
{
    public class Industry
    {
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "SectorId")]
        public int SectorId { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public DateTime Date { get; set; }

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

        private string dateForSerialization;

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            this.dateForSerialization = this.Date.ToString("MM/dd/yyyy",
                CultureInfo.InvariantCulture);
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext context)
        {
            this.dateForSerialization = "1900-01-01";
        }
    }
}
