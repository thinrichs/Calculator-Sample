﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Tests;
using CalculationEngine;

namespace CalculatorTests.Tests
{
    /// <summary>
    /// Summary description for WhenCalculationFactorIsOver99999
    /// </summary>
    [TestClass]
    public class WhenCalculationFactorIsOver99999 : CalculationViewTest
    {
        public override void CreateContext()
        {
            presenter = new CalculationPresenter(view);
            presenter.OnViewInitialized();
        }

        public override void Because()
        {
            view.Function = CalculationActions.Add;
            view.Factor = 999999;            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Calculation_Should_Result_In_ArithmeticException()
        {
            presenter.OnCalculation();
        }
    }
}
