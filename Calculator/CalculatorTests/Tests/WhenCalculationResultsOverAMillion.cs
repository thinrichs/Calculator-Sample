using System;
using CalculationEngine;
using Calculator.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests.Tests
{
    /// <summary>
    /// Summary description for WhenCalculationResultsOverAMillion
    /// </summary>
    [TestClass]
    public class WhenCalculationResultsOverAMillion : CalculationViewTest
    {
        public override void CreateContext()
        {
            presenter = new CalculationPresenter(view);
            presenter.OnViewInitialized();
        }

        public override void Because()
        {
            view.Function = CalculationActions.Add;
            view.Factor = 99999;
            presenter.OnCalculation();

            view.Function = CalculationActions.Multiply;
            view.Factor = 99999;
        }

        [TestMethod]
        [ExpectedException(typeof (ArithmeticException))]
        public void Calculation_Should_Result_In_ArithmeticException()
        {
            presenter.OnCalculation();
        }
    }
}