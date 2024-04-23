using h2dYatırım.Entities;
using Newtonsoft.Json;

namespace h2dYatırım.Services
{
    public static class CoinService
    {
        public static async Task<List<CoinData>> ServiceAsync()
        {
            List<CoinData> result = new List<CoinData>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://api.coincap.io/v2/assets";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        CoinDataResponse responseData = JsonConvert.DeserializeObject<CoinDataResponse>(jsonContent);

                        foreach (var coin in responseData.Data)
                        {
                            result.Add(coin);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"API isteği başarısız oldu. Hata kodu: {response.StatusCode}");
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu: {ex.Message}");
                    return result;
                }
            }
        }

        public static async Task<CoinData> ServiceGetAsync(List<CoinData> coins, string id)
        {
            CoinData result = coins.Find(item => item.Id == id);

            var foundItem = coins.Find(item => item.Id == id);

            return result;
        }
    }

}
