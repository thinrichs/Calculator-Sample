using System.Collections.Generic;
using System.Linq;

namespace CalculationEngine
{
    public class CalculationController
    {
        private static int highestCalculationID;
        private static readonly Dictionary<int, CalculationSet> sets = new Dictionary<int, CalculationSet>();

        public static CalculationSet NewCalculationSet
        {
            get
            {
                highestCalculationID++;
                sets[highestCalculationID] = new CalculationSet {ID = highestCalculationID};
                return sets[highestCalculationID];
            }
        }

        public static CalculationSet GetCalculationSet(int setID)
        {
            CalculationSet result = null;
            sets.TryGetValue(setID, out result);
            return result;
        }

        public static CalculationSet AddToCalculationSet(int setID, Function function, double factor)
        {
            CalculationSet set = GetCalculationSet(setID);

            if (set != null)
            {
                // if it's our initial start, we just want to load up the value, not perform a function on it
                if (set.Count() == 0)
                {
                    function = CalculationActions.Add;
                }
                var newCalculation = new Calculation(set.CurrentResult, function, factor);
                set.PreviousResult = set.CurrentResult;
                set.CurrentResult = newCalculation.Result;
                set.Add(newCalculation);
            }
            return set;
        }
    }
}