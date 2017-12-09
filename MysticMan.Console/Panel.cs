using System;
using System.Linq;

namespace MysticMan.ConsoleApp{
  public class Panel {
    private string _content;
    public Position Position { get; set; }
    public Size Size { get; set; }

    public int Top => Position.Top;
    public int Bottom => Position.Top + Size.Height;

    public void SetContent(string content) {
      _content = content;
    }

    public void Draw() {
      // Build buffer based on the Size
      char[,] buffer = new char[Size.Width, Size.Height];

      char[,] contentBuffer = CreateStringBuffer(_content);
      // Output buffer
      for (int i = 0; i < buffer.GetLength(0); i++) {
        for (int j = 0; j < buffer.GetLength(1); j++) {
          char c = buffer[i, j];
          if (i < contentBuffer.GetLength(0) && j < contentBuffer.GetLength(1)) {
            c = contentBuffer[i, j];
          }
          Console.SetCursorPosition(Position.Left + i, Position.Top + j);
          Console.Write(c);
        }
      }
    }

    private static char[,] CreateStringBuffer(string text) {
      string[] lines = text.Split('\n');
      int maxX = lines.Max(l => l.Length);
      int maxY = lines.Length;

      char[,] buffer = new char[maxX, maxY];
      int x = 0;
      int y = 0;
      foreach (char current in text) {
        if (current == '\r') {
          continue;
        }
        if (current == '\n') {
          y = y + 1;
          x = 0;
        }
        else {
          if (x >= buffer.GetLength(0)) {
            continue;
          }
          if (y >= buffer.GetLength(1)) {
            continue;
          }

          buffer[x, y] = current;
          x = x + 1;
        }
      }
      return buffer;
    }
  }
}