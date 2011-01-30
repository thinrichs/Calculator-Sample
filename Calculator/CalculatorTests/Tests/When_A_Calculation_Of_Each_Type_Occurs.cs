using System;
using System.Linq;
using System.Xml.Linq;
using CalculationEngine;
using Calculator.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Core;

namespace CalculatorTests.Tests
{
    /// <summary>
    /// Summary description for WhenACalculationOfEachTypeOccurs
    /// </summary>
    [TestClass]
    public class When_A_Calculation_Of_Each_Type_Occurs : CalculationViewTest
    {
        public override void Because()
        {
            view.Function = CalculationActions.Add;
            view.Factor = Math.PI;
            presenter.OnCalculation();

            view.Function = CalculationActions.Multiply;
            view.Factor = Math.PI;
            presenter.OnCalculation();

            view.Function = CalculationActions.Subtract;
            view.Factor = Math.PI;
            presenter.OnCalculation();

            view.Function = CalculationActions.Divide;
            view.Factor = Math.PI;
            presenter.OnCalculation();
        }

        [TestMethod]
        public void Calculation_History_XML_Should_Have_Four_Calculations()
        {
            XElement xml = XElement.Parse(view.CalculationSet.HistoryAsXML);
            xml.DescendantNodes().Count().ShouldEqual(4);
        }

        [TestMethod]
        public void Calculation_Result_Should_Equal_Two_And_Change()
        {
            // I had to manually do the math from above to come up with this number.  Is there a better way?
            view.CalculationSet.CurrentResult.ShouldEqual(2.1415926535897931);
        }
    }
}