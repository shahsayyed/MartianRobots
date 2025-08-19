using MartianRobots.Core.Models;

namespace MartianRobots.Core.Instructions
{
    public class RightInstruction : IInstruction
    {
        public void Execute(Position pos, int maxX, int maxY)
        {
            pos.Orientation = pos.Orientation == 'N' ? 'E' :
                             pos.Orientation == 'E' ? 'S' :
                             pos.Orientation == 'S' ? 'W' : 'N';
        }
    }
}
