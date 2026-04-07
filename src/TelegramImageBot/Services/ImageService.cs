using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TelegramImageBot.Services
{
    public class ImageService
    {
        private readonly HttpClient _httpClient;
        private const string AccessKey = "6wqw1qwCBHJSVIl4FjNbU4LtgOSa4-rcOxNh3VMzcTg";

        public ImageService()
        {
            _httpClient = new HttpClient();
           
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "TelegramImageBot/1.0");
        }

        public async Task<List<string>> SearchImagesAsync(string query, int count = 3)
        {
            string url = $"https://api.unsplash.com/search/photos?query={query}&client_id={AccessKey}&per_page={count}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;
                JsonElement results = root.GetProperty("results");

                List<string> imageUrls = new List<string>();

                foreach (JsonElement item in results.EnumerateArray())
                {
                    string imageUrl = item.GetProperty("urls").GetProperty("regular").GetString();
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        imageUrls.Add(imageUrl);
                    }
                }

                return imageUrls;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API da xatolik yuz berdi: {ex.Message}");
                return new List<string>();
            }
        }
    }
}