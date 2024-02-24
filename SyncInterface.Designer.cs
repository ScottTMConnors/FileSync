namespace DesktopApplication {
    partial class DataCopyTool {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            dynamicPanel = new FlowLayoutPanel();
            addRow = new Button();
            SaveButton = new Button();
            LoadButton = new Button();
            ClearButton = new Button();
            SuspendLayout();
            // 
            // dynamicPanel
            // 
            dynamicPanel.AutoScroll = true;
            dynamicPanel.BorderStyle = BorderStyle.FixedSingle;
            dynamicPanel.Location = new Point(12, 12);
            dynamicPanel.Name = "dynamicPanel";
            dynamicPanel.Size = new Size(571, 322);
            dynamicPanel.TabIndex = 7;
            // 
            // addRow
            // 
            addRow.Location = new Point(174, 340);
            addRow.Name = "addRow";
            addRow.Size = new Size(75, 23);
            addRow.TabIndex = 8;
            addRow.Text = "Add Task";
            addRow.UseVisualStyleBackColor = true;
            addRow.Click += addRow_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(12, 340);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 11;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Location = new Point(93, 340);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(75, 23);
            LoadButton.TabIndex = 13;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(508, 340);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(75, 23);
            ClearButton.TabIndex = 14;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // DataCopyTool
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 376);
            Controls.Add(ClearButton);
            Controls.Add(LoadButton);
            Controls.Add(SaveButton);
            Controls.Add(addRow);
            Controls.Add(dynamicPanel);
            Name = "DataCopyTool";
            Text = "Parallel Data copy";
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel dynamicPanel;
        private Button addRow;
        private Button SaveButton;
        private Button LoadButton;
        private Button ClearButton;
    }
}
