using WeatherMonitoring.models;

namespace WeatherMonitoring.Interactors;

public interface IWeatherDataParser
{
    WeatherState? Parse(string data);
}