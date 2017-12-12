using System;
using System.Linq;

namespace MysticMan.ConsoleApp.Fields{
  public abstract class InputFieldBase : StringField {
    private readonly IScreenInfo _screenInfo;

    protected InputFieldBase(IScreenReader screenReader, IScreenInfo screenInfo) {
      _screenInfo = screenInfo;
      ScreenReader = screenReader;
    }

    private IScreenReader ScreenReader { get; }

    public void Read() {
      bool isValid = false;


      do {
        Draw();
        string input = ScreenReader.ReadLine(new Position(Left + (GetValue()?.Length ?? 0) + 1, Top));

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
