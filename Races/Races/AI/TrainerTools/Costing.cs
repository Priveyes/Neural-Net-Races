using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Races.AI.TrainerTools
{
    class Costing
    {
        public double SigmoidPrime(double sigmoidValue, double z)
        {
            double denominator = Math.Pow(sigmoidValue, 2);

            double numerator = Math.Exp(-z);

            return numerator / denominator;
        }

        public static double error(double outputvalue, double targetvalue)
        {
            double errorvalue = 0;

            errorvalue = (outputvalue - targetvalue);

            return errorvalue;
        }

        public static double costcalc(double errorvalue)
        {
            double cost = 0;

            cost = Math.Abs(0.5 * (Math.Pow(errorvalue, 2)));

            return cost;
        }

    }
}
