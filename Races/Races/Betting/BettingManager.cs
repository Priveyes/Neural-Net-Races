using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Races.Betting
{
    /// <summary>
    /// Betting Manager, Handles the Race History Data - Both Training Data and Test Data
    /// </summary>
    class BettingManager
    {

        private int[,] i = new int[4,8];



        private double[][] TrainingRaceHistory = new double[4][]; // 2d Jagged Arrays, Training and test data.
        public double[][] _TrainingRaceHistory { get { return TrainingRaceHistory; } set { TrainingRaceHistory = value; } }

        private double[][] TestRaceHistory = new double[4][];

        public double[][] _TestRaceHistory { get { return TestRaceHistory; } set { TestRaceHistory = value; } }

        /// <summary>
        /// BettingManager constructor
        /// </summary>
        /// <param name="_i">The number of races to go through</param>
        public BettingManager(int _i)
        {

            int x = 0;

            int tr = _i / 4 * 3;

            int te = _i / 4;

            while (x < 4)
            {
                TrainingRaceHistory[x] = new double[tr];
                TestRaceHistory[x] = new double[te];
                x++;
            }
        }

        //Keeps track of bets made

        //Manages payouts

        //Records "historical" race data


        public void generateOdds()
        {
                //Look at current horses and generate odds for a winner of each race
        }


        public void payout()
        {
                //Pays out to a player if they betted correctly
        }



    }
}
