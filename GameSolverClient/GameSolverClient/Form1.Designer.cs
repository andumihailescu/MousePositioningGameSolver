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
            SuspendLayout();
            // 
            // connectToServerBtn
            // 
            connectToServerBtn.Location = new Point(464, 162);
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
            txtReceived.Location = new Point(38, 50);
            txtReceived.Margin = new Padding(3, 2, 3, 2);
            txtReceived.Name = "txtReceived";
            txtReceived.Size = new Size(194, 280);
            txtReceived.TabIndex = 2;
            txtReceived.Text = "";
            // 
            // solveTheGameBtn
            // 
            solveTheGameBtn.Location = new Point(467, 249);
            solveTheGameBtn.Name = "solveTheGameBtn";
            solveTheGameBtn.Size = new Size(75, 23);
            solveTheGameBtn.TabIndex = 3;
            solveTheGameBtn.Text = "Solve the game";
            solveTheGameBtn.UseVisualStyleBackColor = true;
            solveTheGameBtn.Click += solveTheGameBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(solveTheGameBtn);
            Controls.Add(txtReceived);
            Controls.Add(connectToServerBtn);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_Close;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button connectToServerBtn;
        private RichTextBox txtReceived;
        private Button solveTheGameBtn;
    }
}
