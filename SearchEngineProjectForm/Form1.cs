using CSC322_SearchEngineProject;
using System;
using System.IO;
using System.Windows.Forms;

namespace SearchEngineProjectForm
{
    public partial class Form1 : Form
    {
        private Form2 f = new Form2();
        public static readonly string Documents = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
        public static readonly string DirectoryToSaveIndex = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppIndexFiles");

        public Form1()
        {
            InitializeComponent();
        }

        private ProgressBar pg = new ProgressBar();

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = textBox2.Text;
            var query = Query.Create(input);
            var collector = Collector.Create(16);
            f.Searcher.Search(query, collector);
            f.Results = collector.GetTopDocs();
            f.ShowDialog();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                Controls.Add(pg);
                pg.Location = new System.Drawing.Point(330, 225);
                pg.Size = new System.Drawing.Size(147, 18);
            }));
            for (int i = 0; i < 50; i++)
            {
                backgroundWorker1.ReportProgress(i);
            }
            var dir = new DocDirectory(new DirectoryInfo(Documents));
            var indexer = new IndexWriter(new DirectoryInfo(DirectoryToSaveIndex));
            indexer.AddDirectory(dir);
            f.Searcher = new IndexSearcher(new IndexReader(new DirectoryInfo(DirectoryToSaveIndex)));
            textBox2.Invoke(new MethodInvoker(delegate { textBox2.Focus(); }));
            backgroundWorker1.ReportProgress(100);
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            pg.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate { Controls.Remove(pg); }));
            button1.Enabled = true;
        }
    }
}