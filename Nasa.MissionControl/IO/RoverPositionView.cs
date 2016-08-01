using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Nasa.MissionControl.Mission;
using static Nasa.MissionControl.IO.Color;

namespace Nasa.MissionControl.IO
{
  /// <summary>
  /// Provides an animated view of rover positions
  /// </summary>
  internal class RoverPositionView : IView<IList<Rover>>, IDisposable
  {
    public RoverPositionView(int throttle)
    {
      _throttle = throttle;
      _visitedCoordinates = new HashSet<Coordinate>();
    }

    private IList<Rover> _rovers;
    private readonly HashSet<Coordinate> _visitedCoordinates;
    private int _xBounds = 0;
    private int _yBounds = 0;
    private readonly int _throttle;


    public void Initialize(IList<Rover> rovers)
    {
      _rovers = rovers;
      Render();
    }

    public void Update(Action<IList<Rover>> updateView)
    {
      updateView(_rovers);
      Render();
    }

    public void Render()
    {
      Console.Clear();

      if (_rovers == null || _rovers.Count == 0) return;

      var maxXPosition = Convert.ToInt32(_rovers.Max(r => r.Location.X));
      var maxYPosition = Convert.ToInt32(_rovers.Max(r => r.Location.Y));
      if (maxXPosition > _xBounds) _xBounds = maxXPosition;
      if (maxYPosition > _yBounds) _yBounds = maxYPosition;

      Console.WriteLine(" -- Rovers -- ");

      foreach (var rover in _rovers)
      {
        Console.WriteLine(rover);
        _visitedCoordinates.Add(rover.Location);
      }

      Console.WriteLine(" -- Map View -- ");

      for (var y = _yBounds; y >= 0; y--)
      {
        for (var x = 0; x <= _xBounds; x++)
        {
          var position = new Coordinate(x, y);
          var rover = _rovers.FirstOrDefault(r => r.Location == position);
          if (rover != null)
          {
            using (Red) Console.Write($"{rover.Bearing}");
          }
          else if (_visitedCoordinates.Contains(position))
          {
            using (DarkRed) Console.Write(" ");
          }
          else
          {
            Console.Write(" ");
          }
        }
        Console.WriteLine();
      }

      // Slow down enough to see it animate.
      Thread.Sleep(_throttle);
    }

    public void Dispose()
    {
      Console.WriteLine("Done. Press any key to continue.");
      Console.Read();
    }
  }
}
