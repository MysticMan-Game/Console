using System;

namespace MysticMan.ConsoleApp{
  internal class ConsoleScreenInfo : IScreenInfo {
    /// <inheritdoc />
    public int Width {
      get => Console.WindowWidth;
      set => Console.WindowWidth = value;
    }

    /// <inheritdoc />
    public int Height {
      get => Console.WindowHeight;
      set => Console.WindowHeight = value;
    }
  }
}