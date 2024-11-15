namespace carandbike1
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            Uname = new TextBox();
            PassTb = new TextBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            pictureBox5 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            timer = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(664, 621);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.CadetBlue;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(808, 73);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(188, 91);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // Uname
            // 
            Uname.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Uname.ForeColor = SystemColors.MenuHighlight;
            Uname.Location = new Point(808, 263);
            Uname.Multiline = true;
            Uname.Name = "Uname";
            Uname.Size = new Size(242, 39);
            Uname.TabIndex = 2;
            Uname.UseWaitCursor = true;
            // 
            // PassTb
            // 
            PassTb.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PassTb.ForeColor = SystemColors.MenuHighlight;
            PassTb.Location = new Point(808, 356);
            PassTb.Multiline = true;
            PassTb.Name = "PassTb";
            PassTb.PasswordChar = '*';
            PassTb.Size = new Size(245, 41);
            PassTb.TabIndex = 3;
            // 
            // pictureBox3
            // 
            pictureBox3.BorderStyle = BorderStyle.Fixed3D;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(739, 263);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(43, 49);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BorderStyle = BorderStyle.Fixed3D;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(739, 356);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(49, 51);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 5;
            pictureBox4.TabStop = false;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(939, 458);
            button1.Name = "button1";
            button1.Size = new Size(123, 53);
            button1.TabIndex = 6;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(784, 458);
            button2.Name = "button2";
            button2.Size = new Size(114, 53);
            button2.TabIndex = 7;
            button2.Text = "Clear";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(-4, 0);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(28, 621);
            pictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox5.TabIndex = 8;
            pictureBox5.TabStop = false;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // timer
            // 
            timer.AutoSize = true;
            timer.BackColor = Color.White;
            timer.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            timer.Location = new Point(30, 586);
            timer.Name = "timer";
            timer.Size = new Size(55, 23);
            timer.TabIndex = 9;
            timer.Text = "Time:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.CadetBlue;
            ClientSize = new Size(1131, 615);
            Controls.Add(timer);
            Controls.Add(pictureBox5);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(PassTb);
            Controls.Add(Uname);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TextBox Uname;
        private TextBox PassTb;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Button button1;
        private Button button2;
        private PictureBox pictureBox5;
        private System.Windows.Forms.Timer timer1;
        private Label timer;
    }
}
