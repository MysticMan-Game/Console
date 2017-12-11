using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game {
  public class SmallGameSection : GameSectionBase {
    public SmallGameSection(IScreenWriter screenWriter, IScreenInfo screenInfo) : base(screenWriter, screenInfo) {
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
   ═╚═════════╧═════════╧═════════╧═════════╧═════════╝ 
     Moves left:   1          Round / Level: 002 / 001  
     Move timer:  10 sec";
      SetContent(content);
      MovesField = CreateField<NumberField>(17, Top + 22, 3);
      RoundField = CreateField<NumberField>(45, Top + 22, 3);
      LevelField = CreateField<NumberField>(51, Top + 22, 3);
      TimerField = CreateField<NumberField>(17, Top + 23, 3);
    }

    protected override IDictionary<string, Position> CellPositions => new Dictionary<string, Position> {
      {"A1", new Position(9, 3)},
      {"B1", new Position(19, 3)},
      {"C1", new Position(29, 3)},
      {"D1", new Position(39, 3)},
      {"E1", new Position(49, 3)},
      {"A2", new Position(9, 7)},
      {"B2", new Position(19, 7)},
      {"C2", new Position(29, 7)},
      {"D2", new Position(39, 7)},
      {"E2", new Position(49, 7)},
      {"A3", new Position(9, 11)},
      {"B3", new Position(19, 11)},
      {"C3", new Position(29, 11)},
      {"D3", new Position(39, 11)},
      {"E3", new Position(49, 11)},
      {"A4", new Position(9, 15)},
      {"B4", new Position(19, 15)},
      {"C4", new Position(29, 15)},
      {"D4", new Position(39, 15)},
      {"E4", new Position(49, 15)},
      {"A5", new Position(9, 19)},
      {"B5", new Position(19, 19)},
      {"C5", new Position(29, 19)},
      {"D5", new Position(39, 19)},
      {"E5", new Position(49, 19)}
    };
  }
}
