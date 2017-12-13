namespace MysticMan.ConsoleApp.Fields{
  public class BooleanInputField : InputField<bool> {
    /// <inheritdoc />
    public BooleanInputField(IScreenReader screenReader, IScreenInfo screenInfo) : base(screenReader, screenInfo) {
    }

    /// <inheritdoc />
    protected override void SetInput(string input) {
      Input = input?.ToUpperInvariant() == "Y";
    }

    public override void Clear() {
      int valueLength = GetValue()?.Length ?? 0;
      int inputLength = ScreenInfo.Width;
      string emptyValue = new string('\0', valueLength + inputLength + 1);
      Write(emptyValue);
    }
  }
}