using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScores
{
    public static class StringConverter
    {
        static bool doubleScoreNextRound;
        static bool doubleScoreTwoRoundsAway;

        static StringConverter()
        {
            doubleScoreNextRound = false;
            doubleScoreTwoRoundsAway = false;

        }

        public static List<int> ConvertScoresToInt(List<string> scoresRaw)
        {
            List<int> scores = new List<int>();

            for(int i = 0; i < scoresRaw.Count; i++)
            {
                scores.Add(GetScorePerRound(scoresRaw[i]));
            }

            return scores;
        }

        private static int GetScorePerRound(string scoreRaw)
        {
            int score = 0;
            int thisRoundScore;
            bool charIsNumber = false;

            for(int i = 0; i < scoreRaw.Length; i++)
            {
                charIsNumber = int.TryParse(scoreRaw[i].ToString(), out thisRoundScore);

                if (charIsNumber)
                {
                    score += AddNumberToScore(scoreRaw, i, thisRoundScore);
                }
                else
                {
                    if(scoreRaw[i] == '/')
                    {
                        score += AddSpareToScore(scoreRaw, i);
                    }
                    else if(scoreRaw[i] == 'X')
                    {
                        score += AddStrikeToScore(scoreRaw, i);
                    }
                }
                
            }

            return score;
        }

        private static int AddNumberToScore(string scoreRaw, int i, int thisRoundScore)
        {
            if (scoreRaw[i + 2] != ' ')
            {
                doubleScoreNextRound = false;
                doubleScoreTwoRoundsAway = false;
            }

            if (doubleScoreNextRound)
            {
                doubleScoreNextRound = false;

                return thisRoundScore * 2;
            }
            else if (!doubleScoreNextRound && doubleScoreTwoRoundsAway)
            {
                doubleScoreTwoRoundsAway = false;

                return thisRoundScore * 2;
            }
            else
            {
                return thisRoundScore;
            }
        }

        private static int AddSpareToScore(string scoreRaw, int i)
        {
            if (scoreRaw[i + 1] != ' ')
            {
                doubleScoreNextRound = false;
                doubleScoreTwoRoundsAway = false;
            }

            if (!doubleScoreNextRound && doubleScoreTwoRoundsAway)
            {
                doubleScoreTwoRoundsAway = false;
                doubleScoreNextRound = true;

                return (10 - int.Parse(scoreRaw[i - 1].ToString())) * 2;
            }
            else
            {
                doubleScoreNextRound = true;

                return 10 - int.Parse(scoreRaw[i - 1].ToString());
            }

        }

        private static int AddStrikeToScore(string scoreRaw, int i)
        {
            int score = 0;

            if (scoreRaw[i + 1] != ' ')
            {
                doubleScoreNextRound = false;
                doubleScoreTwoRoundsAway = false;
            }

            if (doubleScoreNextRound)
            {
                score += 10;

                doubleScoreNextRound = false;
            }
            if (!doubleScoreNextRound && doubleScoreTwoRoundsAway)
            {
                score += 10;

                doubleScoreTwoRoundsAway = false;
            }
            score += 10;

            doubleScoreNextRound = true;
            doubleScoreTwoRoundsAway = true;

            return score;
        }
    }
}
