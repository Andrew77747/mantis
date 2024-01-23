using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFramework
{
    /// <summary>
    /// класс ожидания отображения элемента
    /// </summary>
    public static class Wait
    {
        private const int DefaultTimeOutSeconds = 60;

        private static readonly WebDriverWait _wait = new WebDriverWait(Browser.Driver,
            TimeSpan.FromSeconds(DefaultTimeOutSeconds));

        private static void SimpleWait(Func<bool> condition, Action doAction, int timeOut)
        {
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.Elapsed <= TimeSpan.FromMilliseconds(timeOut))
            {
                if (condition())
                {
                    return;
                }

                Thread.Sleep(100);
            }

            doAction();
        }

        public static void WaitFor(Func<bool> condition, string message, int timeOut)
        {
            void DoAction() => throw new TimeoutException($"{message} during {timeOut} milliseconds");
            SimpleWait(condition, DoAction, timeOut);
        }

        public static IWebElement WaitForVisible(IWebElement element, int timeOut)
        {
            bool Condition()
            {
                try
                {
                    return element.IsDisplayed();
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            }

            WaitFor(Condition, "Didn't find element", timeOut);
            return element;
        }

        public static IWebElement WaitForAttribute(IWebElement element, string attribute, string value, int timeOut)
        {
            bool Condition() => element.GetAttribute(attribute) == value;
            WaitFor(Condition, "Attribute value isn't changed", timeOut);
            return element;
        }

        public static IWebElement WaitForTextUpdate(IWebElement element, string text, int timeOut)
        {
            bool Condition() => element.Text == text;
            WaitFor(Condition, "Text isn't changed", timeOut);
            return element;
        }

        public static IList<IWebElement> WaitForExists(IList<IWebElement> elements, int timeOut)
        {
            bool Condition() => elements.Count != 0;
            WaitFor(Condition, "Didn't find elements", timeOut);
            return elements;
        }

        public static IWebElement WaitForDisable(IWebElement element, int timeOut)
        {
            bool Condition() => !element.IsEnabled();
            WaitFor(Condition, "Element isn't disabled", timeOut);
            return element;
        }

        public static IWebElement WaitForEnable(IWebElement element, int timeOut)
        {
            bool Condition() => element.IsEnabled();
            WaitFor(Condition, "Element isn't enabled", timeOut);
            return element;
        }

        /// <summary>
        /// Ждем появления текста внутри элемента
        /// </summary>
        public static IWebElement WaitForText(IWebElement element, int timeOut)
        {
            bool Condition() => element.Text != "";
            WaitFor(Condition, "Text is not shown", timeOut);
            return element;
        }

        /// <summary>
        /// Ожидает появление любого текста в value элемента
        /// </summary>
        /// <param name="element">элемент</param>
        public static void UntilTextToBePresentInElementValue(IWebElement element)
        {
            _wait.Until(driver =>
            {
                try
                {
                    return !string.IsNullOrEmpty(element.GetAttribute("value"));
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Ожидает появление текста в value элемента
        /// </summary>
        /// <param name="locator">локатор</param>
        /// <param name="text">текст</param>
        public static void UntilTextToBePresentInElementValue(By locator, string text)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(locator, text));
        }

        /// <summary>
        /// Ожидает пока элемент исчезнет
        /// </summary>
        /// <param name="locator">локатор</param>
        public static void UntilInvisibilityOfElementLocated(By locator)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        /// <summary>
        /// Ожидает пока фрейм станет доступным и переключается на него
        /// </summary>
        /// <param name="locator">локатор</param>
        public static void UntilFrameToBeAvailableAndSwitchToIt(By locator)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));
        }

        /// <summary>
        /// Ожидает пока элемент не появится на странице
        /// </summary>
        /// <param name="locator">локатор</param>
        public static void UntilElementExists(By locator)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        /// <summary>
        /// Ожидает пока чекбокс не будет выбран
        /// </summary>
        /// <param name="locator">локатор</param>
        public static void UntilElementToBeSelected(By locator)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeSelected(locator));
        }

        /// <summary>
        /// Ожидает пока элемент не станет видимым
        /// </summary>
        /// <param name="locator">локатор</param>
        public static void UntilElementIsVisible(By locator)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        /// <summary>
        /// Ожидает пока элемент не станет кликабельным
        /// </summary>
        /// <param name="locator">локатор</param>
        public static void UntilElementToBeClickable(By locator)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public static IWebElement UntilElementToBeClickable(IWebElement element)
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        /// <summary>
        /// Ожидает пока в элементе не появится текст
        /// </summary>
        /// <param name="element">элемент</param>
        /// <param name="text">текст</param>
        public static void UntilTextToBePresentInElement(IWebElement element, string text)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, text));
        }

        /// <summary>
        /// Ожидает, пока переданные элементы окажутся видны на странице.
        /// Используется для реализации абстрактного метода WaitForLoading базового типа страницы CorePage.
        /// </summary>
        /// <param name="elements">элементы, видимость которых требуется дождаться</param>
        /// <param name="timeout">тайм-аут, по истечению которого будет выброшено исключение</param>
        public static void ForElementsToBeDisplayed(IEnumerable<IWebElement> elements, int? timeout = 5)
        {
            Until(_ => elements.All(element => element.Displayed), timeout);
        }

        /// <summary>
        /// Ожидает заданное условие
        /// </summary>
        /// <param name="condition">условие</param>
        /// <param name="timeout">тайм-аут</param>
        public static void Until(Func<IWebDriver, bool> condition, int? timeout = null)
        {
            var wait = timeout is null
                ? _wait
                : new WebDriverWait(Browser.Driver, TimeSpan.FromSeconds(timeout.Value));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(condition);
        }

        public static IWebElement WaitForFocus(IWebElement element, IWebDriver driver, int timeOut)
        {
            bool Condition() => element.IsFocused(driver);
            WaitFor(Condition, "Element isn't focused", timeOut);
            return element;
        }

        public static IAlert WaitForAlert(IWebDriver driver, int timeOut)
        {
            bool Condition() => IsAlertShown(driver);
            WaitFor(Condition, "Alert isn't shown", timeOut);
            return driver.SwitchTo().Alert();
        }

        private static bool IsAlertShown(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        // мои методы
        
        // //Ждем определенный интервал
        // public void WaitInterval(int interval = 2000)
        // {
        //     Thread.Sleep(interval);
        // }
        //
        // //Ждем определенный интервал
        // public void WaitIntervalWithDelay(int interval = 2000)
        // {
        //     Task.Delay(TimeSpan.FromMilliseconds(interval)).Wait();
        // }
        //
        // //Ждем появления элемента в Дом
        // public void WaitElement(By by)
        // {
        //     _wait.Until(d => IsElementExists(by));
        // }
        //
        // //Ждем появления видимости элемента Displayed
        // public void WaitElementDisplayed(By by)
        // {
        //     WaitElement(by);
        //     _wait.Until(d => IsElementDisplayed(by));
        // }
        //
        // //Ждем появления видимости элемента Displayed веб элемент
        // public void WaitElementDisplayed(IWebElement element)
        // {
        //     _wait.Until(d => IsElementDisplayed(element));
        // }
        //
        // //Ждем появления видимости элементов Displayed
        // public void WaitForElementsDisplayed(By by)
        // {
        //     _wait.Until(d => IsElementExistsAndDisplayed(by));
        // }
        //
        // //Ждем пока элементы не исчезнут из Дом
        // public void WaitElementNotExists(By selector)
        // {
        //     //_wait.Until(d => d.FindElements(selector).Count == 0);
        //     _wait.Until(d => IsElementsNotExist(selector));
        // }
        //
        // //Ждем исчезновения видимости элемента по локатору
        // public void WaitForElementNotDisplayed(By by)
        // {
        //     _wait.Until(d => IsElementsNotDisplayed(by));
        // }
        //
        // //Ждем исчезновения видимости элемента (веб элемент)
        // public void WaitForElementNotDisplayed(IWebElement elem)
        // {
        //     _wait.Until(d => IsElementNotDisplayed(elem));
        // }
        //
        // //Ждем исчезновения элемента Depricated
        // public void WaitElementInvisible(By by)
        // {
        //     _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        // }
        //
        // //Ждем загрузки страницы
        // public void WaitPageLoaded(By textSelector, string pageName, By selector1, By selector2, By selector3)
        // {
        //     _wait.Until(d => IsPageLoaded(textSelector, pageName, selector1, selector2, selector3));
        // }
        //
        // //Ждем точное число элементов
        // public void WaitForElementsCount(By by, int elementsCount)
        // {
        //     _wait.Until(d => FindElements(by).Count.Equals(elementsCount));
        // }
        //
        // //Ждем увеличения числа элементов
        // public void WaitForMoreElements(int countBefore, By by)
        // {
        //     _wait.Until(d => IsElementsIncreased(countBefore, by));
        // }
        //
        // //Ждем уменьшение числа элементов
        // public void WaitForLessElements(int countBefore, By by)
        // {
        //     _wait.Until(d => IsElementsDecreased(countBefore, by));
        // }
        //
        // //Ждем, что количество элементов увеличится на один
        // public void WaitForElementsIncreasedByOne(By by)
        // {
        //     _wait.Until(d => IsElementsIncreasedByOne(by));
        // }
        //
        // //Ждем, что количество элементов уменьшится на один
        // public void WaitForElementsDecreasedByOne(By by)
        // {
        //     _wait.Until(d => IsElementsDecreasedByOne(by));
        // }
        //
        // //Ждем изменение значения атрибута по локатору
        // public void WaitForAttributeContains(By element, string attribute, string value)
        // {
        //     _wait.Until(d => IsAttributeContainsValue(element, attribute, value));
        // }
        //
        // //Ждем изменение значения атрибута веб элемент
        // public void WaitForAttributeContains(IWebElement element, string attribute, string value)
        // {
        //     _wait.Until(d => IsAttributeContainsValue(element, attribute, value));
        // }
        //
        // //Ждем когда у элемента не будет данного значения атрибута по локатору
        // public void WaitForAttributeNotContains(By element, string attribute, string value)
        // {
        //     _wait.Until(d => IsAttributeNotContainsValue(element, attribute, value));
        // }
        //
        // //Ждем когда у элемента не будет данного значения атрибута по локатору веб элемент
        // public void WaitForAttributeNotContains(IWebElement element, string attribute, string value)
        // {
        //     _wait.Until(d => IsAttributeNotContainsValue(element, attribute, value));
        // }
        //
        // //Ждем появления видимости элементов Displayed с кастомным таймаутом, который настраивается в поле _customDriverWait
        // public void WaitElementDisplayedWithCustomWait(By by)
        // {
        //     _customDriverWait.Until(d => IsElementDisplayed(by));
        // }
        //
        // //Ждем появления алерта
        // public void WaitAlert()
        // {
        //     _wait.Until(d => IsAlertPresent());
        // }
        //
        // //Ждем появления нового окна, по умолчанию ждем появления нового второго окна, когда открыто только одно основное окно
        // public void WaitNewWindowOpen(int windowCount = 2)
        // {
        //     _wait.Until(d => _driver.WindowHandles.Count == windowCount);
        // }
        //
        // //Ждем закрытия нового окна, по умолчанию ждем закрытия второго нового второго окна, которое было открыто
        // public void WaitNewWindowClose(int windowCount = 1)
        // {
        //     _wait.Until(d => _driver.WindowHandles.Count == windowCount);
        // }
        //
        // //Ждем появления нового окна, когда открыто много окон
        // public void WaitNewWindowOpenWhenManyOpen(int windowCount)
        // {
        //     _wait.Until(d => _driver.WindowHandles.Count == windowCount + 1);
        // }
        //
        // //Ждем закрытия нового окна, когда открыто много окон
        // public void WaitNewWindowCloseWhenManyOpen(int windowCount)
        // {
        //     _wait.Until(d => _driver.WindowHandles.Count == windowCount - 1);
        // }
        //
        // //Ждем появления текста в элементе
        // public void WaitTextAppear(By by, string text)
        // {
        //     _wait.Until(d => IsTextContains(by, text));
        // }
        //
        // //Ждем загрузки страницы
        // public void WaitForLoadingPage()
        // {
        //     _wait.Until(
        //         d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        // }
        //
        //
        // ////
        // public void WaitForFrame(By by)
        // {
        //     _wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(by));
        // }
        
    }
}