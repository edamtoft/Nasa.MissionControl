namespace Nasa.MissionControl.IO
{
  public interface IReadable
  {
    bool ReadNext(out string next);
  }
}
