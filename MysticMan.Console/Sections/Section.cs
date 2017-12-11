using System;
using System.Collections.Generic;
using System.Linq;
using MysticMan.ConsoleApp.Fields;

namespace MysticMan.ConsoleApp.Sections {
  public class Section {
    private string _content;
    private readonly List<FieldBase> _fields = new List<FieldBase>();
    private bool _isInitialized;

    protected Section(IScreenWriter screenWriter, IScreenInfo screenInfo) {
      ScreenInfo = screenInfo;
      ScreenWriter = screenWriter;
      Position = new Position(0, 0);
      Size = new Size(screenInfo.Width, screenInfo.Height);
    }

    public Position Position { get; }
    public Size Size { get; }

    public int Top {
      get => Position.Top;
      set => Position.Top = value;
    }

    public int Bottom => Position.Top + Size.Height;

    protected IScreenWriter ScreenWriter { get; }
    public IScreenInfo ScreenInfo { get; }

    public bool IsInitialized => _isInitialized;

    public void Initialize() {
      OnInitialize();

      _isInitialized = true;
    }

    protected virtual void OnInitialize() {
    }

    public void SetContent(string content) {
      _content = content;
      char[,] contentBuffer = CreateStringBuffer(_content);
      Size.Width = contentBuffer.GetLength(0);
      Size.Height = contentBuffer.GetLength(1);
    }

    public void Draw() {
      if (!IsInitialized) {
        throw new InvalidOperationException($"You must initialize the {nameof(Section)} before you can draw it.");
      }
      // Build buffer based on the Size
      char[,] contentBuffer = CreateStringBuffer(_content);
      char[,] buffer = new char[Size.Width, Size.Height];

      WriteBuffer(buffer, contentBuffer);

      foreach (FieldBase field in _fields.OrderBy(f => f.Top)) {
        Type fieldtype = field.GetType();
        if (fieldtype.IsSubclassOf(typeof(InputFieldBase))) {
          ((InputFieldBase)field).Read();
        }
        else {
          field.Draw();
        }
      }
    }

    private void WriteBuffer(char[,] buffer, char[,] contentBuffer){
// Output buffer
      for (int i = 0; i < buffer.GetLength(0); i++){
        for (int j = 0; j < buffer.GetLength(1); j++){
          char c = buffer[i, j];
          if (i < contentBuffer.GetLength(0) && j < contentBuffer.GetLength(1)){
            c = contentBuffer[i, j];
          }
          ScreenWriter.Write(c, Position.Left + i, Position.Top + j);
        }
      }
    }

    private static char[,] CreateStringBuffer(string text) {
      string[] lines = text.Split('\n');
      int maxX = lines.Max(l => l.Length);
      int maxY = lines.Length;

      char[,] buffer = new char[maxX, maxY];
      int x = 0;
      int y = 0;
      foreach (char current in text) {
        if (current == '\r') {
          continue;
        }
        if (current == '\n') {
          y = y + 1;
          x = 0;
        }
        else {
          if (x >= buffer.GetLength(0)) {
            continue;
          }
          if (y >= buffer.GetLength(1)) {
            continue;
          }

          buffer[x, y] = current;
          x = x + 1;
        }
      }
      return buffer;
    }

    protected TField CreateField<TField>(int x, int y, int length) where TField : FieldBase, new() {
      TField field = new TField {
        Left = x,
        Top = y,
        Length = length,
        AutoDraw = true,
        ScreenWriter = ScreenWriter
      };
      _fields.Add(field);
      return field;
    }

    public void AddField(FieldBase field) {
      _fields.Add(field);
    }

    public void Clear() {
      char[,] buffer = new char[Size.Width, Size.Height];
      foreach (FieldBase field in _fields.OrderBy(f => f.Top)) {
        field.AutoDraw = false;
      }
      WriteBuffer(buffer, buffer);
    }

  }
}
