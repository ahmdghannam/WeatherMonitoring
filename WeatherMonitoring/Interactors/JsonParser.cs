using System.Text.Json;
using WeatherMonitoring.models;

namespace WeatherMonitoring.Interactors;

public class JsonParser : IWeatherDataParser
{
    public WeatherState? Parse(string data)
    {
        try
        {
            var doc = JsonDocument.Parse(data);

            var root = doc.RootElement;

            string? location = root.GetProperty("Location").GetString();
            double? temperature = root.TryGetProperty("Temperature", out var t) && t.TryGetDouble(out var tempVal) ? tempVal : null;
            double? humidity = root.TryGetProperty("Humidity", out var h) && h.TryGetDouble(out var humVal) ? humVal : null;

            if (location is null || temperature is null || humidity is null)
                return null;

            return new WeatherState(location, temperature.Value, humidity.Value);
        }
        catch
        {
            return null;
        }
    }
}