using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;

namespace AlarmsAndClockSmokeTests
{
    [TestClass]
    public class AlarmsAndClockSmokeTests
    {

        static WindowsDriver<WindowsElement> sessionAlarms;
        [ClassInitialize]
        public static void PrepareForTestingAlarms(TestContext testContext)
        {
            Debug.WriteLine("Hello ClassInitialize");
            AppiumOptions appOptions = new AppiumOptions();
            appOptions.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            appOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");
            sessionAlarms = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appOptions);
        }

        [ClassCleanup]
        public static void CleanupAfterAllAlarmsTests()
        {
            //Debug.WriteLine("Hello ClassCleanup");
            //if (sessionAlarms != null)
            //{
            //    sessionAlarms.Quit();
            //}
        }

        [TestMethod]
        public void JustAnotherTest()
        {
            Debug.WriteLine("Hello another test.");
        }

        [TestMethod]
        public void TestAlarmsAndClockIsLaunchingSuccessfully()
        {
            Assert.AreEqual("Relógio", sessionAlarms.Title, false, $"Actual title doesn't match expected title: {sessionAlarms.Title}");
        }

        [TestMethod]
        public void VerifyNewClockCanBeAdded()
        {
            //click the top button in top section of the app
            sessionAlarms.FindElementByAccessibilityId("ClockButton").Click();
            sessionAlarms.FindElementByName("Adicionar nova cidade").Click();
            //Thread.Sleep(1000);

            WebDriverWait wait = new WebDriverWait(sessionAlarms, TimeSpan.FromSeconds(10));


            var txtLocation = sessionAlarms.FindElementByName("Inserir um local");
            wait.Until(pred => txtLocation.Displayed);
            txtLocation.SendKeys("São Paulo");
            txtLocation.SendKeys(Keys.ArrowDown);
            txtLocation.SendKeys(Keys.Enter);

            sessionAlarms.FindElementByName("Adicionar").Click();

            var ClockItems = sessionAlarms.FindElementsByClassName("ListViewItem");
            bool wasClockTileFound = false;
            WindowsElement tileFound = null;

            foreach (var clockTile in ClockItems)
            {
                if (clockTile.Text.StartsWith("São Paulo, Brasil"))
                {
                    Debug.WriteLine("Clock found");
                    wasClockTileFound = true;
                    if (clockTile.Size.Width > 16)
                    {
                        tileFound = clockTile;
                        break;
                    }
                }
            }

            Assert.IsTrue(wasClockTileFound, "No Clock tile found.");
            Actions actionsForRightClick = new Actions(sessionAlarms);
            actionsForRightClick.MoveToElement(tileFound);
            actionsForRightClick.ContextClick();
            actionsForRightClick.Perform();

            var deleteClock = sessionAlarms.FindElementByAccessibilityId("ContextMenuDelete");
            deleteClock.Click();

        }
    }
}
