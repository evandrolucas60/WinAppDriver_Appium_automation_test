using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcRunnerAppiumV3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WindowsDriver<WindowsElement> sessionCalc;
            AppiumOptions appOptions = new AppiumOptions();
            appOptions.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

            sessionCalc = new WindowsDriver<WindowsElement>(
                new Uri("http://127.0.0.1:4723"),
                appOptions
                );

            sessionCalc.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            var btnTwo = sessionCalc.FindElementByName("Dois");
            var btnPlus = sessionCalc.FindElementByName("Mais");
            var btnEquals = sessionCalc.FindElementByName("Igual a");

            btnTwo.Click();
            btnPlus.Click();
            btnTwo.Click();
            btnEquals.Click();

            var txtResults = sessionCalc.FindElementByAccessibilityId("CalculatorResults");

            Console.WriteLine($"Value shown by calculator: {txtResults.Text}");

            if (txtResults.Text.EndsWith("4"))
            {
                Console.WriteLine("The result is correct");
            }
            else
            {
                Console.WriteLine("The result is incorrect");
            }

            txtResults.SendKeys(Keys.Escape);
            txtResults.SendKeys("2");
            txtResults.SendKeys("+");
            txtResults.SendKeys("2");
            txtResults.SendKeys("=");

            Console.WriteLine($"Value shown by calculator: {txtResults.Text}");

            if (txtResults.Text.EndsWith("4"))
            {
                Console.WriteLine("The result is correct");
            }
            else
            {
                Console.WriteLine("The result is incorrect");
            }

        }
    }
}
