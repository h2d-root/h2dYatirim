using h2dYatirim.Domain.Entity;
using h2dYatırım.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace h2dYatirim.Infrastructure.Services
{
    public class ShareService
    {
        public static async Task<List<ShareCertificate>> GetStockDataAsync()
        {
            List<ShareCertificate> result = new List<ShareCertificate>();

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://api.collectapi.com/economy/liveBorsa";
                client.DefaultRequestHeaders.Add("authorization", "apikey 3JUCcNJqG6lJaVxsn5SXbB:0bKwqw7x95LKAZuFtlFtGi");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        ShareData data = JsonConvert.DeserializeObject<ShareData>(jsonContent);

                        foreach (var item in data.Result)
                        {
                            result.Add(item);
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
    }
}