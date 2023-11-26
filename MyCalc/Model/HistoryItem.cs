using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalc.Model
{
    public class HistoryItem
    {
        public string Expresion { get; set; }
        public string Number {get;set; }

        public HistoryItem()
        {
            Expresion = string.Empty;
            Number = string.Empty;
        }

        public HistoryItem(string expresion, string number)
        {
            Expresion=expresion;
            Number=number;
        }
    }
}
