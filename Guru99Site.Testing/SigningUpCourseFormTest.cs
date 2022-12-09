using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace Guru99Site.Testing
{
    public class SigningUpCourseFormTest
    {
        private static IWebDriver _chromeDriver;
        private static readonly string _url = "http://demo.guru99.com/test/guru99home/";
        private static int WAIT = 10;

        [Fact]
        public void SigningUpForCourseSuccessfully()
        {
            // Set up the ChromeDriver instance
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            ChromeDriver _chromeDriver = new ChromeDriver();
            _chromeDriver.Url = _url;
            _chromeDriver.Manage().Window.Maximize();
            _chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(WAIT);

            // Arrange
            WebElement emailText = (WebElement)_chromeDriver.FindElement(By.XPath(".//*[@id='philadelphia-field-email']"));
            WebElement course = (WebElement)_chromeDriver.FindElement(By.Id("awf_field-91977689"));
            WebElement submitBtn = (WebElement)_chromeDriver.FindElement(By.XPath(".//*[@id='philadelphia-field-submit']"));

            string emailToRegister = "tester123@test.com";
            string courseToRegister = "sap-abap";

            // Act
            emailText.SendKeys(emailToRegister);
            var selectedCourse = new SelectElement(course);
            selectedCourse.SelectByValue(courseToRegister);
            submitBtn.Click();

            // Assert
            IAlert alert = _chromeDriver.SwitchTo().Alert();

            string expectedText = "Form Submitted Successfully...";
            string alertText = alert.Text;

            alertText.Should().Be(expectedText);

            // Cleaning up
            _chromeDriver.Close();
            _chromeDriver.Quit();
        }
    }
}
