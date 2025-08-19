namespace MartianRobots.Core.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Orientation { get; set; }
        public bool Lost { get; set; }

        public Position(int x, int y, char orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            Lost = false;
        }

    }
}