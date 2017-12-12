﻿using System;
using System.Text;

namespace MysticMan.ConsoleApp.Engine{
  public interface IGameEngine {
    void Initialize();
    void MoveUp();
    void MoveDown();
    void MoveLeft();
    void MoveRight();
    GameEngineState State { get;}
    int MovesLeft { get; }
    void Start();
    void Resolve(string solution);
    event EventHandler WallReached;
  }
}