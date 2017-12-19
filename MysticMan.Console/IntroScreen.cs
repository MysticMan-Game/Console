using MysticMan.ConsoleApp.Sections.Intro;

namespace MysticMan.ConsoleApp {
  public class IntroScreen {
    private readonly IntroHeaderSection _headerSection;

    public IntroScreen(string title, IScreenWriter screenWriter, IScreenReader screenReader, IScreenInfo screenInfo) {
      screenWriter.Title = title;
      _headerSection = new IntroHeaderSection(screenWriter, screenInfo, screenReader);
      _headerSection.Initialize();
    }

    public string PlayerName => _headerSection.PlayerName;

    public void Run() {
      _headerSection.Draw();
    }
  }
}
