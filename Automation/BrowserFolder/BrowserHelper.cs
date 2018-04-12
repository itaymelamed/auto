using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.TestsFolder;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Automation.BrowserFolder
{
    public class BrowserHelper
    {
        readonly IWebDriver _driver;

        public BrowserHelper(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool WaitForElement(IWebElement el, string elName, int timeOut = 30, bool throwEx = true)
        {
            var error = string.Empty;

            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => {
                    try
                    {
                        MoveToEl(el);
                        return el.Displayed;
                    }
                    catch
                    {
                        return false;
                    }
                });

                MoveToEl(el);
            }
            catch(Exception e)
            {
                if (throwEx)
                    throw new NUnit.Framework.AssertionException($"Could not find element: {elName}. Error: {e.Message}.");
                return false;
            }

            return true;
        }

        public void WaitForElementDiss(IWebElement el, int timeOut = 30)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => {
                    try
                    {
                        return !el.Displayed;
                    }
                    catch
                    {
                        return true;
                    }
                });
            }
            catch
            {
                throw new NUnit.Framework.AssertionException($"Element was not dissapeared. timeOut.");
            }
        }

        public void SetText(IWebElement el, string text)
        {
            WaitUntillTrue(() => 
            {
                MoveToEl(el);
                el.SendKeys(text);
                return true;
            });
        }

        public void DragElement(IWebElement drag, IWebElement drop)
        {
            var error = string.Empty;

            WaitUntillTrue(() => {
                try
                {
                    WaitForElement(drag, nameof(drag), 30);
                    WaitForElement(drop, nameof(drop), 30);
                    Actions ac = new Actions(_driver);
                    ac.DragAndDrop(drag, drop);
                    ac.Build().Perform();
                    return true;
                }
                catch(Exception e)
                {
                    error = e.Message;
                    return false;
                }
            }, error, 20);
        }

        public void ScrollToEl(IWebElement el)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true)", el);
        }

        public void ScrollToBottom()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public void ScrollToTop()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, 0)");
        }

        public bool WaitUntillTrue(Func<bool> func, string ex = "", int timeOut = 30, bool throwEx = true)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => {
                    try
                    {
                        return func();
                    }
                    catch(Exception e)
                    {
                        ex += e.Message; 
                        return false;
                    }
                });

                return true;
            }
            catch(Exception e)
            {
                if (throwEx)
                    throw new NUnit.Framework.AssertionException($"{ex}. {e.Message}");
                return false;
            }
        }

        public IWebElement ExecutUntillTrue(Func<IWebElement> func, string ex = "", int timeOut = 30, bool throwEx = true)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => {
                    try
                    {
                        func();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });

                return func();
            }
            catch(Exception e)
            {
                if(throwEx)
                    throw new NUnit.Framework.AssertionException(e.Message);
                return null;
            }
        }

        public void ClickJavaScript(IWebElement el)
        {
            try
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", el);
            }
            catch(Exception e)
            {
                throw new NUnit.Framework.AssertionException(e.Message);
            }
        }

        public void Click(IWebElement el, string elName, int timeOut = 30, bool throwex = true)
        {
            var error = "";
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => {
                    try
                    {
                        el.Click();
                        MoveToEl(el);
                        return true;
                    }
                    catch(Exception e)
                    {
                        error = e.Message;
                        Thread.Sleep(1000);
                        return false;
                    }
                });
            }
            catch
            {
                if(throwex)
                    throw new NUnit.Framework.AssertionException($"Failed to click on element {elName}. Error:{error}");
            }
        }

        public void Hover(IWebElement el)
        {
            ExecuteUntill(() => 
            {
                Actions action = new Actions(_driver);
                action.MoveToElement(el).Perform();  
            });
        }

        public void ExecuteUntill(Action action, int timeOut = 40)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => {
                    try
                    {
                        action();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            catch
            {
                throw new NUnit.Framework.AssertionException($"Time Out.");
            }
        }

        public void WaitUntill(List<IWebElement> els, Func<List<IWebElement>, bool> func, int timeOut = 30)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => {
                    try
                    {
                        return func(els) == true;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            catch
            {
                throw new NUnit.Framework.AssertionException($"Time Out.");
            }
        }

        public IWebElement FindElement(By by, string elName, int timeOut = 30)
        {
            IWebElement el = null;

            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => {
                    try
                    {
                        el = _driver.FindElement(by);
                        MoveToEl(el);
                        return el.Displayed;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            catch
            {
                throw new NUnit.Framework.AssertionException($"Element {elName} wasn't exsist.");
            }

            return el;
        }

        public void MoveElToFocus(IWebElement el)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(el);
            el.Click();
        }

        public void MoveToEl(IWebElement el)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(el);
        }

        public bool IsClickble(IWebElement el, string elName, bool throwex = true)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(d => {
                    el.Click();
                    return true;
                });

                return true;
            }
            catch
            {
                if (throwex)
                    throw new NUnit.Framework.AssertionException($"Element {elName} was not clickble.");
                return false;
            }
        }

        public bool WaitForUrlToChange(string reqUrl,int timeOut = 30, bool throwEx = true)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(d => d.Url.ToLower() == reqUrl.ToLower());
                return true;
            }
            catch
            {
                if (throwEx)
                    throw new NUnit.Framework.AssertionException($"Expected Url was: {reqUrl} but actual:{_driver.Url}");
                return false;
            }
        }

        public void SelectFromDropDown(IWebElement el, string value)
        {
            SelectElement select = new SelectElement(el);
            select.SelectByValue(value);
        }

        public void ConfirmAlarem()
        {
            WaitUntillTrue(() => 
            {
                IAlert alert = _driver.SwitchTo().Alert();
                alert.Accept();
                return true;
            }, "", 10, false);                                  
        }

        public bool CheckAttribute(IWebElement el)
        {
            try
            {
                return el.GetAttribute("checked") == "true";
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateElsDissabled(List<IWebElement> els)
        {
            return WaitUntillTrue(() => els.All(t => !t.Enabled), "", 30, false);
        }

        public bool RefreshUntillQuery(Func<bool> func, string err = "", int timeOut = 30)
        {
            WaitUntillTrue(() => 
            {
                var url = _driver.Url;
                _driver.Navigate().GoToUrl(url + "?test=test");
                return WaitUntillTrue(func, "", 10);
                
            }, err, timeOut);

            return true;
        }

        public bool RefreshUntill(Func<bool> func, string err = "", int timeOut = 30)
        {
            WaitUntillTrue(() =>
            {
                Base.MongoDb.UpdateSteps("Refreshing Page");
                _driver.Navigate().Refresh();
                return func();

            }, err, timeOut);

            return true;
        }

        public void ClickEsc()
        {
            _driver.FindElement(By.XPath("body")).SendKeys(Keys.Escape);
        }

        public void MoveToIframe(string frameName)
        {
            _driver.SwitchTo().Frame(frameName);
        }

        public void ClickByPoint(IWebElement el, int x, int y)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(el, x, y).Click();
        }

        public void SelectDate(string day)
        {
            Click(_driver.FindElement(By.LinkText(day)), day);
        }

        public IWebElement FindElement(string locator)
        {
            return _driver.FindElement(By.CssSelector(locator));
        }
    }
}