namespace MysticMan.ConsoleApp.Fields{
  internal class StringInputField : InputField<string> {
    public StringInputField(IScreenReader screenReader):base(screenReader) {
      
    }

    protected override void SetInput(string input) {
      Input = input;
    }
  }
}
