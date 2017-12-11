namespace MysticMan.ConsoleApp{
  public interface IScreenReader {

    string ReadLine();

    string ReadLine(Position position);
  }
}
