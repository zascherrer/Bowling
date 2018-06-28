using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace BowlingScores
{
    public static class FileReader
    {
        static List<string> scores;
        static string path;
        static StreamReader reader;

        static FileReader()
        {
            scores = new List<string>();
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Scores.txt");
            reader = new StreamReader(path);
        }

        public static List<string> ReadFile()
        {
            try
            {
                scores.Add(reader.ReadLine());

                while (scores[scores.Count - 1] != null)
                {
                    Console.WriteLine(scores[scores.Count - 1]);

                    scores.Add(reader.ReadLine());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
            }
            finally
            {
                reader.Close();
            }

            scores.RemoveAt(scores.Count - 1);

            return scores;
        }
    }
}
