using Races.AI.NeuronTypes;
using Races.AI.TrainerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Races.AI.NeuralNetwork
{
    class MultilayerNeuralNet
    {
        int InputLayerSize;

        int NumHiddenLayers;

        int HiddenLayerSize;

        int OutputLayerSize;

        //[Layers][Neurons]
        Neuron[][] Neurons;

        public Neuron[][] _Neurons { get { return Neurons; } set { Neurons = value; } }

        double[] FinalOutPut;

        public double[] _FinalOutPut { get { return FinalOutPut; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_inSize"></param>
        /// <param name="_numHLay"></param>
        /// <param name="_hidSize"></param>
        /// <param name="_outSize"></param>
        public MultilayerNeuralNet(int _inSize, int _numHLay, int _hidSize, int _outSize)
        {
            InputLayerSize = _inSize;

            NumHiddenLayers = _numHLay;

            HiddenLayerSize = _hidSize;

            OutputLayerSize = _outSize;


            //Instantiate Jagged Arrays of Neurons

            int TotalLayers = NumHiddenLayers + 2;

            Neurons = new Neuron[TotalLayers][];

            Neurons[0] = new InputNeuron[InputLayerSize];

            for (int i = 0; i < NumHiddenLayers; i++)
            {
                Neurons[i + 1] = new HiddenNeuron[HiddenLayerSize];
            }

            Neurons[TotalLayers - 1] = new OutputNeuron[OutputLayerSize];

            //Instantiate the layers based on the inputs
            for (int i = 0; i < InputLayerSize; i++)
            {
                Neurons[0][i] = new InputNeuron(HiddenLayerSize);
            }

            for (int i = 0; i < NumHiddenLayers; i++)
            {
                for (int j = 0; j < HiddenLayerSize; j++)
                {
                    if (i == 0)
                    {
                        Neurons[i + 1][j] = new HiddenNeuron(InputLayerSize, HiddenLayerSize);
                    }
                    if (i == (NumHiddenLayers - 1))
                    {
                        Neurons[i + 1][j] = new HiddenNeuron(HiddenLayerSize, OutputLayerSize);
                    }
                    else
                    {
                        Neurons[i + 1][j] = new HiddenNeuron(HiddenLayerSize, HiddenLayerSize);
                    }
                }
            }

            for (int i = 0; i < OutputLayerSize; i++)
            {
                Neurons[NumHiddenLayers + 1][i] = new OutputNeuron(HiddenLayerSize);
            }
            //Get input values

            
        }

        public void StartNN(double[] _inputs)
        {
            NetworkForwardInput(_inputs);
        }
        /// <summary>
        /// 
        /// </summary>
        private void NetworkForwardInput(double[] _inputs)
        {
            //Network Forward Input
            for (int i = 0; i < InputLayerSize; i++)
            {
                InputNeuron result = (InputNeuron)Neurons[0][i];
                result._inputValue = _inputs[i] / 10;
            }

            //Multiply inputs by weights of synapses
            foreach (InputNeuron inpN in Neurons[0])
            {
                for (int i = 0; i < inpN._numOutGoingSynapse; i++)
                {
                    inpN._outputs[i] = inpN._weights[i] * inpN._inputValue;
                }
            }
            NetworkForwardHidden();
        }

        /// <summary>
        /// 
        /// </summary>
        private void NetworkForwardHidden()
        {
            //Network Forward Hidden

            for (int h = 0; h < NumHiddenLayers; h++)
            {
                int i = 0;
                foreach (HiddenNeuron hidN in Neurons[h + 1])
                {
                    int j = 0;
                    foreach (Neuron inpN in Neurons[h])
                    {
                        hidN._inputs[j] = inpN._outputs[i];
                        j++;
                    }
                    i++;
                }
                foreach (HiddenNeuron hidN in Neurons[h + 1])
                {
                    hidN._preSig = 0;
                    foreach (double value in hidN._inputs)
                    {
                        hidN._preSig += value;
                    }
                }

                foreach (HiddenNeuron hidN in Neurons[h + 1])
                {
                    hidN._postSig = hidN.Activation(hidN._preSig);
                }

                foreach (HiddenNeuron hidN in Neurons[h + 1])
                {
                    for (int x = 0; x < hidN._numOutGoingSynapse; x++)
                    {
                        hidN._outputs[x] = hidN._weights[x] * hidN._postSig;
                    }
                }
            }
            NetworkForwardOutput();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void NetworkForwardOutput()
        {
            //Network Forward Output

            FinalOutPut = new double[OutputLayerSize];

            int i = 0;
            foreach (OutputNeuron outN in Neurons[NumHiddenLayers + 1])
            {
                int j = 0;
                foreach (HiddenNeuron hidN in Neurons[NumHiddenLayers])
                {
                    outN._inputs[j] = hidN._outputs[i];
                    j++;
                }
                i++;
            }

            foreach (OutputNeuron outN in Neurons[NumHiddenLayers + 1])
            {
                outN._preSig = 0;
                foreach (double value in outN._inputs)
                {
                    outN._preSig += value;
                }
            }

            int o = 0;
            foreach (OutputNeuron outN in Neurons[NumHiddenLayers + 1])
            {
                outN._postSig = outN.Activation(outN._preSig);
                FinalOutPut[o] = outN._postSig;
                o++;
            }
        }

        public void FinalOutPutDisplay()
        {
            int i = 1;
            foreach (double value in FinalOutPut)
            {
                Console.WriteLine("Final value of Output " + i + ": " + (value * 100).ToString());
                i++;
            }
        }

        public void Train(int _previousRaceMoves)
        {
            Training trainer = new Training();

            double target = _previousRaceMoves / 100;

            trainer.WeightAdjustment(Neurons, NumHiddenLayers, target);
        }
    }
}
