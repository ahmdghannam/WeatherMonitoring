namespace WeatherMonitoring.models;

public record class WeatherState
{
    public string? Location { get; private set; }
    public double? Temperature { get; private set; }
    public double? Humidity { get; private set; }

    public WeatherState()
    {
    }

    public WeatherState(string location, double temperature, double humidity)
    {
        this.Location = location;
        this.Temperature = temperature;
        this.Humidity = humidity;
    }

    public void UpdateState(string? location, double? temperature, double? humidity)
    {
        this.Location = location;
        this.Temperature = temperature;
        this.Humidity = humidity;
    }
}