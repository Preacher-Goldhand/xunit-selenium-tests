using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Guru99Site.Testing
{
    public class SigningUpCourseFormTest
    {
        private static WebDriver _chromeDriver;
        private static readonly string _url = "http://demo.guru99.com/test/guru99home/";

        // Find WebElements on the page
        [FindsBy(How = How.XPath, Using = ".//*[@id='philadelphia-field-email']")]
        [CacheLookup]
        private WebElement _emailText;

        [FindsBy(How = How.Id, Using = "awf_field-91977689")]
        [CacheLookup]
        private WebElement _course;

        [FindsBy(How = How.XPath, Using = ".//*[@id='philadelphia-field-submit']")]
        [CacheLookup]
        private WebElement _submitBtn;

        [Fact]
        public void SigningUpForCourseSuccessfully()
        {
            FillInForm();
            AssertResult();
            Quit();
        }

        // Set up the ChromeDriver instance
        [Fact]
        public static WebDriver GetInstance()
        {
            if (_chromeDriver == null)
            {
                _chromeDriver = new ChromeDriver("C:\\Users\\rench\\Desktop\\Programming\\Testing_Course\\ChromeDriver");
                _chromeDriver.Url = _url;
                _chromeDriver.Manage().Window.Maximize();
            }
            return _chromeDriver;
        }

        // Cleaning up
        [Fact]
        public static void Quit()
        {
            GetInstance();
            _chromeDriver.Close();
            _chromeDriver.Quit();
        }

        [Fact]
        private void FillInForm()
        {
            GetInstance();
            _emailText.SendKeys("tester123@gmail.com");
            var selectedCourse = new SelectElement(_course);
            selectedCourse.SelectByValue("sap-abap");
            _submitBtn.Click();
        }

        [Fact]
        private void AssertResult()
        {
            GetInstance();
            IAlert alert = _chromeDriver.SwitchTo().Alert();

            string expectedText = "Form Submitted Successfully...";
            string alertText = alert.Text;

            alertText.Should().Be(expectedText);
        }
    }
}