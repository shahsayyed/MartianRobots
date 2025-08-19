using MartianRobots.Core.Instructions;
using MartianRobots.Core.Models;
using Xunit;

namespace MartianRobots.Tests;

public class InstructionsTests
{
    [Fact]
    public void ForwardInstruction_ShouldMoveForward_WhenWithinBounds()
    {
        var position = new Position(1, 1, 'N');
        var scents = new HashSet<(int, int)>();
        var instruction = new ForwardInstruction();

        instruction.Execute(position, 5, 5, scents);

        Assert.Equal(1, position.X);
        Assert.Equal(2, position.Y);
        Assert.False(position.Lost);
    }


    [Fact]
    public void ForwardInstruction_ShouldMarkLost_WhenOutOfBounds()
    {
        var position = new Position(5, 5, 'N');
        var scents = new HashSet<(int, int)>();
        var instruction = new ForwardInstruction();

        // Move out of bounds
        instruction.Execute(position, 5, 5, scents);

        Assert.True(position.Lost);
        Assert.Contains((5, 5), scents);
    }

    [Fact]
    public void RightInstruction_ShouldChangeOrientation()
    {
        var position = new Position(0, 0, 'N');
        var scents = new HashSet<(int, int)>();
        var instruction = new RightInstruction();

        instruction.Execute(position, 5, 5, scents);

        Assert.Equal('E', position.Orientation);
    }

    [Fact]
    public void LeftInstruction_ShouldChangeOrientation()
    {
        var position = new Position(0, 0, 'N');
        var scents = new HashSet<(int, int)>();
        var instruction = new LeftInstruction();

        instruction.Execute(position, 5, 5, scents);

        Assert.Equal('W', position.Orientation);
    }
}
