using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;

namespace Internet_Spider
{
    public class Download
    {
        public void download(List<string> title, List<string> text, int count)
        {
            using (NewsContext db = new NewsContext())
            {
                if (db.Items.Count() == 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        News n = new News { Title = title[i], Text = text[i] };

                        db.Items.Add(n);
                        db.SaveChanges();
                    }
                }
                else {
                    var context = db.Items.ToList();
                    for (int i = 0; i < count; i++)
                    {
                        foreach (var content in context)
                        {
                            if (content.Title == title[i]) break;
                            else
                            {
                                News n = new News { Title = title[i], Text = text[i] };

                                db.Items.Add(n);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                MessageBox.Show("Новости занесены в Базу Данных!");
            }
            
        }
    }
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
    public class NewsContext : DbContext
    {
        public NewsContext() : base("DefaultConnection")
        {
        }
        public DbSet<News> Items { get; set; }
    }
}
