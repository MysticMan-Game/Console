using System;
using System.Collections.Generic;
using MysticMan.ConsoleApp.Extensions;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game {
  public abstract class GameSectionBase : Section {
    private string _statistics;

    protected GameSectionBase(IScreenWriter screenWriter, IScreenInfo screenInfo, IScreenReader screenReader) : base(screenWriter, screenInfo) {
      ScreenReader = screenReader;
    }

    protected NumberField MovesField { get; set; }
    protected NumberField RoundField { get; set; }
    protected NumberField LevelField { get; set; }
    protected NumberField TimerField { get; set; }

    protected StringInputField SolutionInputField { get; set; }

    protected BooleanInputField PlayAgainField { get; set; }

    public int Moves {
      get => MovesField.Value;
      set => MovesField.Value = value;
    }

    public int Rounds {
      get => RoundField.Value;
      set => RoundField.Value = value;
    }

    public int Level {
      get => LevelField.Value;
      set => LevelField.Value = value;
    }

    public int Timer {
      get => TimerField.Value;
      set => TimerField.Value = value;
    }

    protected abstract IDictionary<string, Position> CellPositions { get; }
    public abstract int XCounter { get; }
    public abstract int YCounter { get; }
    public IScreenReader ScreenReader { get; set; }

    public void ShowMysticMan(string field, ConsoleColor foregroundColor) {
      if (CellPositions.ContainsKey(field)) {
        Position position = CellPositions[field];
        ScreenWriter.Write("X", Position.Left + position.Left, Position.Top + position.Top, foregroundColor);
      }
    }

    public string GetSolution() {
      SolutionInputField.Read();
      string solution = SolutionInputField.Input;
      SolutionInputField.Clear();
      return solution;
    }

    public void PressEnterScreen() {
      // http://patorjk.com/software/taag/#p=testall&v=2&f=Small%20Slant&t=PRESS%20SPACE

      string value = @"
   ___  ___  ____________  _______  ___  _________
  / _ \/ _ \/ __/ __/ __/ / __/ _ \/ _ |/ ___/ __/
 / ___/ , _/ _/_\ \_\ \  _\ \/ ___/ __ / /__/ _/  
/_/  /_/|_/___/___/___/_/___/_/_ /_/_|_\___/___/  

     __________    _____________   ___  ______
    /_  __/ __ \  / __/_  __/ _ | / _ \/_  __/
     / / / /_/ / _\ \  / / / __ |/ , _/ / /   
    /_/  \____/ /___/ /_/ /_/ |_/_/|_| /_/    
";

      Notify(value);
    }

    private void Notify(string value) {
      value = value.Substring(2, value.Length - 4);
      int valueHeight = value.Split('\n').Length;

      char[,] stringBuffer = CreateStringBuffer(value);
      char[,] buffer = GetContentBuffer();

      int left = buffer.GetLength(0) / 2 - stringBuffer.GetLength(0) / 2;
      left = Math.Max(4, left);
      int top  = (buffer.GetLength(1) - valueHeight) / 2;
      WriteContent();
      Write(value,left, top );
      Draw();
    }

    public void WinningScreen() {
      string value = @"
  ________   __  _______  _      ______  _  __
 / ___/ _ | /  |/  / __/ | | /| / / __ \/ |/ /
/ (_ / __ |/ /|_/ / _/   | |/ |/ / /_/ /    / 
\___/_/ |_/_/  /_/___/   |__/|__/\____/_/|_/  
";

      Notify(value);
    }

    public void LostScreen() {
      string value = @"
   ________   __  _______  __   ____  __________
 / ___/ _ | /  |/  / __/ / /  / __ \/ __/_  __/
/ (_ / __ |/ /|_/ / _/  / /__/ /_/ /\ \  / /   
\___/_/ |_/_/  /_/___/ /____/\____/___/ /_/      
";

      Notify(value);
    }

    public bool PlayAgain() {
      PlayAgainField.Read();
      PlayAgainField.Clear();
      return PlayAgainField.Input;
    }

    protected void SetStats(string statistics) {
      _statistics = statistics;
      AppendLine(_statistics);
    }

    public void Refresh() {
      WriteContent();
      Draw();
    }

    public void IndicateField(string field, Signal signal) {

      FormatedString formatedString = GetIndicationFieldValue(signal);

      string value = formatedString.Text;
      char[,] stringBuffer = CreateStringBuffer(value);
      if (CellPositions.ContainsKey(field)) {
        Position position = CellPositions[field];
        int left = Position.Left + (position.Left - (stringBuffer.GetLength(0) / 2));
        int top = Position.Top + (position.Top - (stringBuffer.GetLength(1) / 2));
        ScreenWriter.Write(value, left,top,formatedString.ForegroundColor, formatedString.BackgroundColor);
      }
    }

    protected virtual FormatedString GetIndicationFieldValue(Signal signal){
      FormatedString formatedString = new FormatedString("X") {
        ForegroundColor =ConsoleColor.Green,
        BackgroundColor = ScreenInfo.DefaultBackgroundColor
      };
      return formatedString;
    }
  }

  public class FormatedString {
    private readonly string _text;

    public FormatedString(string text) {
      _text = text;
    }

    public ConsoleColor ForegroundColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; }

    public string Text => _text;
  }
}
