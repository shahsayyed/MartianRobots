using MartianRobots.Core.Models;

namespace MartianRobots.Core.Instructions
{
    public class ForwardInstruction : IInstruction
    {
        public void Execute(Position pos, int maxX, int maxY)
        {
            switch (pos.Orientation)
            {
                case 'N':
                    if (pos.Y < maxY) pos.Y++;
                    break;
                case 'E':
                    if (pos.X < maxX) pos.X++;
                    break;
                case 'S':
                    if (pos.Y > 0) pos.Y--;
                    break;
                case 'W':
                    if (pos.X > 0) pos.X--;
                    break;
            }
        }
    }
}
