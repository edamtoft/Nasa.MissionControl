using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO.Tests
{
  [TestClass]
  public class CoordinateParserTests
  {
    [TestMethod]
    public void ParseTest()
    {
      var parser = new CoordinateParser();

      var coordinate = parser.Parse("5 5");
      Assert.AreEqual(new Coordinate(5, 5), coordinate);
    }
  }
}