﻿namespace MysticMan.ConsoleApp{
  public class GameField : Panel {

    public GameField() {
      string content = @"    ║    A    │    B    │    C    │    D    │   E    ║ 
   ═╔═════════╤═════════╤═════════╤═════════╤════════╗ 
    ║         │         │         │         │        ║ 
   1║         │         │         │         │        ║ 
    ║         │         │         │         │        ║ 
   ─╟─────────┼─────────┼─────────┼─────────┼────────╢ 
    ║         │         │         │         │        ║ 
   2║         │         │         │         │        ║ 
    ║         │         │         │         │        ║ 
   ─╟─────────┼─────────┼─────────┼─────────┼────────╢ 
    ║         │         │         │         │        ║ 
   3║         │         │         │         │        ║ 
    ║         │         │         │         │        ║ 
   ─╟─────────┼─────────┼─────────┼─────────┼────────╢ 
    ║         │         │         │         │        ║ 
   4║         │         │         │         │        ║ 
    ║         │         │         │         │        ║ 
   ─╟─────────┼─────────┼─────────┼─────────┼────────╢ 
    ║         │         │         │         │        ║ 
   5║         │         │         │         │        ║ 
    ║         │         │         │         │        ║ 
   ═╚═════════╧═════════╧═════════╧═════════╧════════╝ 
     Moves left:   1          Round / Level: 002 / 001  
     Move timer:  10 sec";
      SetContent(content);
    }

  }
}
