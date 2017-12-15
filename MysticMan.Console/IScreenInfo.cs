using System;

namespace MysticMan.ConsoleApp{
  public interface IScreenInfo {
    int Width { get;  }
    int Height { get;  }
    Position CursorPosition { get; }
    ConsoleColor DefaultBackgroundColor { get;  }
  }
}
