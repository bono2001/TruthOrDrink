using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TruthOrDrink.Services
{
    public class QuotesService
    {
        private const string ApiUrl = "https://zenquotes.io/api/random";
        private readonly HttpClient _httpClient;

        public QuotesService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> FetchRandomQuoteAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ruwe API-respons: {responseBody}"); // Log de respons

                var quoteItems = JsonSerializer.Deserialize<List<Quote>>(responseBody);

                if (quoteItems != null && quoteItems.Count > 0)
                {
                    string quote = quoteItems[0].Q;
                    string author = quoteItems[0].A;

                    Console.WriteLine($"Quote: {quote}");
                    Console.WriteLine($"Author: {author}");

                    if (!string.IsNullOrEmpty(quote) && !string.IsNullOrEmpty(author))
                    {
                        return $"{quote} - {author}";
                    }

                    Console.WriteLine("Lege quote of auteur gevonden. Gebruik fallback.");
                    return "Het leven is wat je ervan maakt. - Onbekend";
                }

                throw new Exception("Geen quotes gevonden in de API-respons.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het ophalen van quotes: {ex.Message}");
                return "Fout bij ophalen van quote. - Onbekend";
            }
        }

        private class Quote
        {
            [JsonPropertyName("q")]
            public string Q { get; set; } // De tekst van de quote

            [JsonPropertyName("a")]
            public string A { get; set; } // De auteur

            [JsonPropertyName("h")]
            public string H { get; set; } // HTML-weergave
        }
    }
}
