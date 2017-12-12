using System.Collections.Generic;

namespace MysticMan.ConsoleApp.Fields{
  public abstract class InputField<TInput> : InputFieldBase {
    protected InputField(IScreenReader screenReader, IScreenInfo screenInfo):base(screenReader, screenInfo) {
      
    }

    public TInput Input { get; protected set; }
  }
}
