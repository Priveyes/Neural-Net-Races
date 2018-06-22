using Races.AI.NeuronTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Races.AI.NeuralNetwork
{
    class SingleHiddenLayerNN
    {
        int InputLayerSize;
        int HiddenLayerSize;
        int OutputLayerSize;

        List<InputNeuron> inputNeurons = new List<InputNeuron>();

        public List<InputNeuron> _inputNeurons { get { return inputNeurons; } set { inputNeurons = value; } }

        List<HiddenNeuron> hiddenNeurons = new List<HiddenNeuron>(); //List of a List

        public List<HiddenNeuron> _hiddenNeurons { get { return hiddenNeurons; } set { hiddenNeurons = value; } }

        List<OutputNeuron> outputNeurons = new List<OutputNeuron>();

        public List<OutputNeuron> _outputNeurons { get { return outputNeurons; } set { outputNeurons = value; } }


        double[] FinalOutPut;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_inSize"></param>
        /// <param name="_hidSize"></param>
        /// <param name="_outSize"></param>
        public SingleHiddenLayerNN(int _inSize, int _hidSize, int _outSize)
        {
            InputLayerSize = _inSize;

            HiddenLayerSize = _hidSize;

            OutputLayerSize = _outSize;

            for (int i = 0; i < InputLayerSize; i++)
            {
                int NumSynapse = HiddenLayerSize;

                InputNeuron InpN = new InputNeuron(NumSynapse);
                inputNeurons.Add(InpN);
            }

            for (int i = 0; i < HiddenLayerSize; i++)
            {
                int NumSynapse = OutputLayerSize;
                int NumInputSynapse = InputLayerSize;

                HiddenNeuron HidN = new HiddenNeuron(NumInputSynapse, NumSynapse);
                hiddenNeurons.Add(HidN);
            }

            for (int i = 0; i < OutputLayerSize; i++)
            {
                int NumInputSynapse = HiddenLayerSize;

                OutputNeuron OutN = new OutputNeuron(NumInputSynapse);
                outputNeurons.Add(OutN);
            }

            for (int i = 0; i < InputLayerSize; i++)
            {
                Console.WriteLine("Please input Value " + (i + 1));
                inputNeurons[i]._inputValue = double.Parse(Console.ReadLine());
            }

            NetworkForwardInput();
            NetworkForwardHidden();

            double[] FinalOutPut = NetworkForwardOutput();

            FinalOutPutDisplay();


        }

        private void NetworkForwardInput()
        {
            foreach (InputNeuron inpN in inputNeurons)
            {
                for (int i = 0; i < inpN._numOutGoingSynapse; i++)
                {
                    inpN._outputs[i] = inpN._weights[i] * inpN._inputValue;
                }
            }
        }

        private void NetworkForwardHidden()
        {
            int i = 0;
            foreach (HiddenNeuron hidN in hiddenNeurons)
            {
                int j = 0;
                foreach (InputNeuron inpN in inputNeurons)
                {
                    hidN._inputs[j] = inpN._outputs[i];
                    j++;
                }
                i++;
            }

            foreach (HiddenNeuron hidN in hiddenNeurons)
            {
                foreach (double value in hidN._inputs)
                {
                    hidN._preSig += value;
                }
            }

            foreach (HiddenNeuron hidN in hiddenNeurons)
            {
                hidN._postSig = hidN.Sigmoid(hidN._preSig);
            }
            foreach (HiddenNeuron hidN in hiddenNeurons)
            {
                for (int x = 0; x < hidN._numOutGoingSynapse; x++)
                {
                    hidN._outputs[x] = hidN._weights[x] * hidN._postSig;
                }
            }
        }

        private double[] NetworkForwardOutput()
        {

            FinalOutPut = new double[OutputLayerSize];

            int i = 0;
            foreach (OutputNeuron outN in outputNeurons)
            {
                int j = 0;
                foreach (HiddenNeuron hidN in hiddenNeurons)
                {
                    outN._inputs[j] = hidN._outputs[i];
                    j++;
                }
                i++;
            }

            foreach (OutputNeuron outN in outputNeurons)
            {
                foreach (double value in outN._inputs)
                {
                    outN._preSig += value;
                }
            }

            int o = 0;
            foreach (OutputNeuron outN in outputNeurons)
            {
                outN._postSig = outN.Sigmoid(outN._preSig);
                FinalOutPut[o] = outN._postSig;
                o++;
            }
            return FinalOutPut;
        }

        public void FinalOutPutDisplay()
        {
            int i = 1;
            foreach (double value in FinalOutPut)
            {
                Console.WriteLine("Final value of Output " + i + ": " + value.ToString());
                i++;
            }
        }
    }
}
