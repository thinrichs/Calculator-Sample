using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculationEngine;

namespace CalculatorTests.Mocks
{
    class MockCalculationView : ICalculationView
    {
        public CalculationSet CalculationSet { get; set; }
        public IList<Function> PossibleFunctions { get; set; }
        public double Factor { get; set; }
        public Function Function { get; set; }

        internal bool _historySent;
        public void SendHistoryAsDownload()
        {
            _historySent = true;
        }
    }
}
