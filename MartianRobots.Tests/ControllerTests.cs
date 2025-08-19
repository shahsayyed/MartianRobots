using MartianRobots.Core.Services;

namespace MartianRobots.Tests;

public class ControllerTests
{
    [Fact]
    public void RunRobots_ValidInput_ReturnsExpectedResult()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "5 3\n1 1 E\nRFRFRFRF\n3 2 N\nFRRFLLFFRRFLL\n0 3 W\nLLFFFLFLFL";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.True(isValid);
        Assert.Equal("1 1 E\n3 3 N LOST\n0 0 W", result);
    }
}
