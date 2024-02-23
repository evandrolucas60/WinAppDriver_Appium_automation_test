using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace AutomationWinAppDriver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WindowsDriver<WindowsElement> CalcSession;

            AppiumOptions desiredCapabilities = new AppiumOptions();
            desiredCapabilities
                .AddAdditionalCapability("app", @"C:\Windows\System32\calc.exe");

            CalcSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desiredCapabilities);

            if (CalcSession == null)
            {
                Console.WriteLine("Application notepad not started.");
                return;
            }

            Console.WriteLine($"Application title: {CalcSession.Title}");
            CalcSession.Manage().Window.Maximize();

            //var screenShot = CalcSession.GetScreenshot();
            //screenShot.SaveAsFile($".\\Screenshot{DateTime.Now.ToString("ddMMyyyyhhmmss")}.png", OpenQA.Selenium.ScreenshotImageFormat.Png);

            //CalcSession.Quit();m
        }
    }
}
