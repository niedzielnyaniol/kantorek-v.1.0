using ClassLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace ClassLibrary.Concrete
{
    public class NbpXmlToCurrencyParser : IXMLParser<Currency>
    {
        public IEnumerable<Currency> Parse(string xml)
        {
            try
            {
                var xmlReader = XDocument.Load(new System.IO.StringReader(xml));

                IEnumerable<Currency> currencies =
                    from currency in xmlReader.Root.Descendants("pozycja")
                    select new Currency()
                    {
                        Name = currency.Element("nazwa_waluty").Value,
                        NameShortcut = currency.Element("kod_waluty").Value,
                        Converter = int.Parse(currency.Element("przelicznik").Value),
                        ExchangeRate = double.Parse(currency.Element("kurs_sredni").Value)
                    };

                return currencies;
            }
            catch (Exception exc)
            {
                throw new Exception("XML parse fail", exc);
            }
        }
    }
}
