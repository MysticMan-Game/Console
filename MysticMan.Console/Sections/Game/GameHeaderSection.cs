using System;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game{
  public class GameHeaderSection : HeaderSection {
    private StringField _infoLineOneField;
    private StringField _infoLineTwoField;
    private StringField _infoLineThreeField;

    /// <inheritdoc />
    public GameHeaderSection(IScreenWriter screenWriter, IScreenInfo screenInfo) : base(screenWriter, screenInfo){
    }

    /// <inheritdoc />
    protected override void OnInitialize() {
      base.OnInitialize();

      _infoLineOneField = CreateField<StringField>(2, Top + 4, 50);
      _infoLineOneField.ForeGround = ConsoleColor.Green;
      _infoLineTwoField = CreateField<StringField>(2, Top + 5, 50);
      _infoLineThreeField = CreateField<StringField>(2, Top + 6, 50);

    }

    public string InfoLineOne {
      get => _infoLineOneField.Value;
      set => _infoLineOneField.Value = value;
    }

    public string InfoLineTwo {
      get => _infoLineTwoField.Value;
      set => _infoLineTwoField.Value = value;
    }

    public string InfoLineThree {
      get => _infoLineThreeField.Value;
      set => _infoLineThreeField.Value = value;
    }
  }
}
