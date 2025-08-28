using System.Text.Json.Serialization;

namespace WeatherMonitoring.models;

public abstract class WeatherBot
{


    protected WeatherState state = new WeatherState();
    public bool _isEnabled { get; private set; }
    public abstract string BotName { get; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    
    protected void EnableBot()
    {
        if (!_isEnabled)
        {
            Console.WriteLine($" {BotName} is enabled");
        }

        _isEnabled = true;
    }

    protected void DisableBot()
    {
        if (_isEnabled)
        {
            Console.WriteLine($" {BotName} is disabled");
        }

        _isEnabled = false;
    }

    public void UpdateState(WeatherState newState)
    {
        state.UpdateState(newState.Location, newState.Temperature, newState.Humidity);
        ReactToStateChange();
    }

    protected abstract void ReactToStateChange();

    public override string ToString()
    {
        return $"name {BotName} message {Message} isEnabled {_isEnabled} ";
    }
}