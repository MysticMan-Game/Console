using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game{
  public abstract class GameSectionBase : Section {
    protected NumberField MovesField { get; set; }
    protected NumberField RoundField { get; set; }
    protected NumberField LevelField { get; set; }
    protected NumberField TimerField { get; set; }

    protected GameSectionBase(IScreenWriter screenWriter, IScreenInfo screenInfo) : base(screenWriter, screenInfo) {
    }

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
    public abstract int XCounter{ get; }
    public abstract int YCounter{ get; }

    public void ShowMysticMan(string field) {
      if (CellPositions.ContainsKey(field)) {
        Position position = CellPositions[field];
        ScreenWriter.Write("X", Position.Left + position.Left, Position.Top + position.Top);
      }
    }
  }
}
