namespace HomestayManagementSystem
{
    public partial class mainMenu : Form
    {
        public mainMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void signin_button_Click(object sender, EventArgs e)
        {
            if(username_textBox.Text == "admin" && password_textBox.Text == "admin123")
            {
                this.Hide();
                AdminMenu adminMenu_Form = new AdminMenu();
                adminMenu_Form.ShowDialog();
                this.Close();
            }
            else if (username_textBox.Text == "user" && password_textBox.Text == "user123")
            {
                this.Hide();
                GuestMenu guestMenu_Form = new GuestMenu(username_textBox.Text);
                guestMenu_Form.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}
