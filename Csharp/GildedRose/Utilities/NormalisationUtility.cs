using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.Utilities
{
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

        public static string NegativeToNever(int numericValue)
        {
            return numericValue < 0 ? "Never" : numericValue.ToString();
        }
    }
}
