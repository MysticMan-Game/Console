namespace MysticMan.ConsoleApp{
  public interface IScreenInfo {
    int Width { get; set; }
    int Height { get; set; }
    Position CursorPosition { get; }
  }
}
