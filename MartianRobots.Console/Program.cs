using MartianRobots.Core.Services;
using System;

string sampleInput = @"5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL";

var controller = new RobotControllerService();
var (isValid, result) = controller.MoveRobots(sampleInput);
Console.WriteLine(result);
