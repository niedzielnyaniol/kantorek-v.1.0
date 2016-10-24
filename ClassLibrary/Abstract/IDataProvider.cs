using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Abstract
{
    public interface IDataProvider
    {
        string GetStringFromXML(string url);
    }

    public interface IDataProviderAsync
    {
        Task<string> GetStringFromXMLAsync(string url);
    }
}
