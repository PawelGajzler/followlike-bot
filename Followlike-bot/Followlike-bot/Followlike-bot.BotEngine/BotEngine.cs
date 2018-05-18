using Followlike_bot.BotEngine.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Followlike_bot.BotEngine
{
    public class BotEngine : IBotEngine
    {
        private CancellationTokenSource cancellationTokenSource;
        private Random delayGenerator;

        public BotEngine()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            this.delayGenerator = new Random();
        }

        public bool Stop()
        {
            cancellationTokenSource.Cancel();
            return true;
        }

        public Task<bool> Start()
        {
            return Task.Factory.StartNew<bool>(() =>
            {
                using(IWebDriver chromeDriver=new ChromeDriver())
                {
                    chromeDriver.Navigate().GoToUrl("https://www.followlike.net");
                    Task.Delay(15000);
                    IWebElement link = chromeDriver.FindElement(By.PartialLinkText("ys"));
                    if (link == null)
                    {
                        return false;
                    }
                    link.Click();
                    Task.Delay(delayGenerator.Next(3000, 5000));
                    IReadOnlyCollection<IWebElement> coinButtons;
                    do
                    {
                        if (cancellationTokenSource.IsCancellationRequested)
                        {
                            break;
                        }
                        coinButtons = chromeDriver.FindElements(By.CssSelector(".btn-group-sm>.btn, .btn-sm"));
                        foreach(IWebElement element in coinButtons)
                        {
                            Task.Delay(delayGenerator.Next(1000, 2000));
                            element.Click();
                            foreach(string handle in chromeDriver.WindowHandles)
                            {
                                chromeDriver.SwitchTo().Window(handle);
                                IWebElement webElement = chromeDriver.FindElement(By.Id("subscribe-button"));
                                if (webElement != null)
                                {
                                    Task.Delay(delayGenerator.Next(1000, 3000));
                                    webElement.Click();
                                    chromeDriver.Close();
                                    break;
                                }
                            }
                        }
                        Task.Delay(delayGenerator.Next(1000, 2000));
                        IWebElement loadMoreButton = chromeDriver.FindElement(By.CssSelector("button, html input[type=button], input[type=reset], input[type=submit]"));
                        loadMoreButton.Click();
                        Task.Delay(delayGenerator.Next(3000, 5000));
                    } while (coinButtons.Count <= 0);
                }
                return true;
            }, cancellationTokenSource.Token);
        }
    }
}
