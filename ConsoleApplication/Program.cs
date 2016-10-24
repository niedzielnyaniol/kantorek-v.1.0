using ClassLibrary.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataProvider = new NBPDataProvider();
            var dataParser = new NbpXmlToCurrencyParser();
            string url = "http://www.nbp.pl/kursy/xml/lasta.xml";
            CalculateCurrency calc = new CalculateCurrency();

            var currencies = dataParser.Parse(dataProvider.GetStringFromXML(url));

            int i = 1;
            string curr;
            Currency c1, c2;
            double quantity;


            foreach (var item in currencies)
            {
                Console.WriteLine(i++ + " - " + item.NameShortcut + " - " + item.Name);
            }

            try
            {
                Console.WriteLine("Wybierz numer pierwszej waluty");
                curr = Console.ReadLine();
                c1 = currencies.First(c => c.NameShortcut == curr.ToUpper());
                Console.WriteLine("Wybierz numer drugiej waluty");
                curr = Console.ReadLine();
                c2 = currencies.First(c => c.NameShortcut == curr.ToUpper());
                Console.WriteLine("Wybierz ilość");
                quantity = double.Parse(Console.ReadLine());

                Console.WriteLine(calc.Calculate(c1, c2, quantity) + " " + c2.NameShortcut);
            }
            catch (Exception exc)
            {
                throw new Exception("Waluta o podanej nazwie nie istnieje", exc);
            }


        }
    }
}
