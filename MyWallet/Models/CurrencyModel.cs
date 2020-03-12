using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyWallet.Models
{
    public class CurrenciesModel
    {
        [JsonProperty(PropertyName = "value")]
        public List<CurrencyModel> Currencies { get; set; }
    }

    public class CurrencyModel
    {
        [JsonProperty(PropertyName = "simbolo")]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "nomeFormatado")]
        public string Name { get; set; }
    }
}
