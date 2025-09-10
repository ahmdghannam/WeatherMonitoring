namespace WeatherMonitoring.models;

public class SnowBot : WeatherBot
{
    protected override string BotName { get; set; } = "SnowBot";
    protected sealed override string Message { get; set; }
    private readonly double _temperatureThreshold;

    SnowBot(double temperatureThreshold, string message)
    {
        this._temperatureThreshold = temperatureThreshold;
        this.Message = message;
    }

    public override void ReactToStateChange()
    {
        if (state.Temperature < _temperatureThreshold)
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