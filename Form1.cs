using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
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
            bool result = true;
/*
            byte sum = 0;
            byte res = 0;
            byte[] buf;
            int i;

            buf = new byte[6];

            buf[0] = 0x06;
            buf[1] = 0x00;
            buf[2] = 0x00;
            buf[3] = 0x48;
            buf[4] = 0x44;
            buf[5] = 0x52;
*/

            button1.Enabled = false;
            checkBox1.Enabled = false;
            progressBar1.Value = progressBar1.Minimum;

            listBox1.Items.Clear();

            // file 1 processing
            listBox1.Items.Add("Processing File 1...");
            listBox1.Items.Add("File 1 is valid.");
            progressBar1.Value = (progressBar1.Maximum - progressBar1.Minimum) * (1/3);

            // file 2 processing
            if (result)
            {
                listBox1.Items.Add("Processing File 2...");
                listBox1.Items.Add("File 2 is valid.");
                progressBar1.Value = (progressBar1.Maximum - progressBar1.Minimum) * (2 / 3);
            }

/*            
            for (i = 0; i < 6; i++)
            {
                sum += buf[i];
            }
            res = (byte)(0xff - sum);

            listBox1.Items.Add("Checksum : " + Convert.ToString(res, 16).PadLeft(2, '0'));
            listBox1.Items.Add("Record valid");
*/

            if (result)
            {
                Form2 form2 = new Form2();
                try
                {
                    DialogResult dresult = form2.ShowDialog();
                    if (dresult == System.Windows.Forms.DialogResult.OK)
                    {
                        listBox1.Items.Add("result : " + result);
                        listBox1.Items.Add("Output Header : TBD");
                    }
                    else
                    {
                        listBox1.Items.Add("User cancelled operation");
                        result = false;
                    }
                }
                catch
                {
                    listBox1.Items.Add("Exception caught!");
                }
                finally
                {
                    form2.Dispose();
                }
            }

            if (result)
            {
                Form3 form3 = new Form3();
                try
                {
                    DialogResult dresult = form3.ShowDialog();
                    if (dresult == System.Windows.Forms.DialogResult.OK)
                    {
                        listBox1.Items.Add("result : " + result);
                        listBox1.Items.Add("Output Start Address : TBD");
                    }
                    else
                    {
                        listBox1.Items.Add("User cancelled operation");
                        result = false;
                    }
                }
                catch
                {
                    listBox1.Items.Add("Exception caught!");
                }
                finally
                {
                    form3.Dispose();
                }
            }

            if (result)
            {
                listBox1.Items.Add("Output processing...");
                listBox1.Items.Add("Output complete.");
            }
            progressBar1.Value = progressBar1.Maximum;
            button1.Enabled = true;
            checkBox1.Enabled = true;
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
            checkBox1.Enabled = true;
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
            checkBox1.Enabled = true;

            // add file into a simple label control:
            textBox4.Text = FileList[0];
        }
    }
}
