using ClassLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Concrete
{
    public class CalculateCurrency : ICalculateCurrent
    {
        public double Calculate(Currency cur1, Currency curr2, double quantity)
        {
            double tmp1 = curr2.ReplaceToPln(quantity);

            return cur1.ReplaceFromPln(tmp1);
        }
    }
}
