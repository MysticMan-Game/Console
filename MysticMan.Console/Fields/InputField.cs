using System.Collections.Generic;

namespace MysticMan.ConsoleApp.Fields{
  internal abstract class InputField<TInput> : InputFieldBase {
    protected InputField(IScreenReader screenReader):base(screenReader) {
      
    }

    public TInput Input { get; protected set; }
  }
}
