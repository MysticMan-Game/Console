using System.Collections.Generic;
using MysticMan.ConsoleApp.Panels;

namespace MysticMan.ConsoleApp {
  public class GamePanel : Panel {
    private Dictionary<string, Position> _fieldPosition;

    public GamePanel(IScreenWriter screenWriter) : base(screenWriter) {
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
      InitPositions();
      SetContent(content);
    }

    private void InitPositions() {
      _fieldPosition = new Dictionary<string, Position> {
        {"A1", new Position(9, 3) },
        {"B1", new Position(19, 3) },
        {"C1", new Position(29, 3) },
        {"D1", new Position(39, 3) },
        {"E1", new Position(49, 3) },
        {"A2", new Position(9, 7) },
        {"B2", new Position(19, 7) },
        {"C2", new Position(29, 7) },
        {"D2", new Position(39, 7) },
        {"E2", new Position(49, 7) },
        {"A3", new Position(9, 11) },
        {"B3", new Position(19, 11) },
        {"C3", new Position(29, 11) },
        {"D3", new Position(39, 11) },
        {"E3", new Position(49, 11) },
        {"A4", new Position(9, 15) },
        {"B4", new Position(19, 15) },
        {"C4", new Position(29, 15) },
        {"D4", new Position(39, 15) },
        {"E4", new Position(49, 15) },
        {"A5", new Position(9, 19) },
        {"B5", new Position(19, 19) },
        {"C5", new Position(29, 19) },
        {"D5", new Position(39, 19) },
        {"E5", new Position(49, 19) },
      };
    }

    public void ShowMysticMan(string field) {
      Position position = _fieldPosition[field];
      ScreenWriter.Write("X", Position.Left + position.Left, Position.Top + position.Top);
    }
  }
}
