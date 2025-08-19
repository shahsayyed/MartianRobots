using MartianRobots.Core.Models;

namespace MartianRobots.Core.Instructions
{
    public class ForwardInstruction : IInstruction
    {
        public void Execute(Position pos, int maxX, int maxY, HashSet<(int, int)> scents)
        {
            var (nextX, nextY) =
            pos.Orientation switch
            {
                'N' => (pos.X, pos.Y + 1),
                'S' => (pos.X, pos.Y - 1),
                'E' => (pos.X + 1, pos.Y),
                'W' => (pos.X - 1, pos.Y),
                _ => (pos.X, pos.Y)
            };

            // Check if the next position is out of bounds
            if (nextX < 0 || nextX > maxX || nextY < 0 || nextY > maxY)
            {
                // Robot is out of bounds - will only allow once
                if (!scents.Contains((pos.X, pos.Y)))
                {
                    scents.Add((pos.X, pos.Y));
                    pos.Lost = true;
                }
            }
            else
            {
                pos.X = nextX;
                pos.Y = nextY;
            }
        }
    }
}
