namespace MysticMan.ConsoleApp.Fields{
  public class NumberField : Field<int>{
    public override void Draw(){
      string fmt = $"{{0, {Length}}}";
      string msg = string.Format(fmt, Value);
      ScreenWriter.Write(msg, Left, Top);
    }

    /// <inheritdoc />
    public NumberField(IScreenWriter screenWriter) : base(screenWriter){
    }
  }
}
