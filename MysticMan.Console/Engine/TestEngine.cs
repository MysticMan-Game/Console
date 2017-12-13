using System;
using System.Text;

namespace MysticMan.ConsoleApp.Engine {
  public class TestEngine : IGameEngine {
    private readonly StringBuilder _moveState = new StringBuilder();
    private int _maxLevelsCounter;
    private int _maxMoveCounter;
    private int _maxRoundsCounter;

    private bool RoundsLeft => Round < _maxRoundsCounter;
    private bool LevelsLeft => Level < _maxLevelsCounter;
    public event EventHandler WallReached;

    /// <inheritdoc />
    public void StartNextRound() {
      MovesLeft = _maxMoveCounter;
      _moveState.Clear();
      if (State == GameEngineState.WaitingForNextRound) {
        Round++;
      }
      else if (State == GameEngineState.WaitingForNextLevel) {
        Round = 0;
        Level++;
      }
      UpdateState();
    }

    /// <inheritdoc />
    public void PrepareNextRound() {
      UpdateState();
    }

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

    /// <inheritdoc />
    public GameEngineState State { get; private set; }

    /// <inheritdoc />
    public int MovesLeft { get; private set; }

    /// <inheritdoc />
    public bool NextRoundAvailable => true;

    /// <inheritdoc />
    public int Level { get; private set; }

    /// <inheritdoc />
    public int Round { get; private set; }

    /// <inheritdoc />
    public void Start() {
      _moveState.Clear();
      _maxMoveCounter = MovesLeft = 5;
      _maxRoundsCounter = 3;
      _maxLevelsCounter = 2;
      Level = 0;
      Round = 0;

      UpdateState();
    }

    /// <inheritdoc />
    public void Resolve(string solution) {
      UpdateState();
    }

    private void UpdateState() {

      switch (State) {
        case GameEngineState.Initialized:
          State = GameEngineState.WaitingForMove;
          break;
        case GameEngineState.WaitingForMove:
          if (_moveState.ToString() == "<<<") {
            OnWallReached();
          }
          else if (_moveState.ToString() == "^>_<") {
            State = GameEngineState.GameLost;
          }
          else if (_moveState.ToString() == "><><") {
            State = GameEngineState.GameWon;
          }
          else {
            MovesLeft--;
          }
          if (State == GameEngineState.WaitingForMove) {
            State = MovesLeft > 0 ? GameEngineState.WaitingForMove : GameEngineState.WaitingForResolving;
          }
          break;
        case GameEngineState.WaitingForResolving:
          State = GameEngineState.WaitingForNextRound;
          break;
        case GameEngineState.GameLost:
          break;
        case GameEngineState.GameWon:
          if (RoundsLeft) {
            State = GameEngineState.WaitingForNextRound;
          }
          else if (LevelsLeft) {
            State = GameEngineState.WaitingForNextLevel;
          }
          break;
        case GameEngineState.WaitingForNextRound:
          State = GameEngineState.WaitingForMove;
          break;
        case GameEngineState.WaitingForNextLevel:
          State = GameEngineState.WaitingForMove;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    protected virtual void OnWallReached() {
      WallReached?.Invoke(this, EventArgs.Empty);
    }
  }
}
