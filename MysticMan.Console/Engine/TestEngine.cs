using System;
using System.Text;

namespace MysticMan.ConsoleApp.Engine{
  public class TestEngine : IGameEngine {

    private readonly StringBuilder _moveState = new StringBuilder();
    private int _moveCounter;
    public event EventHandler WallReached;

    /// <inheritdoc />
    public void Initialize() {
      State = GameEngineState.Initialized;
    }

    /// <inheritdoc />
    public void MoveUp() {
      _moveState.Append("^");
      UpdateState();
    }

    /// <inheritdoc />
    public void MoveDown() {
      _moveState.Append("_");
      UpdateState();
    }

    /// <inheritdoc />
    public void MoveLeft() {
      _moveState.Append("<");
      UpdateState();
    }

    /// <inheritdoc />
    public void MoveRight() {
      _moveState.Append(">");
      UpdateState();
    }

    private void UpdateState() {

      if (_moveState.ToString() == "<<<") {
        OnWallReached();
      }
      else {
        _moveCounter--;
      }
      if (State == GameEngineState.WaitingForMove) {
        State = _moveCounter > 0 ? GameEngineState.WaitingForMove : GameEngineState.WaitingForResolving;
      }
    }

    /// <inheritdoc />
    public GameEngineState State { get; private set; }

    /// <inheritdoc />
    public int MovesLeft => _moveCounter;

    /// <inheritdoc />
    public void Start() {
      _moveState.Clear();
      _moveCounter = 5;
      State = GameEngineState.WaitingForMove;
    }

    /// <inheritdoc />
    public void Resolve(string solution) {
      State = GameEngineState.Initialized;
    }

    protected virtual void OnWallReached() {
      WallReached?.Invoke(this, EventArgs.Empty);
    }
  }
}
