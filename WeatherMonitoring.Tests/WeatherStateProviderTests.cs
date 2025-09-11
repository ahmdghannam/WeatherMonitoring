namespace WeatherMonitoring.Tests;

using Moq;
using WeatherMonitoring;
using WeatherMonitoring.models;
using Xunit;

public class WeatherStateProviderTests
{
    [Fact]
    public void UpdateState_ShouldNotifySubscribers()
    {
        // Arrange
        var provider = new WeatherStateProvider();

        var mockBot = new Mock<WeatherBot>(); 
        provider.AddSubscriber(mockBot.Object);

        var newState = new WeatherState("London", 25, 60);

        // Act
        provider.UpdateState(newState);

        // Assert
        mockBot.Verify(
            bot => bot.UpdateState(It.Is<WeatherState>(
                s => s.Location == "London" &&
                     s.Temperature == 25 &&
                     s.Humidity == 60
            )), Times.Once);
    }
    
    [Fact]
    public void UpdateState_AllSubscribersNotified()
    {
        var provider = new WeatherStateProvider();
        var bot1 = new Mock<WeatherBot>();
        var bot2 = new Mock<WeatherBot>();

        provider.AddSubscriber(bot1.Object);
        provider.AddSubscriber(bot2.Object);

        var state = new WeatherState("London", 25, 60);
        provider.UpdateState(state);

        bot1.Verify(b => b.UpdateState(It.IsAny<WeatherState>()), Times.Once);
        bot2.Verify(b => b.UpdateState(It.IsAny<WeatherState>()), Times.Once);
    }
    [Fact]
    public void UpdateState_NoSubscribers_DoesNotThrow()
    {
        var provider = new WeatherStateProvider();
        var state = new WeatherState("Paris", 20, 50);
        provider.UpdateState(state); // Should not throw
    }
    

}