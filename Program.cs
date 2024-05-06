using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductSearch
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SearchEngine searchEngine = new SearchEngine();

            Console.Write("Enter the product name to search for: ");
            string productName = Console.ReadLine();

            string searchResults = await searchEngine.SearchProductAsync(productName);

            if (searchResults != null)
            {
                Console.WriteLine($"Search results for '{productName}':");
                Console.WriteLine(searchResults);
            }
            else
            {
                Console.WriteLine("Failed to retrieve search results.");
            }

            Console.ReadLine();
        }
    }

    public class SearchEngine
    {
        private readonly HttpClient _httpClient;

        public SearchEngine()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> SearchProductAsync(string productName)
        {
            try
            {
                string encodedProductName = Uri.EscapeDataString(productName);
                string searchUrl = $"https://www.example.com/search?q={encodedProductName}";

                HttpResponseMessage response = await _httpClient.GetAsync(searchUrl);
                response.EnsureSuccessStatusCode();

                string searchResults = await response.Content.ReadAsStringAsync();
                return searchResults;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the search
                Console.WriteLine($"An error occurred while searching for the product: {ex.Message}");
                return null;
            }
        }
    }
}