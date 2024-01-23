namespace GitHubInformationGrabber
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
            txtOrg = new TextBox();
            txtToken = new TextBox();
            btnGo = new Button();
            SuspendLayout();
            // 
            // txtOrg
            // 
            txtOrg.Location = new Point(12, 12);
            txtOrg.Name = "txtOrg";
            txtOrg.Size = new Size(475, 31);
            txtOrg.TabIndex = 0;
            txtOrg.Text = "Org";
            // 
            // txtToken
            // 
            txtToken.Location = new Point(12, 49);
            txtToken.Name = "txtToken";
            txtToken.Size = new Size(475, 31);
            txtToken.TabIndex = 2;
            txtToken.Text = "Token";
            // 
            // btnGo
            // 
            btnGo.Location = new Point(375, 86);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(112, 34);
            btnGo.TabIndex = 3;
            btnGo.Text = "Go";
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 130);
            Controls.Add(btnGo);
            Controls.Add(txtToken);
            Controls.Add(txtOrg);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtOrg;
        private TextBox txtToken;
        private Button btnGo;
    }
}
