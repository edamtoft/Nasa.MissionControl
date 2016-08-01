using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO.Tests
{
  [TestClass]
  public class RoverParserTests
  {
    [TestMethod]
    public void ParseTest()
    {
      var parser = new RoverParser();
      var rover = parser.Parse("1 2 N");
      Assert.AreEqual(new Coordinate(1, 2), rover.Location);
      Assert.AreEqual(Direction.N, rover.Bearing);
    }

    [TestMethod]
    public void ParserIsCaseInsensitive()
    {
      var parser = new RoverParser();
      var rover = parser.Parse("1 2 n");
      Assert.AreEqual(new Coordinate(1, 2), rover.Location);
      Assert.AreEqual(Direction.N, rover.Bearing);
    }

    [TestMethod]
    public void ParserCanHandleMultipleDigits()
    {
      var parser = new RoverParser();
      var rover = parser.Parse("100 100 N");
      Assert.AreEqual(new Coordinate(100, 100), rover.Location);
      Assert.AreEqual(Direction.N, rover.Bearing);
    }
  }
}