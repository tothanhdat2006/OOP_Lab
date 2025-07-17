namespace HomestayManagementSystem
{
    public partial class mainMenu : Form
    {
        public mainMenu()
        {
            InitializeComponent();
        }

        public void RestoreMenu()
        {
            this.Controls.Clear();
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void signin_button_Click(object sender, EventArgs e)
        {
            if(username_textBox.Text == "admin")
            {
                this.Hide();
                AdminMenu adminMenu_Form = new AdminMenu();
                adminMenu_Form.Tag = this;
                adminMenu_Form.Show(this);
                Hide();
            }
            else if (username_textBox.Text == "user")
            {
                this.Hide();
                GuestMenu guestMenu_Form = new GuestMenu();
                guestMenu_Form.Tag = this;
                guestMenu_Form.Show(this);
                Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}
