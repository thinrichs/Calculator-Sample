using System.Collections.Generic;
using CalculationEngine;

namespace CalculatorTests.Mocks
{
    internal class MockCalculationView : ICalculationView
    {
        internal bool _historySent;

        #region ICalculationView Members

        public CalculationSet CalculationSet { get; set; }
        public IList<Function> PossibleFunctions { get; set; }
        public double Factor { get; set; }
        public Function Function { get; set; }

        #endregion

        public void SendHistoryAsDownload()
        {
            _historySent = true;
        }
    }
}