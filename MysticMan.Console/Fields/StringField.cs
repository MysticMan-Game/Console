﻿using System;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp{
  public class StringField : Field<string>{
    public ConsoleColor ForeGround{ get; set; }

    public StringField() {
      ForeGround = Console.ForegroundColor;
    }

    public override void Draw() {
      string value01 = GetValue();
      string value = value01?.Substring(0, Math.Min(value01.Length, Length));
      Write(value);
    }

    protected virtual string GetValue() {
      return Value;
    }

    protected virtual void Write(string value){
      ScreenWriter.Write(value, Left, Top, ForeGround);
    }
  }
}
