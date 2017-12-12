namespace MysticMan.ConsoleApp.Fields{
  public abstract class FieldBase{
    public int Left{ get; set; }
    public int Top{ get; set; }
    public int Length{ get; set; }
    public bool AutoDraw{ get; set; }
    public  IScreenWriter ScreenWriter { get; set; }
    public abstract void Draw();

    public abstract void Clear();
  }
}
