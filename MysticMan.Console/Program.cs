using System;
using System.Diagnostics;
using System.Timers;

namespace MysticMan.ConsoleApp {
  internal class Program {
    private IntroScreen _introScreen;
    private MainScreen _mainScreen;

    private static void Main() {
      new Program().Run();
    }

    private void Run() {
      Console.CursorVisible = false;

      Console.Clear();
      IScreenReader screenReader = new ConsoleScreenReader();
      IScreenWriter screenWriter = new ConsoleScreenWriter();
      IScreenInfo screenInfo = new ConsoleScreenInfo();

      _introScreen = new IntroScreen("MysticMan - Game", screenWriter, screenReader, screenInfo);
      _introScreen.Run();

      // Initialize the MainScreen
      _mainScreen = new MainScreen("MysticMan - Game", screenWriter, screenInfo){
        InfoLineOne = "Press the following Keys to modify the counters",
        InfoLineTwo = "Moves: +/- (NumPad) | Level: l/L | Rounds: r/R",
        Level = _introScreen.Level
      };

      // Initialize a timer 
      Timer timer = new Timer(1000);
      timer.Elapsed += (sender, eventArgs) => { _mainScreen.Timer += 1; };
      timer.Start();

      // Run the static mainScreen
      _mainScreen.Run(InputLoop);

      timer.Stop();

      Console.SetCursorPosition(0, _mainScreen.EndOfScreen + 2);
      Console.WriteLine("GAME OVER - Press ENTER to Quit");
    }

    private void InputLoop() {
      // Enter the input loop
      Console.TreatControlCAsInput = true;
      bool exitLoop = false;
      do {
        // Read a key from input and decide what todo;
        ConsoleKeyInfo input = Console.ReadKey(true);
        switch (input.Key) {
          case ConsoleKey.R:
            if (input.KeyChar == 'R') {
              _mainScreen.Rounds -= 1;
            }
            if (input.KeyChar == 'r') {
              _mainScreen.Rounds += 1;
            }
            break;
          case ConsoleKey.L:
            if (input.KeyChar == 'L') {
              _mainScreen.Level -= 1;
            }
            if (input.KeyChar == 'l') {
              _mainScreen.Level += 1;
            }
            break;
          case ConsoleKey.W:
            WallSound();
            break;
          case ConsoleKey.Add:
          case ConsoleKey.OemPlus:
            _mainScreen.Moves += 1;
            break;
          case ConsoleKey.Subtract:
          case ConsoleKey.OemMinus:
            _mainScreen.Moves -= 1;
            break;
          case ConsoleKey.M:
            for (int i = 0; i < Math.Min(_mainScreen.MaxXCells, _mainScreen.MaxYCells) ; i++ ) {
              _mainScreen.ShowMysticMan($"{(char)(65 +i)}{i +1}");
            }

            int min = Math.Min(_mainScreen.MaxXCells, _mainScreen.MaxYCells);
            for (int i = min - 1; i >=0; i--) {
              _mainScreen.ShowMysticMan($"{(char)(65 + i)}{min - i}");
            }
            break;
          case ConsoleKey.D1:
            if ((input.Modifiers & ConsoleModifiers.Control) != 0) {
              _mainScreen.SetGameSection(GameSection.Small);
            }
            break;
          case ConsoleKey.D2:
            if ((input.Modifiers & ConsoleModifiers.Control) != 0) {
              _mainScreen.SetGameSection(GameSection.Medium);
            }
            break;
          case ConsoleKey.D3:
            if ((input.Modifiers & ConsoleModifiers.Control) != 0) {
              _mainScreen.SetGameSection(GameSection.Large);
            }
            break;
          case ConsoleKey.D4:
            if ((input.Modifiers & ConsoleModifiers.Control) != 0) {
              _mainScreen.SetGameSection(GameSection.XLarge);
            }
            break;
          case ConsoleKey.Q:
            exitLoop = true;
            break;
          case ConsoleKey.C:
            exitLoop = (input.Modifiers & ConsoleModifiers.Control) != 0;
            break;
        }
      } while (!exitLoop);
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
