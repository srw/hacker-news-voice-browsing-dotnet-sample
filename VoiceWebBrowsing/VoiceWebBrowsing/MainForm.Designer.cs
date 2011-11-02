namespace VoiceWebBrowsing
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.CommandTextBox = new System.Windows.Forms.TextBox();
            this.ReceiveHackerNewsButton = new System.Windows.Forms.Button();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.StopButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ArticleNumberUpDown = new System.Windows.Forms.NumericUpDown();
            this.ReadArticleButton = new System.Windows.Forms.Button();
            this.SaveArticleButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ArticleNumberUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Voice Command";
            // 
            // CommandTextBox
            // 
            this.CommandTextBox.Location = new System.Drawing.Point(115, 27);
            this.CommandTextBox.Name = "CommandTextBox";
            this.CommandTextBox.ReadOnly = true;
            this.CommandTextBox.Size = new System.Drawing.Size(453, 20);
            this.CommandTextBox.TabIndex = 1;
            // 
            // ReceiveHackerNewsButton
            // 
            this.ReceiveHackerNewsButton.Location = new System.Drawing.Point(6, 27);
            this.ReceiveHackerNewsButton.Name = "ReceiveHackerNewsButton";
            this.ReceiveHackerNewsButton.Size = new System.Drawing.Size(135, 20);
            this.ReceiveHackerNewsButton.TabIndex = 2;
            this.ReceiveHackerNewsButton.Text = "ReceiveHackerNews";
            this.ReceiveHackerNewsButton.UseVisualStyleBackColor = true;
            this.ReceiveHackerNewsButton.Click += new System.EventHandler(this.ReceiveHackerNewsButton_Click);
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerSupportsCancellation = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(486, 27);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(50, 20);
            this.StopButton.TabIndex = 3;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SaveArticleButton);
            this.groupBox1.Controls.Add(this.ArticleNumberUpDown);
            this.groupBox1.Controls.Add(this.ReadArticleButton);
            this.groupBox1.Controls.Add(this.ReceiveHackerNewsButton);
            this.groupBox1.Controls.Add(this.StopButton);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(553, 68);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manual Command";
            // 
            // ArticleNumberUpDown
            // 
            this.ArticleNumberUpDown.Location = new System.Drawing.Point(406, 27);
            this.ArticleNumberUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.ArticleNumberUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ArticleNumberUpDown.Name = "ArticleNumberUpDown";
            this.ArticleNumberUpDown.Size = new System.Drawing.Size(47, 20);
            this.ArticleNumberUpDown.TabIndex = 5;
            this.ArticleNumberUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ReadArticleButton
            // 
            this.ReadArticleButton.Location = new System.Drawing.Point(154, 27);
            this.ReadArticleButton.Name = "ReadArticleButton";
            this.ReadArticleButton.Size = new System.Drawing.Size(118, 20);
            this.ReadArticleButton.TabIndex = 4;
            this.ReadArticleButton.Text = "Read Article #";
            this.ReadArticleButton.UseVisualStyleBackColor = true;
            this.ReadArticleButton.Click += new System.EventHandler(this.ReadArticleButton_Click);
            // 
            // SaveArticleButton
            // 
            this.SaveArticleButton.Location = new System.Drawing.Point(278, 27);
            this.SaveArticleButton.Name = "SaveArticleButton";
            this.SaveArticleButton.Size = new System.Drawing.Size(122, 20);
            this.SaveArticleButton.TabIndex = 6;
            this.SaveArticleButton.Text = "Save Article #";
            this.SaveArticleButton.UseVisualStyleBackColor = true;
            this.SaveArticleButton.Click += new System.EventHandler(this.SaveArticleButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 138);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CommandTextBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Voice Web Browsing";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ArticleNumberUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CommandTextBox;
        private System.Windows.Forms.Button ReceiveHackerNewsButton;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown ArticleNumberUpDown;
        private System.Windows.Forms.Button ReadArticleButton;
        private System.Windows.Forms.Button SaveArticleButton;
    }
}

