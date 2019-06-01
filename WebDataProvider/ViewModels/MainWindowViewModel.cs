using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.ComponentModel;
using System.Windows.Input;
using WebDataProvider.BusinessLogic;
using WebDataProvider.Helpers;

namespace WebDataProvider.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            GetDataCommand = new DelegateCommand(SaveExecute);
            SettingsCommand = new DelegateCommand(SettingsExecute);
        }

        #region Commands

        public ICommand GetDataCommand { get; set; }

        public ICommand SettingsCommand { get; set; }

        #endregion

        #region Actions

        private void SettingsExecute(object param)
        {
            ChromeOptions options = new ChromeOptions();
            if (!string.IsNullOrEmpty(Proxy))
            {
                var proxy = new Proxy();
                proxy.Kind = ProxyKind.Manual;
                proxy.IsAutoDetect = false;
                proxy.HttpProxy = Proxy;
                proxy.SslProxy = Proxy;
                options.Proxy = proxy;
            }
           
            options.AddArgument("ignore-certificate-errors");

            Driver = new ChromeDriver(@"D:\Programowanie\OLX\chromedriver", options);
            Driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Can execute is not working
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecuteGettingData(object parameter)
        {
            return Driver != null ? true : false;
        }


        #endregion

            #region Properties

        public ChromeDriver Driver { get; set; }

        private string _proxy;
        public string Proxy
        {
            get => _proxy;
            set
            {
                _proxy = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged(nameof(Proxy));
            }
        }

        #endregion

        #region Privete Methods

        private void SaveExecute(object param)
        {
            var scraper = new DataScraper(Driver);

            for (int i = 1; i <= 500; i++)
            {
                var listOfLinks = scraper.GetLinksToOrders(i);
                foreach (var linkToSingleOrder in listOfLinks)
                {
                    var singleOrderData = scraper.GetSingleOrderData(linkToSingleOrder);
                }
            }
        }

        #endregion

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
