using System.Text.Json.Serialization;

namespace WeatherMonitoring.models;

public class RainBot : WeatherBot
{
    public override string BotName => "RainBot";
    [JsonPropertyName("humidityThreshold")]
    public double HumidityThreshold { get; set; }
    public RainBot() { }
    public RainBot(double humidityThreshold, string message)
    {
        this.HumidityThreshold = humidityThreshold;
        this.Message = message;
    }


    protected override void ReactToStateChange()
    {
        if (state.Temperature > HumidityThreshold)
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
        return $"name {BotName} message {Message}  threshold {HumidityThreshold}";
    }
}