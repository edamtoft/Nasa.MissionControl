using System;
using System.Collections.Generic;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO
{
  /// <summary>
  /// Writes the end location of the rovers to the console
  /// </summary>
  internal class ConsoleRoverView : IView<IList<Rover>>, IDisposable
  {
    private IList<Rover> _rovers;

    private void Print()
    {
      if (_rovers == null) return;
      Console.Clear();
      Console.WriteLine("-- Rover Locations --");
      foreach (var rover in _rovers)
      {
        Console.WriteLine($"{rover}");
      }
      Console.WriteLine("---------------------");
    }



    public void Initialize(IList<Rover> rovers)
    {
      _rovers = rovers;
      Print();
    }

    public void Update(Action<IList<Rover>> updateView)
    {
      updateView(_rovers);
      Print();
    }

    public void Dispose()
    {
      Console.WriteLine("Done. Press any key to continue.");
      Console.Read();
    }
  }
}
