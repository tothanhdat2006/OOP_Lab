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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			menuStrip1 = new MenuStrip();
			tableLayoutPanel1 = new TableLayoutPanel();
			preview_label = new Label();
			selection_label = new Label();
			room_selection_panel = new Panel();
			panel2_test = new Panel();
			linkLabel1 = new LinkLabel();
			label3 = new Label();
			label2 = new Label();
			label1 = new Label();
			comboBox1 = new ComboBox();
			panel1 = new Panel();
			panel2 = new Panel();
			label9 = new Label();
			label8 = new Label();
			label7 = new Label();
			label5 = new Label();
			label6 = new Label();
			tableLayoutPanel2 = new TableLayoutPanel();
			label4 = new Label();
			picture_panel = new Panel();
			tableLayoutPanel1.SuspendLayout();
			room_selection_panel.SuspendLayout();
			panel2_test.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			SuspendLayout();
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new Size(20, 20);
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(1062, 24);
			menuStrip1.TabIndex = 1;
			menuStrip1.Text = "menuStrip1";
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.BackColor = SystemColors.GradientActiveCaption;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.40589F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.5941124F));
			tableLayoutPanel1.Controls.Add(preview_label, 1, 0);
			tableLayoutPanel1.Controls.Add(selection_label, 0, 0);
			tableLayoutPanel1.Controls.Add(room_selection_panel, 0, 1);
			tableLayoutPanel1.Controls.Add(panel1, 1, 1);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 24);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 95F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel1.Size = new Size(1062, 649);
			tableLayoutPanel1.TabIndex = 3;
			tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
			// 
			// preview_label
			// 
			preview_label.Anchor = AnchorStyles.None;
			preview_label.AutoSize = true;
			preview_label.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			preview_label.Location = new Point(804, 1);
			preview_label.Name = "preview_label";
			preview_label.Size = new Size(136, 29);
			preview_label.TabIndex = 1;
			preview_label.Text = "PREVIEW";
			// 
			// selection_label
			// 
			selection_label.Anchor = AnchorStyles.None;
			selection_label.AutoSize = true;
			selection_label.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
			selection_label.Location = new Point(215, 1);
			selection_label.Name = "selection_label";
			selection_label.Size = new Size(252, 29);
			selection_label.TabIndex = 0;
			selection_label.Text = "ROOM SELECTION";
			// 
			// room_selection_panel
			// 
			room_selection_panel.BackColor = SystemColors.Control;
			room_selection_panel.Controls.Add(panel2_test);
			room_selection_panel.Controls.Add(label1);
			room_selection_panel.Controls.Add(comboBox1);
			room_selection_panel.Dock = DockStyle.Fill;
			room_selection_panel.Location = new Point(3, 35);
			room_selection_panel.Name = "room_selection_panel";
			room_selection_panel.Size = new Size(677, 611);
			room_selection_panel.TabIndex = 2;
			room_selection_panel.Resize += room_selection_panel_Resize;
			// 
			// panel2_test
			// 
			panel2_test.AllowDrop = true;
			panel2_test.BackColor = SystemColors.ActiveCaption;
			panel2_test.Controls.Add(linkLabel1);
			panel2_test.Controls.Add(label3);
			panel2_test.Controls.Add(label2);
			panel2_test.Location = new Point(3, 64);
			panel2_test.Name = "panel2_test";
			panel2_test.Size = new Size(674, 125);
			panel2_test.TabIndex = 3;
			panel2_test.ControlAdded += Panel2Test_ControlAdded;
			panel2_test.DragDrop += Panel2Test_DragDrop;
			panel2_test.DragEnter += Panel2Test_DragEnter;
			// 
			// linkLabel1
			// 
			linkLabel1.AutoSize = true;
			linkLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
			linkLabel1.Location = new Point(268, 86);
			linkLabel1.Name = "linkLabel1";
			linkLabel1.Size = new Size(143, 28);
			linkLabel1.TabIndex = 2;
			linkLabel1.TabStop = true;
			linkLabel1.Text = "Book this room";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F);
			label3.Location = new Point(257, 48);
			label3.Name = "label3";
			label3.Size = new Size(154, 28);
			label3.TabIndex = 1;
			label3.Text = "Status: Available";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.Location = new Point(271, 17);
			label2.Name = "label2";
			label2.Size = new Size(122, 31);
			label2.TabIndex = 0;
			label2.Text = "Room 101";
			label2.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(3, 6);
			label1.Name = "label1";
			label1.Size = new Size(119, 20);
			label1.TabIndex = 2;
			label1.Text = "Search for room:";
			// 
			// comboBox1
			// 
			comboBox1.AutoCompleteCustomSource.AddRange(new string[] { "101 (Empty)", "102 (Rented)", "103 (Rented)" });
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(123, 3);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(551, 28);
			comboBox1.TabIndex = 1;
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.Control;
			panel1.Controls.Add(panel2);
			panel1.Controls.Add(tableLayoutPanel2);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(686, 35);
			panel1.Name = "panel1";
			panel1.Size = new Size(373, 611);
			panel1.TabIndex = 3;
			// 
			// panel2
			// 
			panel2.Controls.Add(label9);
			panel2.Controls.Add(label8);
			panel2.Controls.Add(label7);
			panel2.Controls.Add(label5);
			panel2.Controls.Add(label6);
			panel2.Location = new Point(0, 0);
			panel2.Name = "panel2";
			panel2.Size = new Size(376, 205);
			panel2.TabIndex = 1;
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Font = new Font("Segoe UI", 12F);
			label9.Location = new Point(46, 161);
			label9.Name = "label9";
			label9.Size = new Size(120, 28);
			label9.TabIndex = 7;
			label9.Text = "- Price: 800$";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Font = new Font("Segoe UI", 12F);
			label8.Location = new Point(46, 121);
			label8.Name = "label8";
			label8.Size = new Size(129, 28);
			label8.TabIndex = 6;
			label8.Text = "- Balcony: Yes";
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Font = new Font("Segoe UI", 12F);
			label7.Location = new Point(46, 84);
			label7.Name = "label7";
			label7.Size = new Size(193, 28);
			label7.TabIndex = 5;
			label7.Text = "- Capacity: 8 persons";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 12F);
			label5.Location = new Point(46, 46);
			label5.Name = "label5";
			label5.Size = new Size(167, 28);
			label5.TabIndex = 4;
			label5.Text = "- Status: Available";
			label5.Click += label5_Click;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label6.Location = new Point(143, 15);
			label6.Name = "label6";
			label6.Size = new Size(122, 31);
			label6.TabIndex = 3;
			label6.Text = "Room 101";
			label6.TextAlign = ContentAlignment.MiddleCenter;
			label6.Click += label6_Click;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 1;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel2.Controls.Add(label4, 0, 0);
			tableLayoutPanel2.Controls.Add(picture_panel, 0, 1);
			tableLayoutPanel2.Location = new Point(3, 208);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10.10101F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 89.89899F));
			tableLayoutPanel2.Size = new Size(370, 396);
			tableLayoutPanel2.TabIndex = 0;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Top;
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label4.Location = new Point(140, 0);
			label4.Name = "label4";
			label4.Size = new Size(90, 31);
			label4.TabIndex = 0;
			label4.Text = "Picture";
			// 
			// picture_panel
			// 
			picture_panel.Dock = DockStyle.Fill;
			picture_panel.Location = new Point(3, 43);
			picture_panel.Name = "picture_panel";
			picture_panel.Size = new Size(364, 350);
			picture_panel.TabIndex = 1;
			// 
			// GuestMenu
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1062, 673);
			Controls.Add(tableLayoutPanel1);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "GuestMenu";
			Text = "GuestMenu";
			Load += GuestMenu_Load;
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			room_selection_panel.ResumeLayout(false);
			room_selection_panel.PerformLayout();
			panel2_test.ResumeLayout(false);
			panel2_test.PerformLayout();
			panel1.ResumeLayout(false);
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private MenuStrip menuStrip1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label selection_label;
        private Label preview_label;
        private Panel room_selection_panel;
        private Label label1;
        private ComboBox comboBox1;
		private Panel panel2_test;
		private Label label2;
		private LinkLabel linkLabel1;
		private Label label3;
		private Panel panel1;
		private TableLayoutPanel tableLayoutPanel2;
		private Panel panel2;
		private Label label5;
		private Label label6;
		private Label label4;
		private Label label8;
		private Label label7;
		private Label label9;
		private Panel picture_panel;
	}
}