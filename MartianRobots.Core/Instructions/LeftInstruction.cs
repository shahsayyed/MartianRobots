using MartianRobots.Core.Models;

namespace MartianRobots.Core.Instructions
{
    public class LeftInstruction : IInstruction
    {
        public void Execute(Position pos, int maxX, int maxY)
        {
            pos.Orientation = pos.Orientation == 'N' ? 'W' :
                             pos.Orientation == 'W' ? 'S' :
                             pos.Orientation == 'S' ? 'E' : 'N';
        }
    }
}