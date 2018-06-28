using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace BowlingScores
{
    public static class FileWriter
    {
        static string path;
        static StreamWriter writer;

        static FileWriter()
        {
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Scores.txt");
            writer = new StreamWriter(path, true);
        }

        public static void WriteScoresToFile(List<int> scores)
        {
            try
            {
                for(int i = 0; i < scores.Count; i++)
                {
                    writer.WriteLine(scores[i]);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
            }
            finally
            {
                writer.Close();
            }
        }
    }
}
