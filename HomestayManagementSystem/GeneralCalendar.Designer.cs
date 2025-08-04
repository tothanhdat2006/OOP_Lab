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
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            room_flowPanel = new FlowLayoutPanel();
            nextWeek_button = new Button();
            prevWeek_button = new Button();
            panel1.SuspendLayout();
            weekCalendar_table.SuspendLayout();
            SuspendLayout();
            // 
            // returnAdminMenu_button
            // 
            returnAdminMenu_button.Location = new Point(9, 9);
            returnAdminMenu_button.Name = "returnAdminMenu_button";
            returnAdminMenu_button.Size = new Size(75, 25);
            returnAdminMenu_button.TabIndex = 5;
            returnAdminMenu_button.Text = "Back";
            returnAdminMenu_button.UseVisualStyleBackColor = true;
            returnAdminMenu_button.Click += returnAdminMenu_button_Click;
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title.Location = new Point(427, 0);
            title.Name = "title";
            title.Size = new Size(152, 45);
            title.TabIndex = 4;
            title.Text = "Calendar";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(weekCalendar_table);
            panel1.Controls.Add(room_flowPanel);
            panel1.Location = new Point(0, 42);
            panel1.Name = "panel1";
            panel1.Size = new Size(1077, 678);
            panel1.TabIndex = 6;
            // 
            // weekCalendar_table
            // 
            weekCalendar_table.AutoScroll = true;
            weekCalendar_table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            weekCalendar_table.ColumnCount = 7;
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857132F));
            weekCalendar_table.Controls.Add(label6, 5, 0);
            weekCalendar_table.Controls.Add(label5, 4, 0);
            weekCalendar_table.Controls.Add(label4, 3, 0);
            weekCalendar_table.Controls.Add(label2, 1, 0);
            weekCalendar_table.Controls.Add(label1, 0, 0);
            weekCalendar_table.Controls.Add(label3, 2, 0);
            weekCalendar_table.Controls.Add(label7, 6, 0);
            weekCalendar_table.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            weekCalendar_table.Location = new Point(209, 6);
            weekCalendar_table.Name = "weekCalendar_table";
            weekCalendar_table.RowCount = 1;
            weekCalendar_table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            weekCalendar_table.Size = new Size(865, 669);
            weekCalendar_table.TabIndex = 2;
            weekCalendar_table.Paint += weekCalendar_table_Paint;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(784, 1);
            label7.Margin = new Padding(45, 0, 3, 0);
            label7.Name = "label7";
            label7.Size = new Size(37, 21);
            label7.TabIndex = 6;
            label7.Text = "Sun";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(661, 1);
            label6.Margin = new Padding(45, 0, 3, 0);
            label6.Name = "label6";
            label6.Size = new Size(32, 21);
            label6.TabIndex = 5;
            label6.Text = "Sat";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(538, 1);
            label5.Margin = new Padding(45, 0, 3, 0);
            label5.Name = "label5";
            label5.Size = new Size(28, 21);
            label5.TabIndex = 4;
            label5.Text = "Fri";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(415, 1);
            label4.Margin = new Padding(45, 0, 3, 0);
            label4.Name = "label4";
            label4.Size = new Size(36, 21);
            label4.TabIndex = 3;
            label4.Text = "Thu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(292, 1);
            label3.Margin = new Padding(45, 0, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(41, 21);
            label3.TabIndex = 2;
            label3.Text = "Wed";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(169, 1);
            label2.Margin = new Padding(45, 0, 3, 0);
            label2.Name = "label2";
            label2.Size = new Size(35, 21);
            label2.TabIndex = 1;
            label2.Text = "Tue";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(46, 1);
            label1.Margin = new Padding(45, 0, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(42, 21);
            label1.TabIndex = 0;
            label1.Text = "Mon";
            // 
            // room_flowPanel
            // 
            room_flowPanel.BorderStyle = BorderStyle.FixedSingle;
            room_flowPanel.Location = new Point(3, 7);
            room_flowPanel.Name = "room_flowPanel";
            room_flowPanel.Size = new Size(200, 668);
            room_flowPanel.TabIndex = 0;
            // 
            // nextWeek_button
            // 
            nextWeek_button.Location = new Point(972, 11);
            nextWeek_button.Name = "nextWeek_button";
            nextWeek_button.Size = new Size(105, 25);
            nextWeek_button.TabIndex = 7;
            nextWeek_button.Text = "Next week";
            nextWeek_button.UseVisualStyleBackColor = true;
            // 
            // prevWeek_button
            // 
            prevWeek_button.Location = new Point(861, 11);
            prevWeek_button.Name = "prevWeek_button";
            prevWeek_button.Size = new Size(105, 25);
            prevWeek_button.TabIndex = 8;
            prevWeek_button.Text = "Previous week";
            prevWeek_button.UseVisualStyleBackColor = true;
            // 
            // GeneralCalendar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(prevWeek_button);
            Controls.Add(nextWeek_button);
            Controls.Add(panel1);
            Controls.Add(returnAdminMenu_button);
            Controls.Add(title);
            Name = "GeneralCalendar";
            Size = new Size(1080, 720);
            panel1.ResumeLayout(false);
            weekCalendar_table.ResumeLayout(false);
            weekCalendar_table.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button returnAdminMenu_button;
        private Label title;
        private Panel panel1;
        private FlowLayoutPanel room_flowPanel;
        private TableLayoutPanel weekCalendar_table;
        private Label label1;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button nextWeek_button;
        private Button prevWeek_button;
    }
}
