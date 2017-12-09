namespace MysticMan.ConsoleApp{
  public abstract class FieldBase{
    public int PosX{ get; set; }
    public int PosY{ get; set; }
    public int Length{ get; set; }
    public bool AutoDraw{ get; set; }
    public abstract void Draw();
  }

  public abstract class Field<TValue> : FieldBase{
    private TValue _value;

    public TValue Value{
      get => _value;
      set{
        _value = value;
        if (AutoDraw){
          Draw();
        }
      }
    }
  }
}