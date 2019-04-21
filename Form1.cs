using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace Internet_Spider
{
    public partial class Form1 : Form
    {    
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "Подождите, идет процесс закачки новостей";
            label2.Visible = true;

            IWebDriver Browser = new PhantomJSDriver();
            int count = Int32.Parse(textBox3.Text);
            Browser.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMinutes(3));
            Browser.Navigate().GoToUrl("http://news.tut.by");

            TextNews textObj = new TextNews() { Browser = Browser};

            List<IWebElement> res = textObj.find(".l-columns.air-30 .news-entry.small.pic.ni a:not(.rubric-title__link) span .entry-head");
                 
            if(res.Count < count) { count = res.Count; }

            List<string> title = textObj.translateStrTitle(res);
            List<string> text = textObj.translateStrText(count);
                
            for (int a = 0; a < count; a++)
            {
                textBox1.AppendText("\n\t"+title[a] + "\n");
                textBox1.AppendText("\n\t"+text[a] + "\n");
            }

            Browser.Quit();

            label2.Text = "Подождите, идет процесс сохранения новостей";

            Download p = new Download();
            p.download(title, text, count);
            
            label2.Visible = false;

        }
                
        private void button1_Click(object sender, EventArgs e)
        {
            using(NewsContext conn = new NewsContext())
            {
                if (conn.Items.Count() == 0) { MessageBox.Show("База Данных не содержит новостей"); }
                else
                {
                    var context = conn.Items.ToList();
                    foreach (var text in context)
                    {
                        textBox2.AppendText(text.ID + ")\t" + text.Title + "\n\t" + text.Text + "\n");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (NewsContext conn = new NewsContext())
            {
                using (var sw = File.AppendText("Test.txt"))
                {                    
                    var context = conn.Items.ToList();
                    foreach (var text in context)
                    {
                        sw.WriteLine(text.ID + ")\t" + text.Title + "\n" + text.Text + "\n");
                    }
                }
                MessageBox.Show("Запись в файл прошла успешно!");
            }
        }
    }
}
