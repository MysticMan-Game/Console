using System;
using System.Linq;
using System.Text.RegularExpressions;
using MysticMan.ConsoleApp.Engine;
using MysticMan.ConsoleApp.Sections.Game;
using MysticMan.Logic;

namespace MysticMan.ConsoleApp {
  public class MainScreen {
    private readonly IScreenWriter _screenWriter;
    private readonly IScreenInfo _screenInfo;
    private readonly GameHeaderSection _headerSection;
    private GameSectionBase _gameSection;
    private readonly IScreenReader _screenReader;

    public MainScreen(string title, IScreenWriter screenWriter, IScreenInfo screenInfo, IScreenReader screenReader) {
      _screenWriter = screenWriter;
      _screenInfo = screenInfo;
      _screenReader = screenReader;
      _screenWriter.Title = title;

      _headerSection = new GameHeaderSection(_screenWriter, screenInfo);
      _headerSection.Initialize();

      SetGameSection(GameSection.Small);
    }

    public int Moves {
      get => _gameSection.Moves;
      set => _gameSection.Moves = value;
    }

    public int Rounds {
      get => _gameSection.Rounds;
      set => _gameSection.Rounds = value;
    }

    public int Level {
      get => _gameSection.Level;
      set => _gameSection.Level = value;
    }

    public int Timer {
      get => _gameSection.Timer;
      set => _gameSection.Timer = value;
    }

    public string InfoLineOne {
      get => _headerSection.InfoLineOne;
      set => _headerSection.InfoLineOne = value;
    }

    public string InfoLineTwo {
      get => _headerSection.InfoLineTwo;
      set => _headerSection.InfoLineTwo = value;
    }

    public int EndOfScreen => _gameSection.Bottom;

    public void Run(Action inputLoop) {
      PrintScreen();
      inputLoop();
    }

    public void SetGameSection(GameSection gameSection) {
      _gameSection?.Clear();

      switch (gameSection) {
        case GameSection.Small:
          _gameSection = new SmallGameSection(_screenWriter, _screenInfo, _screenReader);
          break;
        case GameSection.Medium:
          _gameSection = new MediumGameSection(_screenWriter, _screenInfo, _screenReader);
          break;
        case GameSection.Large:
          _gameSection = new LargeGameSection(_screenWriter, _screenInfo, _screenReader);
          break;
        case GameSection.XLarge:
          _gameSection = new XtraLargeGameSection(_screenWriter, _screenInfo, _screenReader);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(gameSection), gameSection, null);
      }
      _gameSection.Top = _headerSection.Bottom + 1;
      _gameSection.Initialize();
      _gameSection.Draw();
      //if (Console.WindowHeight < _gameSection.Bottom + 5) {
      //  Console.WindowHeight = _gameSection.Bottom + 5;
      //}
    }

    private void PrintScreen() {
      _screenWriter.Clear();
      _headerSection.Draw();
      _gameSection.Draw();
    }

    public void IndicateField(string field, Signal signal) {
      _gameSection.IndicateField(field, signal);
    }

    public int MaxXCells => _gameSection.XCounter;
    public int MaxYCells => _gameSection.YCounter;

    public string AskForSolution() {
      string solution = _gameSection.GetSolution();
      return solution;
    }

    public void ShowPressSpaceToStart() {
      _gameSection.PressEnterScreen();
    }

    public void RefreshGameSection() {
      _gameSection.Refresh();
    }

    public void ShowLostScreen() {
      _gameSection.LostScreen();
    }

    public bool AskPlayAgain() {
      return _gameSection.PlayAgain();
    }

    public void ShowWinningScreen() {
      _gameSection.WinningScreen();
    }

    public void ShowSolution(ISolutionResult solutionResult) {
      IndicateField(solutionResult.AnsweredPosition, Signal.PlayerStart);
      Regex regex = new Regex("(?<char>[A-Za-z])(?<number>[0-9]{1,2})");
      Match match = regex.Match(solutionResult.AnsweredPosition);
      string character = match.Groups["char"].Value;
      string number = match.Groups["number"].Value;
      int left = character[0] - 65;
      int top = int.Parse(number) - 1;
      IndicateField(solutionResult.MagicMan, Signal.MysticMan);

      Func<int, int, string> buildPosition = (left1, top1) => $"{(char)(left1 + 65)}{top1 + 1}";
      for (int i = 0; i < solutionResult.Moves.ToList().Count; i++) {
        string move = solutionResult.Moves.ElementAt(i);
        Signal signal = Signal.Unknown;
        switch (move) {
          case "left":
            signal = Signal.MoveLeft;
            left = Math.Max(left - 1, 0);
            break;
          case "right":
            signal = Signal.MoveRight;
            left = Math.Min(left + 1, 4);
            break;
          case "down":
            signal = Signal.MoveDown;
            top = Math.Min(top + 1, 4);
            break;
          case "up":
            signal = Signal.MoveUp;
            top = Math.Max(top - 1, 0);
            break;
        }
        string position =  buildPosition(left, top);
        if (i == solutionResult.Moves.Count()-1) {
          IndicateField(position, Signal.LastMove);
        }
        else {
          IndicateField(position, signal);
        }
      }
    }
  }
}
