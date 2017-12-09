using System;

namespace MysticMan.ConsoleApp{
  public class StringField : Field<string>{
    private readonly ConsoleColor _defaultForeGround;
    public ConsoleColor ForeGround{ get; set; }

    public StringField(){
      ForeGround = _defaultForeGround = Console.ForegroundColor;
    }

    public override void Draw(){
      Console.SetCursorPosition(PosX, PosY);
      Console.ForegroundColor = ForeGround;
      Console.Write(Value?.Substring(0, Math.Min(Value.Length, Length)));
      Console.ForegroundColor = _defaultForeGround;
    }
  }
}