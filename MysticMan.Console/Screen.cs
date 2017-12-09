using System;
using System.Collections.Generic;
using System.Linq;

namespace MysticMan.ConsoleApp {
  public class Screen {
    private readonly List<FieldBase> _fields = new List<FieldBase>();
    private readonly NumberField _movesField;
    private readonly NumberField _roundField;
    private readonly NumberField _levelField;
    private readonly NumberField _timerField;
    private readonly StringField _infoLineOneField;
    private readonly StringField _infoLineTwoField;
    private readonly Header _header;
    private readonly GameField _gameField;

    public Screen(string title) {
      Console.Title = title;
      _header = new Header {
        Position = new Position(0, 0),
        Size = new Size(55, 7)
      };

      _gameField = new GameField {
        Position = new Position(_header.Bottom + 1, 0),
        Size = new Size(55, 25)
      };
      _movesField = CreateField<NumberField>(17, _header.Bottom + 23, 3);
      _roundField = CreateField<NumberField>(45, _header.Bottom + 23, 3);
      _levelField = CreateField<NumberField>(51, _header.Bottom + 23, 3);
      _timerField = CreateField<NumberField>(17, _header.Bottom + 24, 3);


      _infoLineOneField = CreateField<StringField>(2, _header.Top + 4, 50);
      _infoLineOneField.ForeGround = ConsoleColor.Green;
      _infoLineTwoField = CreateField<StringField>(2, _header.Top + 5, 50);

      if (Console.WindowHeight < _gameField.Bottom + 5) {
        Console.WindowHeight = _gameField.Bottom + 5;
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

    public int EndOfScreen  => _gameField.Bottom;
    

    public void Draw() {
      PrintScreen();
      foreach (FieldBase field in _fields) {
        field.Draw();
      }
    }

    private TField CreateField<TField>(int x, int y, int length) where TField : FieldBase, new() {
      TField field = new TField {
        PosX = x,
        PosY = y,
        Length = length,
        AutoDraw = true
      };
      _fields.Add(field);
      return field;
    }

    private void PrintScreen() {
      Console.Clear();
      _header.Draw();
      _gameField.Draw();
    }
  }
}
