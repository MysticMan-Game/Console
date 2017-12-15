using System;

namespace MysticMan.ConsoleApp.Fields {
  public class BooleanInputField : InputField<bool> {
    /// <inheritdoc />
    public BooleanInputField(IScreenReader screenReader, IScreenInfo screenInfo) : base(screenReader, screenInfo) {
    }

    /// <inheritdoc />
    protected override void SetInput(string input) {
      Input = input?.ToUpperInvariant() == "Y";
    }

    /// <inheritdoc />
    protected override string GetInput(Position position) {
      while (true) {
        ConsoleKeyInfo consoleKeyInfo = ScreenReader.ReadKeyFrom(position);
        if (consoleKeyInfo.KeyChar >= 'A' && consoleKeyInfo.KeyChar <= 'z' || consoleKeyInfo.Key == ConsoleKey.Enter ) {
          switch (consoleKeyInfo.Key) {
            // TODO: handle multilanguage (in german it's J) 
            case ConsoleKey.Enter:
            case ConsoleKey.J:
            case ConsoleKey.Y:
              return "Y";
            default:
              return "N";
          }
        }
      }
    }

    public override void Clear() {
      int valueLength = GetValue()?.Length ?? 0;
      int inputLength = ScreenInfo.Width;
      string emptyValue = new string('\0', valueLength + inputLength + 1);
      Write(emptyValue);
    }
  }
}
