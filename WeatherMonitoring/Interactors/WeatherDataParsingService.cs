using WeatherMonitoring.models;

namespace WeatherMonitoring.Interactors;

public class WeatherDataParsingService
{
    private readonly JsonParser _jsonParser = new JsonParser();
    private readonly XmlParser _xmlParser = new XmlParser();

    public WeatherState? ParseData(string data, WeatherDataForm form)
    {
        return form switch
        {
            WeatherDataForm.JSON => _jsonParser.Parse(data),
            WeatherDataForm.XML => _xmlParser.Parse(data),
            _ => throw new ArgumentOutOfRangeException(nameof(form), $"Unsupported format: {form}")
        };
    }
}