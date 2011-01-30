using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Core;

namespace Calculator.Tests
{
    /// <summary>
    /// Summary description for RealWorld_Grid_Test
    /// </summary>
    [TestClass]
    public class When_A_Calculation_Session_Starts : CalculationViewTest
    {
        [TestMethod]
        public void Calculation_SetID_Shold_No_Longer_Be_Default()
        {
            view.CalculationSet.ID.ShouldBeGreater(0);
        }

        [TestMethod]
        public void Calculation_Set_Should_Have_Zero_Results()
        {
            view.CalculationSet.Count().ShouldEqual(0);
        }

        [TestMethod]
        public void Result_Of_Last_Calculation_Should_Be_Zero()
        {
            view.CalculationSet.CurrentResult.ShouldEqual(0D);
        }

        [TestMethod]
        public void Calculation_History_XML_Should_Have_No_Calculations()
        {
            XElement xml = XElement.Parse(view.CalculationSet.HistoryAsXML);
            xml.DescendantNodes().Count().ShouldEqual(0);
        }
    }
}