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
        private bool file1Selected = false;
        private bool file2Selected = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkOpenFilesSelected()
        {
            if (file1Selected && file2Selected)
            {
                button4.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            progressBar1.Minimum = 0;
            progressBar1.Value = progressBar1.Minimum;

            listBox1.Items.Clear();

            listBox1.Items.Add("Opening Files...");
            int length1 = File.ReadLines(openFileDialog1.FileName).Count();
            listBox1.Items.Add("PBL has " + length1 + " lines");
            int length2 = File.ReadLines(openFileDialog2.FileName).Count();
            listBox1.Items.Add("App has " + length2 + " lines");
            progressBar1.Maximum = (length1 + length2) - 1;

            try
            {
                // file 1 processing
                StreamReader sr1 = new StreamReader(openFileDialog1.FileName);
                StreamReader sr2 = new StreamReader(openFileDialog2.FileName);
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                int file1Count = 0;
                int file2Count = 0;
                int outputCount = 0;
                try
                {
                    string file1header = sr1.ReadLine();
                    file1Count++;
                    progressBar1.Increment(1);

                    string file2header = sr2.ReadLine();
                    file2Count++;
                    progressBar1.Increment(1);

                    listBox1.Items.Add("Writing header : "+ file1header);
                    sw.WriteLine(file1header);
                    outputCount++;

                    listBox1.Items.Add("Copying PBL...");
                    while ((sr1.Peek() >= 0) && (file1Count < (length1 - 1)))
                    {
                        sw.WriteLine(sr1.ReadLine());
                        file1Count++;
                        progressBar1.Increment(1);
                        outputCount++;
                    }
                    listBox1.Items.Add("PBL copied");

                    listBox1.Items.Add("Copying App...");
                    while ((sr2.Peek() >= 0) && (file2Count < (length2 - 1)))
                    {
                        sw.WriteLine(sr2.ReadLine());
                        file2Count++;
                        progressBar1.Increment(1);
                        outputCount++;
                    }
                    listBox1.Items.Add("App copied");

                    string file1address = sr1.ReadLine();
                    progressBar1.Increment(1);

                    string file2address = sr2.ReadLine();
                    progressBar1.Increment(1);

                    listBox1.Items.Add("Writing start address : " + file1address);
                    sw.WriteLine(file1address);
                    outputCount++;

                    listBox1.Items.Add("Written " + outputCount + " lines");
                    listBox1.Items.Add("Concatenation complete.");
                }
                catch
                {
                    listBox1.Items.Add("Exception caught processing files!");
                }
                finally
                {
                    sr1.Close();
                    sr2.Close();
                    sw.Close();
                }
            }
            catch
            {
                listBox1.Items.Add("Exception caught opening files!");
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
            button1.Enabled = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            file1Selected = true;

            checkOpenFilesSelected();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            file2Selected = true;

            checkOpenFilesSelected();
        }

        // This event occurs when the user drags over the form with 
        // the mouse during a drag drop operation 
        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it

        }

        // Occurs when the user releases the mouse over the drop target 
        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // Do something with the data...
            file1Selected = true;

            checkOpenFilesSelected();

            // add file into a simple label control:
            textBox2.Text = FileList[0];
        }

        // This event occurs when the user drags over the form with 
        // the mouse during a drag drop operation 
        private void textBox3_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it

        }

        // Occurs when the user releases the mouse over the drop target 
        private void textBox3_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // Do something with the data...
            file2Selected = true;

            checkOpenFilesSelected();

            // add file into a simple label control:
            textBox3.Text = FileList[0];
        }

        // This event occurs when the user drags over the form with 
        // the mouse during a drag drop operation 
        private void textBox4_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it

        }

        // Occurs when the user releases the mouse over the drop target 
        private void textBox4_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // Do something with the data...
            button1.Enabled = true;

            // add file into a simple label control:
            textBox4.Text = FileList[0];
        }
    }
}
