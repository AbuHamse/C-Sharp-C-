using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Fetching data from API...");
        string data = await FetchDataFromAPI();
        Console.WriteLine("Received data:");
        Console.WriteLine(data);
    }

    static async Task<string> FetchDataFromAPI()
    {
        using HttpClient client = new HttpClient();

        try
        {
            // Simulate fetching data (this URL returns dummy JSON)
            string result = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos/1");
            return result;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
