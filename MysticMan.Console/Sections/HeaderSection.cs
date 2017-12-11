namespace MysticMan.ConsoleApp.Sections{
  public class HeaderSection : Section {
    public HeaderSection(IScreenWriter screenWriter, IScreenInfo screenInfo):base(screenWriter, screenInfo) {
      string header = @"                                                       
                                                       
                                                       
                                                       
                                                       
                                                       
                                                       ";
      SetContent(header);
    }
  }
}
