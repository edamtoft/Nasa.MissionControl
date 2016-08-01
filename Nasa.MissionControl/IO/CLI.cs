using System;
using System.Collections.Generic;

namespace Nasa.MissionControl.IO
{
  internal class CLI : IReadable, IWritable
  {
    public void WriteLine(string line)
    {
      Console.WriteLine(line);
    }

    public bool ReadNext(out string next)
    {
      next = Console.ReadLine();
      return !string.IsNullOrEmpty(next);
    }
  }
}
