using System.Linq;
using CalculationEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Core;

namespace Calculator.Tests
{
    /// <summary>
    /// Summary description for RealWorld_Grid_Test
    /// </summary>
    [TestClass]
    public class When_Checking_Valid_Functions : CalculationViewTest
    {
        public override void Because()
        {
            presenter.OnViewInitialized();
        }

        [TestMethod]
        public void There_Should_Be_Four_Valid_Functions()
        {
            view.PossibleFunctions.Count().ShouldEqual(4);
        }

        [TestMethod]
        public void Valid_Functions_Should_Be_Add_Subtract_Multiply_Divide()
        {
            view.PossibleFunctions.ShouldContain(CalculationActions.Add);
            view.PossibleFunctions.ShouldContain(CalculationActions.Subtract);
            view.PossibleFunctions.ShouldContain(CalculationActions.Multiply);
            view.PossibleFunctions.ShouldContain(CalculationActions.Divide);
        }
    }
}