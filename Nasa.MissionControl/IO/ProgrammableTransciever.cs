using System;
using System.Collections.Generic;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO
{
  /// <summary>
  /// Reads an input source to the end, enqueu
  /// </summary>
  internal class ProgrammableTransciever : IMissionTransciever
  {
    public ProgrammableTransciever(
      IParser<bool> yesNoParser,
      IParser<Rover> roverParser,
      IParser<Command[]> commandParser,
      IParser<IMap> mapParser,
      IReadable readable)
    {
      _yesNoParser = yesNoParser;
      _roverParser = roverParser;
      _commandParser = commandParser;
      _mapParser = mapParser;
      _commands = new Queue<string>();

      string input;
      while (readable.ReadNext(out input))
      {
        _commands.Enqueue(input);
      }
    }

    private readonly IParser<bool> _yesNoParser;
    private readonly IParser<Rover> _roverParser;
    private readonly IParser<Command[]> _commandParser;
    private readonly IParser<IMap> _mapParser;
    private readonly Queue<string> _commands;


    public Command[] RecieveCommands()
    {
      var commands = _commands.Dequeue();
      return _commandParser.Parse(commands);
    }

    public bool RecieveHasNextRover()
    {
      return _commands.Count > 0;
    }

    public IMap RecieveMap()
    {
      var map = _commands.Dequeue();
      return _mapParser.Parse(map);
    }

    public Rover RecieveRoverPosition()
    {
      var rover = _commands.Dequeue();
      return _roverParser.Parse(rover);
    }

    public void SendError(Exception err)
    {
      // No user input/output. No option but to throw error
      throw err;
    }
  }
}
