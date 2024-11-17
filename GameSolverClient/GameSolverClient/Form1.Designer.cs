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
            SuspendLayout();
            // 
            // connectToServerBtn
            // 
            connectToServerBtn.Location = new Point(530, 216);
            connectToServerBtn.Name = "connectToServerBtn";
            connectToServerBtn.Size = new Size(158, 29);
            connectToServerBtn.TabIndex = 1;
            connectToServerBtn.Text = "Connect to Server";
            connectToServerBtn.UseVisualStyleBackColor = true;
            connectToServerBtn.Click += connectToServerBtn_Click;
            // 
            // txtReceived
            // 
            txtReceived.Location = new Point(43, 66);
            txtReceived.Name = "txtReceived";
            txtReceived.Size = new Size(221, 372);
            txtReceived.TabIndex = 2;
            txtReceived.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtReceived);
            Controls.Add(connectToServerBtn);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_Close;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button connectToServerBtn;
        private RichTextBox txtReceived;
    }
}
