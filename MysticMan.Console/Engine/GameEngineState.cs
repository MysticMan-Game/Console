namespace MysticMan.ConsoleApp.Engine{
  public enum GameEngineState {

    Initialized,
    WaitingForMove,
    WaitingForResolving,
    GameLost,
    GameWon,
    WaitingForNextRound,
    WaitingForNextLevel
  }
}
