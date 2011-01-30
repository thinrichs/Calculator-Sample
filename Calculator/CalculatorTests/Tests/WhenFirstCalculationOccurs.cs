using System.Linq;
using System.Xml.Linq;
using CalculationEngine;
using Calculator.Tests;
using CalculatorTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Core;

namespace CalculatorTests.Tests
{
    /// <summary>
    /// Summary description for WhenFirstCalculationOccurs
    /// </summary>
    [TestClass]
    public class When_First_Calculation_Is_Adding_Two : SpecificationContext
    {
        // look at moq?
        private readonly ICalculationView view = new MockCalculationView();
        private CalculationPresenter presenter;

        public override void CreateContext()
        {
            presenter = new CalculationPresenter(view);
            presenter.OnViewInitialized();
        }

        public override void Because()
        {
            view.Function = CalculationActions.Add;
            view.Factor = 2;
            presenter.OnCalculation();
        }

        [TestMethod]
        public void Calculation_History_XML_Should_Have_One_Calculation()
        {
            XElement xml = XElement.Parse(view.CalculationSet.HistoryAsXML);
            xml.DescendantNodes().Count().ShouldEqual(1);
        }

        [TestMethod]
        public void Calculation_Result_Should_Equal_Two()
        {
            view.CalculationSet.CurrentResult.ShouldEqual(2D);
        }
    }
}