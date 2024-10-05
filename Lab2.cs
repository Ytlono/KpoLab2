using System;
using System.IO;

namespace Lab2
{
    internal class Lab2
    {
        private const int _fileCount = 3;

        static void Main(string[] args)
        {
            int size;

            for (int i = 1; i <= _fileCount; i++)
            {
                string fileIn = $"{i}.ChaseData.txt";
                string fileOut = $"{i}.PursuitLog.txt";

                char[] fileInChar = fileIn.ToCharArray();
                fileInChar[0] = i.ToString()[0];
                string newFileIn = new string(fileInChar);

                char[] fileOutChar = fileOut.ToCharArray();
                fileOutChar[0] = i.ToString()[0];
                string newFileOut = new string(fileOutChar);

                using (StreamReader reader = new StreamReader(newFileIn))
                {
                    string line = reader.ReadLine();
                    size = int.Parse(line);
                }

                Console.WriteLine(newFileIn);
                Console.WriteLine(size);

                Game game = new Game(size, newFileIn, newFileOut);
                game.Run();
            }
        }
    }
}
