using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class listChapter : Form
    {
        private int site;
        private List<String> chapters = new List<string>();
        private String path; 

        public listChapter(List<String> c, int s, String p)
        {
            InitializeComponent();

            chapters = c;
            site = s;
            path = p;

            foreach(String str in chapters)
            {
                this.checkedListBox1.Items.Add(str, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void mrDownloader(List<String> c, String path)
        {
            WebClient w = new WebClient();
            List<String> page = new List<string>();
            Regex url = new Regex("(<option value=\")[-./A-Z_a-z0-9]*(\">)");
            Regex image = new Regex("(src=\"http:)[-./A-Z_a-z0-9]*");

            for (int i = 0; i < c.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    if (c.ElementAt(i).Contains(".html"))
                    {
                        String chapter = c.ElementAt(i).Remove(0, c.ElementAt(i).LastIndexOf("/") + 1);
                        chapter = chapter.Remove(chapter.LastIndexOf("."));
                        Directory.CreateDirectory(path + "\\" + chapter);
                        page.Add("http://www.mangareader.net" + c.ElementAt(i));
                        String src = w.DownloadString("http://www.mangareader.net" + c.ElementAt(i));
                        foreach (Match m in url.Matches(src))
                        {
                            String tmp = m.ToString().Remove(m.ToString().LastIndexOf("\""));
                            tmp = tmp.Remove(0, tmp.LastIndexOf("\"") + 1);

                            page.Add("http://www.mangareader.net" + tmp);
                        }

                        for (int j = 0; j < page.Count; j++)
                        {
                            String urli = "";
                            String src2 = w.DownloadString(page.ElementAt(j));
                            Match m = image.Match(src2);
                            urli = m.ToString().Remove(0, 5);
                            w.DownloadFile(urli, path + "\\" + chapter + "\\" + j.ToString() + ".jpg");
                        }

                        page.Clear();
                    }
                    else
                    {
                        Directory.CreateDirectory(path + "\\" + c.ElementAt(i).Remove(0, c.ElementAt(i).LastIndexOf("/") + 1));
                        page.Add("http://www.mangareader.net" + c.ElementAt(i));
                        String src = w.DownloadString("http://www.mangareader.net" + c.ElementAt(i));
                        foreach (Match m in url.Matches(src))
                        {
                            String tmp = m.ToString().Remove(m.ToString().LastIndexOf("\""));
                            tmp = tmp.Remove(0, tmp.LastIndexOf("\"") + 1);

                            page.Add("http://www.mangareader.net" + tmp);
                        }

                        for (int j = 0; j < page.Count; j++)
                        {
                            String urli = "";
                            String src2 = w.DownloadString(page.ElementAt(j));
                            Match m = image.Match(src2);
                            urli = m.ToString().Remove(0, 5);
                            w.DownloadFile(urli, path + "\\" + c.ElementAt(i).Remove(0, c.ElementAt(i).LastIndexOf("/") + 1) + "\\" + j.ToString() + ".jpg");
                        }

                        page.Clear();
                    }
                }
            }

            MessageBox.Show("Téléchargements terminés !", "Terminé", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmDownloader(List<String> c, String path)
        {
            WebClient w = new WebClient();
            List<String> page = new List<string>();
            Regex url = new Regex("<input type=\"button\" id=\"nextbutton\" onclick=\"javascript:window.location='/[-A-Z_a-z0-9]*/[-A-Z_a-z0-9]*/[-A-Z_a-z0-9]*");
            Regex image = new Regex("/files/images/[-_A-Za-z0-9]*/[0-9]*/[-A-Z_a-z0-9]*.jpg");

            for (int i = 0; i < c.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    String tmp = "";
                    String chapter = c.ElementAt(i).Remove(c.ElementAt(i).Length - 1);
                    chapter = chapter.Remove(0, chapter.LastIndexOf("/") + 1);
                    Directory.CreateDirectory(path + "\\" + chapter);
                    String src = w.DownloadString(c.ElementAt(i));
                    
                    Match m = url.Match(src);
                    tmp = m.ToString().Remove(0, m.ToString().LastIndexOf("'") + 1);
                    tmp = tmp.Remove(0, tmp.LastIndexOf("/") + 1);

                    while (tmp != "Comments")
                    {
                        Match img = image.Match(src);
                        try
                        {
                            w.DownloadFile("http://www.citymanga.com" + img.ToString(), path + "\\" + chapter + "\\" + img.ToString().Remove(0, img.ToString().LastIndexOf("/") + 1));
                        }
                        catch (WebException ex)
                        {
                            if (ex.Status != WebExceptionStatus.Success)
                            {

                            }
                        }
                        MessageBox.Show(m.ToString());
                        src = w.DownloadString("http://www.citymanga.com" + m.ToString().Remove(0, m.ToString().LastIndexOf("'") + 1));
                        m = url.Match(src);
                        tmp = m.ToString().Remove(0, m.ToString().LastIndexOf("'") + 1);
                        tmp = tmp.Remove(0, tmp.LastIndexOf("/") + 1);
                    }
                    
                }
            }

            MessageBox.Show("Téléchargements terminés !", "Terminé", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lelDownloader(List<String> c, String path)
        {
            WebClient w = new WebClient();
            Regex name = new Regex("manga = '[-_A-Za-z0-9]*");
            Regex current = new Regex("n = '[0-9]*");
            Regex total = new Regex("current_total_pages = '[0-9]*");
            Regex ext = new Regex("current_ext = '[A-Za-z0-9]*");
            Regex chap = new Regex("chap = '[0-9]*");

            for(int i = 0; i < c.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    String n = "";
                    c.ElementAt(i).Remove(0, 7);
                    String src = w.DownloadString("http://www.lecture-en-ligne.com/" + c.ElementAt(i));
                    String chapter = chap.Match(src).ToString().Remove(0, 8);
                    int t = int.Parse(total.Match(src).ToString().Remove(0, 23));
                    Directory.CreateDirectory(path + "\\" + chapter);
                    n = current.Match(src).ToString().Remove(0, 5);
                    for (int j = int.Parse(n); j <= t; j++)
                    {
                        if (j < 10)
                        {
                            n = "0" + j.ToString();
                        }
                        String url = "http://www.lecture-en-ligne.com/images/mangas/" + name.Match(src).ToString().Remove(0, 9) + "/" + chapter + "/" + n + "." + ext.Match(src).ToString().Remove(0, 15);
                        w.DownloadFile(url, path + "\\" + chapter + "\\" + n + "." + ext.Match(src).ToString().Remove(0, 15));
                    }
                }
            }

            MessageBox.Show("Téléchargements terminés !", "Terminé", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mhDownloader(List<String> c, String path)
        {
            WebClient wb = new WebClient();
            List<String> pages = new List<String>();
            wb.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            String dir = "";

            Regex r = new Regex("<option value=\"[A-Za-z0-9:._/-]*\"");
            Regex image = new Regex("img.src = \"[A-Za-z0-9:._/-]*.jpg");

            for (int i = 0; i < c.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    String beg = c[i];
                    beg = beg.Remove(beg.Length - 1);
                    dir = path + "/" + beg.Substring(beg.LastIndexOf('/') + 1) + "/";

                    Directory.CreateDirectory(dir);

                    String src = wb.DownloadString(c[i]);

                    foreach (Match m in r.Matches(src))
                    {
                        String blah = m.ToString();
                        blah = blah.Remove(blah.Length - 1);
                        blah = blah.Remove(0, blah.LastIndexOf('\"'));
                        blah = blah.Remove(0, 1);
                        pages.Add(blah);
                    }

                    pages = pages.Distinct().ToList();

                    for(int j = 0; j < pages.Count; j++)
                    {
                        src = wb.DownloadString(pages[j]);
                        wb.DownloadFile(image.Match(src).ToString().Remove(0, 11), dir + "/" + (j+1).ToString() + ".jpg");
                    }

                    pages.Clear();

                }
            }

        }

        void mfDownloader(List<String> c, String path)
        {
            WebClient wb = new WebClient();
            wb.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            String dir = "";
            String beg = "";

            Regex r = new Regex("total_pages=[0-9]*");
            Regex image = new Regex("'[A-Za-z0-9/:._-]*.jpg");

            for (int i = 0; i < c.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    beg = c[i].Remove(c[i].LastIndexOf('/'));
                    dir = path + beg.Substring(beg.LastIndexOf('/')+1) + "/";
                    Directory.CreateDirectory(dir);
                    String src = wb.DownloadString(c.ElementAt(i));
                    //MessageBox.Show(src);
                    MessageBox.Show(r.Match(src).ToString());
                    for (int j = 0; j < int.Parse(r.Match(src).ToString().Remove(0, 12)); j++)
                    {
                        src = wb.DownloadString(beg + "/" + (j+1).ToString() + ".html");
                        wb.DownloadFile(image.Match(src).ToString().Substring(image.Match(src).ToString().LastIndexOf('\'')+1), dir + (j+1).ToString() + ".jpg");

                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (site)
            {
                case 1:
                    cmDownloader(chapters, path);
                    break;

                case 2:
                    mrDownloader(chapters, path);
                    break;

                case 3:
                    lelDownloader(chapters, path);
                    break;
                
                case 4:
                    mfDownloader(chapters, path);
                    break;

                case 5:
                    mhDownloader(chapters, path);
                    break;
            }
        }
    }
}
