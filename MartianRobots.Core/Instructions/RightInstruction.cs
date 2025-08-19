using MartianRobots.Core.Models;

namespace MartianRobots.Core.Instructions
{
    public class RightInstruction : IInstruction
    {
        public void Execute(Position pos, int maxX, int maxY, HashSet<(int, int)> scents)
        {
            pos.Orientation = pos.Orientation switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => pos.Orientation
            };
        }
    }
}
