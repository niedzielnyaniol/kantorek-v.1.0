using ClassLibrary.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Abstract
{
    public interface IReplaceCurrency
    {
        double ReplaceToPln(double quantity);
        double ReplaceFromPln(double quantity);
    }
}
