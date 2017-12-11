using System;
using System.Linq;

namespace MysticMan.ConsoleApp.Fields{
  internal abstract class InputFieldBase : StringField {
    protected InputFieldBase(IScreenReader screenReader) {
      ScreenReader = screenReader;
    }

    private IScreenReader ScreenReader { get; }

    public void Read() {
      bool isValid = false;


      do {
        Draw();
        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
        bool cursorIsVisisble = Console.CursorVisible;
        Console.CursorVisible = true;
        string input = ScreenReader.ReadLine();
        if (!cursorIsVisisble) {
          Console.CursorVisible = false;
        }

        try {
          SetInput(input);
          isValid = true;
        }
        catch (Exception) {
          string emptyValue = new string(Enumerable.Repeat('\0', GetValue().Length + 1 + input.Length).ToArray());
          ScreenWriter.Write(emptyValue, Left, Top);
        }
      } while (!isValid);
    }

    protected abstract void SetInput(string input);
  }
}
