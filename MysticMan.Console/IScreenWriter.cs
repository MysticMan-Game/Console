using System;

namespace MysticMan.ConsoleApp{
  public interface IScreenWriter {

    bool Enabled { get; set; }

    string Title { get; set; }

    void Clear();

    void Write(string msg, int left, int top);

    void Write(char c, int left, int top);
    void Write(string value, int left, int top, ConsoleColor foreGround);
  }
}
