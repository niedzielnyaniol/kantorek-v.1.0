using ClassLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Concrete
{
    public class Currency : IReplaceCurrency
    {
        public string Name { get; set; }
        public int Converter { get; set; }
        public string NameShortcut { get; set; }
        public double ExchangeRate { get; set; }

        public double ReplaceToPln(double quantity)
        {
            return Math.Round(quantity / (ExchangeRate * Converter), 2);
        }

        public double ReplaceFromPln(double quantity)
        {
            return Math.Round(quantity * ExchangeRate * Converter, 2);
        }
    }
}
