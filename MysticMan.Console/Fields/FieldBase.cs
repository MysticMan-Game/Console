namespace MysticMan.ConsoleApp.Fields{
  public abstract class FieldBase{
    protected FieldBase(IScreenWriter screenWriter) {
      ScreenWriter = screenWriter;
    }


    public int Left{ get; set; }
    public int Top{ get; set; }
    public int Length{ get; set; }
    public bool AutoDraw{ get; set; }
    public  IScreenWriter ScreenWriter { get; }
    public abstract void Draw();

    public abstract void Clear();
  }
}
