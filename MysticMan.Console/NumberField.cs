using System;

namespace MysticMan.ConsoleApp{
  public class NumberField : Field<int>{
    public override void Draw(){
      Console.SetCursorPosition(PosX, PosY);
      string fmt = $"{{0, {Length}}}";
      string msg = string.Format(fmt, Value);
      Console.Write(msg.Substring(0, Math.Min(msg.Length, Length)));
    }
  }
}