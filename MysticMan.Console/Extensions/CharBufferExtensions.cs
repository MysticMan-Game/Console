using System;

namespace MysticMan.ConsoleApp.Extensions{
  public static class CharBufferExtensions {
    public static void CopyTo(this char[,] source, char[,] target, int left, int top) {
      for (int i = 0; i < source.GetLength(0); i++) {
        for (int j = 0; j < source.GetLength(1); j++) {
          if (left + i < target.GetLength(0) && top + j < target.GetLength(1)) {
            target[left + i, top + j] = source[i, j];
          }
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
  }
}