using Autofac;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl
{
  class Program
  {
    static void Main(string[] args)
    {
      var config = new Config { };
      //var config = new Config { InputFile = null, OutputFile = null, GraphicOutput = true, NoPrompts = false };

      config.Execute<IMission>(mission =>
      {
        mission.Plan();
        mission.Execute();
      });
    }
  }
}
