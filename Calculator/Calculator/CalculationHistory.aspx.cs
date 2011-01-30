using System;
using CalculationEngine;

namespace Calculator
{
    public partial class CalculationHistory : System.Web.UI.Page
    {
        // sourced from application
        public CalculationSet CalculationSet
        {
            get { return Session["CalculationSet"] as CalculationSet; }
            set { Session["CalculationSet"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string history = CalculationSet.HistoryAsXML;
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=calculationHistory.xml");
            Response.AddHeader("Content-Length", history.Length.ToString());
            Response.ContentType = "text/xml";
            Response.Write(history);
            Response.End(); 
        }
    }
}
