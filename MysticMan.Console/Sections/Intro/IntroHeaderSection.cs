using System;
using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Intro {
  public class IntroHeaderSection : HeaderSection {
    private readonly IScreenReader _screenReader;
    private StringInputField _playerNameInputField;
    private NumberRangeInputField _levelInputField;

    /// <inheritdoc />
    public IntroHeaderSection(IScreenWriter screenWriter, IScreenInfo screenInfo, IScreenReader screenReader) : base(screenWriter, screenInfo) {
      _screenReader = screenReader;
    }

    /// <inheritdoc />
    protected override void OnInitialize() {
      base.OnInitialize();
      _playerNameInputField = new StringInputField(_screenReader, ScreenInfo) {
        AutoDraw = false,
        Left = 3,
        Top = 3,
        Length = Console.BufferWidth,
        ScreenWriter = ScreenWriter,
        ForeGround = Console.ForegroundColor,
        Value = "Please enter your name:"
      };
      _levelInputField = new NumberRangeInputField(_screenReader, ScreenInfo) {
        AutoDraw = false,
        Left = 3,
        Top = 4,
        Length = Console.BufferWidth,
        ScreenWriter = ScreenWriter,
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

      AddField(_playerNameInputField);
      AddField(_levelInputField);
    }

    public string PlayerName => _playerNameInputField.Input;
    public int Level => _levelInputField.Input;
  }
}
