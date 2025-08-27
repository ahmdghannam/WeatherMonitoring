namespace WeatherMonitoring.models;

public abstract class WeatherBot
{
    protected WeatherState state = new WeatherState();
    private bool isEnabled = false;
    protected abstract string BotName { get; set; }
    protected abstract string Message { get; set; }

    protected void EnableBot()
    {
        if (!isEnabled)
        {
            Console.WriteLine($" {BotName} is enabled");
        }

        isEnabled = true;
    }

    protected void DisableBot()
    {
        if (isEnabled)
        {
            Console.WriteLine($" {BotName} is disabled");
        }

        isEnabled = false;
    }

    public void UpdateState(WeatherState newState)
    {
        state.UpdateState(newState.Location, newState.Temperature, newState.Humidity);
        ReactToStateChange();
    }

    public abstract void ReactToStateChange();
}