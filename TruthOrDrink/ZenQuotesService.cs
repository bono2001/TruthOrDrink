using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class ZenQuotesService
{
    private readonly HttpClient _httpClient;

    public ZenQuotesService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetRandomQuoteAsync()
    {
        string url = "https://zenquotes.io/api/random";

        try
        {
            // Haal de JSON-string op van de API
            var response = await _httpClient.GetStringAsync(url);
            Console.WriteLine($"API Response: {response}"); // Log de API-respons

            // Deserializeer de JSON-reactie naar een lijst van ZenQuote
            var quotes = JsonSerializer.Deserialize<List<ZenQuote>>(response);

            if (quotes != null && quotes.Count > 0)
            {
                // Controleer of de velden correct gevuld zijn
                Console.WriteLine($"Quote: {quotes[0].Q}, Author: {quotes[0].A}");
                return $"{quotes[0].Q} - {quotes[0].A}";
            }
            else
            {
                Console.WriteLine("Geen quotes gevonden in de API-respons.");
                return "Geen quote beschikbaar.";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fout bij het ophalen van de quote: {ex.Message}");
            return "Fout bij het ophalen van de quote.";
        }
    }


    private class ZenQuote
    {
        [JsonPropertyName("q")] // Map het JSON-veld "q" naar de eigenschap Q
        public string Q { get; set; }

        [JsonPropertyName("a")] // Map het JSON-veld "a" naar de eigenschap A
        public string A { get; set; }

        [JsonPropertyName("h")] // Map het JSON-veld "h" naar de eigenschap H (optioneel)
        public string H { get; set; }
    }
}
