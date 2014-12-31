using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private string path;
        private string url;
        List<String> chapters;

        public Form1()
        {
            InitializeComponent();
            chapters = new List<string>();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDialog1.ShowDialog(this);

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                directoryEntry.Clear();
                directoryEntry.Text = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                directoryEntry.Clear();
            }
        }

        private void parseCM(string u)
        {
            String loc = u.Remove(u.Length - 1);
            loc = loc.Remove(0, loc.LastIndexOf('/'));
            loc = loc.Remove(0, 1);
            path += "\\" + loc;

            Directory.CreateDirectory(path);

            WebClient w = new WebClient();
            String src = w.DownloadString(u);
            Regex r = new Regex("\"http://www.citymanga.com/"+loc+"/[-./A-Z_a-z0-9]*\"");
            foreach (Match m in r.Matches(src))
            {
                String tmp = m.ToString().Remove(0, 1);
                tmp = tmp.Remove(tmp.LastIndexOf("\""));
                chapters.Add(tmp);
            }

            listChapter l = new listChapter(chapters, 1, path);
            l.Show(this);
            //chapters.Clear();
        }

        private void parseLEL(string u)
        {
            String loc = u.Remove(u.Length - 1);
            loc = loc.Remove(0, loc.LastIndexOf('/'));
            loc = loc.Remove(0, 1);
            path += "\\" + loc;

            Directory.CreateDirectory(path);

            WebClient w = new WebClient();
            String src = w.DownloadString(u);
            Regex r = new Regex(loc+"[A-Za-z0-9_/]*(.html)");
            
            foreach (Match m in r.Matches(src))
            {
                chapters.Add(m.ToString());
            }

            //MessageBox.Show(mess);
            listChapter l = new listChapter(chapters, 3, path);
            l.Show(this);
            //chapters.Clear();
        }

        private void parseMR(string u)
        {
            String loc = "";
            Regex r;
            Regex r2;
            loc = u;
            if (u.Contains(".html"))
            {
                loc = u.Remove(u.Length - 5);
            }

            loc = loc.Remove(0, loc.LastIndexOf('/'));
            loc = loc.Remove(0, 1);
            path += "\\" + loc;

            Directory.CreateDirectory(path);

            WebClient w = new WebClient();
            String src = w.DownloadString(u);
            r = new Regex("/[-0-9]*/" + loc + "/[-.A-Za-z0-9]*\">[ -.:<>/A-Z_a-z0-9]*</td>");
            r2 = new Regex("/" + loc + "/[0-9]*\">[ -.:<>/A-Z_a-z0-9]*</td>");


            foreach (Match m in r.Matches(src))
            {
                chapters.Add(m.ToString().Remove(m.ToString().LastIndexOf("\"")));
            }

            foreach (Match m in r2.Matches(src))
            {
                chapters.Add(m.ToString().Remove(m.ToString().LastIndexOf("\"")));
            }

            listChapter l = new listChapter(chapters, 2, path);
            l.Show(this);
            //chapters.Clear();
                
        }

        private void parseMF(String u)
        {
            String loc = u.Remove(u.Length - 1);
            loc = loc.Remove(0, loc.LastIndexOf('/')+1);
            path += "/" + loc + "/" ;

            Directory.CreateDirectory(path);

            WebClient wb = new WebClient();
            wb.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            String src = wb.DownloadString(u);

            Regex r = new Regex(u + "[A-Za-z0-9_/]*(.html)");

            foreach (Match m in r.Matches(src))
            {
                chapters.Add(m.ToString());
            }

            listChapter l = new listChapter(chapters, 4, path);
            l.Show(this);
            //chapters.Clear();
        }

        private void parseAnimea(String u)
        {
            String loc = u.Remove(u.Length - 5);
            loc = loc.Substring(loc.LastIndexOf('/')+1);
            path += "/" + loc + "/";

            Directory.CreateDirectory(path);

            WebClient wb = new WebClient();
            wb.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            String src = wb.DownloadString(u);

            Regex r = new Regex("href=\"/[ -.:<>/A-Z_a-z0-9]*.html\" id=\"ch_[0-9]*\"");

            foreach (Match m in r.Matches(src))
            {
                String tmp = m.ToString().Remove(0, 6);
                tmp = tmp.Remove(tmp.LastIndexOf("html")+4);
                chapters.Add(tmp);
            }

            listChapter l = new listChapter(chapters, 6, path);
            l.Show(this);
        }

        private void parseMangapark(string u)
        {
            WebClient wb = new WebClient();
            string src = wb.DownloadString(u);
        }

        private void parseMangaGo(String u)
        {
            String loc = u.Remove(u.Length - 1);
            loc = loc.Remove(0, loc.LastIndexOf('/') + 1);
            path += "/" + loc + "/";

            Directory.CreateDirectory(path);

            WebClient wb = new WebClient();

            string src = wb.DownloadString(u);

            Regex r = new Regex("<a target=\"_self\" class=\"chico\" href=\"(.*)");

            MatchCollection c = r.Matches(src);
            foreach(Match m in c)
            {
                string s = m.Groups[1].ToString().Remove(m.Groups[1].ToString().LastIndexOf('"'));
                chapters.Add(s);
            }

            chapters = chapters.Distinct().ToList();

            listChapter l = new listChapter(chapters, 7, path);
            l.ShowDialog();

        }

        private void parseMH(String u)
        {
            String loc = u.Remove(u.Length - 1);
            loc = loc.Remove(0, loc.LastIndexOf('/')+1);
            path += "/" + loc + "/";

            Directory.CreateDirectory(path);

            WebClient wb = new WebClient();
            wb.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            String src = wb.DownloadString(u);

            Regex r = new Regex(u + "[A-Za-z0-9_-]*/[A-Za-z0-9_/-]*\"");

            foreach (Match m in r.Matches(src))
            {
                String blah = m.ToString();
                chapters.Add(blah.Remove(blah.Length - 1));
            }

            listChapter l = new listChapter(chapters, 5, path);
            l.Show(this);

        }

        private void parseMangaPage(String u)
        {
            if (u.Contains("citymanga"))
            {
                parseCM(u);
            }

            else if (u.Contains("lecture"))
            {
                parseLEL(u);
            }

            else if (u.Contains("reader"))
            {
                parseMR(u);
            }
            else if (u.Contains("fox"))
            {
                parseMF(u);
            }
            else if (u.Contains("here"))
            {
                parseMH(u);
            }
            else if (u.Contains("animea"))
            {
                parseAnimea(u);
            }
            else if(u.Contains("mangago"))
            {
                parseMangaGo(u);
            }

            else
            {
                MessageBox.Show("Site non supporté", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            if (directoryEntry.Text == "")
            {
                MessageBox.Show("Vous devez choisir un dossier de sortie !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (urlEntry.Text == "")
            {
                MessageBox.Show("Vous devez entrer une URL !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            path = directoryEntry.Text;
            url = urlEntry.Text;

            parseMangaPage(url);
        }

        private void mettreÀJourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Voulez-vous mettre l'application à jour ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string args = "MangaDownloader ";
                args += System.AppDomain.CurrentDomain.FriendlyName;
                System.Diagnostics.Process.Start("Updater.exe", args);
                Environment.Exit(0);
            }
        }

        private void sitesSupportésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sites supportés");
        }
    }
}