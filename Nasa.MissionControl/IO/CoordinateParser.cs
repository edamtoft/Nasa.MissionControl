using System;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO
{
  public class CoordinateParser : IParser<Coordinate>
  {
    public Coordinate Parse(string response)
    {
      var parts = response.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
      int x, y;
      if (!int.TryParse(parts[0], out x) || !int.TryParse(parts[1], out y))
      {
        throw new FormatException("Unable to read coordinates");
      }
      return new Coordinate(x, y);
    }
  }
}
