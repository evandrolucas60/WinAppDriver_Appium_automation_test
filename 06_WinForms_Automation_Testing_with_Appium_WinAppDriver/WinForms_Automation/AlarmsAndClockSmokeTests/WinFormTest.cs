using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
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

        [ClassCleanup]
        public static void CleanupAfterAllAlarmsTests()
        {
            Debug.WriteLine("ClassCleanup");
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

        [TestMethod]
        public void PopupTest()
        {
            _driver.FindElementByName("OpenMessageStrip").Click();

            Thread.Sleep(1000);

            _driver.FindElementByName("Alert").FindElementByName("OK").Click();
        }

        [TestMethod]
        public void TreeTest()
        {
            var tvControl = _driver.FindElementByAccessibilityId("treeView1");

            foreach (var tn in tvControl.FindElementsByTagName("TreeItem"))
            {
                Debug.WriteLine($"*** BEFORE: {tn.Text} - Displayed: {tn.Displayed} - Enabled: {tn.Enabled} - Selected: {tn.Selected}");
            }

            var nodeWorld = tvControl.FindElementByName("World");
            DoubleClick(nodeWorld);

            Thread.Sleep(1000);

            foreach (var tn in tvControl.FindElementsByTagName("TreeItem"))
            {
                Debug.WriteLine($"*** AFTER: {tn.Text} - Displayed: {tn.Displayed} - Enabled: {tn.Enabled} - Selected: {tn.Selected}");
            }

            var nodeAsia = tvControl.FindElementByName("Asia");
            DoubleClick(nodeAsia);

            var nodePakistan = tvControl.FindElementByName("Pakistan");

            WebDriverWait wdvPakistan = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            wdvPakistan.Until(x => nodePakistan.Displayed);

            nodePakistan.Click();
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

                    Thread.Sleep(400);

                    var lenderMenuThirdLevel = newMenu.FindElementByName("Third");

                    Actions actionForRightClick = new Actions(_driver);
                    actionForRightClick.MoveToElement(lenderMenuThirdLevel);
                    actionForRightClick.Click();
                    actionForRightClick.Perform();
                }
            }
        }

        [TestMethod]
        public void GridTest()
        {
            var ratesGrid = _driver.FindElementByName("Rates Grid");
            var allHeaders = ratesGrid.FindElementsByTagName("Header");

            Debug.WriteLine($"*** Headers count: {allHeaders.Count}");

            foreach (var item in allHeaders)
            {
                Debug.WriteLine($"*** {item.Text} - {item.Displayed} - {item.Enabled}");
            }
            var allCells = ratesGrid.FindElementsByTagName("DataItem");
            Debug.WriteLine($"Grid cells count: {allCells.Count}");

            foreach (var gridCell in allCells)
            {
                Debug.WriteLine($"*** Cell Name: {gridCell.GetAttribute("Name")} - Text: {gridCell.Text}");
                if (gridCell.GetAttribute("Name").StartsWith("State Row") && gridCell.Text.Equals("TXT"))
                {
                    gridCell.Click();
                    Actions actsGrid = new Actions(_driver);
                    actsGrid.MoveToElement(gridCell);
                    actsGrid.MoveToElement(gridCell, (gridCell.Size.Width / 4), gridCell.Size.Height / 2);
                    actsGrid.Click();
                    actsGrid.Perform();
                }
            }
        }

        private static void DoubleClick(AppiumWebElement node)
        {
            Actions actsTree = new Actions(_driver);
            actsTree.MoveToElement(node);
            actsTree.DoubleClick();
            actsTree.Perform();
        }
    }
}
