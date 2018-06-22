using Races.AI.NeuronTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Races.AI.TrainerTools
{
    class Training
    {
        double adjustmentValue = 0.5;
        public void WeightAdjustment(Neuron[][] _Neurons, int _numHiddenLayers, double target)
        {
            foreach (OutputNeuron outN in _Neurons[_numHiddenLayers + 1])
            {
                outN._cost = Costing.costcalc(Costing.error(outN._postSig, target));
            }

            if (_numHiddenLayers == 1)
            {
                List<HiddenNeuron> LeftHiddenNeurons = new List<HiddenNeuron>();

                foreach (HiddenNeuron hidN in _Neurons[1])
                {
                    hidN._cost = Costing.costcalc(Costing.error(hidN._postSig, target));
                    LeftHiddenNeurons.Add(hidN);
                }

                LeftHiddenNeurons.Sort((x, y) => x._cost.CompareTo(y._cost));

                for (int k = 0; k < LeftHiddenNeurons[0]._weights.Count(); k++)
                {
                    int index = Array.IndexOf(_Neurons[1], LeftHiddenNeurons[0]);
                    _Neurons[1][index]._weights[k] = _Neurons[1][index]._weights[k] - adjustmentValue * _Neurons[1][index]._cost;

                }

                for (int j = 1; j < LeftHiddenNeurons.Count(); j++)
                {
                    for (int k = 0; k < LeftHiddenNeurons[j]._weights.Count(); k++)
                    {
                        int index = Array.IndexOf(_Neurons[1], LeftHiddenNeurons[j]);
                        _Neurons[1][index]._weights[k] = _Neurons[1][index]._weights[k] - adjustmentValue * _Neurons[1][index]._cost;

                    }
                }

                foreach (InputNeuron inpN in _Neurons[0])
                {

                    for (int i = 0; i < inpN._weights.Count(); i++)
                    {
                        int index = Array.IndexOf(_Neurons[1], LeftHiddenNeurons.First());

                        if (i == index)
                        {
                            inpN._weights[index] = inpN._weights[index] - adjustmentValue * inpN._cost;

                        }
                        else
                        {
                            inpN._weights[i] = inpN._weights[i] - adjustmentValue * inpN._cost;

                        }
                    }
                }
            }
            else
            {
                for (int i = _numHiddenLayers; i > 1; i--)
                {
                    List<HiddenNeuron> hiddenNeurons = new List<HiddenNeuron>();

                    foreach (HiddenNeuron hidN in _Neurons[i])
                    {
                        hidN._cost = Costing.costcalc(Costing.error(hidN._postSig, target));
                        hiddenNeurons.Add(hidN);
                    }

                    hiddenNeurons.Sort((x, y) => x._cost.CompareTo(y._cost));

                    for (int k = 0; k < hiddenNeurons[0]._weights.Count(); k++)
                    {
                        hiddenNeurons[0]._weights[k] = hiddenNeurons[0]._weights[k] - adjustmentValue * hiddenNeurons[0]._cost;

                    }

                    for (int j = 1; j < hiddenNeurons.Count(); j++)
                    {
                        for (int k = 0; k < hiddenNeurons[j]._weights.Count(); k++)
                        {
                            hiddenNeurons[j]._weights[k] = hiddenNeurons[j]._weights[k] - adjustmentValue * hiddenNeurons[j]._cost;

                        }
                    }
                }

                List<HiddenNeuron> LeftHiddenNeurons = new List<HiddenNeuron>();

                foreach (HiddenNeuron hidN in _Neurons[1])
                {
                    hidN._cost = Costing.costcalc(Costing.error(hidN._postSig, target));
                    LeftHiddenNeurons.Add(hidN);
                }

                LeftHiddenNeurons.Sort((x, y) => x._cost.CompareTo(y._cost));


                foreach (InputNeuron inpN in _Neurons[0])
                {
                    for (int i = 0; i < inpN._weights.Count(); i++)
                    {
                        int index = Array.IndexOf(_Neurons[1], LeftHiddenNeurons.First());

                        if (i == index)
                        {
                            inpN._weights[index] = inpN._weights[index] - adjustmentValue * inpN._cost;

                        }
                        else
                        {
                            inpN._weights[i] = inpN._weights[i] - adjustmentValue * inpN._cost;

                        }
                    }
                }
            }
        }
    }
}
