using System.Collections.Generic;

namespace CalculationEngine
{
    public interface ICalculationView
    {
        CalculationSet CalculationSet { get; set; }
        Function Function { get; set; }
        IList<Function> PossibleFunctions { get; set; }
        double Factor { get; set; }
    }
}