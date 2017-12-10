using MysticMan.ConsoleApp.Panels;

namespace MysticMan.ConsoleApp{
  public class HeaderPanel : Panel {
    public HeaderPanel(IScreenWriter screenWriter):base(screenWriter) {
      string header = @"                                                       
                                                       
                                                       
                                                       
                                                       
                                                       
                                                       ";
      SetContent(header);
    }
  }
}
