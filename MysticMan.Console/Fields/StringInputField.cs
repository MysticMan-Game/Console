namespace MysticMan.ConsoleApp.Fields{
  internal class StringInputField : InputField<string> {
    public StringInputField(IScreenReader screenReader, IScreenInfo screenInfo):base(screenReader, screenInfo) {
      
    }

    protected override void SetInput(string input) {
      Input = input;
    }
  }
}
