using Races.AI.NeuralNetwork;
using Races.Betting;
using Races.DisplayManager;
using System;

namespace Races.Race
{
    class Track
    {
        bool timeToRace; //Boolean to identify if the race is on, or finished

        private char[,] racetrack = new char[22, 6]; //Character array for the track (used by display)
        private int trackLength = 20; //Length of our track.
        public int _trackLength { get { return trackLength; } }

        private int numRacers = 4; //Number of horses in the race (Hard coded for now)
        public int _numRacers { get { return numRacers; } }

        private static int numRaces; //Number of races to take place

        public static int _numRaces { get { return numRaces; } }

        private double[] inputs = new double[3];

        double prediction = 0;

        Horse[] Horses = new Horse[4]; //Array of horses (Change this to be affected by numRacers, which in turn can be set by the user

        BettingManager bM;

        MultilayerNeuralNet Mn = new MultilayerNeuralNet(3, 2, 4, 1);

        /// <summary>
        /// Track Constructor
        /// </summary>
        /// <param name="r">The number of races to take place"</param>
        /// <param name="_bM">An instance of the betting manager from Main, to ensure that training and test race history instances are the same</param>
        public Track(int r, BettingManager _bM)
        {
            bM = _bM;
            numRaces = r;

            //Generate the number of horses required for the race
            for (int i = 0; i < numRacers; i++)
            {
                Horse h = new Horse(i + 1, i.ToString());
                Horses[i] = h;
            }
            
        }
        /// <summary>
        /// Getter for the horse array (May be obsolete)
        /// </summary>
        /// <returns>returns the array of horses</returns>
        public Horse[] getHorses()
        {
            return Horses;
        }
        /// <summary>
        /// Race Method
        /// </summary>
        /// <param name="racenumber">Current race iteration, e.g "Race 12"</param>
        /// <param name="training">Training boolean, Our first race is not input to the neural network and neither is the second (First is to generate some data for the horses, second is the NN's first prediction</param>
        public void race(int racenumber, bool training)
        {
            if ( racenumber > 0)
            {
                if (racenumber == 1)
                {
                    training = false;
                    NeuralNet(training);
                }
                else
                {
                    training = true;
                    NeuralNet(training);
                }
            }
            //Clear the track for a new race
            Array.Clear(racetrack, 0, racetrack.Length);

            //Race time
            timeToRace = true;

            //Instantiate display object and pass it the track
            Display d = new Display();

            d.Initialise(racetrack);

            foreach (Horse h in Horses)
            {
                //Add to total races value
                h._totalRaces++;
                //Set them as running in the race
                h._racing = true;
                //Set all the horses to the start of the track
                h._posy = 1;
                //Update them on the array for display
                racetrack[h._posy, h._posx] = Convert.ToChar(h.name);
                //Update the display
                d.Update(racetrack);
            }

            int numSteps = 0;

            int finishedHorses = 0;

            while (timeToRace != false)
            {
                if (finishedHorses < 4)
                {
                    numSteps++;
                    Console.WriteLine("Step number: " + numSteps);
                    foreach (Horse h in Horses)
                    {
                        //Check if the horse has already reached the end of the track
                        if (h._posy == trackLength - 1 || h._posy > trackLength - 1)
                        {
                            if (h._racing == true)
                            {
                                h._racing = false;

                                Console.WriteLine("Horse: " + h.name + "Finished with " + numSteps + " steps");
                                //If the horse has reached the end of the track, record current "step" value of the race as the number of moves made
                                //Use an AVG moves per race function and record that to a value in the horse class
                                h.AvgMoves(numSteps);
                                h._previousRaceMoves = numSteps;

                                finishedHorses++;
                                if (finishedHorses == 1)
                                {
                                    h.wins++;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //If not then his race continues
                        else if (h._posy < trackLength - 1)
                        {
                            //Replace previous position with a dot
                            racetrack[h._posy, h._posx] = '.';

                            //Little bit of maths and a little bit of randomness
                            int move = h._speed / h.age + Tools.Randomiser.rand.Next(-3, 3);
                            //If we end up with 0, then atleast make the horse move one space
                            if (move < 1)
                            {
                                h._posy = h._posy + 1;
                            }
                            else
                            {
                                h._posy = h._posy + move;
                            }
                            //If the horse won't run off the end of the track
                            if (h._posy < trackLength)
                            {
                                //Set his new position on the display
                                racetrack[h._posy, h._posx] = Convert.ToChar(h.name);
                            }
                            //If the horse will run off the end of the track
                            else if (h._posy >= trackLength)
                            {
                                //Take the difference between his final position and the end of the track
                                //Subtract it from the position to get a new position at the end of the track
                                h._posy = h._posy - (h._posy - trackLength);
                                racetrack[h._posy, h._posx] = Convert.ToChar(h.name);
                            }
                        }
                    }
                        //Update the display
                        d.Update(racetrack);
                }
                else
                {
                    timeToRace = false;
                    Console.WriteLine("Race Finished");

                    Console.WriteLine("Prediction for Horse 0 for this race: " + Math.Round(prediction));
                    foreach(Horse h in Horses)
                    {
                        Console.WriteLine("Horse " + h.name + " Finished in " + h._previousRaceMoves + " steps.");
                    }
                    Console.ReadKey();
                }
            }
        }

        private void NeuralNet(bool _training)
        {
            inputs[0] = Horses[0].age;
            inputs[1] = Horses[0]._speed;
            inputs[2] = Horses[0]._movesPerRace;

            if (_training == true)
            {
                Mn.Train(Horses[0]._previousRaceMoves);
            }

            Mn.StartNN(inputs);

            prediction = Mn._FinalOutPut[0] * 100;

            Console.WriteLine("Prediction for next race is: " + Math.Round(prediction));
            Console.ReadKey();
        }
    }
}
