using WeatherMonitoring.Interactors;

namespace WeatherMonitoring.Tests;

public class JsonParserTests
{
    [Fact]
    public void Parse_ValidJson_ReturnsWeatherState()
    {
        string json = @"{ ""Location"": ""London"", ""Temperature"": 25, ""Humidity"": 60 }";
        var parser = new JsonParser();

        var result = parser.Parse(json);

        Assert.NotNull(result);
        Assert.Equal("London", result.Location);
        Assert.Equal(25, result.Temperature);
        Assert.Equal(60, result.Humidity);
    }

    [Fact]
    public void Parse_InvalidJson_ReturnsNull()
    {
        var parser = new JsonParser();
        var result = parser.Parse("invalid json");
        Assert.Null(result);
    }

    [Fact]
    public void Parse_MissingFields_ReturnsNull()
    {
        string json = @"{ ""Location"": ""London"" }";
        var parser = new JsonParser();
        var result = parser.Parse(json);
        Assert.Null(result);
    }
    
    [Fact]
    public void Parse_EmptyString_ReturnsNull()
    {
        var parser = new JsonParser();
        var result = parser.Parse("");
        Assert.Null(result);
    }
    [Fact]
    public void Parse_WhitespaceOnly_ReturnsNull()
    {
        var parser = new JsonParser();
        var result = parser.Parse("   ");
        Assert.Null(result);
    }
    
    [Fact]
    public void Parse_ExtremeValues_ParsesCorrectly()
    {
        string json = @"{ ""Location"": ""Desert"", ""Temperature"": 1000, ""Humidity"": 0 }";
        var parser = new JsonParser();
        var result = parser.Parse(json);

        Assert.Equal(1000, result!.Temperature);
        Assert.Equal(0, result.Humidity);
    }
}