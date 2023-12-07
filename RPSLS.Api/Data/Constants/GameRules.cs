using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Data.Constants
{
    public class GameRules
    {
        public static readonly List<(Move Player, Move Opponent, Result Result)> Rules =
            [
                (Move.Rock, Move.Rock, Result.Draw),
                (Move.Rock, Move.Paper, Result.Lose),
                (Move.Rock, Move.Spock, Result.Lose),
                (Move.Rock, Move.Scissors, Result.Win),
                (Move.Rock, Move.Lizard, Result.Win),

                (Move.Paper, Move.Paper, Result.Draw),
                (Move.Paper, Move.Scissors, Result.Lose),
                (Move.Paper, Move.Lizard, Result.Lose),
                (Move.Paper, Move.Rock, Result.Win),
                (Move.Paper, Move.Spock, Result.Win),

                (Move.Scissors, Move.Scissors, Result.Draw),
                (Move.Scissors, Move.Rock, Result.Lose),
                (Move.Scissors, Move.Spock, Result.Lose),
                (Move.Scissors, Move.Paper, Result.Win),
                (Move.Scissors, Move.Lizard, Result.Win),

                (Move.Spock, Move.Spock, Result.Draw),
                (Move.Spock, Move.Lizard, Result.Lose),
                (Move.Spock, Move.Paper, Result.Lose),
                (Move.Spock, Move.Rock, Result.Win),
                (Move.Spock, Move.Scissors, Result.Win),

                (Move.Lizard, Move.Lizard, Result.Draw),
                (Move.Lizard, Move.Rock, Result.Lose),
                (Move.Lizard, Move.Scissors, Result.Lose),
                (Move.Lizard, Move.Paper, Result.Win),
                (Move.Lizard, Move.Spock, Result.Win),
            ];
    }
}
