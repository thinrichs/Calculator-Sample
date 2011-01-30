using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculationEngine
{
    public class CalculationPresenter
    {
        ICalculationView view;

        public CalculationPresenter(ICalculationView view)
        {
            this.view = view;
        }
       
        public void OnViewInitialized()
        {
            view.CalculationSet = CalculationController.NewCalculationSet;
            view.PossibleFunctions = CalculationActions.Functions;
        }

        public void OnCalculation()
        {
            CalculationController.AddToCalculationSet(view.CalculationSet.ID, view.Function, view.Factor);
        }       
    }
}