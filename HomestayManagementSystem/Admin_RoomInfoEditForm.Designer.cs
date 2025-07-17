namespace HomestayManagementSystem
{
    partial class Admin_RoomInfoEditForm
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
            title = new Label();
            info_flowPanel = new FlowLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title.Location = new Point(404, 0);
            title.Name = "title";
            title.Size = new Size(273, 45);
            title.TabIndex = 5;
            title.Text = "Thông tin phòng";
            // 
            // info_flowPanel
            // 
            info_flowPanel.Location = new Point(240, 158);
            info_flowPanel.Name = "info_flowPanel";
            info_flowPanel.Size = new Size(600, 330);
            info_flowPanel.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(371, 620);
            button1.Name = "button1";
            button1.Size = new Size(75, 25);
            button1.TabIndex = 7;
            button1.Text = "Chỉnh sửa";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(634, 620);
            button2.Name = "button2";
            button2.Size = new Size(75, 25);
            button2.TabIndex = 8;
            button2.Text = "Lưu";
            button2.UseVisualStyleBackColor = true;
            // 
            // Admin_RoomInfoEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(info_flowPanel);
            Controls.Add(title);
            Name = "Admin_RoomInfoEditForm";
            Size = new Size(1080, 720);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label title;
        private FlowLayoutPanel info_flowPanel;
        private Button button1;
        private Button button2;
    }
}
