using CalculationEngine;
using CalculatorTests.Mocks;

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