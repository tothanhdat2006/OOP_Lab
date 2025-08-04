namespace HomestayManagementSystem
{
	public partial class mainMenu : Form
	{
		public mainMenu()
		{
			InitializeComponent();
		}

		private void mainMenu_Load(object sender, EventArgs e)
		{

		}

		private void signin_button_Click(object sender, EventArgs e)
		{
			// Lấy thông tin từ textbox
			string username = username_textBox.Text.Trim();
			string password = password_textBox.Text.Trim();

			// Kiểm tra input rỗng
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập.");
				return;
			}

			// Tạo chuỗi để tìm kiếm trong file credentials
			string credentialToCheck = username + ":" + password;

			// Đường dẫn đến file credentials
			string credentialsPath = @"./Data/Users/credentials.txt";

			// Kiểm tra file có tồn tại không
			if (!System.IO.File.Exists(credentialsPath))
			{
				MessageBox.Show(Path.GetFullPath(credentialsPath) + " không tồn tại.");
				return;
			}

			try
			{
				// Đọc tất cả các dòng trong file credentials
				string[] credentialLines = System.IO.File.ReadAllLines(credentialsPath);

				// Kiểm tra xem thông tin đăng nhập có tồn tại trong file không
				bool isValidCredential = false;
				foreach (string line in credentialLines)
				{
					if (line.Trim() == credentialToCheck)
					{
						isValidCredential = true;
						break;
					}
				}

				if (isValidCredential)
				{
					// Đăng nhập thành công - ẩn form hiện tại

					// Kiểm tra quyền dựa trên username để mở form tương ứng
					if (username == "admin")
					{
                        this.Hide();
                        AdminMenu adminMenu_Form = new AdminMenu();
                        adminMenu_Form.Tag = this;
                        adminMenu_Form.Show(this);
                        Hide();
                    }
					else
					{
                        this.Hide();
                        GuestMenu guestMenu_Form = new GuestMenu();
                        guestMenu_Form.Tag = this;
                        guestMenu_Form.Show(this);
                        Hide();
                    }
				}
				else
				{
					MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác. Vui lòng thử lại.");
					// Xóa mật khẩu để bảo mật
					password_textBox.Clear();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi đọc file thông tin đăng nhập: {ex.Message}");
			}
		}
	}
}
