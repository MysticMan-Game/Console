using System;
using System.Collections.Generic;
using System.Text;
using MysticMan.Logic;

namespace MysticMan.ConsoleApp.Engine {
  public class TestEngine : IGameEngine {
    private readonly StringBuilder _moveState = new StringBuilder();
    private int _maxLevelsCounter;
    private int _maxMoveCounter;
    private int _maxRoundsCounter;
    private Position _startPosition;
    private Position _currentPosition;

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
        Round = 1;
        Level++;
      }
      UpdateState();
    }

    /// <inheritdoc />
    public void PrepareNextRound() {
      _startPosition = new Position(1, 1);
      _currentPosition = new Position(1, 1);
      UpdateState();
    }

    /// <inheritdoc />
    public void Initialize() {
      State = GameEngineState.Initialized;
      _startPosition =  new Position(1, 1);
      _currentPosition =  new Position(1, 1);
    }

    /// <inheritdoc />
    public void MoveUp() {
      if (Move(MoveDirection.Up)) {
        _moveState.Append("^");
        UpdateState();
      }
    }

    private bool Move(MoveDirection direction) {

      switch (direction) {
        case MoveDirection.Left:
          _currentPosition.Left -= 1;
          break;
        case MoveDirection.Right:
          _currentPosition.Left += 1;
          break;
        case MoveDirection.Up:
          _currentPosition.Top -= 1;
          break;
        case MoveDirection.Down:
          _currentPosition.Top += 1;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
      }
      if (_currentPosition.Left < 0) {
        _currentPosition.Left += 1;
        OnWallReached();
        return false;
      }
      if (_currentPosition.Top < 0) {
        _currentPosition.Top += 1;
        OnWallReached();
        return false;
      }
      return true;
    }

    /// <inheritdoc />
    public void MoveDown() {
      if (Move(MoveDirection.Down)) {
        _moveState.Append("_");

        UpdateState();
      }
    }

    /// <inheritdoc />
    public void MoveLeft() {
      if (Move(MoveDirection.Left)) {
        _moveState.Append("<");

        UpdateState();
      }
    }

    /// <inheritdoc />
    public void MoveRight() {
      if (Move(MoveDirection.Right)) {
        _moveState.Append(">");
        UpdateState();
      }
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
    public string CurrentPosition => BuildPosition( _currentPosition.Left, _currentPosition.Top);

    /// <inheritdoc />
    public void Start() {
      _moveState.Clear();
      _maxMoveCounter = MovesLeft = 5;
      _maxRoundsCounter = 3;
      _maxLevelsCounter = 2;
      Level = 1;
      Round = 1;

      UpdateState();
    }

    /// <inheritdoc />
    public ISolutionResult Resolve(string solution) {
      string startPosition = BuildPosition(_startPosition.Left, _startPosition.Top);

      if (string.Equals(solution, startPosition, StringComparison.InvariantCultureIgnoreCase)) {
        State = GameEngineState.GameWon;
      }
      else {
        State = GameEngineState.GameLost;
      }

      List<string> moves = new List<string>();
      for (int i = 0; i < _moveState.Length; i++) {
        char move = _moveState[i];
        switch (move) {
          case '<':
            moves.Add("left");
            break;
          case '>':
            moves.Add("right");
            break;
          case '^':
            moves.Add("up");
            break;
          case '_':
            moves.Add("down");
            break;
        }
      }
      SolutionResult result = new SolutionResult {
        AnsweredPosition = solution,
        MagicMan = startPosition,
        Moves = moves
      };

      return result;
    }

    private string BuildPosition(int left, int top) => $"{(char)(left + 65)}{top + 1}";

    private void UpdateState() {
      switch (State) {
        case GameEngineState.Initialized:
          State = GameEngineState.WaitingForMove;
          break;
        case GameEngineState.WaitingForMove:
          MovesLeft--;
          if (MovesLeft <= 0) {
            State = GameEngineState.WaitingForResolving;
          }
          break;
        case GameEngineState.WaitingForResolving:
          State = GameEngineState.WaitingForNextRound;
          break;
        case GameEngineState.GameLost:
          // If the player is here then he decided to repeate his last round
          _moveState.Clear();
          MovesLeft = _maxMoveCounter;
          State = GameEngineState.WaitingForMove;
          break;
        case GameEngineState.GameWon:
          if (RoundsLeft) {
            State = GameEngineState.WaitingForNextRound;
          }
          else if (LevelsLeft) {
            State = GameEngineState.WaitingForNextLevel;
          }
          else {
            // TODO: implement what happens if the player succeeds all levels :-/
            State = GameEngineState.Initialized;
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

  public enum MoveDirection {
    Left,
    Right,
    Up,
    Down
  }

  public class SolutionResult : ISolutionResult {
    public IEnumerable<string> Moves { get; set; }

    public string MagicMan { get; set; }

    public string AnsweredPosition { get; set; }
  }
}
