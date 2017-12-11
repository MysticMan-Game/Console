using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game{
  public class XtraLargeGameSection : GameSectionBase {
    public XtraLargeGameSection(IScreenWriter screenWriter, IScreenInfo screenInfo) : base(screenWriter, screenInfo) {
    }

    /// <inheritdoc />
    protected override void OnInitialize() {
      base.OnInitialize();

      string content = @"    ║ A │ B │ C │ D │ E │ F │ G │ H │ I │ J │ K │ L │ M │ N │ O ║
   ═╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗
   1║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
   2║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
   3║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
   4║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
   5║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
   6║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
   7║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
   8║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
   9║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
  10║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
  11║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
  12║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
  13║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
  14║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ─╟───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───╢
  15║   │   │   │   │   │   │   │   │   │   │   │   │   │   │   ║
   ═╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝
     Moves left:   1                    Round / Level: 002 / 001
     Move timer:  10 sec";
      SetContent(content);
      MovesField = CreateField<NumberField>(17, Top + 32, 3);
      RoundField = CreateField<NumberField>(55, Top + 32, 3);
      LevelField = CreateField<NumberField>(61, Top + 32, 3);
      TimerField = CreateField<NumberField>(17, Top + 33, 3);
    }

    public override int XCounter => 15;
    public override int YCounter => 15;


    protected override IDictionary<string, Position> CellPositions {
      get {
        Dictionary<string, Position> cells = new Dictionary<string, Position>();
        int xOffset = 6;
        int xDistance = 4;
        int yOffset = 2;
        int yDistance = 2;

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
