using System.Text.Json.Serialization;

namespace WeatherMonitoring.models;

public class SunBot : WeatherBot
{
    public override string BotName => "SunBot";
    [JsonPropertyName("temperatureThreshold")]
    public double TemperatureThreshold { get; set; }
    public SunBot() { }
    public SunBot(double temperatureThreshold, string message)
    {
        this.TemperatureThreshold = temperatureThreshold;
        this.Message = message;
    }

    protected override void ReactToStateChange()
    {
        if (state.Temperature > TemperatureThreshold)
        {
            EnableBot();
            Console.WriteLine(Message);
        }
        else
        {
            DisableBot();
        }
    }
    public override string ToString()
    {
        return $"name {BotName} message {Message}  threshold {TemperatureThreshold}";
    }
}