using System;
using System.Xml.Linq;

namespace CalculationEngine
{
    public class Calculation
    {
        private readonly Function function;
        private readonly double previousResult;

        public Calculation(double previousResult, Function function, double factor)
        {
            this.previousResult = Math.Round(previousResult, 5);
            this.function = function;
            Factor = Math.Round(factor, 5);
        }

        public double Factor { get; private set; }

        public double Result
        {
            get
            {
                double result = default(double);
                // I wish I could use a case here.  I'm not happy with this condition chaining
                if (function == CalculationActions.Add)
                {
                    result = previousResult + Factor;
                }
                else if (function == CalculationActions.Subtract)
                {
                    result = previousResult - Factor;
                }
                else if (function == CalculationActions.Divide)
                {
                    if (Factor != 0)
                    {
                        result = previousResult/Factor;
                    }
                    else
                    {
                        throw new ArithmeticException("This calculator does not support division by zero");
                    }
                }
                else if (function == CalculationActions.Multiply)
                {
                    result = previousResult*Factor;
                }
                // calculator only supports results up to 1M
                if (result > 1000000)
                {
                    throw new ArithmeticException("This calculator only supports results up to 1,000,000");
                }
                if (Factor > 99999)
                {
                    throw new ArgumentOutOfRangeException("This calculator only supports numbers up to 99999");
                }

                return Math.Round(result, 5);
            }
        }

        public XElement AsXml
        {
            get
            {
                return new XElement("Calculation",
                                    new XAttribute("StartingValue", Math.Round(previousResult, 5)),
                                    new XAttribute("Action", function.Action),
                                    new XAttribute("Factor", Math.Round(Factor, 5)),
                                    new XAttribute("Result", Math.Round(Result, 5)));
            }
        }
    }
}