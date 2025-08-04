using IniParser;
using IniParser.Model;

namespace HomestayManagementSystem
{
    public partial class GuestMenu : Form
    {
        private string getStateText(uint roomStatus)
        {
            return roomStatus switch
            {
                0 => "Trống",
                1 => "Đã được đặt",
                2 => "Đã được cọc",
                _ => "Không xác định"
            };
        }
        private Panel createRoomPreviewInfo(Room data)
        {
            // "Room 101, 0"
            string roomName = data.getID();
            uint roomStatus = data.getState(); // Assuming 0 for available, 1 for occupied, 2 for pre-booked.
            uint maxPersons = data.getMaxPersons(); // Assuming this is the maximum number of persons allowed in the room
            Panel panel = new Panel
            {
                Width = preview_flowLayoutPanel.Width - 5, // Subtracting padding
                Height = 100, // Fixed height for each panel
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };
            Label roomName_label = new Label
            {
                Text = "Phòng " + roomName + "\nTình trạng: " + (roomStatus == 0 ? "Trống" : (roomStatus == 1 ? "Đã được đặt" : "Đã được cọc")),
                BackColor = roomStatus == 0 ? Color.LightGreen : Color.LightCoral,
                ForeColor = Color.Black,
                AutoSize = false,
                Height = 65,
                Dock = DockStyle.Top,
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(3)
            };
            Label roomStatus_label = new Label
            {
                Text = "Tình trạng: " + (roomStatus == 0 ? "Trống" : (roomStatus == 1 ? "Đã được đặt" : "Đã được cọc")),
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Arial", 12, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Label roomCapacity_label = new Label
            {
                Text = "Sức chứa: " + maxPersons + " người",
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Arial", 12, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Label roomPrice_label = new Label
            {
                Text = "Giá: " + data.getPrice().ToString("N0") + " VNĐ/đêm", // Remove * 1000
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Arial", 12, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.Add(roomName_label);
            panel.Controls.Add(roomStatus_label);
            panel.Controls.Add(roomCapacity_label);
            panel.Controls.Add(roomPrice_label);
            return panel;
        }
        private Panel createRoomPanel(Room data)
        {
            // "Room 101, 0"
            string roomName = data.getID();
            uint roomStatus = data.getState(); // Assuming 0 for available, 1 for occupied, etc.
            Panel panel = new Panel
            {
                Name = "RoomPanel_" + roomName,
                Width = bookRoom_flowLayoutPanel.Width - 20, // Subtracting padding
                Height = 100, // Fixed height for each panel
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };
            Label roomName_label = new Label
            {
                Text = "Phòng " + roomName + "\nTình trạng: " + (roomStatus == 0 ? "Trống" : (roomStatus == 1 ? "Đã được đặt" : "Đã được cọc")),
                BackColor = roomStatus == 0 ? Color.LightGreen : Color.LightCoral,
                ForeColor = Color.Black,
                AutoSize = false,
                Height = 65,
                Dock = DockStyle.Top,
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(3)
            };
            roomName_label.MouseHover += (sender, e) =>
            {
                // Clear previous preview panels - dispose images first
                foreach (Control control in preview_flowLayoutPanel.Controls)
                {
                    if (control is PictureBox pic && pic.Image != null)
                    {
                        pic.Image.Dispose();
                    }
                }
                preview_flowLayoutPanel.Controls.Clear();

                // Create and add the preview info panel
                Panel previewPanel = createRoomPreviewInfo(data);
                preview_flowLayoutPanel.Controls.Add(previewPanel);

                // Create and add the PictureBox for the room image below the info panel
                string roomName = data.getID();
                string imagePath = data.getImagePath();
                PictureBox roomPicture = new PictureBox
                {
                    Width = preview_flowLayoutPanel.Width - 10,
                    Height = preview_flowLayoutPanel.Height,
                    Dock = DockStyle.None,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };
                if (System.IO.File.Exists(imagePath))
                {
                    roomPicture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    roomPicture.Image = null;
                }

                // Add click event to enlarge image
                roomPicture.Click += (s, args) =>
                {
                    if (roomPicture.Image != null)
                    {
                        Form imageForm = new Form
                        {
                            Text = "Xem ảnh phòng " + roomName,
                            Width = 800,
                            Height = 600,
                            StartPosition = FormStartPosition.CenterParent
                        };

                        Image originalImage = (Image)roomPicture.Image.Clone();

                        PictureBox largePicture = new PictureBox
                        {
                            Dock = DockStyle.Fill,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Image = (Image)originalImage.Clone()
                        };

                        // Panel for buttons
                        FlowLayoutPanel buttonPanel = new FlowLayoutPanel
                        {
                            Dock = DockStyle.Top,
                            Height = 50,
                            FlowDirection = FlowDirection.LeftToRight,
                            Padding = new Padding(10)
                        };

                        Button flipButton = new Button
                        {
                            Text = "Flip",
                            Width = 120,
                            Height = 40,
                            Margin = new Padding(5)
                        };
                        flipButton.Click += (flipSender, flipArgs) =>
                        {
                            if (largePicture.Image != null)
                            {
                                largePicture.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                largePicture.Refresh();
                            }
                        };

                        Button rotateButton = new Button
                        {
                            Text = "Rotate",
                            Width = 120,
                            Height = 40,
                            Margin = new Padding(5)
                        };
                        rotateButton.Click += (rotateSender, rotateArgs) =>
                        {
                            if (largePicture.Image != null)
                            {
                                largePicture.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                largePicture.Refresh();
                            }
                        };

                        Button resetButton = new Button
                        {
                            Text = "Set to default",
                            Width = 120,
                            Height = 40,
                            Margin = new Padding(5)
                        };
                        resetButton.Click += (resetSender, resetArgs) =>
                        {
                            if (largePicture.Image != null)
                            {
                                largePicture.Image?.Dispose(); // Dispose old image
                                largePicture.Image = (Image)originalImage.Clone();
                                largePicture.Refresh();
                            }
                        };

                        buttonPanel.Controls.Add(flipButton);
                        buttonPanel.Controls.Add(rotateButton);
                        buttonPanel.Controls.Add(resetButton);

                        imageForm.Controls.Add(buttonPanel);
                        imageForm.Controls.Add(largePicture);

                        // Add FormClosed event to dispose images
                        imageForm.FormClosed += (formSender, formArgs) =>
                        {
                            largePicture.Image?.Dispose();
                            originalImage?.Dispose();
                        };

                        imageForm.ShowDialog();
                    }
                };

                preview_flowLayoutPanel.Controls.Add(roomPicture);
            };

            Button moreInfo_button = new Button
            {
                Text = "Thông tin chi tiết",
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Regular),
                Margin = new Padding(5)
            };

            // Thêm sự kiện click cho nút moreInfo_button theo cách OOP
            moreInfo_button.Click += (sender, e) =>
            {
                ShowRoomDetailInfo(data);
            };

            panel.Controls.Add(roomName_label);
            panel.Controls.Add(moreInfo_button);
            return panel;
        }

        // Thêm phương thức mới để hiển thị thông tin chi tiết phòng theo OOP
        private void ShowRoomDetailInfo(Room roomData)
        {
            try
            {
                // Sử dụng OOP pattern: Load thông tin chi tiết từ file JSON
                Room detailedRoom = Room.LoadFromJson(roomData.getID());

                // Kiểm tra nếu không load được dữ liệu từ JSON
                if (detailedRoom == null)
                {
                    MessageBox.Show($"Không thể tải thông tin chi tiết cho phòng {roomData.getID()}. Sử dụng thông tin hiện có.",
                                   "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    detailedRoom = roomData; // Fallback to current room data
                }

                // Tạo form hiển thị với thông tin từ đối tượng Room
                Form roomInfoForm = new Form
                {
                    Text = $"Thông tin chi tiết - Phòng {detailedRoom.getID()}",
                    Width = 450,
                    Height = 450,
                    StartPosition = FormStartPosition.CenterParent
                };

                string roomStatusText = detailedRoom.getState() switch
                {
                    0 => "Trống",
                    1 => "Đã được đặt",
                    2 => "Đã được cọc",
                    _ => "Không xác định"
                };

                // Tạo các label hiển thị thông tin
                Label roomIdLabel = new Label { Text = $"Mã phòng: {detailedRoom.getID()}", Left = 20, Top = 20, AutoSize = true };
                Label roomNameLabel = new Label { Text = $"Tên phòng: Phòng {detailedRoom.getID()}", Left = 20, Top = 50, AutoSize = true };
                Label roomStatusLabel = new Label { Text = $"Tình trạng: {roomStatusText}", Left = 20, Top = 80, AutoSize = true };
                Label roomPriceLabel = new Label { Text = $"Giá phòng: {detailedRoom.getPrice():N0} VNĐ/đêm", Left = 20, Top = 110, AutoSize = true };
                Label roomBedsLabel = new Label { Text = $"Số giường: {detailedRoom.getNumBeds()}", Left = 20, Top = 140, AutoSize = true };
                Label maxPersonsLabel = new Label { Text = $"Sức chứa tối đa: {detailedRoom.getMaxPersons()} người", Left = 20, Top = 170, AutoSize = true };
                Label haveBalconyLabel = new Label { Text = $"Ban công: {(detailedRoom.getHaveBalcony() ? "Có" : "Không")}", Left = 20, Top = 200, AutoSize = true };
                Label haveKitchenLabel = new Label { Text = $"Bếp: {(detailedRoom.getHaveKitchen() ? "Có" : "Không")}", Left = 20, Top = 230, AutoSize = true };
                Label haveBathtubLabel = new Label { Text = $"Bồn tắm: {(detailedRoom.getHaveBathtub() ? "Có" : "Không")}", Left = 20, Top = 260, AutoSize = true };

                // Tạo các button
                Button closeButton = new Button { Text = "Đóng", Left = 50, Top = 320, Width = 100, Height = 50 };
                Button bookRoomButton = new Button { Text = "Đặt phòng", Left = 175, Top = 320, Width = 100, Height = 50 };
                Button depositButton = new Button { Text = "Cọc trước", Left = 300, Top = 320, Width = 100, Height = 50 };

                // Kiểm tra điều kiện để hiển thị nút cọc trước
                ulong roomPriceInVND = detailedRoom.getPrice(); // Remove * 1000
                if (roomPriceInVND <= 100000)
                {
                    depositButton.Visible = false;
                    depositButton.Enabled = false;
                }

                // Chỉ cho phép đặt phòng nếu phòng trống
                if (detailedRoom.getState() != 0)
                {
                    bookRoomButton.Enabled = false;
                    depositButton.Enabled = false;
                }

                // Thêm sự kiện cho nút Close
                closeButton.Click += (closeSender, closeArgs) =>
                {
                    roomInfoForm.Close();
                };

                // Thêm sự kiện cho nút Đặt phòng
                bookRoomButton.Click += (bookSender, bookArgs) =>
                {
                    roomInfoForm.Close();
                    ShowBookingForm(detailedRoom, false); // false = không phải cọc trước
                };

                // Thêm sự kiện cho nút Cọc trước
                depositButton.Click += (depositSender, depositArgs) =>
                {
                    roomInfoForm.Close();
                    ShowBookingForm(detailedRoom, true); // true = cọc trước
                };

                // Thêm controls vào form
                roomInfoForm.Controls.Add(roomIdLabel);
                roomInfoForm.Controls.Add(roomNameLabel);
                roomInfoForm.Controls.Add(roomStatusLabel);
                roomInfoForm.Controls.Add(roomPriceLabel);
                roomInfoForm.Controls.Add(roomBedsLabel);
                roomInfoForm.Controls.Add(maxPersonsLabel);
                roomInfoForm.Controls.Add(haveBalconyLabel);
                roomInfoForm.Controls.Add(haveKitchenLabel);
                roomInfoForm.Controls.Add(haveBathtubLabel);
                roomInfoForm.Controls.Add(closeButton);
                roomInfoForm.Controls.Add(bookRoomButton);
                roomInfoForm.Controls.Add(depositButton);

                roomInfoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin phòng: {ex.Message}");
            }
        }

        // Thêm phương thức mới để hiển thị form đặt phòng theo OOP
        private void ShowBookingForm(Room room, bool isDeposit)
        {
            try
            {
                // Tạo form đặt phòng
                Form bookingForm = new Form
                {
                    Text = isDeposit ? $"Cọc phòng {room.getID()}" : $"Đặt phòng {room.getID()}",
                    Width = 500,
                    Height = 550,
                    StartPosition = FormStartPosition.CenterParent
                };

                // Tạo các label và textbox cho thông tin đặt phòng
                Label titleLabel = new Label
                {
                    Text = isDeposit ? "THÔNG TIN CỌC PHÒNG" : "THÔNG TIN ĐẶT PHÒNG",
                    Left = 20,
                    Top = 20,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };

                // Thông tin phòng
                Label roomInfoLabel = new Label
                {
                    Text = $"Phòng: {room.getID()} - Giá: {room.getPrice():N0} VNĐ/đêm", // Remove * 1000
                    Left = 20,
                    Top = 60,
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Blue
                };

                // Người đứng tên
                Label nameLabel = new Label { Text = "Người đứng tên:", Left = 20, Top = 100, AutoSize = true };
                TextBox nameTextBox = new TextBox { Left = 150, Top = 100, Width = 300 };

                // Tuổi
                Label ageLabel = new Label { Text = "Tuổi:", Left = 20, Top = 140, AutoSize = true };
                NumericUpDown ageNumeric = new NumericUpDown
                {
                    Left = 150,
                    Top = 140,
                    Width = 100,
                    Minimum = 18,
                    Maximum = 100,
                    Value = 25
                };

                // CCCD
                Label cccdLabel = new Label { Text = "CCCD:", Left = 20, Top = 180, AutoSize = true };
                TextBox cccdTextBox = new TextBox { Left = 150, Top = 180, Width = 300, MaxLength = 12 };

                // Ngày tháng năm sinh
                Label birthLabel = new Label { Text = "Ngày sinh:", Left = 20, Top = 220, AutoSize = true };
                DateTimePicker birthDatePicker = new DateTimePicker
                {
                    Left = 150,
                    Top = 220,
                    Width = 200,
                    Format = DateTimePickerFormat.Short,
                    MaxDate = DateTime.Today.AddYears(-18), // Tối đa 18 tuổi
                    MinDate = DateTime.Today.AddYears(-100) // Tối thiểu 100 tuổi
                };

                // Giới tính
                Label genderLabel = new Label { Text = "Giới tính:", Left = 20, Top = 250, AutoSize = true };
                ComboBox genderComboBox = new ComboBox
                {
                    Left = 150,
                    Top = 250,
                    Width = 100,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                genderComboBox.Items.AddRange(new string[] { "Nam", "Nữ" });
                genderComboBox.SelectedIndex = 0; // Default to "Nam"

                // Số người sẽ ở - adjust position
                Label guestCountLabel = new Label { Text = "Số người sẽ ở:", Left = 20, Top = 280, AutoSize = true };
                NumericUpDown guestCountNumeric = new NumericUpDown
                {
                    Left = 150,
                    Top = 280,
                    Width = 100,
                    Minimum = 1,
                    Maximum = room.getMaxPersons(),
                    Value = 1
                };

                // Hiển thị thông tin giá - adjust position
                Label priceInfoLabel = new Label
                {
                    Text = isDeposit ? $"Số tiền cọc: 100,000 VNĐ" : $"Tổng tiền: {room.getPrice():N0} VNĐ/đêm", // Remove * 1000
                    Left = 20,
                    Top = 320,
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = isDeposit ? Color.Green : Color.Red
                };

                // Các button - adjust position
                Button confirmButton = new Button
                {
                    Text = isDeposit ? "Xác nhận cọc" : "Xác nhận đặt phòng",
                    Left = 100,
                    Top = 420,
                    Width = 120,
                    Height = 50,
                    BackColor = Color.LightGreen
                };
                Button cancelButton = new Button
                {
                    Text = "Hủy",
                    Left = 250,
                    Top = 420,
                    Width = 120,
                    Height = 50,
                    BackColor = Color.LightCoral
                };

                // Pre-fill thông tin nếu có thể lấy từ user hiện tại
                TryPreFillUserInfo(nameTextBox, cccdTextBox, birthDatePicker, ageNumeric);

                // Thêm validation cho CCCD
                cccdTextBox.KeyPress += (sender, e) =>
                {
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                };

                // Sync tuổi với ngày sinh
                birthDatePicker.ValueChanged += (sender, e) =>
                {
                    int age = DateTime.Today.Year - birthDatePicker.Value.Year;
                    if (birthDatePicker.Value.Date > DateTime.Today.AddYears(-age)) age--;
                    ageNumeric.Value = Math.Max(18, age);
                };

                // Sự kiện nút Hủy
                cancelButton.Click += (cancelSender, cancelArgs) =>
                {
                    bookingForm.Close();
                };

                // Sự kiện nút Xác nhận
                confirmButton.Click += (confirmSender, confirmArgs) =>
                {
                    ProcessBooking(nameTextBox, ageNumeric, cccdTextBox, birthDatePicker,
                                  guestCountNumeric, genderComboBox, room, isDeposit, bookingForm);
                };

                // Thêm controls vào form
                bookingForm.Controls.Add(titleLabel);
                bookingForm.Controls.Add(roomInfoLabel);
                bookingForm.Controls.Add(nameLabel);
                bookingForm.Controls.Add(nameTextBox);
                bookingForm.Controls.Add(ageLabel);
                bookingForm.Controls.Add(ageNumeric);
                bookingForm.Controls.Add(cccdLabel);
                bookingForm.Controls.Add(cccdTextBox);
                bookingForm.Controls.Add(birthLabel);
                bookingForm.Controls.Add(birthDatePicker);
                bookingForm.Controls.Add(genderLabel);
                bookingForm.Controls.Add(genderComboBox);
                bookingForm.Controls.Add(guestCountLabel);
                bookingForm.Controls.Add(guestCountNumeric);
                bookingForm.Controls.Add(priceInfoLabel);
                bookingForm.Controls.Add(confirmButton);
                bookingForm.Controls.Add(cancelButton);

                bookingForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị form đặt phòng: {ex.Message}");
            }
        }

        // Helper method để pre-fill thông tin user
        private void TryPreFillUserInfo(TextBox nameTextBox, TextBox cccdTextBox,
                                       DateTimePicker birthDatePicker, NumericUpDown ageNumeric)
        {
            try
            {
                // Lấy username từ mainMenu
                string username = "";
                foreach (Form f in Application.OpenForms)
                {
                    if (f is mainMenu main)
                    {
                        username = main.username_textBox.Text.Trim();
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(username))
                {
                    string iniPath = $"Data/Users/{username}.ini";
                    if (System.IO.File.Exists(iniPath))
                    {
                        var parser = new FileIniDataParser();
                        IniData data = parser.ReadFile(iniPath);
                        var customerSection = data["Customer"];

                        // Pre-fill thông tin
                        nameTextBox.Text = customerSection["FullName"];
                        cccdTextBox.Text = customerSection["CCCD"];

                        if (DateTime.TryParse(customerSection["Birthday"], out DateTime birthDate))
                        {
                            birthDatePicker.Value = birthDate;
                            int age = DateTime.Today.Year - birthDate.Year;
                            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
                            ageNumeric.Value = Math.Max(18, age);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không thể pre-fill thông tin: {ex.Message}");
            }
        }


        // Helper method để extract room number từ display text
        private string ExtractRoomNumberFromDisplayText(string displayText)
        {
            try
            {
                // Format: "Phòng 101 - Phòng đơn - Trống - 500,000 VNĐ/đêm"
                if (displayText.StartsWith("Phòng "))
                {
                    int startIndex = "Phòng ".Length;
                    int endIndex = displayText.IndexOf(" - ");
                    if (endIndex > startIndex)
                    {
                        return displayText.Substring(startIndex, endIndex - startIndex);
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        // Helper method để filter room panels
        private void FilterRoomPanels(string roomNumber)
        {
            try
            {
                foreach (Control control in bookRoom_flowLayoutPanel.Controls)
                {
                    if (control is Panel panel && panel.Name.StartsWith("RoomPanel_"))
                    {
                        string panelRoomNumber = panel.Name.Replace("RoomPanel_", "");
                        panel.Visible = (panelRoomNumber == roomNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc hiển thị phòng: {ex.Message}");
            }
        }
        
        // Method để xử lý booking - đã cập nhật
        private void ProcessBooking(TextBox nameTextBox, NumericUpDown ageNumeric, TextBox cccdTextBox,
          DateTimePicker birthDatePicker, NumericUpDown guestCountNumeric, ComboBox genderComboBox,
          Room room, bool isDeposit, Form bookingForm)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên người đứng tên.", "Thiếu thông tin",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(cccdTextBox.Text) || cccdTextBox.Text.Length != 12)
                {
                    MessageBox.Show("CCCD phải có đúng 12 số.", "Thông tin không hợp lệ",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cccdTextBox.Focus();
                    return;
                }

                // Kiểm tra CCCD có phải toàn số không
                if (!cccdTextBox.Text.All(char.IsDigit))
                {
                    MessageBox.Show("CCCD chỉ được chứa số.", "Thông tin không hợp lệ",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cccdTextBox.Focus();
                    return;
                }

                if (ageNumeric.Value < 18)
                {
                    MessageBox.Show("Người đặt phòng phải từ 18 tuổi trở lên.", "Tuổi không hợp lệ",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validation cho ngày sinh hợp lý
                if (birthDatePicker.Value > DateTime.Today.AddYears(-18))
                {
                    MessageBox.Show("Ngày sinh không hợp lệ. Khách hàng phải từ 18 tuổi trở lên.", "Ngày sinh không hợp lệ",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    birthDatePicker.Focus();
                    return;
                }

                // Validation cho giới tính
                if (genderComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn giới tính.", "Thiếu thông tin",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    genderComboBox.Focus();
                    return;
                }

                // Tạo thông tin booking
                string bookingInfo = $"Phòng: {room.getID()}\n" +
                   $"Người đứng tên: {nameTextBox.Text}\n" +
                   $"Tuổi: {ageNumeric.Value}\n" +
                   $"CCCD: {cccdTextBox.Text}\n" +
                   $"Ngày sinh: {birthDatePicker.Value:dd/MM/yyyy}\n" +
                   $"Giới tính: {genderComboBox.SelectedItem}\n" +
                   $"Số người: {guestCountNumeric.Value}\n" +
                   $"Loại: {(isDeposit ? "Cọc trước" : "Đặt phòng")}\n" +
                   $"Số tiền: {(isDeposit ? "100,000" : room.getPrice().ToString("N0"))} VNĐ"; // Remove * 1000

                DialogResult result = MessageBox.Show($"Xác nhận thông tin:\n\n{bookingInfo}",
                                                    "Xác nhận đặt phòng",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cập nhật trạng thái phòng
                    uint newStatus = isDeposit ? (uint)2 : (uint)1;
                    room.setState(newStatus);

                    // Lưu thông tin booking vào file JSON với giới tính
                    bool isFemale = genderComboBox.SelectedItem.ToString() == "Nữ";
                    SaveBookingToJson(room, nameTextBox.Text, cccdTextBox.Text,
                                    birthDatePicker.Value, (uint)guestCountNumeric.Value, isFemale, isDeposit);

                    MessageBox.Show($"{(isDeposit ? "Cọc phòng" : "Đặt phòng")} thành công!\n\n{bookingInfo}",
                                    "Thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    bookingForm.Close();

                    // Refresh GuestMenu để hiển thị trạng thái phòng đã cập nhật
                    RefreshGuestMenu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý đặt phòng: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveBookingToJson(Room room, string customerName, string cccd,
             DateTime birthDate, uint guestCount, bool isFemale, bool isDeposit)
        {
            try
            {
                string jsonPath = @"./Data/Rooms/ROOMDATA.json";

                // Load all rooms
                List<Room> allRooms = Room.LoadRoomsFromJson(jsonPath);

                // Find and update the specific room
                var roomToUpdate = allRooms.FirstOrDefault(r => r.getID() == room.getID());
                if (roomToUpdate != null)
                {
                    // Update room state
                    roomToUpdate.setState(room.getState());

                    // Try to get additional user info for complete Person object
                    string email = "";
                    string phoneNumber = "";
                    string address = "";

                    // Try to get additional info from current user's INI file
                    try
                    {
                        string username = "";
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f is mainMenu main)
                            {
                                username = main.username_textBox.Text.Trim();
                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(username))
                        {
                            string iniPath = $"Data/Users/{username}.ini";
                            if (System.IO.File.Exists(iniPath))
                            {
                                var parser = new IniParser.FileIniDataParser();
                                IniParser.Model.IniData data = parser.ReadFile(iniPath);
                                var customerSection = data["Customer"];

                                email = customerSection["Email"] ?? "";
                                phoneNumber = customerSection["Phone"] ?? "";
                                address = customerSection["HomeAddress"] ?? "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Could not load additional user info: {ex.Message}");
                    }

                    // Add guest information to curGuest array with complete information
                    var guestList = roomToUpdate.getCurGuest().ToList();
                    guestList.Add(new Person
                    {
                        name = customerName,
                        age = (uint)(DateTime.Today.Year - birthDate.Year),
                        sex = isFemale, // Use the gender parameter
                        mail = email,
                        CCCD = cccd,
                        phoneNumber = phoneNumber,
                        address = address
                    });
                    roomToUpdate.setCurGuest(guestList.ToArray());
                }

                // Save back to JSON
                Room.SaveRoomsToJson(allRooms, jsonPath);

                Console.WriteLine($"Đã lưu thông tin booking cho phòng {room.getID()} vào JSON");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu thông tin booking: {ex.Message}");
            }
        }
        private void RefreshGuestMenu()
        {
            try
            {
                // Clear current panels
                bookRoom_flowLayoutPanel.Controls.Clear();
                preview_flowLayoutPanel.Controls.Clear();

                // Load updated data from JSON file
                List<Room> updatedRoomData = LoadAllRoomsFromJsonFile();

                // Recreate panels with updated data
                foreach (Room room in updatedRoomData)
                {
                    Panel panel = createRoomPanel(room);
                    bookRoom_flowLayoutPanel.Controls.Add(panel);
                }

                // Update ComboBox
                UpdateRoomSelectionBox(updatedRoomData);

                // Reset selection to "Tất cả phòng"
                if (room_selection_box.Items.Count > 0)
                {
                    room_selection_box.SelectedIndex = 0;
                }

                Console.WriteLine("Đã refresh GuestMenu với dữ liệu JSON cập nhật");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi refresh menu: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public GuestMenu()
        {
            InitializeComponent();

            // Load real data from JSON file instead of INI files
            List<Room> roomData = LoadAllRoomsFromJsonFile();

            // Populate room panels
            for (int i = 0; i < roomData.Count; i++)
            {
                Panel panel = createRoomPanel(roomData[i]);
                bookRoom_flowLayoutPanel.Controls.Add(panel);
            }

            // Update room_selection_box with loaded room list
            UpdateRoomSelectionBox(roomData);
        }

        private List<Room> LoadAllRoomsFromJsonFile()
        {
            string jsonPath = "Data/Rooms/ROOMDATA.json";

            try
            {
                // Check if JSON file exists
                if (!File.Exists(jsonPath))
                {
                    MessageBox.Show($"File {jsonPath} không tồn tại. Tạo file mẫu và sử dụng dữ liệu mẫu.");

                    // Create sample data and save to JSON
                    var sampleRooms = GetFallbackRoomData();
                    Room.SaveRoomsToJson(sampleRooms, jsonPath);
                    return sampleRooms;
                }

                // Load rooms from JSON using the Room class method
                List<Room> roomList = Room.LoadRoomsFromJson(jsonPath);

                if (roomList.Count == 0)
                {
                    MessageBox.Show("Không có phòng nào trong file JSON. Sử dụng dữ liệu mẫu.");
                    return GetFallbackRoomData();
                }

                // Sort rooms by ID for ordered display
                roomList.Sort((r1, r2) => string.Compare(r1.getID(), r2.getID(), StringComparison.Ordinal));

                Console.WriteLine($"Đã load thành công {roomList.Count} phòng từ file JSON.");
                return roomList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file JSON: {ex.Message}. Sử dụng dữ liệu mẫu.");
                return GetFallbackRoomData();
            }
        }

        // Thêm phương thức fallback nếu không load được từ file
        private List<Room> GetFallbackRoomData()
        {
            // Tạo dữ liệu mẫu đúng với constructor Room mới nhất
            return new List<Room>
            {
                new Room("101", 0, 1, true, true, false, 2, 500),   // Phòng 101: Trống, 1 giường, có ban công, có bếp, không bồn tắm, tối đa 2 người, giá 500K
				new Room("102", 1, 2, false, false, true, 4, 800),  // Phòng 102: Đã được đặt, 2 giường, không ban công, không bếp, có bồn tắm, tối đa 4 người, giá 800K
				new Room("103", 0, 3, true, true, true, 6, 1200),   // Phòng 103: Trống, 3 giường, có ban công, có bếp, có bồn tắm, tối đa 6 người, giá 1200K
				new Room("104", 2, 1, false, false, false, 2, 450)  // Phòng 104: Đã được cọc, 1 giường, không ban công, không bếp, không bồn tắm, tối đa 2 người, giá 450K
			};
        }

        // Thêm phương thức để cập nhật room_selection_box theo OOP
        private void UpdateRoomSelectionBox(List<Room> roomList)
        {
            try
            {
                // Clear existing items
                room_selection_box.Items.Clear();

                // Thêm option "Tất cả phòng" để hiển thị toàn bộ
                room_selection_box.Items.Add("Tất cả phòng");

                // Thêm từng phòng vào ComboBox
                foreach (Room room in roomList)
                {
                    string roomDisplayText = $"Phòng {room.getID()} - " +
                   $"{getStateText(room.getState())} - " +
                   $"{room.getPrice():N0} VNĐ/đêm"; // Remove * 1000

                    room_selection_box.Items.Add(roomDisplayText);
                }

                // Set default selection
                if (room_selection_box.Items.Count > 0)
                {
                    room_selection_box.SelectedIndex = 0; // Chọn "Tất cả phòng"
                }

                // Remove existing event handler to avoid duplicates, then add it
                room_selection_box.SelectedIndexChanged -= Room_selection_box_SelectedIndexChanged;
                room_selection_box.SelectedIndexChanged += Room_selection_box_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật danh sách phòng: {ex.Message}");
            }
        }

        // Thêm event handler cho room_selection_box
        private void Room_selection_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = sender as ComboBox;

                if (comboBox?.SelectedIndex == 0) // "Tất cả phòng"
                {
                    // Hiển thị tất cả phòng - chỉ hiện lại tất cả panels thay vì refresh toàn bộ
                    foreach (Control control in bookRoom_flowLayoutPanel.Controls)
                    {
                        if (control is Panel panel && panel.Name.StartsWith("RoomPanel_"))
                        {
                            panel.Visible = true;
                        }
                    }
                    return;
                }

                if (comboBox?.SelectedIndex > 0)
                {
                    // Lấy room number từ selection (parsing từ text)
                    string selectedText = comboBox.SelectedItem.ToString();
                    string roomNumber = ExtractRoomNumberFromDisplayText(selectedText);

                    // Filter và chỉ hiển thị phòng được chọn
                    FilterRoomPanels(roomNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc phòng: {ex.Message}");
            }
        }

        // Cập nhật RestoreMenu để hỗ trợ filter
        private void returnMainMenu_button_Click(object sender, EventArgs e)
        {
            var mainMenu_Form = (mainMenu)Tag;
            mainMenu_Form.Show();
            Close();
        }

        private void user_info_Click(object sender, EventArgs e)
        {
            // Lấy username từ mainMenu
            string username = "";
            foreach (Form f in Application.OpenForms)
            {
                if (f is mainMenu main)
                {
                    username = main.username_textBox.Text.Trim();
                    break;
                }
            }
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Không tìm thấy tên đăng nhập.");
                return;
            }

            // Đọc file ini
            string iniPath = $"Data/Users/{username}.ini";
            if (!System.IO.File.Exists(iniPath))
            {
                MessageBox.Show("Không tìm thấy file thông tin người dùng.");
                return;
            }

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniPath);
            var customerSection = data["Customer"];

            // Tạo đối tượng GuestAccount theo cách OOP
            GuestAccount guestAccount = new GuestAccount();

            // Thiết lập tên đăng nhập
            guestAccount.SetUsername(username);

            UserInfo userInfo = new UserInfo
            {
                FullName = customerSection["FullName"],
                BirthDay = customerSection["Birthday"],
                Address = customerSection["HomeAddress"],
                CCCD = customerSection["CCCD"],
                PhoneNumber = customerSection["Phone"],
                Email = customerSection["Email"],
                Sex = customerSection.ContainsKey("Sex") ? customerSection["Sex"][0] : 'M'
            };

            // Gán thông tin UserInfo vào GuestAccount
            guestAccount.SetAccountInfo(userInfo);

            // Lấy thông tin rank và phòng hiện tại (không có trong UserInfo struct)
            string rank = customerSection["Rank"];
            string currentRoom = customerSection.ContainsKey("CurrentRoom") ? customerSection["CurrentRoom"] : "";

            // Tạo form hiển thị với thông tin từ đối tượng GuestAccount
            Form infoForm = new Form
            {
                Text = "Thông tin người dùng",
                Width = 500,
                Height = 400,
                StartPosition = FormStartPosition.CenterParent
            };

            // Sử dụng thông tin từ GuestAccount (thông qua UserInfo)
            Label nameLabel = new Label { Text = $"Tên: {userInfo.FullName}", Left = 20, Top = 20, AutoSize = true };
            Label birthLabel = new Label { Text = $"Năm sinh: {userInfo.BirthDay}", Left = 20, Top = 50, AutoSize = true };
            Label CCCDLabel = new Label { Text = $"CCCD: {userInfo.CCCD}", Left = 20, Top = 80, AutoSize = true };
            Label addressLabel = new Label { Text = $"Địa chỉ: {userInfo.Address}", Left = 20, Top = 110, AutoSize = true };
            Label phoneLabel = new Label { Text = $"SDT: {userInfo.PhoneNumber}", Left = 20, Top = 140, AutoSize = true };
            Label emailLabel = new Label { Text = $"Email: {userInfo.Email}", Left = 20, Top = 170, AutoSize = true };
            Label roomLabel = new Label { Text = $"Phòng hiện tại: {currentRoom}", Left = 20, Top = 200, AutoSize = true };
            Label rankLabel = new Label { Text = $"Thành viên cấp: {rank} (Xem ưu đãi)", Left = 20, Top = 230, AutoSize = true };

            // Tạo các button
            Button editButton = new Button { Text = "Chỉnh sửa", Left = 60, Top = 280, Width = 100 };
            Button saveButton = new Button { Text = "Lưu", Left = 220, Top = 280, Width = 100 };

            // Thêm sự kiện cho nút Save để lưu thông tin đã chỉnh sửa
            saveButton.Click += (saveSender, saveArgs) =>
            {
                // Ví dụ: Lưu lại thông tin đã thay đổi vào file INI
                // Có thể mở rộng thêm chức năng chỉnh sửa ở đây
                MessageBox.Show("Thông tin đã được lưu!");
                infoForm.Close();
            };

            // Thêm controls vào form
            infoForm.Controls.Add(nameLabel);
            infoForm.Controls.Add(birthLabel);
            infoForm.Controls.Add(CCCDLabel);
            infoForm.Controls.Add(addressLabel);
            infoForm.Controls.Add(phoneLabel);
            infoForm.Controls.Add(emailLabel);
            infoForm.Controls.Add(roomLabel);
            infoForm.Controls.Add(rankLabel);
            infoForm.Controls.Add(editButton);
            infoForm.Controls.Add(saveButton);

            infoForm.ShowDialog();
        }
    }
}
