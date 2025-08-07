using IniParser;
using IniParser.Model;

namespace HomestayManagementSystem
{
	public partial class mainMenu : Form
	{
		public mainMenu()
		{
			InitializeComponent();
			this.AcceptButton = signin_button;
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

		private void signup_button_Click(object sender, EventArgs e)
		{
			try
			{
				Form signupForm = new Form
				{
					Text = "Sign Up - Create New Account",
					Width = 550,
					Height = 600,
					StartPosition = FormStartPosition.CenterParent
				};

				Label titleLabel = new Label
				{
					Text = "CREATE NEW ACCOUNT",
					Left = 20,
					Top = 20,
					Font = new Font("Arial", 14, FontStyle.Bold),
					AutoSize = true
				};

				// Username
				Label usernameLabel = new Label { Text = "Username:", Left = 20, Top = 60, AutoSize = true };
				TextBox usernameTextBox = new TextBox { Left = 150, Top = 60, Width = 300 };

				// Password
				Label passwordLabel = new Label { Text = "Password:", Left = 20, Top = 100, AutoSize = true };
				TextBox passwordTextBox = new TextBox { Left = 150, Top = 100, Width = 300, UseSystemPasswordChar = true };

				// Confirm Password
				Label confirmPasswordLabel = new Label { Text = "Confirm Password:", Left = 20, Top = 140, AutoSize = true };
				TextBox confirmPasswordTextBox = new TextBox { Left = 150, Top = 140, Width = 300, UseSystemPasswordChar = true };

				// Full Name
				Label nameLabel = new Label { Text = "Full Name:", Left = 20, Top = 180, AutoSize = true };
				TextBox nameTextBox = new TextBox { Left = 150, Top = 180, Width = 300 };

				// Birth Date
				Label birthLabel = new Label { Text = "Birth Date:", Left = 20, Top = 220, AutoSize = true };
				DateTimePicker birthDatePicker = new DateTimePicker
				{
					Left = 150,
					Top = 220,
					Width = 200,
					Format = DateTimePickerFormat.Short,
					MaxDate = DateTime.Today.AddYears(-18), // Must be at least 18 years old
					Value = DateTime.Today.AddYears(-20)
				};

				// CCCD
				Label cccdLabel = new Label { Text = "CCCD:", Left = 20, Top = 260, AutoSize = true };
				TextBox cccdTextBox = new TextBox { Left = 150, Top = 260, Width = 300, MaxLength = 12 };

				// Address
				Label addressLabel = new Label { Text = "Address:", Left = 20, Top = 300, AutoSize = true };
				TextBox addressTextBox = new TextBox { Left = 150, Top = 300, Width = 300 };

				// Phone
				Label phoneLabel = new Label { Text = "Phone:", Left = 20, Top = 340, AutoSize = true };
				TextBox phoneTextBox = new TextBox { Left = 150, Top = 340, Width = 300, MaxLength = 15 };

				// Email
				Label emailLabel = new Label { Text = "Email:", Left = 20, Top = 380, AutoSize = true };
				TextBox emailTextBox = new TextBox { Left = 150, Top = 380, Width = 300 };

				// Gender
				Label sexLabel = new Label { Text = "Gender:", Left = 20, Top = 420, AutoSize = true };
				ComboBox sexComboBox = new ComboBox
				{
					Left = 150,
					Top = 420,
					Width = 100,
					DropDownStyle = ComboBoxStyle.DropDownList
				};
				sexComboBox.Items.AddRange(new string[] { "Male", "Female" });
				sexComboBox.SelectedIndex = 0;

				// Add input validation
				cccdTextBox.KeyPress += (validationSender, validationArgs) =>
				{
					if (!char.IsDigit(validationArgs.KeyChar) && !char.IsControl(validationArgs.KeyChar))
					{
						validationArgs.Handled = true;
					}
				};

				phoneTextBox.KeyPress += (validationSender, validationArgs) =>
				{
					if (!char.IsDigit(validationArgs.KeyChar) && !char.IsControl(validationArgs.KeyChar))
					{
						validationArgs.Handled = true;
					}
				};

				// Buttons
				Button createButton = new Button
				{
					Text = "Create Account",
					Left = 100,
					Top = 480,
					Width = 120,
					Height = 50,
					BackColor = Color.LightGreen
				};

				Button cancelButton = new Button
				{
					Text = "Cancel",
					Left = 250,
					Top = 480,
					Width = 120,
					Height = 50,
					BackColor = Color.LightCoral
				};

				createButton.Click += (createSender, createArgs) =>
				{
					try
					{
						// Validation
						if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
						{
							MessageBox.Show("Username cannot be empty.", "Validation Error");
							usernameTextBox.Focus();
							return;
						}

						if (usernameTextBox.Text.Contains(":"))
						{
							MessageBox.Show("Username cannot contain ':' character.", "Validation Error");
							usernameTextBox.Focus();
							return;
						}

						if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
						{
							MessageBox.Show("Password cannot be empty.", "Validation Error");
							passwordTextBox.Focus();
							return;
						}

						if (passwordTextBox.Text != confirmPasswordTextBox.Text)
						{
							MessageBox.Show("Passwords do not match.", "Validation Error");
							confirmPasswordTextBox.Focus();
							return;
						}

						if (string.IsNullOrWhiteSpace(nameTextBox.Text))
						{
							MessageBox.Show("Full name cannot be empty.", "Validation Error");
							nameTextBox.Focus();
							return;
						}

						if (string.IsNullOrWhiteSpace(cccdTextBox.Text) || cccdTextBox.Text.Length != 12)
						{
							MessageBox.Show("CCCD must be exactly 12 digits.", "Validation Error");
							cccdTextBox.Focus();
							return;
						}

						if (string.IsNullOrWhiteSpace(phoneTextBox.Text))
						{
							MessageBox.Show("Phone number cannot be empty.", "Validation Error");
							phoneTextBox.Focus();
							return;
						}

						if (string.IsNullOrWhiteSpace(emailTextBox.Text) || !emailTextBox.Text.Contains("@"))
						{
							MessageBox.Show("Please enter a valid email address.", "Validation Error");
							emailTextBox.Focus();
							return;
						}

						string username = usernameTextBox.Text.Trim();

						// Check if username already exists
						string credentialsPath = @"./Data/Users/credentials.txt";
						if (System.IO.File.Exists(credentialsPath))
						{
							string[] existingCredentials = System.IO.File.ReadAllLines(credentialsPath);
							foreach (string line in existingCredentials)
							{
								if (line.StartsWith(username + ":"))
								{
									MessageBox.Show("Username already exists. Please choose a different username.", "Error");
									usernameTextBox.Focus();
									return;
								}
							}
						}

						// Calculate age
						int age = DateTime.Today.Year - birthDatePicker.Value.Year;
						if (birthDatePicker.Value.Date > DateTime.Today.AddYears(-age)) age--;

						// Create user INI file
						string userIniPath = $"Data/Users/{username}.ini";
						Directory.CreateDirectory(Path.GetDirectoryName(userIniPath));

						var parser = new FileIniDataParser();
						IniData data = new IniData();

						data.Sections.AddSection("Customer");
						data["Customer"]["Username"] = username;
						data["Customer"]["FullName"] = nameTextBox.Text.Trim();
						data["Customer"]["Age"] = age.ToString();
						data["Customer"]["Sex"] = sexComboBox.SelectedItem.ToString();
						data["Customer"]["Birthday"] = birthDatePicker.Value.ToString("dd/MM/yyyy");
						data["Customer"]["Email"] = emailTextBox.Text.Trim();
						data["Customer"]["CCCD"] = cccdTextBox.Text.Trim();
						data["Customer"]["Phone"] = phoneTextBox.Text.Trim();
						data["Customer"]["HomeAddress"] = addressTextBox.Text.Trim();

						parser.WriteFile(userIniPath, data);

						// Add to credentials file
						string credentialLine = $"{username}:{passwordTextBox.Text}";
						System.IO.File.AppendAllText(credentialsPath, Environment.NewLine + credentialLine);

						MessageBox.Show($"Account created successfully for user '{username}'!", "Success");
						signupForm.Close();
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Error creating account: {ex.Message}", "Error");
					}
				};

				cancelButton.Click += (cancelSender, cancelArgs) => signupForm.Close();

				// Add all controls to form
				signupForm.Controls.Add(titleLabel);
				signupForm.Controls.Add(usernameLabel);
				signupForm.Controls.Add(usernameTextBox);
				signupForm.Controls.Add(passwordLabel);
				signupForm.Controls.Add(passwordTextBox);
				signupForm.Controls.Add(confirmPasswordLabel);
				signupForm.Controls.Add(confirmPasswordTextBox);
				signupForm.Controls.Add(nameLabel);
				signupForm.Controls.Add(nameTextBox);
				signupForm.Controls.Add(birthLabel);
				signupForm.Controls.Add(birthDatePicker);
				signupForm.Controls.Add(cccdLabel);
				signupForm.Controls.Add(cccdTextBox);
				signupForm.Controls.Add(addressLabel);
				signupForm.Controls.Add(addressTextBox);
				signupForm.Controls.Add(phoneLabel);
				signupForm.Controls.Add(phoneTextBox);
				signupForm.Controls.Add(emailLabel);
				signupForm.Controls.Add(emailTextBox);
				signupForm.Controls.Add(sexLabel);
				signupForm.Controls.Add(sexComboBox);
				signupForm.Controls.Add(createButton);
				signupForm.Controls.Add(cancelButton);

				signupForm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error opening sign-up form: {ex.Message}");
			}
		}
	}
}
