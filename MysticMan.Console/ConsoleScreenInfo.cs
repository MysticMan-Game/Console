using System;

namespace MysticMan.ConsoleApp{
  internal class ConsoleScreenInfo : IScreenInfo {
    public ConsoleScreenInfo() {
      DefaultBackgroundColor = Console.BackgroundColor;
    }

    /// <inheritdoc />
    public int Width => Console.WindowWidth - 1;

    /// <inheritdoc />
    public int Height => Console.WindowHeight -1;

    /// <inheritdoc />
    public Position CursorPosition => new Position(Console.CursorLeft, Console.CursorTop);

    /// <inheritdoc />
    public ConsoleColor DefaultBackgroundColor { get; }
  }
}
