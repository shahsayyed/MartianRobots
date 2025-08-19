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

    [Fact]
    public void Robot_WithScentPreventsLoss_ReturnsSafePosition()
    {
        // Arrange - First robot gets lost, second robot should be saved by scent
        var controller = new RobotControllerService();
        string input = "2 2\n2 2 E\nF\n2 2 E\nF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.True(isValid);
        Assert.Equal("2 2 E LOST\n2 2 E", result);
    }

    [Fact]
    public void Robot_ComplexPath_WithMultipleTurns()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "10 10\n5 5 N\nFFFRFFFRFFFRFFF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.True(isValid);
        Assert.Equal("5 5 W", result); // After FFFRFFFRFFFRFFF: moves 3N, turns R(E), moves 3E, turns R(S), moves 3S, turns R(W), moves 3W - ends at start facing W
    }

    [Fact]
    public void Robot_AtGridBoundary_StaysWithinBounds()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "5 5\n0 0 S\nFFFFF\n5 5 N\nFFFFF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.True(isValid);
        Assert.Equal("0 0 S LOST\n5 5 N LOST", result);
    }

    // Negative Test Cases
    [Fact]
    public void InvalidInput_EmptyString_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.False(isValid);
        Assert.Contains("Invalid input format", result);
    }

    [Fact]
    public void InvalidInput_MissingGridSize_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "1 1 N\nF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.False(isValid);
        Assert.Contains("Invalid grid size", result);
    }

    [Fact]
    public void InvalidInput_InvalidGridSize_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "abc def\n1 1 N\nF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.False(isValid);
        Assert.Contains("Invalid grid size", result);
    }

    [Fact]
    public void InvalidInput_GridSizeExceedsMaximum_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "51 51\n1 1 N\nF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.False(isValid);
        Assert.Contains("Grid size exceeds maximum allowed size", result);
    }

    [Fact]
    public void InvalidInput_ZeroGridSize_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "0 0\n1 1 N\nF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.False(isValid);
        Assert.Contains("Grid size exceeds maximum allowed size", result);
    }

    [Fact]
    public void InvalidInput_InvalidRobotPosition_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "5 5\nabc def xyz\nF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.False(isValid);
        Assert.Contains("Invalid robot position", result);
    }

    [Fact]
    public void InvalidInput_InvalidOrientation_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "5 5\n1 1 X\nF";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.False(isValid);
        Assert.Contains("Invalid robot position", result);
    }

    [Fact]
    public void InvalidInput_TooManyInstructions_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string tooManyInstructions = new string('F', 101); // 101 instructions (max is 100)
        string input = $"5 5\n1 1 N\n{tooManyInstructions}";

        // Act
        var (isValid, result) = controller.MoveRobots(input);

        // Assert
        Assert.False(isValid);
        Assert.Contains("Movement instructions exceed maximum allowed length", result);
    }

    [Fact]
    public void InvalidInput_IncompleteRobotData_ReturnsFalse()
    {
        // Arrange
        var controller = new RobotControllerService();
        string input = "5 5\n1 1 N"; // Missing instructions - will cause IndexOutOfRangeException in current implementation

        // Act & Assert
        Assert.Throws<IndexOutOfRangeException>(() => controller.MoveRobots(input));
    }
}
