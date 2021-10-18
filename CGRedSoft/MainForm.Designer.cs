namespace CGRedSoft
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
            this.btnEnableWall = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numDistanceLblXOffset = new System.Windows.Forms.NumericUpDown();
            this.numDistanceLblYOffset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceLblXOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceLblYOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnableWall
            // 
            this.btnEnableWall.Location = new System.Drawing.Point(6, 93);
            this.btnEnableWall.Name = "btnEnableWall";
            this.btnEnableWall.Size = new System.Drawing.Size(248, 38);
            this.btnEnableWall.TabIndex = 0;
            this.btnEnableWall.Text = "Disabled";
            this.btnEnableWall.UseVisualStyleBackColor = true;
            this.btnEnableWall.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numDistanceLblYOffset);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numDistanceLblXOffset);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnEnableWall);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 141);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Distance label X Offset:";
            // 
            // numDistanceLblXOffset
            // 
            this.numDistanceLblXOffset.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDistanceLblXOffset.Location = new System.Drawing.Point(130, 28);
            this.numDistanceLblXOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numDistanceLblXOffset.Name = "numDistanceLblXOffset";
            this.numDistanceLblXOffset.Size = new System.Drawing.Size(60, 20);
            this.numDistanceLblXOffset.TabIndex = 2;
            this.numDistanceLblXOffset.ValueChanged += new System.EventHandler(this.numDistanceLblXOffset_ValueChanged);
            // 
            // numDistanceLblYOffset
            // 
            this.numDistanceLblYOffset.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDistanceLblYOffset.Location = new System.Drawing.Point(130, 54);
            this.numDistanceLblYOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numDistanceLblYOffset.Name = "numDistanceLblYOffset";
            this.numDistanceLblYOffset.Size = new System.Drawing.Size(60, 20);
            this.numDistanceLblYOffset.TabIndex = 4;
            this.numDistanceLblYOffset.ValueChanged += new System.EventHandler(this.numDistanceLblYOffset_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Distance label Y Offset:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 151);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notepad";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceLblXOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceLblYOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEnableWall;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numDistanceLblXOffset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numDistanceLblYOffset;
        private System.Windows.Forms.Label label2;
    }
}

