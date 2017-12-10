using System;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp{
  public class StringField : Field<string>{
    public ConsoleColor ForeGround{ get; set; }

    public StringField() {
      ForeGround = Console.ForegroundColor;
    }

    public override void Draw(){
      string value = Value?.Substring(0, Math.Min(Value.Length, Length));
      ScreenWriter.Write(value, Left, Top, ForeGround);
    }
  }
}
