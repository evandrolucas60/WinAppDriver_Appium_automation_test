using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;

namespace WinFormsUITesting
{
    [TestClass]
    public class WinFormTest
    {

        static WindowsDriver<WindowsElement> _driver;

        [ClassInitialize]
        public static void FirstsThingsFirst(TestContext testContext)
        {
            AppiumOptions dcWinForms = new AppiumOptions();
            dcWinForms.AddAdditionalCapability("app", @"C:\Users\evandro.silva\Downloads\Application+under+test\DoNotDistrurbMortgageCalculatorFrom1999.exe");
            _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), dcWinForms);
        }

        [TestMethod]
        public void CheckBoxTest()
        {
            var check = _driver.FindElementByName("checkBox1");
            
            check.Click();

            Thread.Sleep(1000);

            Debug.WriteLine($"Value of checkbox is: {check.Selected}");
        }

        [TestMethod]
        public void RadioTest()
        {
            var radioFirst = _driver.FindElementByName("First");

            Debug.WriteLine($"**** Value of radio First: {radioFirst.Selected}");

            radioFirst.Click();
           
            Thread.Sleep(1000);

            Debug.WriteLine($"**** Value of radio First: {radioFirst.Selected}");
        }
        
        
        [TestMethod]
        public void ComboTest()
        {
            var combo = _driver.FindElementByAccessibilityId("comboBox1");
            var open = combo.FindElementByName("Abrir");

            var listItems = combo.FindElementsByTagName("ListItem");
            Debug.WriteLine($"Before: Number of list items found: {listItems.Count}");

            combo.SendKeys(Keys.Down);
            open.Click();

            listItems = combo.FindElementsByTagName("ListItem");
            Debug.WriteLine($"After: Number of list items found: {listItems.Count}");


            foreach (var item in listItems)
            {
                if (item.Text == "KT")
                {
                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                    wait.Until(x => item.Displayed);
                    item.Click();
                }
            }
        }

        [TestMethod]
        public void MenuTest()
        {
            var allMenus = _driver.FindElementsByTagName("MenuItem");

            Debug.WriteLine($"All menu items found by search: {allMenus.Count}");

            foreach (var item in allMenus)
            {
                Debug.WriteLine($"+++++ Menu: {item.GetAttribute("Name")} -  Displayed: {item.Displayed}");
            }

            foreach (var mainMenuItem in allMenus)
            {
                if (mainMenuItem.GetAttribute("Name").Equals("File"))
                {
                    mainMenuItem.Click();
                    var newMenu = mainMenuItem.FindElementByName("New");

                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                    wait.Until(x => newMenu.Displayed);

                    newMenu.Click();
                }
            }
        }
    }
}
