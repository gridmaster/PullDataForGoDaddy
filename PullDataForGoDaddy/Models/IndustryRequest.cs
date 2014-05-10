using System.Collections.Generic;
using Newtonsoft.Json;

namespace PullDataForGoDaddy.Models
{
    public class IndustryRequest : BaseRequestData
    {
        [JsonProperty(PropertyName = "industries")]
        public List<WCFIndustry> industries { get; set; }
    }
}
