using System;

namespace MysticMan.ConsoleApp{
  internal class ConsoleScreenInfo : IScreenInfo {
    /// <inheritdoc />
    public int Width {
      get => Console.WindowWidth -1;
      set => Console.WindowWidth = value +1;
    }

    /// <inheritdoc />
    public int Height {
      get => Console.WindowHeight -1 ;
      set => Console.WindowHeight = value +1 ;
    }

    /// <inheritdoc />
    public Position CursorPosition => new Position(Console.CursorLeft, Console.CursorTop);
  }
}
