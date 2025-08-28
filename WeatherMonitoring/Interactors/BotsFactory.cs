using System.Text.Json;
using WeatherMonitoring.models;

namespace WeatherMonitoring.Interactors;

public class BotsFactory
{
    private readonly string _filePath;

    public BotsFactory(string filePath)
    {
        _filePath = filePath;
    }

    public List<WeatherBot> CreateBots()
    {
        if (!File.Exists(_filePath))
            throw new FileNotFoundException("Bots JSON file not found.", _filePath);

        var json = File.ReadAllText(_filePath);
        using var doc = JsonDocument.Parse(json);

        var bots = new List<WeatherBot>();

        foreach (var property in doc.RootElement.EnumerateObject())
        {
            
            WeatherBot? bot = property.Name switch
            {
                "RainBot" => JsonSerializer.Deserialize<RainBot>(property.Value.GetRawText()),
                "SunBot" => JsonSerializer.Deserialize<SunBot>(property.Value.GetRawText()),
                "SnowBot" => JsonSerializer.Deserialize<SnowBot>(property.Value.GetRawText()),
                _ => throw new JsonException($"Unknown bot name: {property.Name}")
            };

            if (bot != null)
            {
                bots.Add(bot);
            }
        }

        return bots;
    }
}