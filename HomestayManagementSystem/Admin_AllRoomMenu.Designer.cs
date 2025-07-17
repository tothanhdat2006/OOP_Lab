namespace HomestayManagementSystem
{
    partial class Admin_AllRoomMenu
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
            label1 = new Label();
            bookRoom_flowLayoutPanel = new FlowLayoutPanel();
            label2 = new Label();
            filter_flowLayoutPanel = new FlowLayoutPanel();
            label3 = new Label();
            checkedListBox1 = new CheckedListBox();
            label4 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label5 = new Label();
            dateTimePicker2 = new DateTimePicker();
            returnAdminMenu_button = new Button();
            bookRoom_flowLayoutPanel.SuspendLayout();
            filter_flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(338, 0);
            label1.Name = "label1";
            label1.Size = new Size(422, 45);
            label1.TabIndex = 0;
            label1.Text = "Chỉnh sửa thông tin phòng";
            // 
            // bookRoom_flowLayoutPanel
            // 
            bookRoom_flowLayoutPanel.AutoScroll = true;
            bookRoom_flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bookRoom_flowLayoutPanel.BorderStyle = BorderStyle.Fixed3D;
            bookRoom_flowLayoutPanel.Controls.Add(label2);
            bookRoom_flowLayoutPanel.Location = new Point(0, 44);
            bookRoom_flowLayoutPanel.Name = "bookRoom_flowLayoutPanel";
            bookRoom_flowLayoutPanel.Size = new Size(835, 673);
            bookRoom_flowLayoutPanel.TabIndex = 1;
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
            // filter_flowLayoutPanel
            // 
            filter_flowLayoutPanel.BorderStyle = BorderStyle.Fixed3D;
            filter_flowLayoutPanel.Controls.Add(label3);
            filter_flowLayoutPanel.Controls.Add(checkedListBox1);
            filter_flowLayoutPanel.Controls.Add(label4);
            filter_flowLayoutPanel.Controls.Add(dateTimePicker1);
            filter_flowLayoutPanel.Controls.Add(label5);
            filter_flowLayoutPanel.Controls.Add(dateTimePicker2);
            filter_flowLayoutPanel.Location = new Point(833, 44);
            filter_flowLayoutPanel.Name = "filter_flowLayoutPanel";
            filter_flowLayoutPanel.Size = new Size(247, 673);
            filter_flowLayoutPanel.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(46, 30);
            label3.TabIndex = 0;
            label3.Text = "Lọc";
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "Phòng trống", "Phòng đã cọc", "Phòng đang ở" });
            checkedListBox1.Location = new Point(3, 33);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(234, 58);
            checkedListBox1.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 94);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 3;
            label4.Text = "Từ ngày:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(3, 112);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(203, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 138);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 5;
            label5.Text = "Đến ngày:";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(3, 156);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(203, 23);
            dateTimePicker2.TabIndex = 4;
            // 
            // returnAdminMenu_button
            // 
            returnAdminMenu_button.Location = new Point(17, 9);
            returnAdminMenu_button.Name = "returnAdminMenu_button";
            returnAdminMenu_button.Size = new Size(75, 25);
            returnAdminMenu_button.TabIndex = 3;
            returnAdminMenu_button.Text = "Quay về";
            returnAdminMenu_button.UseVisualStyleBackColor = true;
            returnAdminMenu_button.Click += returnAdminMenu_button_Click;
            // 
            // Admin_AllRoomMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(returnAdminMenu_button);
            Controls.Add(filter_flowLayoutPanel);
            Controls.Add(bookRoom_flowLayoutPanel);
            Controls.Add(label1);
            Margin = new Padding(5);
            Name = "Admin_AllRoomMenu";
            Size = new Size(1080, 720);
            bookRoom_flowLayoutPanel.ResumeLayout(false);
            bookRoom_flowLayoutPanel.PerformLayout();
            filter_flowLayoutPanel.ResumeLayout(false);
            filter_flowLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private FlowLayoutPanel bookRoom_flowLayoutPanel;
        private Label label2;
        private FlowLayoutPanel filter_flowLayoutPanel;
        private Label label3;
        private CheckedListBox checkedListBox1;
        private Label label4;
        private DateTimePicker dateTimePicker1;
        private Label label5;
        private DateTimePicker dateTimePicker2;
        private Button returnAdminMenu_button;
    }
}
