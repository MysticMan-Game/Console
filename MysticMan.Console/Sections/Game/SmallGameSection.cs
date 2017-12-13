using System;
using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game {
  public class SmallGameSection : GameSectionBase {
    public SmallGameSection(IScreenWriter screenWriter, IScreenInfo screenInfo, IScreenReader screenReader) : base(screenWriter, screenInfo, screenReader) {
    }

    /// <inheritdoc />
    protected override void OnInitialize() {
      base.OnInitialize();
      string content = @"    ║    A    │    B    │    C    │    D    │    E    ║ 
   ═╔═════════╤═════════╤═════════╤═════════╤═════════╗ 
    ║         │         │         │         │         ║ 
   1║         │         │         │         │         ║ 
    ║         │         │         │         │         ║ 
   ─╟─────────┼─────────┼─────────┼─────────┼─────────╢ 
    ║         │         │         │         │         ║ 
   2║         │         │         │         │         ║ 
    ║         │         │         │         │         ║ 
   ─╟─────────┼─────────┼─────────┼─────────┼─────────╢ 
    ║         │         │         │         │         ║ 
   3║         │         │         │         │         ║ 
    ║         │         │         │         │         ║ 
   ─╟─────────┼─────────┼─────────┼─────────┼─────────╢ 
    ║         │         │         │         │         ║ 
   4║         │         │         │         │         ║ 
    ║         │         │         │         │         ║ 
   ─╟─────────┼─────────┼─────────┼─────────┼─────────╢ 
    ║         │         │         │         │         ║ 
   5║         │         │         │         │         ║ 
    ║         │         │         │         │         ║ 
   ═╚═════════╧═════════╧═════════╧═════════╧═════════╝";

      string stats = @"     Moves left:   1          Round / Level: 002 / 001  
     Move timer:  10 sec";

      SetContent(content);
      SetStats(stats);
      MovesField = CreateField<NumberField>(17, Top + 22, 3);
      RoundField = CreateField<NumberField>(45, Top + 22, 3);
      LevelField = CreateField<NumberField>(51, Top + 22, 3);
      TimerField = CreateField<NumberField>(17, Top + 23, 3);
      SolutionInputField = new StringInputField(ScreenReader, ScreenInfo) {
        Left = 5,
        Top = Top + 24,
        Length = ScreenInfo.Width,
        ScreenWriter = ScreenWriter,
        Value = "Please enter your expected solution (e.g. \"A1\"):",
        ForeGround = ConsoleColor.Yellow
      };
      PlayAgainField = new BooleanInputField(ScreenReader, ScreenInfo) {
        Left = 5,
        Top = Top + 24,
        Length = ScreenInfo.Width,
        ScreenWriter = ScreenWriter,
        Value = "Want to play again? (Y/N):",
        ForeGround = ConsoleColor.Yellow
      };
    }

    public override int XCounter => 5;
    public override int YCounter => 5;

    protected override IDictionary<string, Position> CellPositions {
      get {
        Dictionary<string, Position> cells = new Dictionary<string, Position>();
        int xOffset = 9;
        int xDistance = 10;
        int yOffset = 3;
        int yDistance = 4;

        for (int left = 0; left < XCounter; ++left) {
          for (int top = 0; top < YCounter; ++top) {
            string key = $"{(char)(65 + left)}{top + 1}";
            cells.Add(key, new Position(xOffset + left * xDistance, yOffset + top * yDistance));
          }
        }
        return cells;
      }
    }
   
  }
}
