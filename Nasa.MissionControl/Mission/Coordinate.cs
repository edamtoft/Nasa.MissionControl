using System;

namespace Nasa.MissionControl.Mission
{
  /// <summary>
  /// Represents a cartesian coordinate point
  /// </summary>
  public struct Coordinate : IEquatable<Coordinate>
  {
    public Coordinate(double x, double y)
    {
      X = x;
      Y = y;
    }

    public readonly double X;
    public readonly double Y;

    public override string ToString() => ToString(' ');

    public string ToString(char delimiter) => $"{X}{delimiter}{Y}";

    public override bool Equals(object obj) => obj is Coordinate && Equals((Coordinate)obj);

    public bool Equals(Coordinate other) => X == other.X && Y == other.Y;

    public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

    public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);
    public static bool operator !=(Coordinate a, Coordinate b) => !a.Equals(b);

    public Coordinate At(int degrees, double distance = 1)
    {
      const int decimalPrecision = 10; // prevent rounding issues.
      var rad = Radian(degrees);
      var dx = distance * Math.Sin(rad);
      var dy = distance * Math.Cos(rad);
      var x = Math.Round(X + dx, decimalPrecision);
      var y = Math.Round(Y + dy, decimalPrecision);
      return new Coordinate(x,y);
    }

    public static Coordinate From(double x, double y) => new Coordinate(x, y);

    private static double Radian(int degrees)
    {
      return (Math.PI / 180) * degrees;
    }

    public Coordinate At(Direction direction, double distance = 1) => At((int)direction, distance);
  }
}
