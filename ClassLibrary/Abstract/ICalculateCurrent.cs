using ClassLibrary.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Abstract
{
    public interface ICalculateCurrent
    {
        double Calculate(Currency cur1, Currency curr2, double quantity);
    }
}
