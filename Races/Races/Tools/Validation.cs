using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Races.Tools
{
    class Validation
    {
        /// <summary>
        /// Simple input validation methods
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>

        public static bool validRaceMenuFormat(string format)
        {

            if (int.TryParse(format, out int result))
            {
                if (result <= 50)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool validMenuFormat12(string format)
        {
            if (char.TryParse(format, out char result))
            {
                if (result == '1' || result == '2')
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid value");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid value");
                return false;
            }
        }

        public static bool validMenuFormatYN(string format)
        {
            if(char.TryParse(format, out char result))
            {
                if (result == 'Y' || result == 'N' || result == 'y' || result == 'n')
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid value");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid value");
                return false;
            }
        }
    }
}
