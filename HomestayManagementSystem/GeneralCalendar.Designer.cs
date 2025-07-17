namespace HomestayManagementSystem
{
    partial class GeneralCalendar
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
            title = new Label();
            panel1 = new Panel();
            weekCalendar_table = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            checkedListBox1 = new CheckedListBox();
            room_flowPanel = new FlowLayoutPanel();
            panel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // returnAdminMenu_button
            // 
            returnAdminMenu_button.Location = new Point(9, 9);
            returnAdminMenu_button.Name = "returnAdminMenu_button";
            returnAdminMenu_button.Size = new Size(75, 25);
            returnAdminMenu_button.TabIndex = 5;
            returnAdminMenu_button.Text = "Quay về";
            returnAdminMenu_button.UseVisualStyleBackColor = true;
            returnAdminMenu_button.Click += returnAdminMenu_button_Click;
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title.Location = new Point(427, 0);
            title.Name = "title";
            title.Size = new Size(159, 45);
            title.TabIndex = 4;
            title.Text = "Lịch tổng";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(weekCalendar_table);
            panel1.Controls.Add(flowLayoutPanel2);
            panel1.Controls.Add(room_flowPanel);
            panel1.Location = new Point(0, 42);
            panel1.Name = "panel1";
            panel1.Size = new Size(1077, 678);
            panel1.TabIndex = 6;
            // 
            // weekCalendar_table
            // 
            weekCalendar_table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            weekCalendar_table.ColumnCount = 7;
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            weekCalendar_table.Location = new Point(209, 6);
            weekCalendar_table.Name = "weekCalendar_table";
            weekCalendar_table.RowCount = 1;
            weekCalendar_table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            weekCalendar_table.Size = new Size(865, 669);
            weekCalendar_table.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(checkedListBox1);
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(200, 63);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "Phòng trống", "Phòng đã cọc", "Phòng đang ở" });
            checkedListBox1.Location = new Point(3, 3);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(197, 58);
            checkedListBox1.TabIndex = 2;
            // 
            // room_flowPanel
            // 
            room_flowPanel.BorderStyle = BorderStyle.FixedSingle;
            room_flowPanel.Location = new Point(3, 72);
            room_flowPanel.Name = "room_flowPanel";
            room_flowPanel.Size = new Size(200, 603);
            room_flowPanel.TabIndex = 0;
            // 
            // GeneralCalendar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(returnAdminMenu_button);
            Controls.Add(title);
            Name = "GeneralCalendar";
            Size = new Size(1080, 720);
            panel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button returnAdminMenu_button;
        private Label title;
        private Panel panel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel room_flowPanel;
        private TableLayoutPanel weekCalendar_table;
        private CheckedListBox checkedListBox1;
    }
}
