namespace Lab2
{
    enum State
    {
        Winner,
        Looser,
        Playing,
        NotInGame
    }

    class Player
    {
        public string name;
        public int location;
        public State state = State.NotInGame;
        public int distanceTraveled = 0;

        public Player(string name)
        {
            this.name = name;
            this.location = -1;
        }

        public void Move(int steps)
        {
            location = (location + steps) % Game.size;

            if (location < 0)
            {
                location += Game.size;
            }
        }
    }
}
