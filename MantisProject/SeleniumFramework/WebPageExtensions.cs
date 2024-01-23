using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

namespace SeleniumFramework
{
    public static class WebPageExtensions
    {
        public static void MoveCursorToElement(this CorePage page, IWebElement element)
        {
            element.WaitForVisible();
            Actions chain = new Actions(page.Driver);
            chain.MoveToElement(element).Perform();
        }

        /// <summary>
        /// Открытие новой вкладки браузера и переход на нее
        /// </summary>
        public static void OpenNewTab(this CorePage page)
        {
            page.Driver.ExecuteJavaScript("window.open('', 'tab2');");
            page.Driver.SwitchTo().Window("tab2");
        }

        /// <summary>
        /// Закрываем одну вкладку и переключаемся на другую
        /// </summary>
        public static void CloseOneTab(this CorePage page, string handle)
        {
            page.Driver.Close();
            page.Driver.SwitchTo().Window(handle);
        }

        /// <summary>
        /// Посылаем комбинацию клавиш в окно, не привязываясь к элементу
        /// </summary>
        public static void SendKeysToFrame(this CorePage page, string keys)
        {
            Actions chain = new Actions(page.Driver);
            chain.SendKeys(keys).Perform();
        }

        /// <summary>
        /// Проверка на наличие алерта на странице
        /// </summary>
        public static bool IsAlertOpened(this CorePage page)
        {
            try
            {
                page.Driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public static void AcceptAlert(this CorePage page)
        {
            try
            {
                var alert = page.Driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException)
            {
            }
        }

        /// <summary>
        /// Получение текста алерта
        /// </summary>
        public static TResultMessage GetAlertText<TResultMessage>(this CorePage page)
        {
            return JsonConvert.DeserializeObject<TResultMessage>(page.Driver.SwitchTo().Alert().Text);
        }
        
        /// <summary>
        /// Получаем элемент из Shadow DOM
        /// </summary>
        public static IWebElement FindShadowElement(this CorePage page, string shadowDom, string locator)
        {
            return Wait.UntilElementToBeClickable((IWebElement)((IJavaScriptExecutor)page.Driver)
                    .ExecuteScript($"return document.querySelector('{shadowDom}').shadowRoot.querySelector('{locator}');"));
        }
    }
}