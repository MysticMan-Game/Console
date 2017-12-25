namespace MysticMan.ConsoleApp.Fields{
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

    /// <inheritdoc />
    public override void Clear() {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    protected Field(IScreenWriter screenWriter) : base(screenWriter){
    }
  }
}
