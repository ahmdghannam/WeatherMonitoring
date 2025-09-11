using System.Xml.Linq;
using WeatherMonitoring.models;

namespace WeatherMonitoring.Interactors;

public class XmlParser : IWeatherDataParser
{
    public WeatherState? Parse(string data)
    {
        try
        {
            var doc = XDocument.Parse(data);
            var root = doc.Element("WeatherData");
            if (root == null) return null;

            string? location = root.Element("Location")?.Value;
            double? temperature = double.TryParse(root.Element("Temperature")?.Value, out var tempVal) ? tempVal : null;
            double? humidity = double.TryParse(root.Element("Humidity")?.Value, out var humVal) ? humVal : null;

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