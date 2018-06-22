using System;

namespace Races.AI.NeuronTypes
{
    class Neuron
    {
        double cost;

        public double _cost { get { return cost; } set { cost = value; } }

        int numIncomingSynapse;

        public int _numIncomingSynapse { get { return numIncomingSynapse; } set { numIncomingSynapse = value; } }

        int numOutGoingSynapse;

        public int _numOutGoingSynapse { get { return numOutGoingSynapse; } set { numOutGoingSynapse = value; } }

        double[] weights;

        public double[] _weights { get { return weights; } set { weights = value; } }

        double[] inputs;

        public double[] _inputs { get { return inputs; } set { inputs = value; } }

        double[] outputs;

        public double[] _outputs { get { return outputs; } set { weights = value; } }

        double preSig;
        public double _preSig { get { return preSig; } set { preSig = value; } }
        double postSig;
        public double _postSig { get { return postSig; } set { postSig = value; } }

        public Neuron(int sI, int sO)
        {
            numIncomingSynapse = sI;
            numOutGoingSynapse = sO;
            inputs = new double[numIncomingSynapse];
            weights = new double[numOutGoingSynapse];
            outputs = new double[numOutGoingSynapse];

            int i = 0;
            while (i < numOutGoingSynapse)
            {
                double random = Tools.Randomiser.rand.Next(-1, 2);
                if (random == 0)
                {
                    continue;
                }
                //weights[i] = random / 100;             
                weights[i] = 1 / random;
                i++;
            }
        }

        public double Activation(double z)
        {
            return Sigmoid(z);
        }

        public double Sigmoid(double z)
        {
            return 1 / (1 + Math.Exp(-z));
        }

        public double HyperBolicTan(double z)
        {
            double numerator = Math.Exp(z) - Math.Exp(-z);
            double denominator = Math.Exp(z) + Math.Exp(-z);
            Console.WriteLine(numerator.ToString());
            Console.WriteLine(denominator.ToString());

            return numerator / denominator;
        }

        public double ReLU(double z)
        {
            return Math.Max(0, z);
        }

        public double LeakyReLU(double z)
        {
            if (z < 0)
            {
                return 0.01 * z;
            }
            else
            {
                return z;
            }
        }
    }
}
