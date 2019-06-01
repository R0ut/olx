using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebDataProvider.Models;

namespace WebDataProvider.BusinessLogic
{
    /// <summary>
    /// Class with logic to get data from web.
    /// </summary>
    public class DataScraper
    {
        private ChromeDriver _driver;
        private IReadOnlyCollection<IWebElement> _listOfParameters;

        public DataScraper(ChromeDriver driver)
        {
            _driver = driver;
        }


        public List<string> GetLinksToOrders(int pageNumber)
        {
            var links = new List<string>();
            _driver.Navigate().GoToUrl($"https://www.olx.pl/motoryzacja/samochody/?page={pageNumber}");

            var aElements = _driver.FindElements(By.XPath("//a[contains(@class, 'thumb') and contains(@class, 'vtop') and contains(@class, 'inlblk') " +
                "and contains(@class, 'rel') and contains(@class, 'tdnone') and contains(@class, 'linkWithHash') and contains(@class, 'scale4')" +
                " and contains(@class, 'detailsLink')]"));

            foreach (var item in aElements)
            {
                links.Add(item.GetAttribute("href").ToString());
            }

            return links;
        }

        public DataModel GetSingleOrderData(string linkToOrder)
        {
            var model = new DataModel();
            _driver.Navigate().GoToUrl(linkToOrder);
            if (IsOlxOrder(linkToOrder))
            {

            }
            else
            {
                model.PhoneNumber = GetPhoneNumberOtomoto();
                model.Location = GetLocationOtomoto();
                GetCarParameters();
                model.Model = GetModelOtomoto();
                model.Brand = GetBrandOtomoto();
                model.Fuel = GetFuelTypeOtomoto();
                model.HorsePower = GetHorsePowerOtomoto();
                model.Price = GetPriceOtomoto();
                model.Year = GetYearOtomoto();
            }

            return model;
        }

        #region otomoto
        private string GetPhoneNumberOtomoto()
        {
            IWebElement button = _driver.FindElement(By.ClassName("seller-phones__button")); // locate the button, can be done with any other selector
            Actions actions = new Actions(_driver);
            actions.MoveToElement(button).Click().Build().Perform();
            // Tell webdriver to wait
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            // Test the autocomplete response - Explicit Wait
            var autocomplete = wait.Until(x => x.FindElements(By.ClassName("seller-phones__number")).FirstOrDefault(y => y.Text.Length > 3));

            return autocomplete.Text;
        }

        private string GetLocationOtomoto()
        {
            return _driver.FindElement(By.ClassName("seller-box__seller-address__label")).Text;
        }

        private void GetCarParameters()
        {
            _listOfParameters = _driver.FindElements(By.ClassName("offer-params__item"));
        }

        private string GetModelOtomoto()
        {
            var model = _listOfParameters.FirstOrDefault(x => x.Text.Contains("Model")).Text;
            return model != null ? Regex.Replace(model, "Model pojazdu\\r\\n", string.Empty) : "Brak danych";
        }
        private string GetBrandOtomoto()
        {
            var brand = _listOfParameters.FirstOrDefault(x => x.Text.Contains("Marka")).Text;
            return brand != null ? Regex.Replace(brand, "Marka pojazdu\\r\\n", string.Empty) : "Brak danych";
        }

        private string GetFuelTypeOtomoto()
        {
            var fuelType = _listOfParameters.FirstOrDefault(x => x.Text.Contains("paliwa")).Text;
            return fuelType != null ? Regex.Replace(fuelType, "Rodzaj paliwa\\r\\n", string.Empty) : "Brak danych";
        }

        private string GetHorsePowerOtomoto()
        {
            var power = _listOfParameters.FirstOrDefault(x => x.Text.Contains("Moc"))?.Text;
            return power != null ? Regex.Replace(power, "Moc\\r\\n", string.Empty) : "Brak danych";
        }

        private string GetYearOtomoto()
        {
            var year = _listOfParameters.FirstOrDefault(x => x.Text.Contains("produkcji")).Text;
            return year != null ? Regex.Replace(year, "Rok produkcji\\r\\n", string.Empty) : "Brak danych";
        }

        private string GetPriceOtomoto()
        {
            return _driver.FindElement(By.ClassName("offer-price__number")).Text;
        }

        #endregion


        private bool IsOlxOrder(string link)
        {
            return link.Contains("olx.pl") ? true : false;
        }

        private void temp()
        {
            //Driver.Navigate().GoToUrl("https://www.otomoto.pl/oferta/seat-leon-2-0-tdi-dsg-seat-sound-el-fotel-kamera-acc-fv23-ID6BUcRn.html?;promoted"); 
            //IWebElement button = Driver.FindElement(By.ClassName("seller-phones__button")); // locate the button, can be done with any other selector
            //Actions action = new Actions(Driver);
            //action.MoveToElement(button).Perform(); // move to the button
            //button.Click();
            //var result = Driver.FindElement(By.ClassName("seller-phones__number")).Text;
        }
    }
}
