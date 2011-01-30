using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Core;
using CalculationEngine;
using CalculatorTests.Mocks;
using Calculator.Tests;

namespace CalculatorTests.Tests
{
    /// <summary>
    /// Summary description for WhenFirstCalculationOccurs
    /// </summary>
    [TestClass]
    public class When_First_Calculation_Is_Adding_Two : SpecificationContext
    {
        CalculationPresenter presenter;
        // look at moq?
        ICalculationView view = new MockCalculationView();

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
