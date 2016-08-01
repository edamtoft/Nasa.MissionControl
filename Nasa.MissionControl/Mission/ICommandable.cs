using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.MissionControl.Mission
{
  public interface ICommandable
  {
    void Recieve(Command command);
  }
}
