using System;
using System.Diagnostics;

namespace MysticMan.ConsoleApp{
  internal class ConsoleScreenReader : IScreenReader {
    public string ReadLine(Position position) {
      bool cursorIsVisisble = Console.CursorVisible;
      Console.CursorVisible = true;
      Console.SetCursorPosition(position.Left, position.Top);

      string input = Console.ReadLine();

      if (!cursorIsVisisble) {
        Trace.WriteLine("Cursor will be disabled after ReadLine");
        Console.CursorVisible = false;
      }

      return input;

    }

    /// <inheritdoc />
    public ConsoleKeyInfo ReadKeyFrom(Position position) {
      bool cursorIsVisisble = Console.CursorVisible;
      Console.CursorVisible = true;
      Console.SetCursorPosition(position.Left, position.Top);

      ConsoleKeyInfo input = Console.ReadKey(true);

      if (!cursorIsVisisble) {
        Trace.WriteLine("Cursor will be disabled after ReadLine");
        Console.CursorVisible = false;
      }

      return input;
    }
  }
}
