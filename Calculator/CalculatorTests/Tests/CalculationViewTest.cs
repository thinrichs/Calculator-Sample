using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Tests;
using CalculatorTests.Mocks;
using CalculationEngine;

namespace Calculator.Tests
{
    public class CalculationViewTest : SpecificationContext
    {
        protected CalculationPresenter presenter;
        // look at moq?
        protected ICalculationView view = new MockCalculationView();

        public override void CreateContext()
        {
            presenter = new CalculationPresenter(view);
            presenter.OnViewInitialized();
        }    
    }
}
