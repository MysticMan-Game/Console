using System;
using System.Text.RegularExpressions;

namespace MysticMan.ConsoleApp.Fields{
  public class SolutionInputField: StringInputField{
    private Regex _regex;

    /// <inheritdoc />
    public SolutionInputField(Size size, IScreenReader screenReader, IScreenWriter screenWriter, IScreenInfo screenInfo) : base(screenReader, screenWriter, screenInfo){


      // Create a regular expression based on the size
      string characterMatch = $"[A-{(char)(65 + size.Width)}]";
      string numberMatch = size.Height < 10 ? $"[1-{size.Height}]" : $"([1-9]|1[0-{size.Height - 10}])";
      _regex = new Regex(characterMatch + numberMatch, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
    }

    /// <inheritdoc />
    protected override void SetInput(string input) {
      if (_regex.IsMatch(input)) {
        base.SetInput(input);
      }
      else {
        throw new FormatException($"Invalid input {input}");
      }
    }
  }
}
