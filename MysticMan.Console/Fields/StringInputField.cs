using System;
using System.Linq;

namespace MysticMan.ConsoleApp.Fields{
  public class StringInputField : InputField<string> {
    public StringInputField(IScreenReader screenReader, IScreenInfo screenInfo):base(screenReader, screenInfo) {
      
    }
    
    protected override void SetInput(string input) {
      Input = input;
    }

    /// <inheritdoc />
    public override void Clear() {
      int valueLength = GetValue()?.Length ?? 0;
      int inputLength = Input?.Length ?? 0;
      string emptyValue = new string('\0', valueLength + inputLength + 1);
      Write(emptyValue);
    }
  }
}
