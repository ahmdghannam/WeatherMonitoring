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

    public void UpdateState(double location, double temperature, double humidity)
    {
        _currentState.UpdateState(location, temperature, humidity);
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