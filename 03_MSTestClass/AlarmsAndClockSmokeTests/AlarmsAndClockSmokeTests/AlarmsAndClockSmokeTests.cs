using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace AlarmsAndClockSmokeTests
{
    [TestClass]
    public class AlarmsAndClockSmokeTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            WindowsDriver<WindowsElement> sessionAlarms;
            AppiumOptions appOptions = new AppiumOptions();
            appOptions.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

            appOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");

            sessionAlarms = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appOptions);

            Assert.AreEqual("Relógio", sessionAlarms.Title, false, $"Actual title doesn't match expected title: {sessionAlarms.Title}");
        }
    }
}
