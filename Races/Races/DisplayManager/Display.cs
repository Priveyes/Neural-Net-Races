using System;
using System.Collections.Generic;
using System.Linq;
using Races.AI.NeuronTypes;

namespace Races.DisplayManager
{
    class Display
    {
        /// <summary>
        /// Initialise the display
        /// </summary>
        /// <param name="_track">Pass the track through to the display method to be initialised</param>
        public void Initialise(char[,] _track)
        {
            for (int y = 0; y < 22; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    if (y == 0 || y == 21)
                    {
                        //Upper and lower borders
                        _track[y, x] = '-';
                    }
                    else if (x == 0 || x == 5)
                    {
                        //Side borders
                        _track[y, x] = '|';
                    }
                    else
                    {
                        //Everything inside is blank
                        _track[y, x] = ' ';
                    }
                }
            }
            Update(_track);
        }

        /// <summary>
        /// Update method for the display
        /// </summary>
        /// <param name="_track">The race track to be displayed</param>
        public void Update(char[,] _track)
        {
            //Clear whatever is currently on screen
            Console.Clear();


            //Write out the track to the screen
            for (int y = 0; y < 22; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    Console.Write(" " + _track[y, x] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Method for displaying the weights of the Standard, Single Hidden Layer, Neural Network
        /// </summary>
        /// <param name="_inputNeurons"></param>
        /// <param name="_hiddenNeurons"></param>
        public void StandardNN(List<InputNeuron> _inputNeurons, List<HiddenNeuron> _hiddenNeurons)
        {
            Console.Clear();
            Console.Write("Inputs: ");

            foreach (InputNeuron inpN in _inputNeurons)
            {
                Console.Write(inpN._inputValue + ", ");
            }

            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.WriteLine("Input Neurons Synapse Weights");
            int inp = 1;
            foreach (InputNeuron inpN in _inputNeurons)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Neuron " + inp);
                for (int i = 0; i < inpN._weights.Count(); i++)
                {
                    Console.WriteLine("Synapse " + (i + 1) + ": " + inpN._weights[i] + " ");
                }
                Console.WriteLine(" ");
                inp++;
            }

            int hid = 1;
            Console.WriteLine(" ");
            Console.WriteLine("Hidden Neurons Synapse Weights");
            foreach (HiddenNeuron hidN in _hiddenNeurons)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Neuron " + hid);
                for (int i = 0; i < hidN._weights.Count(); i++)
                {
                    Console.Write("Synapse " + (i + 1) + ": " + hidN._weights[i] + " ");
                }
                Console.WriteLine(" ");
                hid++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Neurons"></param>
        public void ExperimentalNN(Neuron[][] _Neurons)
        {
            Console.Clear();
            Console.Write("Inputs: ");

            foreach (InputNeuron inpN in _Neurons[0])
            {
                Console.Write((inpN._inputValue * 10) + ", ");
            }

            Console.WriteLine();

            Console.WriteLine("Input Neurons Synapse Weights");
            int inp = 1;
            foreach (InputNeuron inpN in _Neurons[0])
            {
                Console.WriteLine(" ");
                Console.WriteLine("Neuron " + inp);
                for (int i = 0; i < inpN._weights.Count(); i++)
                {
                    Console.WriteLine("Synapse " + (i + 1) + ": " + inpN._weights[i] + " ");
                }

                Console.WriteLine(" ");
                inp++;
            }

            for (int h = 0; h < _Neurons.Count() - 2; h++)
            {
                int hid = 1;
                Console.WriteLine(" ");
                Console.WriteLine("Hidden Neurons Synapse Weights");
                foreach (HiddenNeuron hidN in _Neurons[h + 1])
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Neuron " + hid);
                    for (int i = 0; i < hidN._weights.Count(); i++)
                    {
                        Console.WriteLine("Synapse " + (i + 1) + ": " + hidN._weights[i] + " ");
                    }
                    Console.WriteLine(" ");
                    hid++;
                }

            }
        }
    }
}
