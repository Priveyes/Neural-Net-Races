using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Races.Tools;

namespace Races.Race
{
    class Horse
    {
        public string name;

        //Position x == Horses "Lane"
        private int posx;
        //Position y == How far along the track they are
        private int posy = 1;

        public int _posy { get { return posy; } set { posy = value; } }
        public int _posx { get { return posx; } set { posy = value; } }

        private bool racing = false;

        public bool _racing { get { return racing; } set { racing = value; } }
        
        //Keep a running record of the horses wins
        public int wins = 0;

        //The age of the horse (randomised) - Added to after every race (Unrealistic but easier to demonstrate neural net)Da
        public int age;

        //Speed is better for hard ground
        private int speed = 0;

        public int _speed { get { return speed; } }

        //Strength is better for soft ground
        private int strength = 0;

        public int _strength { get { return speed; } }

        private int totalRaces = 0;

        public int _totalRaces { get { return totalRaces; } set { totalRaces = value; } }

        private int totalMoves = 0;

        public int _totalMoves { get { return totalMoves; } }

        private int movesPerRace = 0;

        public int _movesPerRace { get { return movesPerRace; } }

        private int previousRaceMoves = 0;

        public int _previousRaceMoves { get { return previousRaceMoves; } set { previousRaceMoves = value; } }

        /// <summary>
        /// Constructor for Horse
        /// </summary>
        /// <param name="_position">The position of the horse on the track</param>
        /// <param name="_name">The name of the horse</param>
        public Horse(int _position, string _name)
        {
            //Randomise Age, speed and strength when we make a horse
            posx = _position;

            name = _name;

            age = Randomiser.rand.Next(1, 5);

            speed = Randomiser.rand.Next(1, 5);

            strength = Randomiser.rand.Next(1, 5);
        }

        public void AvgMoves(int _movesThisRace)
        {
            totalMoves += _movesThisRace;
            movesPerRace = totalMoves / totalRaces;
        }
    }
}
