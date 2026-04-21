using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.Utilities
{
    // KRB: I've added this as a utility as it could be extended to add other formatting capabilities when the time comes. It cleans up the main code.
    public class NormalisationUtility
    {
        public static int NumberOrDefault(string input, int defaultValue)
        {
            try
            {
                return int.Parse(input);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
        }
    }
}
