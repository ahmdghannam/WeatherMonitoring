namespace WeatherMonitoring.models;

public class RainBot : WeatherBot
{
    protected override string BotName { get; set; } = "RainBot";
    protected sealed override string Message { get; set; } 
    private readonly double _humidityThreshold;

    RainBot(double humidityThreshold, string message)
    {
        this._humidityThreshold = humidityThreshold;
        this.Message = message;
    }
    

    public override void ReactToStateChange()
    {
        if (state.Temperature > _humidityThreshold)
        {
            EnableBot();
            Console.WriteLine(Message);
        }
        else
        {
            DisableBot();
        }
    }
}