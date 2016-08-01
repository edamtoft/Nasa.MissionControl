using System;
using System.Collections.Generic;
using System.Linq;

namespace Nasa.MissionControl.Mission
{
  /// <summary>
  /// A command targeted at a specific rover
  /// </summary>
  public struct RoverCommand
  {
    public RoverCommand(Guid target, Command command)
    {
      Target = target;
      Command = command;
    }

    public void SendTo(IEnumerable<Rover> rovers)
    {
      var target = Target;
      foreach (
        var rover in
        from rover in rovers
        where rover.Id == target
        select rover)
      {
        Command.SendTo(rover);
      }
    }

    public readonly Guid Target;
    public readonly Command Command;

    public override string ToString() => Command.ToString();

    public static implicit operator Command(RoverCommand roverCommand) => roverCommand.Command;
  }
}
