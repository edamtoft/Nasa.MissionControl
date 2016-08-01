using System;

namespace Nasa.MissionControl.Mission
{
  /// <summary>
  /// Represents a rover which can be guided with commands
  /// </summary>
  public class Rover : IEquatable<Rover>, ITrackable
  {
    public Rover(int x, int y, Direction bearing)
    {
      Location = new Coordinate(x, y);
      Bearing = bearing;
      Id = Guid.NewGuid();
    }

    public Rover(Coordinate location, Direction bearing)
    {
      Location = location;
      Bearing = bearing;
      Id = Guid.NewGuid();
    }

    public event Action<Coordinate> WhenMoved;
    public Guid Id { get; }
    public Coordinate Location { get; private set; }
    public Direction Bearing { get; private set; }

    public void Recieve(Command command)
    {
      switch (command)
      {
        case Command.TurnLeft:
          Turn(-90);
          break;
        case Command.TurnRight:
          Turn(90);
          break;
        case Command.MoveForward:
          Move();
          break;
      }
    }

    private void Turn(int degrees)
    {
      Bearing = (Direction)(((int)Bearing + (degrees + 360)) % 360);
    }

    private void Move()
    {
      Location = Location.At(Bearing);
      WhenMoved?.Invoke(Location);
    }

    internal Rover CreateSimulation() => new Rover(Location, Bearing);

    public override string ToString() => $"{Location} {Bearing}";

    //
    // Equality
    //
    public override int GetHashCode() => Id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Rover);
    public bool Equals(ITrackable other) => Equals(other as Rover);
    public bool Equals(Rover other) => other != null && other.Id == Id;
    

    //
    // Factory
    //
    public static Rover At(int x, int y, Direction bearing = Direction.N) => new Rover(x, y, bearing);
  }
}
