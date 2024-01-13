namespace DesktopApplication {
    partial class ObjectScheduleForm {
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
            SourceLabel = new Label();
            DestinationLabel = new Label();
            SuspendLayout();
            // 
            // SourceLabel
            // 
            SourceLabel.AutoSize = true;
            SourceLabel.Location = new Point(91, 109);
            SourceLabel.Name = "SourceLabel";
            SourceLabel.Size = new Size(43, 15);
            SourceLabel.TabIndex = 0;
            SourceLabel.Text = "Source";
            // 
            // DestinationLabel
            // 
            DestinationLabel.AutoSize = true;
            DestinationLabel.Location = new Point(549, 109);
            DestinationLabel.Name = "DestinationLabel";
            DestinationLabel.Size = new Size(67, 15);
            DestinationLabel.TabIndex = 1;
            DestinationLabel.Text = "Destination";
            // 
            // ObjectScheduleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DestinationLabel);
            Controls.Add(SourceLabel);
            Name = "ObjectScheduleForm";
            Text = "ObjectScheduleForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label SourceLabel;
        private Label DestinationLabel;
    }
}