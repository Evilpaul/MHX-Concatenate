using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace mhx_concatenate
{
    public partial class Form1 : Form
    {
        private bool pblFileOK = false;
        private bool appFileOK = false;
        private bool pnoFileOK = false;
        private bool saveFileOK = false;

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
            if (pblFileOK && appFileOK && saveFileOK &&
                ((!checkBox1.Checked) || (checkBox1.Checked && pnoFileOK)))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataStore pbl_file = new dataStore(this);
            dataStore app_file = new dataStore(this);
            dataStore pno_file = new dataStore(this);

            listBox1.Items.Clear();

            pbl_file.processFile(openFileDialog1.FileName);
            app_file.processFile(openFileDialog2.FileName);
            if (checkBox1.Checked)
            {
                pno_file.processFile(openFileDialog3.FileName);
            }

            button1.Enabled = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = pbl_file.getDataCount() + app_file.getDataCount() + pno_file.getDataCount() + 2;
            progressBar1.Value = progressBar1.Minimum;

            try
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                try
                {
                    addLogText("Writing output file");
                    addLogText("Writing header : " + pbl_file.getHeaderDecoded());
                    sw.WriteLine(pbl_file.getHeader());
                    progressBar1.Increment(1);

                    addLogText("Writing " + (pbl_file.getDataCount() + app_file.getDataCount() + pno_file.getDataCount()) + " data lines");
                    int i;
                    for (i = 0; i < pbl_file.getDataCount(); i++)
                    {
                        sw.WriteLine(pbl_file.getDataLine(i));
                        progressBar1.Increment(1);
                    }
                    for (i = 0; i < app_file.getDataCount(); i++)
                    {
                        sw.WriteLine(app_file.getDataLine(i));
                        progressBar1.Increment(1);
                    }
                    for (i = 0; i < pno_file.getDataCount(); i++)
                    {
                        sw.WriteLine(pno_file.getDataLine(i));
                        progressBar1.Increment(1);
                    }

                    addLogText("Writing start address : " + pbl_file.getStartAddress());
                    sw.WriteLine(pbl_file.getStartAddress());
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

            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox3.Text = openFileDialog2.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog3.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox1.Text = openFileDialog3.FileName;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox4.Text = saveFileDialog1.FileName;
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            saveFileOK = true;
            checkValidFiles();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pblFileOK = true;
            checkValidFiles();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            appFileOK = true;
            checkValidFiles();
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            pnoFileOK = true;
            checkValidFiles();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }

            checkValidFiles();
        }
    }
}
