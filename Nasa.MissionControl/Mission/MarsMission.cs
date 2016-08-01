using System;
using System.Collections.Generic;
using System.Linq;
using Nasa.MissionControl.IO;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl
{
  /// <summary>
  /// Represents a mars rover mission
  /// </summary>
  public class MarsMission : IMission
  {
    public MarsMission(
      IMissionTransciever transciever,
      IView<IList<Rover>> view)
    {
      _transciever = transciever;
      _view = view;
      _rovers = new List<Rover>();
      _commands = new Queue<RoverCommand>();
    }

    private readonly IMissionTransciever _transciever;
    private readonly IView<IList<Rover>> _view;
    private List<Rover> _rovers;
    private Queue<RoverCommand> _commands;
    private bool _missionIsGo;

    /// <summary>
    /// Prompt for and enqueue commands, simulating the result of each command
    /// in order to ensure that it will not destroy the rover. If anything goes wrong
    /// during the mission planning, the mission will be aborted in order to analyze
    /// the error and re-plan the mission. (Simple parsing errors may or may not be
    /// handled depeneding on which transciever is supplied)
    /// </summary>
    public void Plan()
    {
      try
      {
        using (var map = _transciever.RecieveMap())
        do
        {
          var rover = _transciever.RecieveRoverPosition();
          var roverSimulation = rover.CreateSimulation();
          
          map.Track(roverSimulation);
          _rovers.Add(rover);

          foreach (var command in _transciever.RecieveCommands())
          {
            command.SendTo(roverSimulation);
            _commands.Enqueue(new RoverCommand(rover.Id, command));
          }
        }
        while (_transciever.RecieveHasNextRover());
        _missionIsGo = true;
      }
      catch(Exception missionError)
      {
        _transciever.SendError(missionError);
        _missionIsGo = false;
      }
    }

    /// <summary>
    /// Execute each command in the order it was recieved.
    /// This step is deliberately not error checked becuase we've already simulated the
    /// mission in the planning step.
    /// </summary>
    public void Execute()
    {
      _view.Initialize(_rovers);
      while (_missionIsGo && _commands.Count > 0)
      {
        var command = _commands.Dequeue();
        _view.Update(rovers => command.SendTo(rovers));
      }
    }
  }
}
