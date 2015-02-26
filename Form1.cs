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

        private async Task<int> DoAsync(List<string> inFiles, string outFile, IProgress<int> progress, IProgress<string> progress_str, CancellationToken token)
        {
            int no_done = 0;
            int no_total = 0;
            int no_parsed = 0;
            FileClass the_file = new FileClass(this, progress_str);

            progress.Report(no_parsed);

            foreach (string filename in inFiles)
            {
                await the_file.processFile(filename, token).ConfigureAwait(continueOnCapturedContext: false);
                progress.Report(no_parsed += (100 / inFiles.Count));

                if (token.IsCancellationRequested)
                {
                    progress_str.Report("Operation Cancelled");
                    return -1;
                }
            }

            no_total = the_file.getDataCount() + 2;
            progress.Report(0);

            try
            {
                StreamWriter sw = new StreamWriter(outFile);
                try
                {
                    progress_str.Report("Writing output file");
                    progress_str.Report("Writing header : " + the_file.getHeaderDecoded());
                    await sw.WriteLineAsync(the_file.getHeader());
                    no_done++;
                    progress.Report((no_done / no_total) * 100);

                    progress_str.Report("Writing " + the_file.getDataCount() + " data lines");
                    int i;
                    for (i = 0; i < the_file.getDataCount(); i++)
                    {
                        await sw.WriteLineAsync(the_file.getDataLine(i));
                        no_done++;
                        double blah = (double)no_done / (double)no_total;
                        progress.Report((int)(blah * 100));
                        if (token.IsCancellationRequested)
                        {
                            progress_str.Report("Operation Cancelled");
                            return -1;
                        }
                    }

                    progress_str.Report("Writing start address : " + the_file.getDecodedStartAddress());
                    await sw.WriteLineAsync(the_file.getStartAddress());
                    progress.Report(100);

                    progress_str.Report("Concatenation complete.");
                }
                catch
                {
                    progress_str.Report("Exception caught processing files!");
                }
                finally
                {
                    sw.Close();
                }
            }
            catch
            {
                progress_str.Report("Exception caught opening files!");
            }
            
            return 1;
        }

        private async void concatenateButton_Click(object sender, EventArgs e)
        {
            concatenateButton.Enabled = false;

            IProgress<string> progress_str = new Progress<string>(status =>
            {
                listBox1.Items.Add(status);
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

                await new Form1().DoAsync(inFiles, saveFileDialog1.FileName, progress, progress_str, cts.Token);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch(Exception ex)
            {
                progress_str.Report("Exception caught processing files!");
            }
            finally
            {
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
