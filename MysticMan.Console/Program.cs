using System;
using System.Timers;

namespace MysticMan.ConsoleApp{
  internal class Program{
    private static void Main(){
      Console.CursorVisible = false;

      // Initialize the Screen
      ConsoleScreenWriter consoleScreenWriter = new ConsoleScreenWriter();
      Screen screen = new Screen("MysticMan - Game", consoleScreenWriter){
        InfoLineOne = "Press the following Keys to modify the counters",
        InfoLineTwo = "Moves: +/- (NumPad) | Level: l/L | Rounds: r/R"
      };

      // Initialize a timer 
      Timer timer = new Timer(1000);
      timer.Elapsed += (sender, eventArgs) => { screen.Timer += 1; };
      timer.Start();

      // Draw the static screen
      screen.Draw();

      // Enter the input loop
      Console.TreatControlCAsInput = true;
      bool exitLoop = false;
      do{
        // Read a key from iput and decide what todo;
        ConsoleKeyInfo input = Console.ReadKey(true);
        switch (input.Key){
          case ConsoleKey.R:
            if (input.KeyChar == 'R'){
              screen.Rounds -= 1;
            }
            if (input.KeyChar == 'r'){
              screen.Rounds += 1;
            }
            break;
          case ConsoleKey.L:
            if (input.KeyChar == 'L'){
              screen.Level -= 1;
            }
            if (input.KeyChar == 'l'){
              screen.Level += 1;
            }
            break;
          case ConsoleKey.W:
            WallSound();
            break;
          case ConsoleKey.Add:
          case ConsoleKey.OemPlus:
            screen.Moves += 1;
            break;
          case ConsoleKey.Subtract:
          case ConsoleKey.OemMinus:
            screen.Moves -= 1;
            break;
          case ConsoleKey.M:
            screen.ShowMysticMan("A1");
            screen.ShowMysticMan("B2");
            screen.ShowMysticMan("C3");
            screen.ShowMysticMan("D4");
            screen.ShowMysticMan("E5");
            break;
          case ConsoleKey.Q:
            exitLoop = true;
            break;
          case ConsoleKey.C:
            exitLoop = (input.Modifiers & ConsoleModifiers.Control) != 0;
            break;
        }
      } while (!exitLoop);

      timer.Stop();

      Console.SetCursorPosition(0, screen.EndOfScreen + 2);
      Console.WriteLine("GAME OVER - Press ENTER to Quit");
    }

    private static void WallSound() {
      for (int i = 0; i < 2; i++) {
        Console.Beep(620, 420);
        Console.Beep(680, 400);
        Console.Beep(550, 300);
      }
      Console.Beep(440, 600);
    }
  }
}
