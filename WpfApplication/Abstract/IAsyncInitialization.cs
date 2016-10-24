using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Abstract
{
    internal interface IAsyncInitialization
    {
        Task Initialization { get; }
    }
}
