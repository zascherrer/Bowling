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
                    if (scoreRaw[i + 2] != ' ')
                    {
                        doubleScoreNextRound = false;
                        doubleScoreTwoRoundsAway = false;
                    }

                    if (doubleScoreNextRound)
                    {
                        score += thisRoundScore * 2;

                        doubleScoreNextRound = false;
                    }
                    else if(!doubleScoreNextRound && doubleScoreTwoRoundsAway)
                    {
                        score += thisRoundScore * 2;

                        doubleScoreTwoRoundsAway = false;
                    }
                    else
                    {
                        score += thisRoundScore;
                    }
                }
                else
                {
                    if(scoreRaw[i] == '/')
                    {
                        if(scoreRaw[i+1] != ' ')
                        {
                            doubleScoreNextRound = false;
                            doubleScoreTwoRoundsAway = false;
                        }
                        
                        if (!doubleScoreNextRound && doubleScoreTwoRoundsAway)
                        {
                            score += (10 - int.Parse(scoreRaw[i - 1].ToString())) * 2;

                            doubleScoreTwoRoundsAway = false;
                        }
                        else
                        {
                            score += 10 - int.Parse(scoreRaw[i - 1].ToString());
                        }

                        doubleScoreNextRound = true;
                    }
                    else if(scoreRaw[i] == 'X')
                    {
                        if(scoreRaw[i+1] != ' ')
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
                    }
                }
                
            }

            return score;
        }
    }
}
