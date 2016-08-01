using System;

namespace Nasa.MissionControl.Mission
{
  public interface IMissionTransciever
  {
    IMap RecieveMap();

    bool RecieveHasNextRover();

    Rover RecieveRoverPosition();

    Command[] RecieveCommands();

    void SendError(Exception error);
  }
}
