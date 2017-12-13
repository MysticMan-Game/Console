using System;
using MysticMan.ConsoleApp.Sections.Game;

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

    public void ShowMysticMan(string field) {
      _gameSection.ShowMysticMan(field);
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
  }

  public enum GameSection {
    Small,
    Medium,
    Large,
    XLarge
  }
}
