using System;
using System.Collections.Generic;
using System.Linq;

namespace Nasa.MissionControl.Mission
{
  /// <summary>
  /// Represents a rectangular map of a plateau
  /// </summary>
  public class Plateau : IMap
  {
    public Plateau(Coordinate maxBounds)
    {
      _maxBounds = maxBounds;
      _trackedObjects = new Dictionary<ITrackable, Action<Coordinate>>();
    }

    readonly Coordinate _maxBounds;
    readonly Dictionary<ITrackable, Action<Coordinate>> _trackedObjects;

    public bool IsOnMap(Coordinate point)
    {
      var coordinate = point;
      if (coordinate.X < 0 || coordinate.Y < 0) return false;
      return coordinate.X <= _maxBounds.X && coordinate.Y <= _maxBounds.Y;
    }

    public void Track<T>(T trackable) where T : ITrackable
    {
      var type = typeof(T);
      if (!IsOnMap(trackable.Location))
      {
        throw new InvalidOperationException($"{type.Name} cannot be added off the map");
      }
      if (_trackedObjects.ContainsKey(trackable))
      {
        throw new InvalidOperationException($"{type.Name} is already tracked");
      }
      if (_trackedObjects.Keys.Any(o => o.Location == trackable.Location))
      {
        throw new InvalidOperationException($"{type.Name} cannot be placed in a space that is already occupied");
      }

      // Subscribe to event
      var whenRoverMoved = CheckMovement(trackable, type);
      trackable.WhenMoved += whenRoverMoved;
      _trackedObjects.Add(trackable, whenRoverMoved);
    }

    Action<Coordinate> CheckMovement(ITrackable trackable, Type type)
    {
      return newLocation =>
      {
        if (!IsOnMap(newLocation))
        {
          throw new InvalidOperationException($"{type.Name} has traveled off the map");
        } 
        if (_trackedObjects.Any(o => o.Key != trackable && o.Key.Location == newLocation))
        {
          throw new InvalidOperationException($"{type.Name} has collided with an object");
        }
      };
    }

    public void Dispose()
    {
      //Unsubscribe from events
      foreach (var roverEvent in _trackedObjects)
      {
        roverEvent.Key.WhenMoved -= roverEvent.Value;
      }
    }
  }
}
