namespace MartianRobots.Core.Services
{
    public interface IRobotControllerService
    {
        (bool isValid, string result) MoveRobots(string input);
    }
}