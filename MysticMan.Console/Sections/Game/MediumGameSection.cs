using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game{
  public class MediumGameSection : GameSectionBase {
    public MediumGameSection(IScreenWriter screenWriter, IScreenInfo screenInfo) : base(screenWriter, screenInfo) {
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
    }

    protected override IDictionary<string, Position> CellPositions => new Dictionary<string, Position> {
      {"A1", new Position(8, 3)},
      {"B1", new Position(16, 3)},
      {"C1", new Position(24, 3)},
      {"D1", new Position(32, 3)},
      {"E1", new Position(40, 3)},
      {"F1", new Position(48, 3)},
      {"G1", new Position(56, 3)},
      {"H1", new Position(64, 3)},
      {"A2", new Position(8, 9)},
      {"B2", new Position(16, 7)},
      {"C2", new Position(24, 7)},
      {"D2", new Position(32, 7)},
      {"E2", new Position(40, 7)},
      {"F2", new Position(48, 7)},
      {"G2", new Position(56, 7)},
      {"H2", new Position(64, 7)},
      {"A3", new Position(8, 11)},
      {"B3", new Position(16, 11)},
      {"C3", new Position(24, 11)},
      {"D3", new Position(32, 11)},
      {"E3", new Position(40, 11)},
      {"F3", new Position(48, 11)},
      {"G3", new Position(56, 11)},
      {"H3", new Position(64, 11)},
      {"A4", new Position(8, 15)},
      {"B4", new Position(16, 15)},
      {"C4", new Position(24, 15)},
      {"D4", new Position(32, 15)},
      {"E4", new Position(40, 15)},
      {"F4", new Position(48, 15)},
      {"G4", new Position(56, 15)},
      {"H4", new Position(64, 15)},
      {"A5", new Position(8, 19)},
      {"B5", new Position(16, 19)},
      {"C5", new Position(24, 19)},
      {"D5", new Position(32, 19)},
      {"E5", new Position(40, 19)},
      {"F5", new Position(48, 19)},
      {"G5", new Position(56, 19)},
      {"H5", new Position(64, 19)},
      {"A6", new Position(8, 23)},
      {"B6", new Position(16, 23)},
      {"C6", new Position(24, 23)},
      {"D6", new Position(32, 23)},
      {"E6", new Position(40, 23)},
      {"F6", new Position(48, 23)},
      {"G6", new Position(56, 23)},
      {"H6", new Position(64, 23)},
      {"A7", new Position(8, 27)},
      {"B7", new Position(16, 27)},
      {"C7", new Position(24, 27)},
      {"D7", new Position(32, 27)},
      {"E7", new Position(40, 27)},
      {"F7", new Position(48, 27)},
      {"G7", new Position(56, 27)},
      {"H7", new Position(64, 27)},
      {"A8", new Position(8, 31)},
      {"B8", new Position(16, 31)},
      {"C8", new Position(24, 31)},
      {"D8", new Position(32, 31)},
      {"E8", new Position(40, 31)},
      {"F8", new Position(48, 31)},
      {"G8", new Position(56, 31)},
      {"H8", new Position(64, 31)}
    };
  }
}