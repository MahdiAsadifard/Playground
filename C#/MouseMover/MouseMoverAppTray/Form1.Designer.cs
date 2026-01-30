namespace MouseMoverAppTray
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button_save = new Button();
            button_cancel = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            trackBar_tick = new TrackBar();
            trackBar_space = new TrackBar();
            label_second = new Label();
            label_pixel = new Label();
            ((System.ComponentModel.ISupportInitialize)trackBar_tick).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_space).BeginInit();
            SuspendLayout();
            // 
            // button_save
            // 
            button_save.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button_save.Enabled = false;
            button_save.Location = new Point(12, 148);
            button_save.Name = "button_save";
            button_save.Size = new Size(94, 29);
            button_save.TabIndex = 0;
            button_save.Text = "Save";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // button_cancel
            // 
            button_cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button_cancel.Location = new Point(121, 148);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(94, 29);
            button_cancel.TabIndex = 0;
            button_cancel.Text = "Cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(310, 15);
            label1.Name = "label1";
            label1.Size = new Size(15, 20);
            label1.TabIndex = 2;
            label1.Text = "s";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 15);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 2;
            label2.Text = "Tick";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(310, 71);
            label3.Name = "label3";
            label3.Size = new Size(25, 20);
            label3.TabIndex = 2;
            label3.Text = "px";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 71);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 2;
            label4.Text = "Space";
            // 
            // trackBar_tick
            // 
            trackBar_tick.Location = new Point(67, 12);
            trackBar_tick.Maximum = 30;
            trackBar_tick.Minimum = 5;
            trackBar_tick.Name = "trackBar_tick";
            trackBar_tick.Size = new Size(197, 56);
            trackBar_tick.TabIndex = 3;
            trackBar_tick.Value = 5;
            trackBar_tick.Scroll += trackBar_tick_Scroll;
            // 
            // trackBar_space
            // 
            trackBar_space.Location = new Point(67, 67);
            trackBar_space.Minimum = 1;
            trackBar_space.Name = "trackBar_space";
            trackBar_space.Size = new Size(197, 56);
            trackBar_space.TabIndex = 3;
            trackBar_space.Value = 1;
            trackBar_space.Scroll += trackBar_space_Scroll;
            // 
            // label_second
            // 
            label_second.AutoSize = true;
            label_second.Location = new Point(289, 15);
            label_second.Name = "label_second";
            label_second.Size = new Size(17, 20);
            label_second.TabIndex = 2;
            label_second.Text = "5";
            // 
            // label_pixel
            // 
            label_pixel.AutoSize = true;
            label_pixel.Location = new Point(289, 71);
            label_pixel.Name = "label_pixel";
            label_pixel.Size = new Size(17, 20);
            label_pixel.TabIndex = 4;
            label_pixel.Text = "1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(347, 189);
            ControlBox = false;
            Controls.Add(label_pixel);
            Controls.Add(trackBar_space);
            Controls.Add(trackBar_tick);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label_second);
            Controls.Add(label1);
            Controls.Add(button_cancel);
            Controls.Add(button_save);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar_tick).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_space).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_save;
        private Button button_cancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TrackBar trackBar_tick;
        private TrackBar trackBar_space;
        private Label label_second;
        private Label label_pixel;
    }
}
