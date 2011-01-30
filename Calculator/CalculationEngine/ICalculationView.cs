using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculationEngine
{
    public interface ICalculationView
    {
        CalculationSet CalculationSet { get; set; }
        Function Function {get; set;}
        IList<Function> PossibleFunctions { get; set; }
        double Factor {get; set;}
    }
}
