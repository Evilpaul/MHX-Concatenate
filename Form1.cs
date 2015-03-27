using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace mhx_concatenate
{
    public partial class Form1 : Form
    {
        private bool inFile1OK = false;
        private bool inFile2OK = false;
        private bool inFile3OK = false;
        private bool inFile4OK = false; 
        private bool outFileOK = false;
        private CancellationTokenSource cts;

        public Form1()
        {
            InitializeComponent();
            cts = new CancellationTokenSource();
        }

        private void checkValidFiles()
        {
            if (inFile1OK && inFile2OK && outFileOK &&
                ((!inFile3CheckBox.Checked) || (inFile3CheckBox.Checked && inFile3OK)) &&
                ((!inFile4CheckBox.Checked) || (inFile4CheckBox.Checked && inFile4OK)))
            {
                concatenateButton.Enabled = true;
            }
            else
            {
                concatenateButton.Enabled = false;
            }
        }

        private Task DoAsync(List<string> inFiles, string outFile, IProgress<int> progress, IProgress<string> progress_str)
        {
            return Task.Run(() =>
				{
					int no_parsed = 0;
					FileClass the_file = new FileClass(this, progress, progress_str);

					progress.Report(no_parsed);

					foreach (string filename in inFiles)
					{
						the_file.processFile(filename);
						progress.Report(no_parsed += (100 / inFiles.Count));
					}

					the_file.outputFile(outFile);
				});
        }

        private async void concatenateButton_Click(object sender, EventArgs e)
        {
            concatenateButton.Enabled = false;

            IProgress<string> progress_str = new Progress<string>(status =>
            {
                listBox1.Items.Add(status);
				listBox1.TopIndex = listBox1.Items.Count - 1;
            });

            try
            {
                IProgress<int> progress = new Progress<int>(percent =>
                {
                    progressBar1.Value = percent;
                });

                listBox1.Items.Clear();

                List<string> inFiles = new List<string>();
                inFiles.Add(openFileDialog1.FileName);
                inFiles.Add(openFileDialog2.FileName);
                if (inFile3CheckBox.Checked) inFiles.Add(openFileDialog3.FileName);
                if (inFile4CheckBox.Checked) inFiles.Add(openFileDialog4.FileName);

                await DoAsync(inFiles, saveFileDialog1.FileName, progress, progress_str);
            }
            catch
            {
                progress_str.Report("Exception caught processing files!");
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                concatenateButton.Enabled = true;
            }
        }

        private void inFile1Button_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                inFile1TextBox.Text = openFileDialog1.FileName;
            }
        }

        private void inFile2Button_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                inFile2TextBox.Text = openFileDialog2.FileName;
            }
        }

        private void inFile3Button_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog3.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                inFile3TextBox.Text = openFileDialog3.FileName;
            }
        }

        private void inFile4Button_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog4.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                inFile4TextBox.Text = openFileDialog4.FileName;
            }
        }

        private void outFileButton_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                outFileTextBox.Text = saveFileDialog1.FileName;
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            outFileOK = true;
            checkValidFiles();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            inFile1OK = true;
            checkValidFiles();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            inFile2OK = true;
            checkValidFiles();
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            inFile3OK = true;
            checkValidFiles();
        }

        private void openFileDialog4_FileOk(object sender, CancelEventArgs e)
        {
            inFile4OK = true;
            checkValidFiles();
        }

        private void inFile3CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (inFile3CheckBox.Checked)
            {
                inFile3Button.Enabled = true;
            }
            else
            {
                inFile3Button.Enabled = false;
            }

            checkValidFiles();
        }

        private void inFile4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (inFile4CheckBox.Checked)
            {
                inFile4Button.Enabled = true;
            }
            else
            {
                inFile4Button.Enabled = false;
            }

            checkValidFiles();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Request cancellation.
            cts.Cancel();
        }
    }
}
