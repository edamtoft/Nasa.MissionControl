using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO
{
  public class PlateauParser : IParser<IMap>
  {
    public PlateauParser(IParser<Coordinate> coordinateParser)
    {
      _coordinateParser = coordinateParser;
    }

    readonly IParser<Coordinate> _coordinateParser;

    public IMap Parse(string response)
    {
      return new Plateau(_coordinateParser.Parse(response));
    }
  }
}
