using System;
using System.Collections.Generic;
using System.Text;

namespace MysticMan.ConsoleApp.Fields {
  internal class NumberRangeInputField : IntInputField {
    public ICollection<int> AllowedValues { get; set; }

    /// <inheritdoc />
    protected override string GetValue() {
      string value = base.GetValue();

      if (AllowedValues != null && AllowedValues.Count > 0) {
        StringBuilder sb = new StringBuilder();
        foreach (int number in AllowedValues) {
          if (sb.Length > 0) {
            sb.Append(", ");
          }
          sb.Append(number);
        }
        return $"{value} [{sb}]";
      }
      return value;
    }

    protected override void SetInput(string input) {
      if (int.TryParse(input, out int inputInteger) && null != AllowedValues) {
        if (AllowedValues.Contains(inputInteger)) {
          Input = inputInteger;
        }
        else {
          throw new ArgumentOutOfRangeException(nameof(input));
        }
      }
    }

    /// <inheritdoc />
    public NumberRangeInputField(IScreenReader screenReader, IScreenInfo screenInfo) : base(screenReader, screenInfo) {
    }
  }
}
