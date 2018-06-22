using Races.AI.NeuralNetwork;
using Races.Betting;
using Races.DisplayManager;
using Races.Race;
using System;

namespace Races.MenuSettings
{
    class MainMenu
    {
        public void InitialMenu()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Would you like to run the neural network independently, or within the test bed?");
            Console.WriteLine("1: Independent");
            Console.WriteLine("2: Test Bed");

            bool IsValid = false;

            while (IsValid == false)
            {

                string input = Console.ReadLine();
                if (Tools.Validation.validMenuFormat12(input) == true)
                {
                    IsValid = true;
                    char c = input.ToCharArray()[0];
                    switch (c)
                    {
                        case '1':
                            RunNeuralNet();
                            break;
                        case '2':
                            RunTestBed();
                            break;
                    }
                }
            }
        }

        private void RunNeuralNet()
        {
            bool run = true;
            while (run == true)
            {
                bool IsValid = false;
                int InputLayerSize = 0;
                int NumHiddenLayers = 0;
                char c1 = '0';
                Console.Clear();
                Console.WriteLine("Basic Neural Network, or Multilayer? 1/2");

                while (IsValid == false)
                {
                    string input = Console.ReadLine();
                    if (Tools.Validation.validMenuFormat12(input) == true)
                    {
                        IsValid = true;
                        c1 = char.Parse(input);
                        Console.Clear();
                        Console.WriteLine("How many Inputs?");
                        InputLayerSize = int.Parse(Console.ReadLine());

                        if (c1 == '2')
                        {
                            Console.WriteLine("How many Hidden Layers?");
                            NumHiddenLayers = int.Parse(Console.ReadLine());
                        }
                        else if (c1 == '1')
                        {
                            NumHiddenLayers = 0;
                        }
                    }
                }

                Console.WriteLine("How many Hidden Neurons per Layer?");
                int HiddenLayerSize = int.Parse(Console.ReadLine());

                Console.WriteLine("How many Outputs?");
                int OutputLayerSize = int.Parse(Console.ReadLine());

                Console.WriteLine("Display Weights at end? Y/N");

                char c2 = Console.ReadKey().KeyChar;

                Console.Clear();

                Display d = new Display();

                switch (c1)
                {
                    case '1':
                        Standard(InputLayerSize, HiddenLayerSize, OutputLayerSize, c2, d);
                        break;
                    case '2':
                        Experimental(InputLayerSize, NumHiddenLayers, HiddenLayerSize, OutputLayerSize, c2, d);
                        break;
                }

                Console.WriteLine(" ");
                Console.WriteLine("Quit? Y/N");
                char q = Console.ReadKey().KeyChar;

                switch (q)
                {
                    case 'y':
                        run = false;
                        break;
                    case 'n':
                        run = true;
                        break;
                }
            }

            Console.ReadKey();


            void Standard(int _InputLayerSize, int _HiddenLayerSize, int _OutputLayerSize, char _c2, Display _d)
            {
                SingleHiddenLayerNN nn = new SingleHiddenLayerNN(_InputLayerSize, _HiddenLayerSize, _OutputLayerSize);
                Console.ReadKey();
                if (_c2 == 'y')
                {
                    _d.StandardNN(nn._inputNeurons, nn._hiddenNeurons);
                }
            }

            void Experimental(int _InputLayerSize, int _NumHiddenLayers, int _HiddenLayerSize, int _OutputLayerSize, char _c2, Display _d)
            {
                bool IsValid;
                double[] inputs = new double[_InputLayerSize];
                for (int i = 0; i < _InputLayerSize; i++)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter input values: " + (i + 1));
                    inputs[i] = double.Parse(Console.ReadLine());
                }


                MultilayerNeuralNet Enn = new MultilayerNeuralNet(_InputLayerSize, _NumHiddenLayers, _HiddenLayerSize, _OutputLayerSize);

                Enn.StartNN(inputs);

                double[] outputValues = Enn._FinalOutPut;

                int j = 1;

                foreach (double value in outputValues)
                {
                    Console.Clear();
                    foreach(double ivalue in inputs)
                    {
                        Console.WriteLine("Input: " + ivalue);
                    }
                    Console.WriteLine("Final Output " + j + ": " + (value * 100));
                    j++;
                }

                Console.WriteLine("Train? Y/N");
                IsValid = false;
                while (IsValid == false)
                {
                    string input = Console.ReadLine();

                    if (Tools.Validation.validMenuFormatYN(input) == true)
                    {
                        IsValid = true;
                        char c = char.Parse(input);
                        switch (c)
                        {
                            case 'y':
                            case 'Y':
                                Console.WriteLine("How many training iterations?");
                                int t = int.Parse(Console.ReadLine());
                                Console.WriteLine("What was the target value?");
                                int target = int.Parse(Console.ReadLine());
                                for (int tr = 0; tr < t; tr++)
                                {
                                    Enn.Train(target);
                                    Enn.StartNN(inputs);
                                    outputValues = Enn._FinalOutPut;
                                    int i = 1;
                                    foreach (double value in outputValues)
                                    {
                                        foreach (double ivalue in inputs)
                                        {
                                            Console.WriteLine("Input: " + ivalue);
                                        }
                                        Console.WriteLine("Final Output " + i + ": " + (value*100));
                                        j++;
                                    }
                                }
                                break;
                            case 'n':
                            case 'N':
                                break;
                        }
                    }
                }
                Console.ReadKey();
                if (_c2 == 'y')
                {
                    _d.ExperimentalNN(Enn._Neurons);
                }
            }
        }

        private void RunTestBed()
        {
            Console.Clear();
        //Validation Boolean
        bool IsValid = false;

            IsValid = false;
            //Ask how many races (iterations) we want to run

            Console.WriteLine("How many races? They will be run iteratively with a pause at the end of each race");

            //Make a little bit of maths to get a 75/25 split
            while (IsValid == false)
            {
                string s = Console.ReadLine();
                if (Tools.Validation.validRaceMenuFormat(s) == true)
                {
                    int i = Convert.ToInt32(s);
                    IsValid = true;
                    RaceTime(i);
                }
                else
                {
                    Console.WriteLine("Please enter a value below 50");
                }
            }
        }

        private void RaceTime(int _i)
        { 
            BettingManager bM = new BettingManager(_i);

            Track t = new Track(_i, bM);
            
            bool Training = true;

            int tr = _i;

            int r = 0;

            while (r < tr)
            {
                //Console.WriteLine("Generating Training Data");
                t.race(r, Training);
                r++;
            }
            Console.ReadKey();
        }
        }
    }
