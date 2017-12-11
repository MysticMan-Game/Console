using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp {
  public class MainScreen {
    private readonly IScreenWriter _screenWriter;
    private readonly List<FieldBase> _fields = new List<FieldBase>();
    private readonly NumberField _movesField;
    private readonly NumberField _roundField;
    private readonly NumberField _levelField;
    private readonly NumberField _timerField;
    private readonly StringField _infoLineOneField;
    private readonly StringField _infoLineTwoField;
    private readonly HeaderPanel _headerPanel;
    private readonly GamePanel _gamePanel;

    public MainScreen(string title, IScreenWriter screenWriter) {
      _screenWriter = screenWriter;
      _screenWriter.Title = title;

      _headerPanel = new HeaderPanel(_screenWriter) {
        Position = new Position(0, 0),
        Size = new Size(55, 7)
      };

      _gamePanel = new GamePanel(_screenWriter) {
        Position = new Position(0, _headerPanel.Bottom + 1),
        Size = new Size(55, 25)
      };
      _movesField = CreateField<NumberField>(17, _headerPanel.Bottom + 23, 3);
      _roundField = CreateField<NumberField>(45, _headerPanel.Bottom + 23, 3);
      _levelField = CreateField<NumberField>(51, _headerPanel.Bottom + 23, 3);
      _timerField = CreateField<NumberField>(17, _headerPanel.Bottom + 24, 3);


      _infoLineOneField = CreateField<StringField>(2, _headerPanel.Top + 4, 50);
      _infoLineOneField.ForeGround = ConsoleColor.Green;
      _infoLineTwoField = CreateField<StringField>(2, _headerPanel.Top + 5, 50);

      if (Console.WindowHeight < _gamePanel.Bottom + 5) {
        Console.WindowHeight = _gamePanel.Bottom + 5;
      }
    }

    public int Moves {
      get => _movesField.Value;
      set => _movesField.Value = value;
    }

    public int Rounds {
      get => _roundField.Value;
      set => _roundField.Value = value;
    }

    public int Level {
      get => _levelField.Value;
      set => _levelField.Value = value;
    }

    public int Timer {
      get => _timerField.Value;
      set => _timerField.Value = value;
    }

    public string InfoLineOne {
      get => _infoLineOneField.Value;
      set => _infoLineOneField.Value = value;
    }

    public string InfoLineTwo {
      get => _infoLineTwoField.Value;
      set => _infoLineTwoField.Value = value;
    }

    public int EndOfScreen  => _gamePanel.Bottom;

    public void Run(Action inputLoop) {
      PrintScreen();
      foreach (FieldBase field in _fields) {
        field.Draw();
      }
      inputLoop();
    }

    private TField CreateField<TField>(int x, int y, int length) where TField : FieldBase, new() {
      TField field = new TField {
        Left = x,
        Top = y,
        Length = length,
        AutoDraw = true,
        ScreenWriter = _screenWriter
      };
      _fields.Add(field);
      return field;
    }

    private void PrintScreen() {
      _screenWriter.Clear();
      _headerPanel.Draw();
      _gamePanel.Draw();
    }

    public void ShowMysticMan(string field) {
      _gamePanel.ShowMysticMan(field);
    }
  }
}
