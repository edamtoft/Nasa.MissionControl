using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.MissionControl.Mission
{
  public static class CommandOps
  {
    public static void SendTo(this Command command, Rover rover)
    {
      rover.Recieve(command);
    }
  }
}
