using System;
using System.Diagnostics;

namespace MysticMan.ConsoleApp{
  internal class ConsoleScreenReader : IScreenReader {
    /// <inheritdoc />
    public string ReadLine() {
      return Console.ReadLine();
    }

    public string ReadLine(Position position) {
      Trace.WriteLine("ConsoleScreenReader.ReadLine entered...");
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
  }
}
