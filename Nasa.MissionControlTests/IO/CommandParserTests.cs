using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO.Tests
{
  [TestClass]
  public class CommandParserTests
  {
    [TestMethod]
    public void ParseTest()
    {
      var parser = new CommandParser();
      var commands = parser.Parse("LRM");

      var expected = new[] { Command.TurnLeft, Command.TurnRight, Command.MoveForward };

      Assert.IsTrue(expected.SequenceEqual(commands));
    }

    [TestMethod]
    public void parserIsCaseInsensitive()
    {
      var parser = new CommandParser();
      var commands = parser.Parse("lrm");

      var expected = new[] { Command.TurnLeft, Command.TurnRight, Command.MoveForward };

      Assert.IsTrue(expected.SequenceEqual(commands));
    }
  }
}