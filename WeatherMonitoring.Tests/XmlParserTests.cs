using WeatherMonitoring.Interactors;
namespace WeatherMonitoring.Tests;

public class XmlParserTests
{
    [Fact]
    public void Parse_ValidXml_ReturnsWeatherState()
    {
        string xml = @"
            <WeatherData>
                <Location>London</Location>
                <Temperature>25</Temperature>
                <Humidity>60</Humidity>
            </WeatherData>";

        var parser = new XmlParser();
        var result = parser.Parse(xml);

        Assert.NotNull(result);
        Assert.Equal("London", result!.Location);
        Assert.Equal(25, result.Temperature);
        Assert.Equal(60, result.Humidity);
    }

    [Fact]
    public void Parse_InvalidXml_ReturnsNull()
    {
        var parser = new XmlParser();
        var result = parser.Parse("<invalid>");
        Assert.Null(result);
    }

    [Fact]
    public void Parse_MissingFields_ReturnsNull()
    {
        string xml = "<WeatherData><Location>London</Location></WeatherData>";
        var parser = new XmlParser();
        var result = parser.Parse(xml);
        Assert.Null(result);
    }
    
    [Fact]
    public void Parse_EmptyString_ReturnsNull()
    {
        var parser = new XmlParser();
        var result = parser.Parse("");
        Assert.Null(result);
    }
    [Fact]
    public void Parse_MissingTemperature_ReturnsNull()
    {
        string xml = @"<WeatherData><Location>NY</Location><Humidity>50</Humidity></WeatherData>";
        var parser = new XmlParser();
        var result = parser.Parse(xml);
        Assert.Null(result);
    }
    [Fact]
    public void Parse_ExtremeValues_ParsesCorrectly()
    {
        string xml = @"<WeatherData><Location>Antarctica</Location><Temperature>-100</Temperature><Humidity>0</Humidity></WeatherData>";
        var parser = new XmlParser();
        var result = parser.Parse(xml);

        Assert.Equal(-100, result!.Temperature);
        Assert.Equal(0, result.Humidity);
    }
    
    
}