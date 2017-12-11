using System;

namespace MysticMan.ConsoleApp{
  internal class ConsoleScreenReader : IScreenReader {
    /// <inheritdoc />
    public string ReadLine() {
      return Console.ReadLine();
    }
  }
}