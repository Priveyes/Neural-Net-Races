using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Races.AI.NeuronTypes
{
    class InputNeuron : Neuron
    {
        public InputNeuron(int sO) : base(0, sO) { }

        double inputValue;

        public double _inputValue { get { return inputValue; } set { inputValue = value; } }

    }
}
