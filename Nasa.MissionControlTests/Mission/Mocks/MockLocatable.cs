using Nasa.MissionControl.Mission;

namespace Nasa.MissionControlTests.Mission.Mocks
{
  class MockLocatable : ILocatable
  {
    public MockLocatable(int x, int y)
    {
      Location = new Coordinate(x, y);
    }


    public Coordinate Location { get; }
  }
}
