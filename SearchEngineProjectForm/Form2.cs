using CSC322_SearchEngineProject;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace SearchEngineProjectForm
{
    public partial class Form2 : Form
    {
        internal ScoredDoc[] Results { get; set; }
        internal IndexSearcher Searcher;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (Results.Length == 0)
                listBox1.Items.Add("No Results Found");
            for (int i = 0; i < Results.Length; i++)
            {
                var docId = Results[i].Docid;
                var document = Searcher.Doc(docId);
                listBox1.Items.Add($"{i + 1}. {document}");
                listBox1.Items.Add($">>{document.FullName}<<");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex / 2;
            var docId = Results[index].Docid;
            var document = Searcher.Doc(docId);
            try
            {
                Process.Start(document.FullName);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(this, $"File can't be opened\n{ex.Message}");
            }
        }

        private void listBox1_ControlRemoved(object sender, ControlEventArgs e)
        {
        }
    }
}