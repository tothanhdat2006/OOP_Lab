namespace HomestayManagementSystem
{
    partial class GuestMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            returnAdminMenu_button = new Button();
            label1 = new Label();
            label2 = new Label();
            preview_flowLayoutPanel = new FlowLayoutPanel();
            preview_label = new Label();
            bookRoom_flowLayoutPanel = new FlowLayoutPanel();
            preview_flowLayoutPanel.SuspendLayout();
            bookRoom_flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // returnAdminMenu_button
            // 
            returnAdminMenu_button.Location = new Point(17, 11);
            returnAdminMenu_button.Name = "returnAdminMenu_button";
            returnAdminMenu_button.Size = new Size(75, 25);
            returnAdminMenu_button.TabIndex = 7;
            returnAdminMenu_button.Text = "Quay về";
            returnAdminMenu_button.UseVisualStyleBackColor = true;
            returnAdminMenu_button.Click += returnMainMenu_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(372, -2);
            label1.Name = "label1";
            label1.Size = new Size(273, 45);
            label1.TabIndex = 4;
            label1.Text = "Thông tin phòng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(197, 30);
            label2.TabIndex = 0;
            label2.Text = "\t\t\t\t\t\tKhung chọn phòng";
            // 
            // preview_flowLayoutPanel
            // 
            preview_flowLayoutPanel.BorderStyle = BorderStyle.Fixed3D;
            preview_flowLayoutPanel.Controls.Add(preview_label);
            preview_flowLayoutPanel.Location = new Point(833, 46);
            preview_flowLayoutPanel.Name = "preview_flowLayoutPanel";
            preview_flowLayoutPanel.Size = new Size(228, 673);
            preview_flowLayoutPanel.TabIndex = 6;
            // 
            // preview_label
            // 
            preview_label.Anchor = AnchorStyles.None;
            preview_label.AutoSize = true;
            preview_label.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            preview_label.Location = new Point(3, 0);
            preview_label.Name = "preview_label";
            preview_label.Size = new Size(110, 25);
            preview_label.TabIndex = 2;
            preview_label.Text = "PREVIEW";
            // 
            // bookRoom_flowLayoutPanel
            // 
            bookRoom_flowLayoutPanel.AutoScroll = true;
            bookRoom_flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bookRoom_flowLayoutPanel.BorderStyle = BorderStyle.Fixed3D;
            bookRoom_flowLayoutPanel.Controls.Add(label2);
            bookRoom_flowLayoutPanel.Location = new Point(1, 46);
            bookRoom_flowLayoutPanel.Name = "bookRoom_flowLayoutPanel";
            bookRoom_flowLayoutPanel.Size = new Size(834, 673);
            bookRoom_flowLayoutPanel.TabIndex = 5;
            // 
            // GuestMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 681);
            Controls.Add(returnAdminMenu_button);
            Controls.Add(label1);
            Controls.Add(preview_flowLayoutPanel);
            Controls.Add(bookRoom_flowLayoutPanel);
            Name = "GuestMenu";
            preview_flowLayoutPanel.ResumeLayout(false);
            preview_flowLayoutPanel.PerformLayout();
            bookRoom_flowLayoutPanel.ResumeLayout(false);
            bookRoom_flowLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button returnAdminMenu_button;
        private Label label1;
        private Label label2;
        private FlowLayoutPanel preview_flowLayoutPanel;
        private FlowLayoutPanel bookRoom_flowLayoutPanel;
        private Label preview_label;
    }
}
