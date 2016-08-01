using System;
using System.Text.RegularExpressions;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO
{
  public class RoverParser : IParser<Rover>
  {
    public Rover Parse(string response)
    {
      var match = Regex.Match(response, @"^(\d+) (\d+) ([NESW])$", RegexOptions.IgnoreCase);
      if (!match.Success)
      {
        throw new FormatException("Rover command does not match expected pattern (X Y [NESW])");
      }
      var x = int.Parse(match.Groups[1].Value);
      var y = int.Parse(match.Groups[2].Value);
      var bearing = (Direction) Enum.Parse(typeof(Direction), match.Groups[3].Value, ignoreCase: true);
      return Rover.At(x, y, bearing);
    }
  }
}
