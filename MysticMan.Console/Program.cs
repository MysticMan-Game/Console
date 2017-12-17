using System;
using MysticMan.ConsoleApp.Engine;
using MysticMan.Logic;

namespace MysticMan.ConsoleApp {
  internal class Program {
    private IntroScreen _introScreen;
    private MainScreen _mainScreen;
    private IGameEngine _engine;
    private bool _exitLoop;

    private static void Main() {
      new Program().Run();
    }

    private void Run() {
      Console.CancelKeyPress += Console_CancelKeyPress;

      Console.CursorVisible = false;
      Console.Clear();
      IScreenReader screenReader = new ConsoleScreenReader();
      IScreenWriter screenWriter = new ConsoleScreenWriter();
      IScreenInfo screenInfo = new ConsoleScreenInfo();

      _introScreen = new IntroScreen("MysticMan - Game", screenWriter, screenReader, screenInfo);
      _introScreen.Run();

      // Initialize the MainScreen
      _mainScreen = new MainScreen("MysticMan - Game", screenWriter, screenInfo, screenReader) {
        InfoLineOne = "Press the following Keys to modify the counters",
        InfoLineTwo = "Moves: +/- (NumPad) | Level: l/L | Rounds: r/R",
        Level = _introScreen.Level
      };

      if (_introScreen.Level > 1) {
        _engine = new TestEngine();
        _engine.Initialize();
        _engine.WallReached += (sender, args) => WallSound();
      }

      // Run the static mainScreen
      _mainScreen.Run(InputLoop);

      Console.SetCursorPosition(0, _mainScreen.EndOfScreen + 2);
      Console.WriteLine("GAME OVER - Press ENTER to Quit");
    }

    private void InputLoop() {
      // Enter the input loop
      _exitLoop = false;
      ISolutionResult solutionResult = null;

      do {
        if (null != _engine) {
          switch (_engine.State) {
            case GameEngineState.Initialized:
              _mainScreen.ShowPressSpaceToStart();
              // TODO: Write hint to the user to hit space to start with the game
              // Read a key from input and decide what todo;
              ConsoleKeyInfo input = Console.ReadKey(true);
              switch (input.Key) {
                case ConsoleKey.Spacebar: {
                  _mainScreen.RefreshGameSection();
                  _engine.Start();
                  break;
                }
              }
              break;
            case GameEngineState.WaitingForMove:
              // Read a key from input and decide what todo;
              input = Console.ReadKey(true);
              switch (input.Key) {
                case ConsoleKey.UpArrow:
                  _engine.MoveUp();
                  break;
                case ConsoleKey.DownArrow:
                  _engine.MoveDown();
                  break;
                case ConsoleKey.LeftArrow:
                  _engine.MoveLeft();
                  break;
                case ConsoleKey.RightArrow:
                  _engine.MoveRight();
                  break;
              }
              break;
            case GameEngineState.WaitingForResolving:

              string currentPosition = _engine.CurrentPosition;
              _mainScreen.IndicateField(currentPosition, Signal.CurrentPosition);
              string solution = _mainScreen.AskForSolution(); 
              solutionResult = _engine.Resolve(solution);
             break;
            case GameEngineState.WaitingForNextLevel:
            case GameEngineState.WaitingForNextRound:
              _mainScreen.RefreshGameSection();
              _engine.StartNextRound();
              break;
            case GameEngineState.GameLost:
              _mainScreen.ShowLostScreen();
              _mainScreen.ShowSolution(solutionResult);
              if (_mainScreen.AskPlayAgain()) {
                _mainScreen.RefreshGameSection();
                _engine.PrepareNextRound();
              }
              else {
                _exitLoop = true;
              }
              break;
            case GameEngineState.GameWon:
              _mainScreen.ShowWinningScreen();
              _mainScreen.ShowSolution(solutionResult);
              if (_engine.NextRoundAvailable) {
                if (_mainScreen.AskPlayAgain()) {
                  _engine.PrepareNextRound();
                }
                else {
                  _exitLoop = true;
                }
              }
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }

          // TODO: update the mainscreen from engine status
          _mainScreen.Moves = _engine.MovesLeft;
          _mainScreen.Level = _engine.Level;
          _mainScreen.Rounds = _engine.Round;
        }
        else {
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
            case ConsoleKey.M: {
              for (int i = Math.Min(_mainScreen.MaxXCells, _mainScreen.MaxYCells) - 1; i >= 0; i--)
                _mainScreen.IndicateField($"{(char)(65 + i)}{i + 1}", Signal.MysticMan);

              int min = Math.Min(_mainScreen.MaxXCells, _mainScreen.MaxYCells);
              for (int i = min - 1; i >= 0; i--)
                _mainScreen.IndicateField($"{(char)(65 + i)}{min - i}", Signal.CurrentPosition);

              break;
            }
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
              _exitLoop = true;
              break;
            case ConsoleKey.C:
              _exitLoop = (input.Modifiers & ConsoleModifiers.Control) != 0;
              break;
          }
        }
      } while (!_exitLoop);
    }

    private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e) {
      _exitLoop = true;
      e.Cancel = false;
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
