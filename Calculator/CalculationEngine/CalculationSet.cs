using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CalculationEngine
{
    public class CalculationSet : List<Calculation>
    {
        public int ID { get; set; }       
        public double CurrentResult {get; set;}
        public double PreviousResult { get; set; }

        public string HistoryAsXML
        {
            get 
            {
                return new XElement("CalculationSet",
                    this.Select(e => e.AsXml)                    
                    ).ToString();
            }
        }
    }
}
