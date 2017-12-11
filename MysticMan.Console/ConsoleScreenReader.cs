using System;

namespace MysticMan.ConsoleApp{
  internal class ConsoleScreenReader : IScreenReader {
    /// <inheritdoc />
    public string ReadLine() {
      return Console.ReadLine();
    }

    public string ReadLine(Position position) {
      Console.SetCursorPosition(position.Left, position.Top);
      bool cursorIsVisisble = Console.CursorVisible;
      Console.CursorVisible = true;

      string input = Console.ReadLine();

      if (!cursorIsVisisble) {
        Console.CursorVisible = false;
      }

      return input;

    }
  }
}
