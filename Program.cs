using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherData
{
    public string Name { get; set; }
    public MainData Main { get; set; }
}

public class MainData
{
    public double Temp { get; set; }
}

class Program
{
    static async Task Main(string[] args)
{
    Console.WriteLine("Enter a city name:");
    string cityName = Console.ReadLine();

    Console.WriteLine("Enter your API key:"); //get api key from website openweathermap.org
    string apiKey = Console.ReadLine();

    WeatherData data = await GetWeatherDataAsync(cityName, apiKey);

    if (data != null)
    {
        Console.WriteLine($"Current temperature in {data.Name}: {data.Main.Temp}°C");
    }
    else
    {
        Console.WriteLine("Unable to retrieve weather data.");
    }
}

    public static async Task<WeatherData> GetWeatherDataAsync(string cityName, string apiKey)
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherData>(json);
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Unable to retrieve weather data. Please check your API key and city name.");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while retrieving weather data: {e.Message}");
            return null;
        }
    }
}
}



