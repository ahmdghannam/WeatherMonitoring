using System.Text.Json;
using WeatherMonitoring.Interactors;

namespace WeatherMonitoring.Tests;

public class BotsFactoryTests
{
    [Fact]
    public void CreateBots_FileDoesNotExist_ThrowsFileNotFoundException()
    {
        var factory = new BotsFactory("nonexistent.json");
        Assert.Throws<FileNotFoundException>(() => factory.CreateBots());
    }

    [Fact]
    public void CreateBots_ValidJson_ReturnsCorrectBots()
    {
        string json = @"{
            ""RainBot"": { ""SomeProp"": 1 },
            ""SunBot"": { ""SomeProp"": 2 }
        }";

        string filePath = Path.GetTempFileName();
        File.WriteAllText(filePath, json);

        var factory = new BotsFactory(filePath);
        var bots = factory.CreateBots();

        Assert.Equal(2, bots.Count);
        Assert.Contains(bots, b => b.GetType().Name == "RainBot");
        Assert.Contains(bots, b => b.GetType().Name == "SunBot");

        File.Delete(filePath);
    }
    [Fact]
    public void CreateBots_EmptyJson_ReturnsEmptyList()
    {
        string filePath = Path.GetTempFileName();
        File.WriteAllText(filePath, "{}");

        var factory = new BotsFactory(filePath);
        var bots = factory.CreateBots();

        Assert.Empty(bots);
        File.Delete(filePath);
    }

    [Fact]
    public void CreateBots_UnknownBot_ThrowsJsonException()
    {
        string json = @"{ ""AlienBot"": {} }";
        string filePath = Path.GetTempFileName();
        File.WriteAllText(filePath, json);

        var factory = new BotsFactory(filePath);
        Assert.Throws<JsonException>(() => factory.CreateBots());

        File.Delete(filePath);
    }
}