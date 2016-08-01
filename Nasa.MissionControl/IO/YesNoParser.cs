using System;

namespace Nasa.MissionControl.IO
{
  public class YesNoParser : IParser<bool>
  {
    public bool Parse(string response)
    {
      if (string.IsNullOrWhiteSpace(response)) return false;
      return
        response.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
        response.Equals("YES", StringComparison.OrdinalIgnoreCase);
    }
  }
}
