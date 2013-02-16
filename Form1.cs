using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace mhx_concatenate
{
    public partial class Form1 : Form
    {
        private bool inFile1OK = false;
        private bool inFile2OK = false;
        private bool inFile3OK = false;
        private bool inFile4OK = false; 
        private bool outFileOK = false;

        public Form1()
        {
            InitializeComponent();
        }

        public void addLogText(string logText)
        {
            listBox1.Items.Add(logText);
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

        private void concatenateButton_Click(object sender, EventArgs e)
        {
            concatenateButton.Enabled = false;

            {
                FileClass the_file = new FileClass(this);

                listBox1.Items.Clear();

                progressBar1.Minimum = 0;
                progressBar1.Maximum = 4;

                the_file.processFile(openFileDialog1.FileName);

                progressBar1.Increment(1);

                the_file.processFile(openFileDialog2.FileName);

                progressBar1.Increment(1);

                if (inFile3CheckBox.Checked)
                {
                    the_file.processFile(openFileDialog3.FileName);
                }

                progressBar1.Increment(1);

                if (inFile4CheckBox.Checked)
                {
                    the_file.processFile(openFileDialog4.FileName);
                }

                progressBar1.Increment(1);
 
                progressBar1.Minimum = 0;
                progressBar1.Maximum = the_file.getDataCount() + 2;
                progressBar1.Value = progressBar1.Minimum;

                try
                {
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                    try
                    {
                        addLogText("Writing output file");
                        addLogText("Writing header : " + the_file.getHeaderDecoded());
                        sw.WriteLine(the_file.getHeader());
                        progressBar1.Increment(1);

                        addLogText("Writing " + the_file.getDataCount() + " data lines");
                        int i;
                        for (i = 0; i < the_file.getDataCount(); i++)
                        {
                            sw.WriteLine(the_file.getDataLine(i));
                            progressBar1.Increment(1);
                        }

                        addLogText("Writing start address : " + the_file.getDecodedStartAddress());
                        sw.WriteLine(the_file.getStartAddress());
                        progressBar1.Increment(1);

                        addLogText("Concatenation complete.");
                    }
                    catch
                    {
                        addLogText("Exception caught processing files!");
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
                catch
                {
                    addLogText("Exception caught opening files!");
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            concatenateButton.Enabled = true;
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
    }
}
