using System;
using System.Collections.Generic;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Intro {
  public class IntroHeaderSection : HeaderSection {
    private readonly IScreenReader _screenReader;
    private StringInputField _playerNameInputField;

    /// <inheritdoc />
    public IntroHeaderSection(IScreenWriter screenWriter, IScreenInfo screenInfo, IScreenReader screenReader) : base(screenWriter, screenInfo) {
      _screenReader = screenReader;
    }

    /// <inheritdoc />
    protected override void OnInitialize() {
      base.OnInitialize();
      _playerNameInputField = new StringInputField(_screenReader, ScreenWriter, ScreenInfo) {
        AutoDraw = false,
        Left = 3,
        Top = 3,
        Length = Console.BufferWidth,
        ForeGround = Console.ForegroundColor,
        Value = "Please enter your name:"
      };

      AddField(_playerNameInputField);
    }

    public string PlayerName => _playerNameInputField.Input;
  }
}
