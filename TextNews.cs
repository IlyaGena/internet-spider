using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Internet_Spider
{
    public class TextNews
    {

        public IWebDriver Browser;

        public List<IWebElement> find(string css)
        {
            return Browser.FindElements(By.CssSelector(css)).ToList();
        }

        public List<string> translateStrTitle(List<IWebElement> title)
        {
            int a = 0;
            List<string> titleNews = new List<string>();
            for (int i = 0; i < title.Count; i++)
            {
                a++;
                titleNews.Add($"\n"+title[i].Text+"\n \n");
            }
            return titleNews;
        }

        public List<string> translateStrText(int b)
        {
            List<IWebElement> newsWebText;
            string textNews = "";
            List<string> text = new List<string>();
            for (int i = 0; i < b; i++)
            {
                Browser.Navigate().GoToUrl("http://news.tut.by");
                textNews = "";
                List<IWebElement> title = find(".l-columns.air-30 .news-entry.small.pic.ni a:not(.rubric-title__link) span .entry-head");
                title[i].Click();
                List<IWebElement> time = find("a time");
                newsWebText = find("#article_body p");
                for (int a = 0; a < newsWebText.Count(); a++)
                {
                    textNews += ("\n\t" + newsWebText[a].Text + "\n");
                }
                text.Add(textNews+"\n\t Дата:"+time[0].Text);
            }
            return text;
        }
        
    }
}
