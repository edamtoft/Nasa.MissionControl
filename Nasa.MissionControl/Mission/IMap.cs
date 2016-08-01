using System;

namespace Nasa.MissionControl.Mission
{
  /// <summary>
  /// Represents a map of an area
  /// </summary>
  public interface IMap : IDisposable
  {
    bool IsOnMap(Coordinate coordinate);

    void Track<T>(T trackable) where T : ITrackable;
  }
}
