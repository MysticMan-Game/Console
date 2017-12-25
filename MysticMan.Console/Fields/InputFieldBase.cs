using System;
using System.Linq;

namespace MysticMan.ConsoleApp.Fields{
  public abstract class InputFieldBase : StringField {
    private readonly IScreenInfo _screenInfo;

    protected InputFieldBase(IScreenReader screenReader, IScreenWriter screenWriter, IScreenInfo screenInfo) : base(screenWriter) {
      _screenInfo = screenInfo;
      ScreenReader = screenReader;
    }

    protected IScreenReader ScreenReader { get; }

    public IScreenInfo ScreenInfo => _screenInfo;

    public void Read() {
      bool isValid = false;

      do {
        Draw();
        Position position = new Position(Left + (GetValue()?.Length ?? 0) + 1, Top);
        string input = GetInput(position);

        try {
          SetInput(input);
          isValid = true;
        }
        catch (Exception) {
          string emptyValue = new string(Enumerable.Repeat('\0', (GetValue()?.Length ?? 0) + 1 + input.Length).ToArray());
          ScreenWriter.Write(emptyValue, Left, Top);
        }
      } while (!isValid);
    }

    protected virtual string GetInput(Position position){
      string input = ScreenReader.ReadLine(position);
      return input;
    }

    protected abstract void SetInput(string input);
  }
}
