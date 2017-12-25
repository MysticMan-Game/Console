namespace MysticMan.ConsoleApp.Fields{
  internal class IntInputField : InputField<int> {
    protected override void SetInput(string input) {
      if (int.TryParse(input, out int inputInteger)) {
        Input = inputInteger;
      }
    }

    /// <inheritdoc />
    public IntInputField(IScreenReader screenReader, IScreenInfo screenInfo, IScreenWriter screenWriter) : base(screenReader, screenWriter, screenInfo){
    }
  }
}
