using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using CalculationEngine;

namespace Calculator
{
    public partial class _Default : Page, ICalculationView
    {
        private CalculationPresenter presenter;

        public bool IsPostBack
        {
            get { return base.IsPostBack; }
        }

        // sourced from application

        #region ICalculationView Members

        public CalculationSet CalculationSet
        {
            get { return Session["CalculationSet"] as CalculationSet; }
            set { Session["CalculationSet"] = value; }
        }

        public IList<Function> PossibleFunctions
        {
            get { return Session["PossibleFunctions"] as IList<Function>; }
            set { Session["PossibleFunctions"] = value; }
        }

        // sourced from view
        public Function Function { get; set; }
        public double Factor { get; set; }

        #endregion

        // called once on initial page load.  Probably should be called on the view from the presenter.
        private void SetupUI()
        {
            ActionDropDown.DataSource = PossibleFunctions;
            ActionDropDown.DataBind();
        }

        // translates our UI representations to system model objects
        private void TranslateUIToModel()
        {
            Function = PossibleFunctions
                .Where(x => x.ID == int.Parse(ActionDropDown.SelectedValue))
                .FirstOrDefault();
            if (!String.IsNullOrEmpty(FactorText.Value))
            {
                double factor;
                if (Double.TryParse(FactorText.Value, out factor))
                {
                    Factor = factor;
                }
            }
        }

        private void TranslateModelToUI()
        {
            // make XML output more HTML friendly
            CaculationXMLLabel.Text = Server.HtmlEncode(CalculationSet.HistoryAsXML)
                .Replace(" ", "&nbsp;")
                .Replace(Environment.NewLine, "<br />");
            FactorText.Value = CalculationSet.CurrentResult.ToString();
        }

        private void ShowError(string message)
        {
            ErrorMessage.Text = message;
            ErrorMessage.Visible = true;
        }

        // give public access to postback, for javascript

        // events are listed in the order they occur
        protected void Page_Init()
        {
            presenter = new CalculationPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                presenter.OnViewInitialized();
                SetupUI();
            }
            else
            {
                TranslateUIToModel();
                ProcessCalculation();
            }
        }

        public void ProcessCalculation()
        {
            try
            {
                // no errors, at least not yet
                ErrorMessage.Visible = false;
                presenter.OnCalculation();
            }
            catch (ArgumentOutOfRangeException rangeError)
            {
                ShowError(rangeError.ParamName);
            }
            catch (ArithmeticException mathError)
            {
                ShowError(mathError.Message);
            }
        }

        public void NewSession(object sender, EventArgs e)
        {
            presenter.OnViewInitialized();
        }

        protected void Page_PreRender()
        {
            // this isn't done in Page_Load so that we have time to process our model changes of the postback
            TranslateModelToUI();
        }
    }
}