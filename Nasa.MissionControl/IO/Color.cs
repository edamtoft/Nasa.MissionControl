using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.MissionControl.IO
{
  internal class Color : IDisposable
  {
    public Color(ConsoleColor bg, ConsoleColor fg = ConsoleColor.White)
    {
      _originalForeground = Console.ForegroundColor;
      _originalBackground = Console.BackgroundColor;

      Console.ForegroundColor = fg;
      Console.BackgroundColor = bg;
    }

    private readonly ConsoleColor _originalForeground;
    private readonly ConsoleColor _originalBackground;

    public void Dispose()
    {
      Console.ForegroundColor = _originalForeground;
      Console.BackgroundColor = _originalBackground;
    }

    public static Color Red => new Color(ConsoleColor.Red);
    public static Color DarkRed => new Color(ConsoleColor.DarkRed);
  }
}
