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
            dynamicPanel.Location = new Point(22, 26);
            dynamicPanel.Margin = new Padding(6, 6, 6, 6);
            dynamicPanel.Name = "dynamicPanel";
            dynamicPanel.Size = new Size(1148, 685);
            dynamicPanel.TabIndex = 7;
            // 
            // addRow
            // 
            addRow.Location = new Point(323, 725);
            addRow.Margin = new Padding(6, 6, 6, 6);
            addRow.Name = "addRow";
            addRow.Size = new Size(139, 49);
            addRow.TabIndex = 8;
            addRow.Text = "Add Task";
            addRow.UseVisualStyleBackColor = true;
            addRow.Click += addRow_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(22, 725);
            SaveButton.Margin = new Padding(6, 6, 6, 6);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(139, 49);
            SaveButton.TabIndex = 11;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Location = new Point(173, 725);
            LoadButton.Margin = new Padding(6, 6, 6, 6);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(139, 49);
            LoadButton.TabIndex = 13;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(1031, 725);
            ClearButton.Margin = new Padding(6, 6, 6, 6);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(139, 49);
            ClearButton.TabIndex = 14;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // DataCopyTool
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1185, 802);
            Controls.Add(ClearButton);
            Controls.Add(LoadButton);
            Controls.Add(SaveButton);
            Controls.Add(addRow);
            Controls.Add(dynamicPanel);
            Margin = new Padding(6, 6, 6, 6);
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
