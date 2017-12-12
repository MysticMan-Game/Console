using System;
using System.Collections.Generic;
using System.Linq;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections.Game{
  public abstract class GameSectionBase : Section {
    protected NumberField MovesField { get; set; }
    protected NumberField RoundField { get; set; }
    protected NumberField LevelField { get; set; }
    protected NumberField TimerField { get; set; }
    
    protected StringInputField SolutionInputField{get; set; }

    protected GameSectionBase(IScreenWriter screenWriter, IScreenInfo screenInfo, IScreenReader screenReader) : base(screenWriter, screenInfo) {
      ScreenReader = screenReader;
    }

    public int Moves {
      get => MovesField.Value;
      set => MovesField.Value = value;
    }

    public int Rounds {
      get => RoundField.Value;
      set => RoundField.Value = value;
    }

    public int Level {
      get => LevelField.Value;
      set => LevelField.Value = value;
    }

    public int Timer {
      get => TimerField.Value;
      set => TimerField.Value = value;
    }

    protected abstract IDictionary<string, Position> CellPositions { get; }
    public abstract int XCounter{ get; }
    public abstract int YCounter{ get; }
    public IScreenReader ScreenReader { get; set; }

    public void ShowMysticMan(string field) {
      if (CellPositions.ContainsKey(field)) {
        Position position = CellPositions[field];
        ScreenWriter.Write("X", Position.Left + position.Left, Position.Top + position.Top);
      }
    }

    public string GetSolution() {
      SolutionInputField.Read();
      string solution = SolutionInputField.Input;
      SolutionInputField.Clear();
      return solution;
    }


    public void PressEnterScreen() {
      // http://patorjk.com/software/taag/#p=testall&v=2&f=Small%20Slant&t=PRESS%20SPACE


      string value = @"
   ___  ___  ____________  _______  ___  _________
  / _ \/ _ \/ __/ __/ __/ / __/ _ \/ _ |/ ___/ __/
 / ___/ , _/ _/_\ \_\ \  _\ \/ ___/ __ / /__/ _/  
/_/  /_/|_/___/___/___/_/___/_/_ /_/_|_\___/___/  

     __________    _____________   ___  ______
    /_  __/ __ \  / __/_  __/ _ | / _ \/_  __/
     / / / /_/ / _\ \  / / / __ |/ , _/ / /   
    /_/  \____/ /___/ /_/ /_/ |_/_/|_| /_/    
";

      value = value.Substring(2, value.Length-4);
      int valueHeight = value.Split('\n').Length;
      
      char[,] stringBuffer = CreateStringBuffer(value);
      char[,] buffer = GetContentBuffer();
      int left =  (buffer.GetLength(0) / 2) - (stringBuffer.GetLength(0) / 2);
      left = Math.Max(3, left);
      buffer = buffer.Stretch(Console.BufferWidth, buffer.GetLength(1));
      stringBuffer.CopyTo(buffer, left, (buffer.GetLength(1) - valueHeight)/2);
      WriteBuffer(buffer);
    }
  }


  public static class CharBufferExtensions {

    public static void CopyTo(this char[,] source, char[,] target, int left, int top) {

      for (int i = 0; i < source.GetLength(0); i++) {
        for (int j = 0; j < source.GetLength(1); j++) {
          if (left + i < target.GetLength(0) && top + j < target.GetLength(1)) {
            target[left + i, top + j] = source[i, j];
          }
        }
      }
    }

    public static char[,] Stretch(this char[,] source, int width, int height) {
      int newWidth = Math.Max(source.GetLength(0), width);
      int newHeight = Math.Max(source.GetLength(1), height);
      char[,] buffer = new char[newWidth,newHeight];
      source.CopyTo(buffer, 0, 0);
      return buffer;
    }
  }

}


