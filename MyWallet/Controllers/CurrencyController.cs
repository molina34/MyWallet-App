using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MyWallet.Models;
using Newtonsoft.Json;

namespace MyWallet.Controllers
{
    public class CurrencyController
    {
        public async Task<List<CurrencyModel>> LoadCurrenciesAsync()
        {
            var currencies = new List<CurrencyModel>();
            try
            {
                var resultJson = await new HttpClient().GetStringAsync("https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json");
                currencies = JsonConvert.DeserializeObject<CurrenciesModel>(resultJson).Currencies;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"LoadAllTransactionsAsync error: {exception.Message}");
            }

            return currencies;
        }
    }
}

