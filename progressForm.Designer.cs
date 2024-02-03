namespace DesktopApplication {
    partial class progressForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            progressBar = new ProgressBar();
            progressLabel = new Label();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.Location = new Point(193, 135);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(200, 46);
            progressBar.TabIndex = 0;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Location = new Point(173, 257);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(78, 32);
            progressLabel.TabIndex = 1;
            progressLabel.Text = "label1";
            // 
            // progressForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(progressLabel);
            Controls.Add(progressBar);
            Name = "progressForm";
            Text = "progressForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar;
        private Label progressLabel;
    }
}