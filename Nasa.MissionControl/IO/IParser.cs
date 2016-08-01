namespace Nasa.MissionControl.IO
{
  /// <summary>
  /// Represents a method of parsing a string to a given data type
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IParser<T>
  {
    T Parse(string response);
  }
}
