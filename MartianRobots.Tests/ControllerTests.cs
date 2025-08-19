using MartianRobots.Core.Services;
using Xunit;

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
        Assert.Equal("1 1 E\n3 3 N LOST\n2 3 S", result);
    }

    [Fact]
    public void SingleRobot_ForwardOnly_ReturnsExpectedPosition()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "5 5\n0 0 N\nFFFF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.True(isValid);
        Assert.Equal("0 4 N", result);
    }

    [Fact]
    public void MultipleRobots_NoLoss_ReturnsAllFinalPositions()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "3 3\n1 1 E\nFRF\n2 2 S\nRFF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.True(isValid);
        Assert.Equal("2 0 S\n0 2 W", result);
    }
}
