using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasa.MissionControl.Mission;
using Nasa.MissionControlTests.Mission.Mocks;

namespace Nasa.MissionControl.Tests
{
  [TestClass]
  public class MarsMissionTests
  {
    [TestMethod]
    public void MissionShouldPlaceRoverAtExpectedLocation()
    {
      var view = new MockRoverView();
      var transciever = new MockMissionTransciever();

      var mission = new MarsMission(transciever, view);

      mission.Plan();
      mission.Execute();

      Assert.IsTrue(view.Rovers.Count == 2);

      Assert.IsTrue(view.Rovers[0].Location == new Coordinate(1,3));
      Assert.IsTrue(view.Rovers[0].Bearing == Direction.N);

      Assert.IsTrue(view.Rovers[1].Location == new Coordinate(5, 1));
      Assert.IsTrue(view.Rovers[1].Bearing == Direction.E);
    }

    [TestMethod]
    public void RoversShouldNotBePlacedUntilMissionIsExecuted()
    {
      var view = new MockRoverView();
      var transciever = new MockMissionTransciever();

      var mission = new MarsMission(transciever, view);

      mission.Plan();

      Assert.IsTrue(view.Rovers == null);
    }
  }
}