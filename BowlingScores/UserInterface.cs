using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScores
{
    public static class UserInterface
    {


        public static void RunProgram()
        {
            List<string> scoresRaw = new List<string>();
            List<int> scores = new List<int>();

            scoresRaw = FileReader.ReadFile();
            scores = StringConverter.ConvertScoresToInt(scoresRaw);

            for(int i = 0; i < scores.Count; i++)
            {
                Console.WriteLine(scores[i]);
            }

            FileWriter.WriteScoresToFile(scores);

            Console.ReadLine();
        }
    }
}
