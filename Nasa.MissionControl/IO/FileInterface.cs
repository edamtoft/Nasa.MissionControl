using System;
using System.Collections.Generic;
using System.IO;

namespace Nasa.MissionControl.IO
{
  internal class FileInterface : IReadable, IDisposable
  {
    public FileInterface(string filePath)
    {
      _input = new FileInfo(filePath).OpenText();
    }

    private readonly StreamReader _input;

    public bool ReadNext(out string next)
    {
      next = _input.ReadLine();
      return !string.IsNullOrWhiteSpace(next);
    }

    public void Dispose()
    {
      _input.Close();
    }

    
  }
}
