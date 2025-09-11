using WeatherMonitoring.models;

namespace WeatherMonitoring;

public class WeatherStateProvider
{
    private readonly WeatherState _currentState = new();

    private readonly List<WeatherBot> _subscribers = [];

    public void AddSubscriber(WeatherBot bot)
    {
        _subscribers.Add(bot);
    }

    public void UpdateState(WeatherState state)
    {
        _currentState.UpdateState(state.Location, state.Temperature, state.Humidity);
        NotifySubscribers();
    }

    private void NotifySubscribers()
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.UpdateState(_currentState);
        }
    }
}