using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasa.MissionControlTests.Mission.Mocks;

namespace Nasa.MissionControl.Mission.Tests
{
  [TestClass]
  public class PlateauTests
  {
    [TestMethod]
    public void PlateauTest()
    {
      var map = new Plateau(new Coordinate(5,5));

      Assert.IsTrue(map.IsOnMap(Coordinate.From(0, 0)));
      Assert.IsTrue(map.IsOnMap(Coordinate.From(0, 5)));
      Assert.IsTrue(map.IsOnMap(Coordinate.From(5, 0)));
      Assert.IsTrue(map.IsOnMap(Coordinate.From(5, 5)));


      Assert.IsFalse(map.IsOnMap(Coordinate.From(-5, -5)));
      Assert.IsFalse(map.IsOnMap(Coordinate.From(10, 10)));
    }

    [TestMethod]
    public void RoverCollisionsDetected()
    {
      var map = new Plateau(Coordinate.From(5, 5));

      var rover = Rover.At(1, 1, Direction.N);
      map.Track(rover);
      map.Track(Rover.At(1, 2));

      try
      {
        rover.Recieve(Command.MoveForward);
        Assert.Fail("Rovers sould have collided");
      }
      catch
      {
        //Success!
      }
    }
  }
}