using System;
using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game{
  public class MediumGameSection : GameSectionBase {
    public MediumGameSection(IScreenWriter screenWriter, IScreenInfo screenInfo, IScreenReader screenReader) : base(screenWriter, screenInfo, screenReader) {
    }

    /// <inheritdoc />
    protected override void OnInitialize() {
      base.OnInitialize();

      string content = @"    ║   A   │   B   │   C   │   D   │   E   │   F   │   G   │   H   ║
   ═╔═══════╤═══════╤═══════╤═══════╤═══════╤═══════╤═══════╤═══════╗
    ║       │       │       │       │       │       │       │       ║
   1║       │       │       │       │       │       │       │       ║
    ║       │       │       │       │       │       │       │       ║
   ─╟───────┼───────┼───────┼───────┼───────┼───────┼───────┼───────╢
    ║       │       │       │       │       │       │       │       ║
   2║       │       │       │       │       │       │       │       ║
    ║       │       │       │       │       │       │       │       ║
   ─╟───────┼───────┼───────┼───────┼───────┼───────┼───────┼───────╢
    ║       │       │       │       │       │       │       │       ║
   3║       │       │       │       │       │       │       │       ║
    ║       │       │       │       │       │       │       │       ║
   ─╟───────┼───────┼───────┼───────┼───────┼───────┼───────┼───────╢
    ║       │       │       │       │       │       │       │       ║
   4║       │       │       │       │       │       │       │       ║
    ║       │       │       │       │       │       │       │       ║
   ─╟───────┼───────┼───────┼───────┼───────┼───────┼───────┼───────╢
    ║       │       │       │       │       │       │       │       ║
   5║       │       │       │       │       │       │       │       ║
    ║       │       │       │       │       │       │       │       ║
   ─╟───────┼───────┼───────┼───────┼───────┼───────┼───────┼───────╢
    ║       │       │       │       │       │       │       │       ║
   6║       │       │       │       │       │       │       │       ║
    ║       │       │       │       │       │       │       │       ║
   ─╟───────┼───────┼───────┼───────┼───────┼───────┼───────┼───────╢
    ║       │       │       │       │       │       │       │       ║
   7║       │       │       │       │       │       │       │       ║
    ║       │       │       │       │       │       │       │       ║
   ─╟───────┼───────┼───────┼───────┼───────┼───────┼───────┼───────╢
    ║       │       │       │       │       │       │       │       ║
   8║       │       │       │       │       │       │       │       ║
    ║       │       │       │       │       │       │       │       ║
   ═╚═══════╧═══════╧═══════╧═══════╧═══════╧═══════╧═══════╧═══════╝
     Moves left:   1                         Round / Level: 002 / 001
     Move timer:  10 sec";
      SetContent(content);
      MovesField = CreateField<NumberField>(17, Top + 34, 3);
      RoundField = CreateField<NumberField>(60, Top + 34, 3);
      LevelField = CreateField<NumberField>(66, Top + 34, 3);
      TimerField = CreateField<NumberField>(17, Top + 35, 3);
      SolutionInputField = new StringInputField(ScreenReader, ScreenInfo) {
        Left = 5,
        Top = Top + 36,
        Length = ScreenInfo.Width,
        ScreenWriter = ScreenWriter,
        Value = "Please enter your expected solution (e.g. \"A1\"):",
        ForeGround = ConsoleColor.Yellow
      };
      PlayAgainField = new BooleanInputField(ScreenReader, ScreenInfo) {
        Left = 5,
        Top = Top + 36,
        Length = ScreenInfo.Width,
        ScreenWriter = ScreenWriter,
        Value = "Want to play again? (Y/N):",
        ForeGround = ConsoleColor.Yellow
      };
    }
    public override int XCounter => 8;
    public override int YCounter => 8;

    protected override IDictionary<string, Position> CellPositions {
      get {
        Dictionary<string, Position> cells = new Dictionary<string, Position>();
        int xOffset = 8;
        int xDistance = 8;
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
