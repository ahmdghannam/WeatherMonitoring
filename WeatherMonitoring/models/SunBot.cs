namespace WeatherMonitoring.models;

public class SunBot : WeatherBot
{
    private readonly double _temperatureThreshold;
    protected override string BotName { get; set; } = "SunBot";
    protected sealed override string Message { get; set; }

    public SunBot(double humedityThreshold, string message)
    {
        _temperatureThreshold = humedityThreshold;
        Message = message;
    }
    

    public override void ReactToStateChange()
    {
        if (state.Temperature > _temperatureThreshold)
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