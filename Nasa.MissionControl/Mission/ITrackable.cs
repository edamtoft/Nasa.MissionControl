using System;

namespace Nasa.MissionControl.Mission
{
  public interface ITrackable : ILocatable, IEquatable<ITrackable>
  {
    event Action<Coordinate> WhenMoved;
  }
}
