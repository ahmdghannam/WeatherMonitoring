using System.Text.Json.Serialization;

namespace WeatherMonitoring.models;

public class SnowBot : WeatherBot
{
    public override string BotName => "SnowBot";

    [JsonPropertyName("temperatureThreshold")]
    public double TemperatureThreshold { get; set; }

    public SnowBot()
    {
    }

    public SnowBot(double temperatureThreshold, string message)
    {
        this.TemperatureThreshold = temperatureThreshold;
        this.Message = message;
    }

    protected override void ReactToStateChange()
    {
        if (state.Temperature < TemperatureThreshold)
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