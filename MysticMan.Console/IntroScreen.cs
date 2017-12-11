using System;
using System.Collections.Generic;
using System.Linq;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp{
  public class IntroScreen {
    private readonly HeaderPanel _headerPanel;
    private readonly StringInputField _playerNameInputField;
    private readonly NumberRangeInputField _levelInputField;
    private readonly List<FieldBase> _fields = new List<FieldBase>();

    public IntroScreen(string title, IScreenWriter screenWriter, IScreenReader screenReader) {
      screenWriter.Title = title;
      _headerPanel = new HeaderPanel(screenWriter) {
        Position = new Position(0, 0),
        Size = new Size(Console.BufferWidth, 5)
      };
      _playerNameInputField = new StringInputField(screenReader) {
        AutoDraw = true,
        Left = 3,
        Top = 3,
        Length = Console.BufferWidth,
        ScreenWriter = screenWriter,
        ForeGround = Console.ForegroundColor,
        Value = "Please enter your name:"
      };
      _levelInputField = new NumberRangeInputField(screenReader) {
        AutoDraw = true,
        Left = 3,
        Top = 4,
        Length = Console.BufferWidth,
        ScreenWriter = screenWriter,
        ForeGround = Console.ForegroundColor,
        AllowedValues = new List<int> {
          1,
          2,
          3,
          4,
          5,
          6
        },
        Value = "Please enter the level you want to start:"
      };

      _fields.Add(_playerNameInputField);
      _fields.Add(_levelInputField);
    }

    public string PlayerName => _playerNameInputField.Input;
    public int Level => _levelInputField.Input;

    public void Run() {
      _headerPanel.Draw();
      foreach (FieldBase field in _fields.OrderBy(f => f.Top)) {
        Type fieldtype = field.GetType();
        if (fieldtype.IsSubclassOf(typeof(InputFieldBase))) {
          ((InputFieldBase)field).Read();
        }
        else {
          field.Draw();
        }
      }
    }
  }
}