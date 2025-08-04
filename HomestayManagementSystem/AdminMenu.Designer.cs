namespace HomestayManagementSystem
{
    partial class AdminMenu
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
            welcome_label = new Label();
            showCalendar_button = new Button();
            updateRoomInfo_button = new Button();
            returnMainMenu_button = new Button();
            SuspendLayout();
            // 
            // welcome_label
            // 
            welcome_label.AutoSize = true;
            welcome_label.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            welcome_label.Location = new Point(431, 141);
            welcome_label.Name = "welcome_label";
            welcome_label.Size = new Size(177, 30);
            welcome_label.TabIndex = 0;
            welcome_label.Text = "Welcome, admin";
            // 
            // showCalendar_button
            // 
            showCalendar_button.Location = new Point(455, 207);
            showCalendar_button.Name = "showCalendar_button";
            showCalendar_button.Size = new Size(119, 52);
            showCalendar_button.TabIndex = 1;
            showCalendar_button.Text = "Calendar";
            showCalendar_button.UseVisualStyleBackColor = true;
            showCalendar_button.Click += showCalendar_button_Click;
            // 
            // updateRoomInfo_button
            // 
            updateRoomInfo_button.Location = new Point(455, 281);
            updateRoomInfo_button.Name = "updateRoomInfo_button";
            updateRoomInfo_button.Size = new Size(119, 60);
            updateRoomInfo_button.TabIndex = 2;
            updateRoomInfo_button.Text = "Room information";
            updateRoomInfo_button.UseVisualStyleBackColor = true;
            updateRoomInfo_button.Click += updateRoomInfo_button_Click;
            // 
            // returnMainMenu_button
            // 
            returnMainMenu_button.Location = new Point(12, 12);
            returnMainMenu_button.Name = "returnMainMenu_button";
            returnMainMenu_button.Size = new Size(75, 25);
            returnMainMenu_button.TabIndex = 8;
            returnMainMenu_button.Text = "Back";
            returnMainMenu_button.UseVisualStyleBackColor = true;
            returnMainMenu_button.Click += returnMainMenu_button_Click;
            // 
            // AdminMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 681);
            Controls.Add(returnMainMenu_button);
            Controls.Add(updateRoomInfo_button);
            Controls.Add(showCalendar_button);
            Controls.Add(welcome_label);
            Name = "AdminMenu";
            Text = "AdminMenu";
            FormClosed += AdminMenu_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label welcome_label;
        private Button showCalendar_button;
        private Button updateRoomInfo_button;
        private Button returnMainMenu_button;
    }
}