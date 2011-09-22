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

        private void button1_Click(object sender, EventArgs e)
        {
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

            button1.Enabled = false;
            progressBar1.Value = progressBar1.Minimum;

            listBox1.Items.Clear();
            listBox1.Items.Add("Record Type : S0 (Block Header)");
            listBox1.Items.Add("Byte Count : 06");
            listBox1.Items.Add("Address : 0x0000");
            listBox1.Items.Add("Data : 484452");
            
            for (i = 0; i < 6; i++)
            {
                sum += buf[i];
                progressBar1.Increment(10);
            }
            res = (byte)(0xff - sum);

            listBox1.Items.Add("Checksum : " + Convert.ToString(res, 16).PadLeft(2, '0'));
            listBox1.Items.Add("Record valid");

            button1.Enabled = true;
            progressBar1.Value = progressBar1.Maximum;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox2.Text = openFileDialog1.FileName;
                label1.Text = openFileDialog1.SafeFileName;
                label5.Text = "0x000000";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox3.Text = openFileDialog2.FileName;
                label2.Text = openFileDialog2.SafeFileName;
                label6.Text = "0x000000";
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

            if (file1Selected && file2Selected)
            {
                button4.Enabled = true;
                radioButton1.Enabled = true;
                label1.Enabled = true;
                radioButton2.Enabled = true;
                label2.Enabled = true;
                radioButton3.Enabled = true;
                maskedTextBox1.Enabled = true;
                radioButton4.Enabled = true;
                label5.Enabled = true;
                radioButton5.Enabled = true;
                label6.Enabled = true;
                radioButton6.Enabled = true;
                numericUpDown1.Enabled = true;
            }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            file2Selected = true;

            if (file1Selected && file2Selected)
            {
                button4.Enabled = true;
                radioButton1.Enabled = true;
                label1.Enabled = true;
                radioButton2.Enabled = true;
                label2.Enabled = true;
                radioButton3.Enabled = true;
                maskedTextBox1.Enabled = true;
                radioButton4.Enabled = true;
                label5.Enabled = true;
                radioButton5.Enabled = true;
                label6.Enabled = true;
                radioButton6.Enabled = true;
                numericUpDown1.Enabled = true;
            }
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

            // add file into a simple label control:
            textBox4.Text = FileList[0];
        }
    }
}
