using MartianRobots.Core.Models;

namespace MartianRobots.Core.Instructions
{
    public interface IInstruction
    {
        void Execute(Position pos, int maxX, int maxY, HashSet<(int, int)> scents);
    }
}