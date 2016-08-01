using Autofac;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl
{
  class Program
  {
    static void Main(string[] args)
    {
      var config = new Config { };
      //var config = new Config { InputFile = "input.txt", OutputFile = "output.txt", GraphicOutput = true };

      config.Execute<IMission>(mission =>
      {
        mission.Plan();
        mission.Execute();
      });
    }
  }
}
