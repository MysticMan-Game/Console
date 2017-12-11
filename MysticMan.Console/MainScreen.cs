using System;
using System.Diagnostics;
using System.Linq;
using MysticMan.ConsoleApp.Sections.Game;

namespace MysticMan.ConsoleApp {
  public class MainScreen {
    private readonly IScreenWriter _screenWriter;
    private readonly GameHeaderSection _headerSection;
    private readonly GameSectionBase _gameSection;

    public MainScreen(string title, IScreenWriter screenWriter, IScreenInfo screenInfo) {
      _screenWriter = screenWriter;
      _screenWriter.Title = title;

      _headerSection = new GameHeaderSection(_screenWriter, screenInfo);
      _headerSection.Initialize();

      _gameSection = new MediumGameSection(_screenWriter, screenInfo) {
        Top = _headerSection.Bottom + 1
      };
      _gameSection.Initialize();

      if (Console.WindowHeight < _gameSection.Bottom + 5) {
        Console.WindowHeight = _gameSection.Bottom + 5;
      }
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

    public int EndOfScreen  => _gameSection.Bottom;

    public void Run(Action inputLoop) {
      PrintScreen();
      inputLoop();
    }

    private void PrintScreen() {
      _screenWriter.Clear();
      _headerSection.Draw();
      _gameSection.Draw();
    }

    public void ShowMysticMan(string field) {
      _gameSection.ShowMysticMan(field);
    }
  }
}
