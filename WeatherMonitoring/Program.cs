using System.Text;
using WeatherMonitoring.Interactors;
using WeatherMonitoring.models;

namespace WeatherMonitoring;

abstract class MainPage
{
    static string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
    static string jsonPath = Path.Combine(projectRoot, "bots.json");

    private static readonly WeatherStateProvider Provider = new();
    private static readonly BotsFactory BotsFactory = new(jsonPath);
    private static readonly WeatherDataParsingService ParsingService = new();

    static void Main(string[] args)
    {
        var bots = BotsFactory.CreateBots();
        SubscribeBotsToProvider(bots);

        while (true)
        {
            var input = ReadUserInput();
            if (input == null)
            {
                Console.WriteLine("INVALID INPUT");
                continue;
            }

            var data = input.Value.Item1;
            var form = input.Value.Item2;
            var weatherState = ParsingService.ParseData(data, form);

            if (weatherState == null)
            {
                Console.WriteLine("INVALID INPUT");
            }
            else
            {
                Provider.UpdateState(weatherState);
            }
        }
    }

    private static (string, WeatherDataForm)? ReadUserInput()
    {
        Console.WriteLine("Please enter the data form you want to insert:");
        Console.WriteLine("1. JSON");
        Console.WriteLine("2. XML");

        var input = int.TryParse(Console.ReadLine(), out var result);

        if (!input)
        {
            Console.WriteLine("Please enter a valid number");
            return null;
        }

        Console.WriteLine("please enter the data");
        var stringBuilder = new StringBuilder();
        string? line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            stringBuilder.AppendLine(line);
        }

        var data = stringBuilder.ToString();

        switch (result)
        {
            case 1: return (data, WeatherDataForm.JSON);
            case 2: return (data, WeatherDataForm.XML);
            default: return null;
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