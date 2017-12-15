using System;

namespace MysticMan.ConsoleApp{
  public class ConsoleScreenWriter :IScreenWriter{
    private string _title;
    private bool _enabled;
    private readonly ConsoleColor _defaultForegroundColor;
    private ConsoleColor _defaultBackgroundColor;

    public ConsoleScreenWriter(bool enabled = true) {
      _defaultForegroundColor = Console.ForegroundColor;
      _defaultBackgroundColor = Console.BackgroundColor;
      _enabled = enabled;
    }
    /// <inheritdoc />
    public bool Enabled {
      get => _enabled;
      set {
        _enabled = value;
        SetTitle();
      }
    }
    
    /// <inheritdoc />
    public string Title {
      get => _title;
      set {
        _title = value;
        SetTitle();
      }
    }

    private void SetTitle(){
      if (Enabled){
        Console.Title = _title;
      }
    }

    /// <inheritdoc />
    public void Clear() {
      if (Enabled) {
        Console.Clear();
      }
    }

    /// <inheritdoc />
    public void Write(string msg, int left, int top) {
      if (Enabled) {
        Console.SetCursorPosition(left, top);
        Console.Write(msg);
      }
    }

    /// <inheritdoc />
    public void Write(char c, int left, int top) {
      if (Enabled) {
        Console.SetCursorPosition(left, top);
        Console.Write(c);
      }
    }

    /// <inheritdoc />
    public void Write(string value, int left, int top, ConsoleColor foreGround) {
      if (Enabled) {
        Console.SetCursorPosition(left, top);
        Console.ForegroundColor = foreGround;
        Console.Write(value);
        Console.ForegroundColor = _defaultForegroundColor;
      }
    }

    /// <inheritdoc />
    public void Write(string value, int left, int top, ConsoleColor foreGround, ConsoleColor backgroundColor) {
      if (Enabled) {
        Console.SetCursorPosition(left, top);
        Console.ForegroundColor = foreGround;
        Console.BackgroundColor = backgroundColor;
        Console.Write(value);
        Console.ForegroundColor = _defaultForegroundColor;
        Console.BackgroundColor = _defaultBackgroundColor;
      }
    }
  }
}
