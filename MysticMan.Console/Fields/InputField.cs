using System.Collections.Generic;

namespace MysticMan.ConsoleApp.Fields{
  public abstract class InputField<TInput> : InputFieldBase {
    protected InputField(IScreenReader screenReader, IScreenWriter screenWriter, IScreenInfo screenInfo):base(screenReader, screenWriter, screenInfo) {
      
    }

    public TInput Input { get; protected set; }
  }
}
