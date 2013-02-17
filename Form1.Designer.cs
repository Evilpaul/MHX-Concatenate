namespace mhx_concatenate
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.concatenateButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.inFile1Button = new System.Windows.Forms.Button();
            this.inFile1TextBox = new System.Windows.Forms.TextBox();
            this.inFile2Button = new System.Windows.Forms.Button();
            this.inFile2TextBox = new System.Windows.Forms.TextBox();
            this.outFileButton = new System.Windows.Forms.Button();
            this.outFileTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.inFile3Button = new System.Windows.Forms.Button();
            this.inFile3TextBox = new System.Windows.Forms.TextBox();
            this.inFile3CheckBox = new System.Windows.Forms.CheckBox();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.inFile4CheckBox = new System.Windows.Forms.CheckBox();
            this.inFile4Button = new System.Windows.Forms.Button();
            this.inFile4TextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog4 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // concatenateButton
            // 
            this.concatenateButton.Enabled = false;
            this.concatenateButton.Location = new System.Drawing.Point(463, 424);
            this.concatenateButton.Name = "concatenateButton";
            this.concatenateButton.Size = new System.Drawing.Size(80, 23);
            this.concatenateButton.TabIndex = 0;
            this.concatenateButton.Text = "Concatenate";
            this.concatenateButton.UseVisualStyleBackColor = true;
            this.concatenateButton.Click += new System.EventHandler(this.concatenateButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 424);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(445, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "mhx";
            this.saveFileDialog1.Filter = "Motorola Hex Files (.mhx;.mot)|*.mhx;*.mot";
            this.saveFileDialog1.Title = "Save As...";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "mhx";
            this.openFileDialog1.Filter = "Motorola Hex Files (.mhx;.mot)|*.mhx;*.mot";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // inFile1Button
            // 
            this.inFile1Button.Location = new System.Drawing.Point(12, 12);
            this.inFile1Button.Name = "inFile1Button";
            this.inFile1Button.Size = new System.Drawing.Size(75, 23);
            this.inFile1Button.TabIndex = 10;
            this.inFile1Button.Text = "Input 1";
            this.inFile1Button.UseVisualStyleBackColor = true;
            this.inFile1Button.Click += new System.EventHandler(this.inFile1Button_Click);
            // 
            // inFile1TextBox
            // 
            this.inFile1TextBox.Location = new System.Drawing.Point(93, 14);
            this.inFile1TextBox.Name = "inFile1TextBox";
            this.inFile1TextBox.ReadOnly = true;
            this.inFile1TextBox.Size = new System.Drawing.Size(450, 20);
            this.inFile1TextBox.TabIndex = 11;
            // 
            // inFile2Button
            // 
            this.inFile2Button.Location = new System.Drawing.Point(12, 41);
            this.inFile2Button.Name = "inFile2Button";
            this.inFile2Button.Size = new System.Drawing.Size(75, 23);
            this.inFile2Button.TabIndex = 12;
            this.inFile2Button.Text = "Input 2";
            this.inFile2Button.UseVisualStyleBackColor = true;
            this.inFile2Button.Click += new System.EventHandler(this.inFile2Button_Click);
            // 
            // inFile2TextBox
            // 
            this.inFile2TextBox.Location = new System.Drawing.Point(93, 43);
            this.inFile2TextBox.Name = "inFile2TextBox";
            this.inFile2TextBox.ReadOnly = true;
            this.inFile2TextBox.Size = new System.Drawing.Size(450, 20);
            this.inFile2TextBox.TabIndex = 13;
            // 
            // outFileButton
            // 
            this.outFileButton.Location = new System.Drawing.Point(12, 125);
            this.outFileButton.Name = "outFileButton";
            this.outFileButton.Size = new System.Drawing.Size(75, 23);
            this.outFileButton.TabIndex = 14;
            this.outFileButton.Text = "Output";
            this.outFileButton.UseVisualStyleBackColor = true;
            this.outFileButton.Click += new System.EventHandler(this.outFileButton_Click);
            // 
            // outFileTextBox
            // 
            this.outFileTextBox.Location = new System.Drawing.Point(93, 127);
            this.outFileTextBox.Name = "outFileTextBox";
            this.outFileTextBox.ReadOnly = true;
            this.outFileTextBox.Size = new System.Drawing.Size(450, 20);
            this.outFileTextBox.TabIndex = 15;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.DefaultExt = "mhx";
            this.openFileDialog2.Filter = "Motorola Hex Files (.mhx;.mot)|*.mhx;*.mot";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(12, 154);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(531, 264);
            this.listBox1.TabIndex = 17;
            // 
            // inFile3Button
            // 
            this.inFile3Button.Enabled = false;
            this.inFile3Button.Location = new System.Drawing.Point(33, 67);
            this.inFile3Button.Name = "inFile3Button";
            this.inFile3Button.Size = new System.Drawing.Size(86, 23);
            this.inFile3Button.TabIndex = 18;
            this.inFile3Button.Text = "Input 3";
            this.inFile3Button.UseVisualStyleBackColor = true;
            this.inFile3Button.Click += new System.EventHandler(this.inFile3Button_Click);
            // 
            // inFile3TextBox
            // 
            this.inFile3TextBox.Location = new System.Drawing.Point(125, 69);
            this.inFile3TextBox.Name = "inFile3TextBox";
            this.inFile3TextBox.ReadOnly = true;
            this.inFile3TextBox.Size = new System.Drawing.Size(418, 20);
            this.inFile3TextBox.TabIndex = 19;
            // 
            // inFile3CheckBox
            // 
            this.inFile3CheckBox.AutoSize = true;
            this.inFile3CheckBox.Location = new System.Drawing.Point(12, 72);
            this.inFile3CheckBox.Name = "inFile3CheckBox";
            this.inFile3CheckBox.Size = new System.Drawing.Size(15, 14);
            this.inFile3CheckBox.TabIndex = 20;
            this.inFile3CheckBox.UseVisualStyleBackColor = true;
            this.inFile3CheckBox.CheckedChanged += new System.EventHandler(this.inFile3CheckBox_CheckedChanged);
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.DefaultExt = "mhx";
            this.openFileDialog3.Filter = "Motorola Hex Files (.mhx;.mot)|*.mhx;*.mot";
            this.openFileDialog3.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog3_FileOk);
            // 
            // inFile4CheckBox
            // 
            this.inFile4CheckBox.AutoSize = true;
            this.inFile4CheckBox.Location = new System.Drawing.Point(12, 96);
            this.inFile4CheckBox.Name = "inFile4CheckBox";
            this.inFile4CheckBox.Size = new System.Drawing.Size(15, 14);
            this.inFile4CheckBox.TabIndex = 21;
            this.inFile4CheckBox.UseVisualStyleBackColor = true;
            this.inFile4CheckBox.CheckedChanged += new System.EventHandler(this.inFile4CheckBox_CheckedChanged);
            // 
            // inFile4Button
            // 
            this.inFile4Button.Enabled = false;
            this.inFile4Button.Location = new System.Drawing.Point(33, 96);
            this.inFile4Button.Name = "inFile4Button";
            this.inFile4Button.Size = new System.Drawing.Size(86, 23);
            this.inFile4Button.TabIndex = 22;
            this.inFile4Button.Text = "Input 4";
            this.inFile4Button.UseVisualStyleBackColor = true;
            this.inFile4Button.Click += new System.EventHandler(this.inFile4Button_Click);
            // 
            // inFile4TextBox
            // 
            this.inFile4TextBox.BackColor = System.Drawing.SystemColors.Control;
            this.inFile4TextBox.Location = new System.Drawing.Point(125, 98);
            this.inFile4TextBox.Name = "inFile4TextBox";
            this.inFile4TextBox.ReadOnly = true;
            this.inFile4TextBox.Size = new System.Drawing.Size(418, 20);
            this.inFile4TextBox.TabIndex = 23;
            // 
            // openFileDialog4
            // 
            this.openFileDialog4.DefaultExt = "mhx";
            this.openFileDialog4.Filter = "Motorola Hex Files (.mhx;.mot)|*.mhx;*.mot";
            this.openFileDialog4.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog4_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 459);
            this.Controls.Add(this.inFile4TextBox);
            this.Controls.Add(this.inFile4Button);
            this.Controls.Add(this.inFile4CheckBox);
            this.Controls.Add(this.inFile3CheckBox);
            this.Controls.Add(this.inFile3TextBox);
            this.Controls.Add(this.inFile3Button);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.outFileTextBox);
            this.Controls.Add(this.outFileButton);
            this.Controls.Add(this.inFile2TextBox);
            this.Controls.Add(this.inFile2Button);
            this.Controls.Add(this.inFile1TextBox);
            this.Controls.Add(this.inFile1Button);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.concatenateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "MHX Concatenate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button concatenateButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button inFile1Button;
        private System.Windows.Forms.TextBox inFile1TextBox;
        private System.Windows.Forms.Button inFile2Button;
        private System.Windows.Forms.TextBox inFile2TextBox;
        private System.Windows.Forms.Button outFileButton;
        private System.Windows.Forms.TextBox outFileTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button inFile3Button;
        private System.Windows.Forms.TextBox inFile3TextBox;
        private System.Windows.Forms.CheckBox inFile3CheckBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.CheckBox inFile4CheckBox;
        private System.Windows.Forms.Button inFile4Button;
        private System.Windows.Forms.TextBox inFile4TextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog4;
    }
}

