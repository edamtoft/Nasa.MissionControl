using System;
using System.Collections.Generic;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControlTests.Mission.Mocks
{
  class MockMissionTransciever : IMissionTransciever
  {
    public MockMissionTransciever()
    {
      _rovers = new Queue<Rover>(new[]
      {
          new Rover(1,2,Direction.N),
          new Rover(3,3,Direction.E)
        });

      _commands = new Queue<Command[]>(new[] {
           new[] {
            Command.TurnLeft, Command.MoveForward, Command.TurnLeft,
            Command.MoveForward, Command.TurnLeft, Command.MoveForward,
            Command.TurnLeft, Command.MoveForward, Command.MoveForward },
           new[]
           {
             Command.MoveForward, Command.MoveForward, Command.TurnRight,
             Command.MoveForward, Command.MoveForward, Command.TurnRight,
             Command.MoveForward, Command.TurnRight, Command.TurnRight,
             Command.MoveForward,
           }
        });
    }

    private Queue<Rover> _rovers;
    private Queue<Command[]> _commands;

    public Command[] RecieveCommands() => _commands.Dequeue();

    public bool RecieveHasNextRover() => _rovers.Count > 0;

    public IMap RecieveMap() => new Plateau(new Coordinate(5, 5));

    public Rover RecieveRoverPosition() => _rovers.Dequeue();

    public void SendError(Exception err) { throw err; }
  }
}
