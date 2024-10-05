using System;
using System.IO;

namespace Lab2
{
    enum GameState
    {
        Start,
        End
    }

    class Game
    {
        public static int size;
        public Player cat;
        public Player mouse;
        public int distance;
        public GameState state;
        public string FileIn;
        public string logFile;
        public StreamReader reader;
        private bool isCatInitialized = false;
        private bool isMouseInitialized = false;

        public Game(int _size, string FileIn, string logFile)
        {
            size = _size;
            this.FileIn = FileIn;
            this.logFile = logFile;
            cat = new Player("Cat");
            mouse = new Player("Mouse");
            reader = new StreamReader(FileIn);
            state = GameState.Start;
        }

        public void Run()
        {
            reader.ReadLine();
            mouse.state = State.Playing;
            cat.state = State.Playing;

            File.WriteAllText(logFile, "Cat and Mouse\n\nCat Mouse  Distance\n-------------------\n");

            while (state != GameState.End)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    if (cat.location != mouse.location)
                    {
                        cat.state = State.Looser;
                    }
                    break;
                }
                Console.WriteLine(line);

                if (line.Equals("P"))
                {
                    DoPrintCommand();
                    continue;
                }

                string[] command = line.Split("\t");

                if (command[0] == "C" && !isCatInitialized)
                {
                    isCatInitialized = true;
                    cat.location = int.Parse(command[1]);
                    continue;
                }

                if (command[0] == "M" && !isMouseInitialized)
                {
                    isMouseInitialized = true;
                    mouse.location = int.Parse(command[1]);
                    continue;
                }

                int steps = int.Parse(command[1]);
                DoMoveCommand(command[0], steps);

                if (command[0] == "M")
                {
                    mouse.distanceTraveled += Math.Abs(steps);
                }
                else if (command[0] == "C")
                {
                    cat.distanceTraveled += Math.Abs(steps);
                }
            }

            File.AppendAllText(logFile, "-------------------\n\n");
            File.AppendAllText(logFile, $"Distance traveled:\tMouse\tCat\n                        {mouse.distanceTraveled}      {cat.distanceTraveled}\n\n");

            if (mouse.state == State.Looser)
            {
                File.AppendAllText(logFile, $"Mouse caught at: {mouse.location}\n");
            }
            if (cat.state == State.Looser) {
                File.AppendAllText(logFile, "Mouse evaded Cat\n");
                File.AppendAllText(logFile, "Cat lost the game.\n");
            }        
        }

        private void DoMoveCommand(string command, int steps)
        {
            switch (command)
            {
                case "M":
                    mouse.Move(steps);
                    break;
                case "C":
                    cat.Move(steps);
                    break;
            }
            distance = GetDistance();

            if (cat.location == mouse.location)
            {
                cat.state = State.Winner;
                mouse.state = State.Looser;
                state = GameState.End;
            }
        }

        private void DoPrintCommand()
        {
            string output = $" {cat.location}\t{mouse.location}\t{distance}\n";

            if (cat.location == -1)
            {
                output = $" ??\t{mouse.location}\n";
            }
            if (mouse.location == -1)
            {
                output = $" {cat.location}\t??\n";
            }

            File.AppendAllText(logFile, output);
        }

        private int GetDistance()
        {
            return Math.Abs(cat.location - mouse.location);
        }
    }
}
