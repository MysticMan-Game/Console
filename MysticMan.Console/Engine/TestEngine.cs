using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        Round = 1;
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
    public string CurrentPosition => "B2";

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
      string startPosition = CalculateMovesRevers("B2");

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

    private string CalculateMovesRevers(string solution) {
      Regex regex = new Regex("(?<char>[A-Za-z])(?<number>[0-9]{1,2})");
      Match match = regex.Match(solution);
      string character = match.Groups["char"].Value;
      string number = match.Groups["number"].Value;
      int left = character[0] - 65;
      int top = int.Parse(number) - 1;

      Func<int, int, string> buildPosition = (left1, top1) => $"{(char)(left1 + 65)}{top1 + 1}";
      string position = "";
      foreach (char move1 in _moveState.ToString().Reverse()) {
        switch (move1) {
          case '>':
            left = Math.Max(left - 1, 0);
            break;
          case '<':
            left = Math.Min(left + 1, 4);
            break;
          case '_':
            top = Math.Max(top - 1, 0);
            break;
          case '^':
            top = Math.Min(top + 1, 4);
            break;
        }
        position = buildPosition(left, top);
      }
      return position;
    }

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

  public interface ISolutionResult {
    IEnumerable<string> Moves { get; }
    string MagicMan { get; }
    string AnsweredPosition { get; }
  }

  public class SolutionResult : ISolutionResult {
    public IEnumerable<string> Moves { get; set; }

    public string MagicMan { get; set; }

    public string AnsweredPosition { get; set; }
  }
}
