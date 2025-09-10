using WeatherMonitoring.Interactors;
using WeatherMonitoring.models;

namespace WeatherMonitoring;

abstract class MainPage
{
    static string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
    static string jsonPath = Path.Combine(projectRoot, "bots.json");
    
    private static readonly WeatherStateProvider Provider = new();
    private static readonly BotsFactory BotsFactory = new(jsonPath);

    static void Main(string[] args)
    {
        var bots = BotsFactory.CreateBots();
        SubscribeBotsToProvider(bots);
        while (true)
        {

        }
    }

    private static void SubscribeBotsToProvider(List<WeatherBot> bots)
    {
        foreach (var bot in bots)
        {
            Provider.AddSubscriber(bot);
        }
    }
}