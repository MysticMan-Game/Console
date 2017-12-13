using System;
using System.Collections.Generic;
using System.Text;

namespace MysticMan.ConsoleApp.Extensions{
  public static class CharBufferExtensions {
    public static void CopyTo(this char[,] source, char[,] target, int left, int top) {
      for (int col = 0; col < source.GetLength(0) && left + col < target.GetLength(0); col++) {
        for (int row = 0; row < source.GetLength(1) && top + row < target.GetLength(1); row++) {
            target[left + col, top + row] = source[col, row];
        }
      }
    }

    public static char[,] Stretch(this char[,] source, int width, int height) {
      int newWidth = Math.Max(source.GetLength(0), width);
      int newHeight = Math.Max(source.GetLength(1), height);
      char[,] buffer = new char[newWidth, newHeight];
      source.CopyTo(buffer, 0, 0);
      return buffer;
    }

    public static int GetWidth(this char[,] buffer) {
      return buffer.GetLength(0);
    }

    public static int GetHeight(this char[,] buffer) {
      return buffer.GetLength(1);
    }

    public static char[] ToCharArray(this char[,] buffer) {
      StringBuilder sb = new StringBuilder();
      for (int row = 0; row < buffer.GetLength(1); row++) {
        for (int col = 0; col < buffer.GetLength(0); col++) {
          char value = buffer[col, row];
          sb.Append(value);
        }
        sb.AppendLine();
      }
      return sb.ToString().ToCharArray();
    }

    public static char[,] ShrinkLines(this char[,] buffer) {
      int lastLineWithContent = 0;
      for (int row = 0; row < buffer.GetLength(1); row++) {
        for (int col = 0; col < buffer.GetLength(0); col++) {
          char value = buffer[col, row];
          if (value != '\0') {
            lastLineWithContent = row;
            break;
          }
        }
      }

      char[,] newBuffer = new char[buffer.GetLength(0), lastLineWithContent+1];
      buffer.CopyTo(newBuffer, 0, 0);
      return newBuffer;
    }
  }
}
