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

            for (i = 0; i < 6; i++)
            {
                sum += buf[i];
            }
            res = (byte)(0xff - sum);

            textBox1.Text = "checksum : " + Convert.ToString(res, 16).PadLeft(2, '0');

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
                maskedTextBox2.Enabled = true;
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
                maskedTextBox2.Enabled = true;
            }
        }
    }
}
