using System;

namespace MysticMan.ConsoleApp{
  public interface IScreenReader {
    string ReadLine(Position position);
    ConsoleKeyInfo ReadKeyFrom(Position position);
  }
}
