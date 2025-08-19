using MartianRobots.Core.Models;

namespace MartianRobots.Core.Instructions
{
    public class LeftInstruction : IInstruction
    {
        public void Execute(Position pos, int maxX, int maxY, HashSet<(int, int)> scents)
        {
            pos.Orientation = pos.Orientation switch
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => pos.Orientation
            };
        }
    }
}