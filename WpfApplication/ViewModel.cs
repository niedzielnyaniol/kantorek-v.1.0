using ClassLibrary.Concrete;
using Nito.AsyncEx;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication.Concrete;

namespace WpfApplication
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Notified Variables

        public ObservableCollection<Currency> Currencies { get; set; }

        private Currency _currentCurrency;
        public Currency CurrentCurrency
        {
            get { return _currentCurrency; }
            set
            {
                if (_currentCurrency != value)
                {
                    _currentCurrency = value;
                    _firstCurrency = "PLN";
                    _secondCurrency = value.NameShortcut;
                    FromPln = true;
                    Result = "";
                    OnPropertyChanged("CurrentCurrency", "FirstCurrency", "SecondCurrency");
                }
            }
        }

        private string _firstCurrency;
        public string FirstCurrency
        {
            get { return _firstCurrency; }
            set
            {
                if (_firstCurrency != value)
                {
                    _firstCurrency = value;
                    OnPropertyChanged("FirstCurrency");
                }
            }
        }

        private string _secondCurrency;
        public string SecondCurrency
        {
            get { return _secondCurrency; }
            set
            {
                if (_secondCurrency != value)
                {
                    _secondCurrency = value;
                    OnPropertyChanged("SecondCurrency");
                }
            }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged("Result");
                }
            }
        }

        #endregion

        #region Flags

        private bool FromPln;

        #endregion

        #region Constructor 
        public ViewModel()
        {
            InitializationNotifier = NotifyTaskCompletion.Create(InitializeAsync());

            FromPln = true;
            FirstCurrency = "PLN";
        }

        #endregion

        #region Commands

        private ICommand _swapCommand;
        public ICommand SwapCommand
        {
            get
            {
                if (_swapCommand == null)
                {
                    _swapCommand = new RelayCommand<object>(
                        (o) =>
                        {
                            string tmp = FirstCurrency;
                            FirstCurrency = SecondCurrency;
                            SecondCurrency = tmp;
                            FromPln = !FromPln;
                            Result = "";
                        });
                }

                return _swapCommand;
            }
        }

        private ICommand _calculateCommand;
        public ICommand CalculateCommand
        {
            get
            {
                if (_calculateCommand == null)
                {
                    _calculateCommand = new RelayCommand<double>(
                        quantity =>
                        {
                            if (FromPln)
                            {
                                Result = CurrentCurrency.ReplaceFromPln(quantity).ToString();
                            }
                            else
                            {
                                Result = CurrentCurrency.ReplaceToPln(quantity).ToString();
                            }
                        }
                    );
                }

                return _calculateCommand;
            }
        }

        #endregion

        #region INotifyTaskCompletion Members

        public INotifyTaskCompletion InitializationNotifier { get; private set; }

        public Task Initialization
        {
            get
            {
                return InitializationNotifier.Task;
            }
        }

        private async Task InitializeAsync()
        {
            var dataProvider = new NBPDataProvider();
            var xmlParser = new NbpXmlToCurrencyParser();
            Currencies = new ObservableCollection<Currency>();
            string url = "http://www.nbp.pl/kursy/xml/lasta.xml";

            //for (int i = 0; i < 100; i++)
            //{

            
            var currenciesList = new List<Currency>(
                xmlParser.Parse(await dataProvider.GetStringFromXMLAsync(url)));

            foreach (var currency in currenciesList)
            {
                Currencies.Add(currency);
            }

            //}

            CurrentCurrency = Currencies.FirstOrDefault(c => c.Name.Equals("euro"));
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(params string[] namesOfProperties)
        {
            foreach (var property in namesOfProperties)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}
