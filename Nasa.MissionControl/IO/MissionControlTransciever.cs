using System;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO
{
  /// <summary>
  /// This transciever is an interactive console for setting up a mars mission. It offers more flexibility and error handling
  /// than the programmable transciever. If an error occurs with a message, it will be reprompted.
  /// </summary>
  internal class MissionControlTransciever : IMissionTransciever
  {
    public MissionControlTransciever(
      IParser<bool> yesNoParser,
      IParser<Rover> roverParser,
      IParser<Command[]> commandParser,
      IParser<IMap> mapParser,
      IWritable writeable,
      IReadable readable)
    {
      _yesNoParser = yesNoParser;
      _roverParser = roverParser;
      _commandParser = commandParser;
      _mapParser = mapParser;
      _writeable = writeable;
      _readable = readable;
    }


    private readonly IParser<bool> _yesNoParser;
    private readonly IParser<Rover> _roverParser;
    private readonly IParser<Command[]> _commandParser;
    private readonly IParser<IMap> _mapParser;
    private readonly IWritable _writeable;
    private readonly IReadable _readable;

    public IMap RecieveMap() => Recieve("Enter plateau size: (X Y)", _mapParser);

    public bool RecieveHasNextRover() => Recieve("Add new rover (yes/no)?", _yesNoParser);

    public Rover RecieveRoverPosition() => Recieve("Enter coordinates for rover: (X Y [NESW])", _roverParser);

    public Command[] RecieveCommands() => Recieve("Enter commands for rover: ([LRM]...)", _commandParser);

    public void SendError(Exception error) => Send($"{error.Message}. The Mission is not a GO.");

    private void Send(string message) => _writeable.WriteLine(message);

    private T Recieve<T>(string prompt, IParser<T> parser)
    {
      T result;
      string errorMessage;
      while (!TryRecieve(prompt, parser, out result, out errorMessage)) { _writeable.WriteLine(errorMessage); };
      return result;
    }

    private bool TryRecieve<T>(string prompt, IParser<T> parser, out T result, out string errorMessage)
    {
      _writeable.WriteLine(prompt);
      string text;
      _readable.ReadNext(out text);
      try
      {
        result = parser.Parse(text);
        errorMessage = null;
        return true;
      }
      catch(Exception err)
      {
        result = default(T);
        errorMessage = err.Message;
        return false;
      }
    }
  }
}
