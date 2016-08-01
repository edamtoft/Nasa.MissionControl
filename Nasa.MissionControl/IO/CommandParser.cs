using System;
using System.Linq;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO
{
  public class CommandParser : IParser<Command[]>
  {
    public Command[] Parse(string response)
    {
      return (from command in response
              let upper = char.ToUpper(command)
              select FromChar(upper)).ToArray();
    }

    static Command FromChar(char command)
    {
      switch (command)
      {
        case 'L': return Command.TurnLeft;
        case 'R': return Command.TurnRight;
        case 'M': return Command.MoveForward;
        default: throw new InvalidOperationException($"Unknown command {command}");
      }
    }
  }
}
