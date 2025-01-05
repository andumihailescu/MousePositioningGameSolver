namespace GameSolverClient
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
            connectToServerBtn = new Button();
            txtReceived = new RichTextBox();
            solveTheGameBtn = new Button();
            kpLabel = new Label();
            kdLabel = new Label();
            kpNumeric = new NumericUpDown();
            kdNumeric = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)kpNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kdNumeric).BeginInit();
            SuspendLayout();
            // 
            // connectToServerBtn
            // 
            connectToServerBtn.Location = new Point(634, 60);
            connectToServerBtn.Margin = new Padding(3, 2, 3, 2);
            connectToServerBtn.Name = "connectToServerBtn";
            connectToServerBtn.Size = new Size(138, 22);
            connectToServerBtn.TabIndex = 1;
            connectToServerBtn.Text = "Connect to Server";
            connectToServerBtn.UseVisualStyleBackColor = true;
            connectToServerBtn.Click += connectToServerBtn_Click;
            // 
            // txtReceived
            // 
            txtReceived.Location = new Point(578, 115);
            txtReceived.Margin = new Padding(3, 2, 3, 2);
            txtReceived.Name = "txtReceived";
            txtReceived.Size = new Size(194, 235);
            txtReceived.TabIndex = 2;
            txtReceived.Text = "";
            // 
            // solveTheGameBtn
            // 
            solveTheGameBtn.Location = new Point(697, 87);
            solveTheGameBtn.Name = "solveTheGameBtn";
            solveTheGameBtn.Size = new Size(75, 23);
            solveTheGameBtn.TabIndex = 3;
            solveTheGameBtn.Text = "Solve the game";
            solveTheGameBtn.UseVisualStyleBackColor = true;
            solveTheGameBtn.Click += solveTheGameBtn_Click;
            // 
            // kpLabel
            // 
            kpLabel.AutoSize = true;
            kpLabel.Location = new Point(614, 10);
            kpLabel.Name = "kpLabel";
            kpLabel.Size = new Size(21, 15);
            kpLabel.TabIndex = 4;
            kpLabel.Text = "KP";
            // 
            // kdLabel
            // 
            kdLabel.AutoSize = true;
            kdLabel.Location = new Point(614, 33);
            kdLabel.Name = "kdLabel";
            kdLabel.Size = new Size(22, 15);
            kdLabel.TabIndex = 5;
            kdLabel.Text = "KD";
            // 
            // kpNumeric
            // 
            kpNumeric.DecimalPlaces = 1;
            kpNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            kpNumeric.Location = new Point(641, 8);
            kpNumeric.Margin = new Padding(3, 2, 3, 2);
            kpNumeric.Name = "kpNumeric";
            kpNumeric.Size = new Size(131, 23);
            kpNumeric.TabIndex = 6;
            kpNumeric.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // kdNumeric
            // 
            kdNumeric.DecimalPlaces = 1;
            kdNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            kdNumeric.Location = new Point(641, 33);
            kdNumeric.Margin = new Padding(3, 2, 3, 2);
            kdNumeric.Name = "kdNumeric";
            kdNumeric.Size = new Size(131, 23);
            kdNumeric.TabIndex = 7;
            kdNumeric.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 361);
            Controls.Add(kdNumeric);
            Controls.Add(kpNumeric);
            Controls.Add(kdLabel);
            Controls.Add(kpLabel);
            Controls.Add(solveTheGameBtn);
            Controls.Add(txtReceived);
            Controls.Add(connectToServerBtn);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_Close;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)kpNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)kdNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button connectToServerBtn;
        private RichTextBox txtReceived;
        private Button solveTheGameBtn;
        private Label kpLabel;
        private Label kdLabel;
        private NumericUpDown kpNumeric;
        private NumericUpDown kdNumeric;
    }
}
