using System;
using System.Threading.Tasks;

namespace Nasa.MissionControl.Mission
{
  /// <summary>
  /// Represents a NASA mission with three distinct steps for
  /// planning, execution, and post-mission evaluation
  /// </summary>
  public interface IMission
  {
    void Plan();

    void Execute();
  }
}
