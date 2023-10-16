using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Check
    {

        #region Check the value (string) - return (string -> digit) int
        public static int CheckValueStringToInt(string val, string field)
        {
            int result; //variabile
            if (int.TryParse(val, out result))
            {
                return result; // TRUE: low <= res <= up
            }

            throw new InvalidDataException($"Wrong data! Please check \"{field} = {val}\" and restart.");
        }
        #endregion


        #region Check the value (string) - return (string -> digit) double
        public static double CheckValueStringToDouble(string val, string field)
        {
            double result; //variabile
            if (val == "")
            {
                return 0.0; // if is NULL
            }
            else if (double.TryParse(val, out result))
            {
                return result; // TRUE: low <= res <= up
            }

            throw new InvalidDataException($"Wrong data! Please check \"{field} = {val}\" and restart.");
        }
        #endregion

    }
}
