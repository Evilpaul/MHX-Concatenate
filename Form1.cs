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
        private dataStore pbl_file;
        private dataStore app_file;

        public Form1()
        {
            InitializeComponent();

            pbl_file = new dataStore(this);
            app_file = new dataStore(this);
        }

        public void addLogText(string logText)
        {
            listBox1.Items.Add(logText);
        }

        private void checkOpenFilesSelected()
        {
            if (pbl_file.isDataValid() && app_file.isDataValid())
            {
                button4.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = pbl_file.getDataCount() + app_file.getDataCount() + 2;
            progressBar1.Value = progressBar1.Minimum;

            try
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                try
                {
                    addLogText("Writing output file");
                    addLogText("Writing header : " + pbl_file.getHeader());
                    sw.WriteLine(pbl_file.getHeader());
                    progressBar1.Increment(1);

                    addLogText("Writing " + (pbl_file.getDataCount() + app_file.getDataCount()) + " data lines");
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
            pbl_file.processFile(openFileDialog1.FileName);
            checkOpenFilesSelected();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            app_file.processFile(openFileDialog2.FileName);
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

            // add file into a simple label control:
            textBox2.Text = FileList[0];
            
            // Do something with the data...
            pbl_file.processFile(FileList[0]);

            checkOpenFilesSelected();
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

            // add file into a simple label control:
            textBox3.Text = FileList[0];
            
            // Do something with the data...
            app_file.processFile(FileList[0]);

            checkOpenFilesSelected();
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
