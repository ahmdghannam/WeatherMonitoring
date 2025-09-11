using WeatherMonitoring.Interactors;

namespace WeatherMonitoring.Tests;

public class WeatherDataParsingServiceTests
{
    [Fact]
    public void ParseData_Json_DelegatesToJsonParser()
    {
        var service = new WeatherDataParsingService();
        string json = @"{ ""Location"": ""London"", ""Temperature"": 25, ""Humidity"": 60 }";

        var result = service.ParseData(json, WeatherDataForm.JSON);

        Assert.NotNull(result);
        Assert.Equal("London", result!.Location);
    }

    [Fact]
    public void ParseData_Xml_DelegatesToXmlParser()
    {
        var service = new WeatherDataParsingService();
        string xml = @"<WeatherData><Location>London</Location><Temperature>25</Temperature><Humidity>60</Humidity></WeatherData>";

        var result = service.ParseData(xml, WeatherDataForm.XML);

        Assert.NotNull(result);
        Assert.Equal("London", result.Location);
    }

    [Fact]
    public void ParseData_UnsupportedFormat_ThrowsArgumentOutOfRangeException()
    {
        var service = new WeatherDataParsingService();
        Assert.Throws<ArgumentOutOfRangeException>(() => service.ParseData("", (WeatherDataForm)99));
    }
    
    [Fact]
    public void ParseData_JsonInvalid_ReturnsNull()
    {
        var service = new WeatherDataParsingService();
        var result = service.ParseData("{ invalid json }", WeatherDataForm.JSON);
        Assert.Null(result);
    }
    [Fact]
    public void ParseData_MultipleCalls_NoSideEffects()
    {
        var service = new WeatherDataParsingService();
        string json = @"{ ""Location"": ""London"", ""Temperature"": 25, ""Humidity"": 60 }";

        var r1 = service.ParseData(json, WeatherDataForm.JSON);
        var r2 = service.ParseData(json, WeatherDataForm.JSON);

        Assert.Equal(r1!.Location, r2!.Location);
    }
}