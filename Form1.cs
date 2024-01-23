using System.Diagnostics;

namespace GitHubInformationGrabber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            this.btnGo.Enabled = false; 
            Cursor.Current = Cursors.WaitCursor;

            GithubMetaScraper scraper = new GithubMetaScraper(txtToken.Text);
            var files = await scraper.GetMetaData(txtOrg.Text);
            foreach(var file in files)
            {       
                Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
            }
            this.btnGo.Enabled = true;
            Cursor.Current = Cursors.Default;

        }
    }
}
