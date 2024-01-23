using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFramework
{
    public static class WebElementExtensions //todo вэйтеры добавил. осталось добавить методы валидации и действий из своего проекта, а потом сравнить что где есть и выбрать лучшее
    {
		/// <summary>
		/// Дефолтный таймаут
		/// </summary>
		private const int DefaultTimeOutMilliseconds = 10000;

        /// <summary>
        /// Ждём, пока элемент станет видимым на странице.
        /// TimeoutException, если не дождались
        /// </summary>
        public static IWebElement WaitForVisible(this IWebElement element, int timeOut= DefaultTimeOutMilliseconds)
		{
			return Wait.WaitForVisible(element, timeOut);
		}
        
        /// <summary>
        /// Ждём, пока у элемента обновится атрибут
        /// TimeoutException, если не дождались
        /// </summary>
        public static IWebElement WaitForAttribute(this IWebElement element, string attribute, string value, int timeOut = DefaultTimeOutMilliseconds)
        {
	        return Wait.WaitForAttribute(element, attribute, value, timeOut);
        }
        
        /// <summary>
        /// Ждём, пока у элемента обновится текст
        /// TimeoutException, если не дождались
        /// </summary>
        public static IWebElement WaitForTextUpdate(this IWebElement element, string text, int timeOut = DefaultTimeOutMilliseconds)
        {
	        return Wait.WaitForTextUpdate(element, text, timeOut);
        }
        
        /// <summary>
        /// Ждём, пока элемент станет доступным на странице.
        /// TimeoutException, если не дождались
        /// </summary>
        public static IWebElement WaitForEnable(this IWebElement element, int timeOut= DefaultTimeOutMilliseconds)
        {
	        return Wait.WaitForEnable(element, timeOut);
        }
        
        /// <summary>
        /// Ждём, пока элемент станет не доступным на странице.
        /// TimeoutException, если не дождались
        /// </summary>
        public static IWebElement WaitForDisable(this IWebElement element, int timeOut= DefaultTimeOutMilliseconds)
        {
	        return Wait.WaitForDisable(element, timeOut);
        }

        /// <summary>
        /// Ждём, пока элемент не пропадет из DOM
        /// </summary>
        public static void WaitForNotExists(this IWebElement element, int timeout = 10000)
        {
            bool Condition() => !Exists(element);
            Wait.WaitFor(Condition, "Element didn't fade from DOM", timeout);
        }

		/// <summary>
		/// Ждём, когда элемент появится в DOM
		/// </summary>
		public static IWebElement WaitForExists(this IWebElement element, int timeout = 10000)
		{
			bool Condition() => Exists(element);
			Wait.WaitFor(Condition, "Element still doesn't exist in DOM", timeout);
			return element;
		}

		/// <summary>
		/// Ждём, пока в списке появится хотя бы один элемент
		/// </summary>
		public static IList<IWebElement> WaitForExists(this IList<IWebElement> elements, int timeOut = DefaultTimeOutMilliseconds)
		{
			return Wait.WaitForExists(elements, timeOut);
		}

        /// <summary>
        /// Ждём, пока элемент скроется со страницы (но останестя в DOM)
        /// </summary>
        public static void WaitForInvisible(this IWebElement element, int timeout = 20000)
        {
            bool Condition() => !element.IsDisplayed();
            Wait.WaitFor(Condition, "Element still visible on page", timeout);
        }

        /// <summary>
        /// ждем, когда внутри элемента появится текст
        /// </summary>
        public static IWebElement WaitForText(this IWebElement element, int timeOut = DefaultTimeOutMilliseconds)
        {
	        return Wait.WaitForText(element, timeOut);
        }
        
        /// <summary>
        /// Ждём, пока элемент станет доступным на странице.
        /// TimeoutException, если не дождались
        /// </summary>
        public static IWebElement WaitForFocus(
	        this IWebElement element, IWebDriver driver, int timeOut= DefaultTimeOutMilliseconds)
        {
	        return Wait.WaitForFocus(element, driver, timeOut);
        }

        /// <summary>
        /// Получаем список позиций в выпадающем списке
        /// </summary>
        public static IList<string> GetDropDownList(this IWebElement dropdown)
		{
			IList<string> positions = new List<string>();
			IList<IWebElement> posElements = dropdown.FindElements(By.TagName("option"));
			foreach (IWebElement option in posElements)
			{
				option.WaitForVisible();
				positions.Add(option.Text);
			}
			return positions;
		}

        private static IWebElement GetElement(this IList<IWebElement> elements, string attribute, string value)
        {
	        foreach (var element in elements)
	        {
		        if (element.GetAttribute(attribute)?.ToLower() == value.ToLower())
		        {
			        return element;
		        }
	        }
	        throw new NoSuchElementException($"didn't find element with {value} in {attribute}");
        }
        
		/// <summary>
		/// Среди списка элементов ищем нужный по тексту
		/// Если не нашли элемент, кидаем ArgumentException
		/// </summary>
		public static IWebElement GetElementByText(this IList<IWebElement> elements, string text)
		{
			return elements.GetElement("innerText", text);
		}

		public static IWebElement GetElementByHref(this IList<IWebElement> elements, string href, long timeout = 10000)
		{
			var sw =  new Stopwatch();
			sw.Start();
			
			while (sw.ElapsedMilliseconds < timeout)
			{
				try
				{
					return elements.GetElement("href", href);
				}
				catch (NoSuchElementException)
				{
					Thread.Sleep(500);
				}
			}
			sw.Stop();
			return null;
		}

		public static IWebElement GetElementById(this IList<IWebElement> elements, string id)
		{
			return elements.GetElement(attribute: "id", value: id);
		}
		
		/// <summary>
		/// Среди списка элементов ищем нужный по тексту и возвращаем его индекс
		/// Если не нашли элемент, кидаем NoElementException
		/// </summary>
		public static int GetElementIndexByText(this IList<IWebElement> elements, string text)
		{
			foreach (IWebElement element in elements)
			{
				if (element.GetAttribute("innerText").ToLower() == text.ToLower())
				{
					return elements.IndexOf(element);
				}
			}
			throw new NoSuchElementException();
		}

		/// <summary>
		/// Проверка наличия элемента в списке по тексту
		/// </summary>
		public static bool IsElementExistInList(this IList<IWebElement> elements, string text)
		{
			try
			{
				elements.GetElementByText(text);
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		public static void DragAndDrop(this IWebElement source, IWebElement target)
		{
			Actions chain = new Actions(((IWrapsDriver)source).WrappedDriver);
			chain.DragAndDrop(source, target).Perform();
		}

		public static void RightClick(this IWebElement element)
		{
			Actions chain = new Actions(((IWrapsDriver)element).WrappedDriver);
			chain.ContextClick(element).Perform();
		}

		/// <summary>
		/// Для элементов, созданных через PageFactory
		/// </summary>
		public static void RightClick(this IWebElement element, IWebDriver driver)
		{
			Actions chain = new Actions(driver);
			chain.ContextClick(element).Perform();
		}

		public static void DoubleClick(this IWebElement element)
		{
			Actions chain = new Actions(((IWrapsDriver)element).WrappedDriver);
			chain.DoubleClick(element).Perform();
		}

		/// <summary>
		/// Для элементов, созданных через PageFactory
		/// </summary>
		public static void DoubleClick(this IWebElement element, IWebDriver driver)
		{
			Actions chain = new Actions(driver);
			chain.DoubleClick(element).Perform();
		}

		/// <summary>
		/// Клик по относительным координатам элемента
		/// </summary>
		public static void Click(this IWebElement element, int offsetX, int offsetY)
		{
			var chain = new Actions(((IWrapsDriver)element).WrappedDriver);
			chain.MoveToElement(element, offsetX, offsetY).Click().Perform();
		}
		
		/// <summary>
		/// Клик по относительным координатам элемента, созданного через PageFactory
		/// </summary>
		public static void Click(this IWebElement element, IWebDriver driver, int offsetX, int offsetY)
		{
			var chain = new Actions(driver);
			chain.MoveToElement(element, offsetX, offsetY).Click().Perform();
		}
		
		/// <summary>
		/// Проверка на наличие класса у элемента
		/// </summary>
		public static bool HasClass(this IWebElement element, string cssclass)
		{
			return element.GetAttribute("class").Contains(cssclass);
		}

		/// <summary>
		/// Проверка на наличие стиля в атрибуте style
		/// </summary>
		public static bool HasStyle(this IWebElement element, string value)
		{
			return element.GetAttribute("style").Contains(value);
		}

		/// <summary>
		/// Проверка на наличие аттрибута у элемента
		/// </summary>
		public static bool HasAttribute(this IWebElement element, string attribute)
		{
			return element.GetAttribute(attribute) != null;
		}

		/// <summary>
		/// Возвращает список текстовых значений списка элементов
		/// </summary>
		public static IList<string> GetValues(this IList<IWebElement> elements)
		{
			IList<string> values = new List<string>();
			foreach (IWebElement element in elements)
			{
				values.Add(element.Text);
			}
			return values;
		}

		/// <summary>
		/// Проверка существования элемента
		/// </summary>
		public static bool Exists(this IWebElement element)
		{
			try
			{
				if (element == null) { return false; }
				if (element.Displayed) { return true; }
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
			catch (StaleElementReferenceException)
			{
				return false;
			}
			catch (TargetInvocationException) //иногда в них заворачиваются StaleElementReferenceException 
			{
				return false;
			}
		}

        public static bool IsDisplayed(this IWebElement element)
        {
	        try
	        {
		        return element.Displayed;
	        }
	        catch (NoSuchElementException)
	        {
		        return false;
	        }
	        catch (StaleElementReferenceException)
	        {
		        return false;
	        }
	        catch (TargetInvocationException) //иногда в них заворачиваются StaleElementReferenceException 
	        {
		        return false;
	        }
	        catch (NullReferenceException)
	        {
		        return false;
	        }
        }
        
        public static bool IsEnabled(this IWebElement element)
        {
	        try
	        {
		        return element.Enabled;
	        }
	        catch (NoSuchElementException)
	        {
		        return false;
	        }
	        catch (StaleElementReferenceException)
	        {
		        return false;
	        }
	        catch (ElementNotInteractableException)
	        {
		        return false;
	        }
	        catch (TargetInvocationException) //иногда в них заворачиваются StaleElementReferenceException 
	        {
		        return false;
	        }
        }

        /// <summary>
        /// Изменение аттрибута элемента
        /// </summary>
        public static void SetAttribute(this IWebElement element, IWebDriver driver, string attribute, string value)
		{
			var js = (IJavaScriptExecutor) driver;
			js.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);",
				element, attribute, value);
		}
        
        /// <summary>
        /// Изменение аттрибута элемента
        /// </summary>
        public static void RemoveAttribute(this IWebElement element, IWebDriver driver, string attribute)
        {
	        var js = (IJavaScriptExecutor) driver;
	        js.ExecuteScript("arguments[0].removeAttribute(arguments[1]);", element, attribute);
        }
        
        /// <summary>
        /// Клик по элементу
        /// </summary>
        public static void Click(this IWebElement element, IWebDriver driver)
        {
	        var jse = (IJavaScriptExecutor) driver;
	        jse.ExecuteScript("arguments[0].click()", element);
        }

        /// <summary>
		/// Возвращает высоту элемента в пикселях
		/// </summary>
		public static int GetHeight(this IWebElement element)
		{
			string cssHeight = element.GetCssValue("height");
			//удаляем 'px' из строки
			string height = cssHeight.Substring(0, cssHeight.Length - 2);
			return Convert.ToInt32(height);
		}

		/// <summary>
		/// Возвращает ширину элемента в пикселях
		/// </summary>
		public static int GetWidth(this IWebElement element)
		{
			string cssWidth = element.GetCssValue("width");
			//удаляем 'px' из строки
			string width = cssWidth.Substring(0, cssWidth.Length - 2);
			return Convert.ToInt32(width);
		}
		
		public static void ClearAndEnterValue(this IWebElement element, string value)
		{
			element.WaitForVisible().Clear();
			element.SendKeys(value);
		}
		
		/// <summary>
		/// Имитация ручного набора текста с задержкой посимвольно
		/// </summary>
		public static void EnterValueBySymbols(this IWebElement element, string value, int timeout=500)
		{
			element.WaitForVisible();
			element.Clear();
			
			foreach (var letter in value)
			{
				Thread.Sleep(timeout);
				element.SendKeys(letter.ToString());
			}
		}

		public static bool IsFocused(this IWebElement element, IWebDriver driver)
		{
			return element.Equals(driver.SwitchTo().ActiveElement());
		}

		/// <summary>
		/// скроллит ниже этого элемента (элемент становится невидим)
		/// </summary>
		public static void ScrollToElement(this IWebElement element, IWebDriver driver)
		{
			driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", element);
		}

		/// <summary>
		/// выбираем элемент по названию (текста кнопки) из выпадающего списка
		/// </summary>
		public static void SelectFromDropDown(this IWebElement element, IWebDriver driver, string text)
		{
			element.Click();
			driver.FindElement(By.XPath($"//*[contains(text(),'{text}')]")).WaitForVisible().Click();
		}
		
		/// <summary>
		/// Выбираем элемент из выпадающего списка средствами селениум без использования курсора
		/// </summary>
		/// <param name="dropDown">элемент список</param>
		/// <param name="value">значение элемента из списка</param>
		public static void SelectDropdownValue(this IWebElement dropDown, string value)
		{
			var elementToDropdown = new SelectElement(dropDown);
			elementToDropdown.SelectByValue(value);
		}
		
		/// <summary>
		/// Выбираем элемент из выпадающего списка средствами селениум без использования курсора
		/// </summary>
		/// <param name="dropDown">элемент список</param>
		/// <param name="text">значение элемента из списка</param>
		public static void SelectDropdownText(this IWebElement dropDown, string text)
		{
			var elementToDropdown = new SelectElement(dropDown);
			elementToDropdown.SelectByText(text);
		}

		/// <summary>
		/// Ждём, пока alert появится на странице.
		/// TimeoutException, если не дождались
		/// </summary>
		public static IAlert WaitForAlert(IWebDriver driver, int timeOut = DefaultTimeOutMilliseconds)
		{
			return Wait.WaitForAlert(driver, timeOut);
		}
		
		/// <summary>
		/// Получаем индекс строки таблицы с помощью текста ячейки
		/// </summary>
		public static int GetRowIndexByCellText(this IWebElement element, string text, int cellIndex)
		{
			IList<IWebElement> tableRow = element.FindElements(By.TagName("tr"));

			foreach (var row in tableRow)
			{
				IList<IWebElement> td = row.FindElements(By.TagName("td"));

				if (td.Count > 0 && td[cellIndex].GetAttribute("innerText").StartsWith(text))
				{
					return tableRow.IndexOf(row);
				}
			}

			throw new NoSuchElementException();
		}
		
		public static IWebElement FindByXpath(
			this IWebElement webElement, IWebDriver driver, string xpath, long timeout=10000)
		{
			webElement.WaitForEnable();
			//TODO: придумать замену слипу
			Thread.Sleep(500);
			webElement.Click();
			IWebElement element = null;

			var sw = new Stopwatch();
			sw.Start();
			while (sw.ElapsedMilliseconds < timeout)
			{
				try
				{
					element = driver.FindElement(By.XPath(xpath));
					break;
				}
				catch (Exception)
				{
					Thread.Sleep(500);
				}
			}
			
			return element;
		}
		
		public static string GetColor(this IWebElement element)
		{
			return element.GetCssValue("color").ConvertToHex();
		}

		private static string ConvertToHex(this string color)
		{
			color = color[..color.LastIndexOf(")", StringComparison.Ordinal)][
				(color.LastIndexOf("(", StringComparison.Ordinal) + 1)..];
			var strings = color.Split(",");

			var hex = ColorTranslator.ToHtml(
				Color.FromArgb(Convert.ToInt32(strings[3]),
					Convert.ToInt32(strings[0]),
					Convert.ToInt32(strings[1]),
					Convert.ToInt32(strings[2])));

			return hex;
		}

		public static IWebElement WaitCheckBoxToBeSelected(this IWebElement element, int timeout = 3)
		{
			Wait.Until(_ => element.Selected, timeout);
			return element;
		}
    }
}