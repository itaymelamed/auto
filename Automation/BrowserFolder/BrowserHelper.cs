﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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

        public bool WaitForElement(IWebElement el, string elName, int timeOut = 60, bool throwEx = true)
        {
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
            catch
            {
                if (throwEx)
                    throw new Exception($"Could not find element: {elName}.");
                return false;
            }

            return true;
        }


        public void WaitForElementDiss(IWebElement el, int timeOut = 30)
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

        public void SetText(IWebElement el, string text)
        {
            MoveToEl(el);
            el.SendKeys(text);
        }

        public void DragElement(IWebElement drag, IWebElement drop)
        {
            var error = string.Empty;

            WaitUntillTrue(() => {
                try
                {
                    WaitForElement(drag, nameof(drag), 60, false);
                    WaitForElement(drag, nameof(drop), 60, false);
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
            }, error, 120);
        }

        public void ScrollToEl(IWebElement el)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", el);
        }

        public void ExecutUntill(IWebElement el,Func<IWebElement, bool> func, int timeOut = 30)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
            wait.Until(d => {
                try
                {
                    MoveToEl(el);
                    return func(el);
                }
                catch
                {
                    return false;
                }
            });
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
                    catch
                    {
                        return false;
                    }
                });

                return true;
            }
            catch
            {
                if (throwEx)
                    throw new Exception($"{ex}");
                return false;
            }
        }

        public bool ExecutUntillTrue(Func<IWebElement> func, string ex = "", int timeOut = 30, bool throwEx = true)
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

                return true;
            }
            catch
            {
                if (throwEx)
                    throw new Exception(ex);
                return false;
            }
        }

        public void ClickJavaScript(IWebElement el)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", el);
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
                    throw new Exception($"Failed to click on element {elName}. Error:{error}");
            }
        }

        public void Hover(IWebElement el)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(el).Perform();
        }

        public void WaitUntill(IWebElement el, Func<IWebElement, bool> func, int timeOut = 30)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeOut));
            wait.Until(d => {
                try
                {
                    MoveToEl(el);
                    return func(el) == true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public void WaitUntill(List<IWebElement> els, Func<List<IWebElement>, bool> func, int timeOut = 30)
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

        public IWebElement FindElement(By by, string elName, int timeOut = 60)
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
                throw new Exception($"Element {elName} wasn't exsist.");
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
                    throw new Exception($"Element {elName} was not clickble.");
                return false;
            }
        }

        public bool WaitForUrlToChange(string reqUrl,int timeOut = 60, bool throwEx = true)
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
                    throw new Exception($"Expected Url was: {reqUrl} but actual:{_driver.Url}");
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
            });
        }

        public bool CheckAttribute(IWebElement el)
        {
            try
            {
                return el.GetAttribute("checked") == "true";
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool ValidateElsDissabled(List<IWebElement> els)
        {
            return WaitUntillTrue(() => els.All(t => !t.Enabled), "", 60, false);
        }
    }
}
