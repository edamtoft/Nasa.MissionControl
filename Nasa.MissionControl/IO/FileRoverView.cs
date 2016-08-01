using System;
using System.Collections.Generic;
using System.IO;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl.IO
{
  /// <summary>
  /// Writes rover positions to a file. Buffers changes and then
  /// writes to file when changes are completed and the dispose method is called.
  /// </summary>
  internal class FileRoverView : IView<IList<Rover>>, IDisposable
  {
    public FileRoverView(string file)
    {
      _fileName = file;
    }

    private readonly string _fileName;

    private IList<Rover> _rovers;

    public void Initialize(IList<Rover> rovers)
    {
      _rovers = rovers;
    }

    public void Update(Action<IList<Rover>> updateView)
    {
      updateView(_rovers);
    }

    public void Dispose()
    {
      if (_rovers == null) return;
      var file = File.CreateText(_fileName);
      foreach (var rover in _rovers)
      {
        file.WriteLine($"{rover}");
      }
      file.Close();
      file.Dispose();
    }
  }
}
