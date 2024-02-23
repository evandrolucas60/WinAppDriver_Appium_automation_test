using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;

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
            Debug.WriteLine("Hello ClassCleanup");
            if (sessionAlarms != null)
            {
                sessionAlarms.Quit();
            }
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
    }
}
