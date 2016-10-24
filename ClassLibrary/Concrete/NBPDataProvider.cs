using ClassLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Concrete
{
    public class NBPDataProvider : IDataProvider, IDataProviderAsync
    {
        public string GetStringFromXML(string url)
        {
            string content = "";

            using (var webClient = new System.Net.WebClient())
            {
                content = webClient.DownloadString(url);
            }

            return content;
        }

        public Task<string> GetStringFromXMLAsync(string url)
        {
            Task<string> content;

            using (var webClient = new System.Net.WebClient())
            {
                content = webClient.DownloadStringTaskAsync(url);
            }

            return content;
        }
    }
}
