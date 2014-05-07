using System;

namespace PullDataForGoDaddy.Models
{
    public class Industry
    {
        public int Id { get; set; }
        public int SectorId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public decimal OneDayPriceChgPerCent { get; set; }
        public string MarketCap { get; set; }
        public decimal PriceToEarnings { get; set; }
        public decimal ROEPerCent { get; set; }
        public decimal DivYieldPerCent { get; set; }
        public decimal DebtToEquity { get; set; }
        public decimal PriceToBook { get; set; }
        public decimal NetProfitMarginMrq { get; set; }
        public decimal PriceToFreeCashFlowMrq { get; set; }
    }
}
