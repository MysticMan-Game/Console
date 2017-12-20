using System;
using System.Collections.Generic;
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
        InfoLineOne = "Use the NumPad to do a move. ",
        InfoLineTwo = "After you finished ur moves try to find \nthe Cell where the MysticMan started.",
      };

        //_engine = new TestEngine();
        _engine = new GameEngine();
        _engine.Initialize();
        _engine.WallReachedEvent += (sender, args) => WallSound();



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
              _mainScreen.SetGameSection(GetCurrentGameSection(_engine.MapSize));
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
                case ConsoleKey.F12:
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
              // Redraw screen, maybee the MapSize has changed
              _engine.StartNextRound();
              _mainScreen.SetGameSection(GetCurrentGameSection(_engine.MapSize));
              _mainScreen.RefreshGameSection();
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
      } while (!_exitLoop);
    }

    private GameSection GetCurrentGameSection(MapSize mapSize) {
      int size = mapSize.Width + mapSize.Height;
      switch (size) {
        case 10:
          return GameSection.Small;
        case 16:
          return GameSection.Medium;
        case 20:
          return GameSection.Large;
        case 30:
          return GameSection.XLarge;
      }
      return GameSection.Small;
    }

    private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e) {
      _exitLoop = true;
      e.Cancel = false;
    }

    private static void WallSound() {
      Console.Beep(440, 600);
    }
  }
}
