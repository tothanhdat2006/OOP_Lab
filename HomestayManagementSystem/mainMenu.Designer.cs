namespace HomestayManagementSystem
{
    partial class mainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            signin_button = new Button();
            username_textBox = new TextBox();
            username_label = new Label();
            password_label = new Label();
            password_textBox = new TextBox();
            signup_button = new Button();
            SuspendLayout();
            // 
            // signin_button
            // 
            signin_button.Location = new Point(622, 373);
            signin_button.Name = "signin_button";
            signin_button.Size = new Size(150, 58);
            signin_button.TabIndex = 3;
            signin_button.Text = "Đăng nhập";
            signin_button.UseVisualStyleBackColor = true;
            signin_button.Click += signin_button_Click;
            // 
            // username_textBox
            // 
            username_textBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            username_textBox.Location = new Point(354, 255);
            username_textBox.Name = "username_textBox";
            username_textBox.Size = new Size(418, 33);
            username_textBox.TabIndex = 1;
            // 
            // username_label
            // 
            username_label.AutoSize = true;
            username_label.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            username_label.Location = new Point(233, 256);
            username_label.Name = "username_label";
            username_label.Size = new Size(115, 32);
            username_label.TabIndex = 2;
            username_label.Text = "Tài khoản";
            // 
            // password_label
            // 
            password_label.AutoSize = true;
            password_label.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            password_label.Location = new Point(233, 296);
            password_label.Name = "password_label";
            password_label.Size = new Size(115, 32);
            password_label.TabIndex = 4;
            password_label.Text = "Mật khẩu";
            // 
            // password_textBox
            // 
            password_textBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            password_textBox.Location = new Point(354, 296);
            password_textBox.Name = "password_textBox";
            password_textBox.Size = new Size(418, 33);
            password_textBox.TabIndex = 2;
            // 
            // signup_button
            // 
            signup_button.Location = new Point(233, 373);
            signup_button.Name = "signup_button";
            signup_button.Size = new Size(150, 58);
            signup_button.TabIndex = 5;
            signup_button.Text = "Đăng ký";
            signup_button.UseVisualStyleBackColor = true;
            // 
            // mainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 681);
            Controls.Add(signup_button);
            Controls.Add(password_label);
            Controls.Add(password_textBox);
            Controls.Add(username_label);
            Controls.Add(username_textBox);
            Controls.Add(signin_button);
            Name = "mainMenu";
            Text = "Giao diện chính";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button signin_button;
        private TextBox username_textBox;
        private Label username_label;
        private Label password_label;
        private TextBox password_textBox;
        private Button signup_button;
    }
}
