using System;
using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game{
  public class LargeGameSection : GameSectionBase {
    public LargeGameSection(IScreenWriter screenWriter, IScreenInfo screenInfo, IScreenReader screenReader) : base(screenWriter, screenInfo, screenReader) {
    }

    /// <inheritdoc />
    protected override void OnInitialize() {
      base.OnInitialize();

      string content = @"    ║  A  │  B  │  C  │  D  │  E  │  F  │  G  │  H  │  I  │  K  ║
   ═╔═════╤═════╤═════╤═════╤═════╤═════╤═════╤═════╤═════╤═════╗
    ║     │     │     │     │     │     │     │     │     │     ║
   1║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
   2║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
   3║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
   4║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
   5║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
   6║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
   7║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
   8║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
   9║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ─╟─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────╢
    ║     │     │     │     │     │     │     │     │     │     ║
  10║     │     │     │     │     │     │     │     │     │     ║
    ║     │     │     │     │     │     │     │     │     │     ║
   ═╚═════╧═════╧═════╧═════╧═════╧═════╧═════╧═════╧═════╧═════╝
     Moves left:   1                    Round / Level: 002 / 001
     Move timer:  10 sec";
      SetContent(content);
      MovesField = CreateField<NumberField>(17, Top + 42, 3);
      RoundField = CreateField<NumberField>(55, Top + 42, 3);
      LevelField = CreateField<NumberField>(61, Top + 42, 3);
      TimerField = CreateField<NumberField>(17, Top + 43, 3);
      SolutionInputField = new StringInputField(ScreenReader, ScreenInfo) {
        Left = 5,
        Top = Top + 44,
        Length = ScreenInfo.Width,
        ScreenWriter = ScreenWriter,
        Value = "Please enter your expected solution (e.g. \"A1\"):",
        ForeGround = ConsoleColor.Yellow
      };
    }


    public override int XCounter => 10;
    public override int YCounter => 10;

    protected override IDictionary<string, Position> CellPositions {
      get {
        Dictionary<string, Position> cells = new Dictionary<string, Position>();
        int xOffset = 7;
        int xDistance = 6;
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
