using IniParser;
using IniParser.Model;
using System.Text.Json;

namespace HomestayManagementSystem
{
    public partial class GuestMenu : Form
    {
        private RoomList roomList; // Use RoomList instead of loading from JSON
        private List<uint> displayedRoomIds = new List<uint>(); // Track displayed rooms

        private string getStateText(uint roomStatus)
        {
            return roomStatus switch
            {
                0 => "Free",
                1 => "Occupied",
                2 => "Deposited",
                _ => "Unknown"
            };
        }

        private Panel createRoomPreviewInfo(Room data)
        {
            try
            {
                if (data == null)
                {
                    return CreateErrorPanel("Room data is null");
                }

                string roomName = data.getID();
                uint roomStatus = data.getState();
                uint maxPersons = data.getMaxPersons();

                Panel panel = new Panel
                {
                    Width = preview_flowLayoutPanel.Width - 5,
                    Height = 120, // Increased height for more info
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                // Use safe polymorphic methods for display with null checks
                string displayInfo;
                string amenitiesText;

                try
                {
                    displayInfo = data.getDisplayInfo() ?? $"Room {roomName}";
                }
                catch (Exception)
                {
                    displayInfo = $"Room {roomName} - {getStateText(roomStatus)}";
                }

                try
                {
                    amenitiesText = data.getAmenitiesText() ?? "Basic room";
                }
                catch (Exception)
                {
                    amenitiesText = GetBasicAmenitiesText(data);
                }

                Label roomName_label = new Label
                {
                    Text = displayInfo,
                    BackColor = roomStatus == 0 ? Color.LightGreen : Color.LightCoral,
                    ForeColor = Color.Black,
                    AutoSize = false,
                    Height = 45,
                    Dock = DockStyle.Top,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(3)
                };

                Label amenitiesLabel = new Label
                {
                    Text = amenitiesText,
                    Dock = DockStyle.Top,
                    Height = 25,
                    BackColor = Color.LightGray,
                    ForeColor = Color.Black,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label roomCapacity_label = new Label
                {
                    Text = "Capacity: " + maxPersons + " people",
                    Dock = DockStyle.Bottom,
                    Height = 25,
                    BackColor = Color.LightGray,
                    ForeColor = Color.Black,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label roomPrice_label = new Label
                {
                    Text = "Price: " + data.getPrice().ToString("N0") + " VND/night",
                    Dock = DockStyle.Bottom,
                    Height = 25,
                    BackColor = Color.LightGray,
                    ForeColor = Color.Black,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                panel.Controls.Add(roomName_label);
                panel.Controls.Add(amenitiesLabel);
                panel.Controls.Add(roomCapacity_label);
                panel.Controls.Add(roomPrice_label);
                return panel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in createRoomPreviewInfo: {ex.Message}");
                return CreateErrorPanel($"Error creating room preview: {ex.Message}");
            }
        }

        private Panel CreateErrorPanel(string errorMessage)
        {
            Panel errorPanel = new Panel
            {
                Width = preview_flowLayoutPanel.Width - 5,
                Height = 120,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackColor = Color.LightCoral
            };

            Label errorLabel = new Label
            {
                Text = errorMessage,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            errorPanel.Controls.Add(errorLabel);
            return errorPanel;
        }

        private string GetBasicAmenitiesText(Room room)
        {
            try
            {
                var amenities = new List<string>();

                if (room.getHaveBalcony()) amenities.Add("Balcony");
                if (room.getHaveKitchen()) amenities.Add("Kitchen");
                if (room.getHaveBathtub()) amenities.Add("Bathtub");

                return amenities.Count > 0 ? string.Join(" | ", amenities) : "Basic room";
            }
            catch (Exception)
            {
                return "Basic room";
            }
        }

        private Panel createRoomPanel(Room data)
        {
            try
            {
                if (data == null)
                {
                    return CreateErrorPanel("Room data is null");
                }

                string roomName = data.getID();
                uint roomStatus = data.getState();

                Panel panel = new Panel
                {
                    Name = "RoomPanel_" + roomName,
                    Width = bookRoom_flowLayoutPanel.Width - 20,
                    Height = 100,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                // Use safe polymorphic methods with fallback
                string displayInfo;
                try
                {
                    displayInfo = data.getDisplayInfo() ?? $"Room {roomName}";
                }
                catch (Exception)
                {
                    displayInfo = $"Room {roomName} - {getStateText(roomStatus)}";
                }

                Label roomName_label = new Label
                {
                    Text = displayInfo,
                    BackColor = roomStatus == 0 ? Color.LightGreen : Color.LightCoral,
                    ForeColor = Color.Black,
                    AutoSize = false,
                    Height = 65,
                    Dock = DockStyle.Top,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(3)
                };

                // Add safe hover event handler with improved image handling
                roomName_label.MouseHover += (sender, e) =>
                {
                    try
                    {
                        // Clear previous preview panels - dispose images first
                        ClearPreviewPanel();

                        // Create and add the preview info panel with error handling
                        Panel previewPanel = createRoomPreviewInfo(data);
                        if (previewPanel != null)
                        {
                            preview_flowLayoutPanel.Controls.Add(previewPanel);
                        }

                        // Create and add the PictureBox for the room image with improved error handling
                        AddRoomImage(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in MouseHover event: {ex.Message}");
                        // Don't show MessageBox here as it interrupts the hover experience
                        // Instead, just clear the panel and show an error panel
                        ClearPreviewPanel();
                        Panel errorPanel = CreateErrorPanel("Error loading room preview");
                        preview_flowLayoutPanel.Controls.Add(errorPanel);
                    }
                };

                Button moreInfo_button = new Button
                {
                    Text = "More info",
                    Dock = DockStyle.Bottom,
                    Height = 30,
                    BackColor = Color.LightBlue,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    Margin = new Padding(5)
                };

                moreInfo_button.Click += (sender, e) =>
                {
                    try
                    {
                        ShowRoomDetailInfo(data);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error showing room details: {ex.Message}");
                    }
                };

                panel.Controls.Add(roomName_label);
                panel.Controls.Add(moreInfo_button);
                return panel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in createRoomPanel: {ex.Message}");
                return CreateErrorPanel($"Error creating room panel: {ex.Message}");
            }
        }

        private void ClearPreviewPanel()
        {
            try
            {
                // Safely dispose of all images and clear controls
                foreach (Control control in preview_flowLayoutPanel.Controls.OfType<Control>().ToList())
                {
                    if (control is PictureBox pic)
                    {
                        if (pic.Image != null)
                        {
                            var image = pic.Image;
                            pic.Image = null; // Remove reference first
                            image.Dispose(); // Then dispose
                        }
                    }
                    preview_flowLayoutPanel.Controls.Remove(control);
                    control.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing preview panel: {ex.Message}");
                // If we can't clear properly, just remove all controls without disposing
                preview_flowLayoutPanel.Controls.Clear();
            }
        }

        private void AddRoomImage(Room data)
        {
            try
            {
                string imagePath = data.getImagePath();

                PictureBox roomPicture = new PictureBox
                {
                    Width = preview_flowLayoutPanel.Width - 10,
                    Height = Math.Max(preview_flowLayoutPanel.Height - 130, 100), // Ensure minimum height
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5),
                    BackColor = Color.LightGray
                };

                // Check if image file exists and is valid
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    try
                    {
                        // Use safer image loading method
                        using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            // Create a copy of the image to avoid file locking issues
                            var originalImage = Image.FromStream(fileStream);
                            roomPicture.Image = new Bitmap(originalImage);
                            originalImage.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading image {imagePath}: {ex.Message}");
                        // Create a placeholder image instead
                        CreatePlaceholderImage(roomPicture, $"Room {data.getID()}\n(Image not available)");
                    }
                }
                else
                {
                    // Create a placeholder for missing image
                    CreatePlaceholderImage(roomPicture, $"Room {data.getID()}\n(No image)");
                }

                // Add click event to enlarge image (only if image is loaded)
                roomPicture.Click += (s, args) =>
                {
                    try
                    {
                        if (roomPicture.Image != null)
                        {
                            ShowImageDialog(data, roomPicture.Image);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error displaying image: {ex.Message}");
                    }
                };

                preview_flowLayoutPanel.Controls.Add(roomPicture);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding room image: {ex.Message}");
                // Add a simple error label instead
                Label errorLabel = new Label
                {
                    Text = "Image unavailable",
                    Width = preview_flowLayoutPanel.Width - 10,
                    Height = 50,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.LightGray,
                    BorderStyle = BorderStyle.FixedSingle
                };
                preview_flowLayoutPanel.Controls.Add(errorLabel);
            }
        }

        private void CreatePlaceholderImage(PictureBox pictureBox, string text)
        {
            try
            {
                // Create a simple bitmap with text
                int width = pictureBox.Width > 0 ? pictureBox.Width : 200;
                int height = pictureBox.Height > 0 ? pictureBox.Height : 150;

                Bitmap placeholder = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.LightGray);
                    using (Font font = new Font("Arial", 12, FontStyle.Bold))
                    using (Brush brush = new SolidBrush(Color.DarkGray))
                    {
                        StringFormat format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString(text, font, brush, new RectangleF(0, 0, width, height), format);
                    }
                }
                pictureBox.Image = placeholder;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating placeholder image: {ex.Message}");
                // If we can't even create a placeholder, just set background color
                pictureBox.BackColor = Color.LightGray;
            }
        }

        private void ShowImageDialog(Room data, Image image)
        {
            try
            {
                if (image == null)
                {
                    MessageBox.Show("No image to display");
                    return;
                }

                string displayInfo;
                try
                {
                    displayInfo = data.getDisplayInfo() ?? $"Room {data.getID()}";
                }
                catch (Exception)
                {
                    displayInfo = $"Room {data.getID()}";
                }

                Form imageForm = new Form
                {
                    Text = "Show images of " + displayInfo,
                    Width = 800,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterParent
                };

                // Create a copy of the image to avoid disposal issues
                Image originalImage = new Bitmap(image);

                PictureBox largePicture = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = new Bitmap(originalImage)
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
                    try
                    {
                        if (largePicture.Image != null)
                        {
                            largePicture.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            largePicture.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error flipping image: {ex.Message}");
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
                    try
                    {
                        if (largePicture.Image != null)
                        {
                            largePicture.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            largePicture.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error rotating image: {ex.Message}");
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
                    try
                    {
                        if (largePicture.Image != null)
                        {
                            largePicture.Image?.Dispose();
                            largePicture.Image = new Bitmap(originalImage);
                            largePicture.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error resetting image: {ex.Message}");
                    }
                };

                buttonPanel.Controls.Add(flipButton);
                buttonPanel.Controls.Add(rotateButton);
                buttonPanel.Controls.Add(resetButton);

                imageForm.Controls.Add(buttonPanel);
                imageForm.Controls.Add(largePicture);

                imageForm.FormClosed += (formSender, formArgs) =>
                {
                    try
                    {
                        largePicture.Image?.Dispose();
                        originalImage?.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error disposing images: {ex.Message}");
                    }
                };

                imageForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in image dialog: {ex.Message}");
            }
        }

        private void ShowBookingForm(Room room, bool isDeposit)
        {
            try
            {
                if (room == null)
                {
                    MessageBox.Show("Room data is null", "Error");
                    return;
                }

                string displayInfo;
                string amenitiesText;

                try
                {
                    displayInfo = room.getDisplayInfo() ?? $"Room {room.getID()}";
                }
                catch (Exception)
                {
                    displayInfo = $"Room {room.getID()}";
                }

                try
                {
                    amenitiesText = room.getAmenitiesText() ?? "Basic room";
                }
                catch (Exception)
                {
                    amenitiesText = GetBasicAmenitiesText(room);
                }

                Form bookingForm = new Form
                {
                    Text = isDeposit ? $"Deposit {displayInfo}" : $"Book {displayInfo}",
                    Width = 500,
                    Height = 650, // Increased height for date range fields
                    StartPosition = FormStartPosition.CenterParent
                };

                Label titleLabel = new Label
                {
                    Text = isDeposit ? "ROOM DEPOSIT INFORMATION" : "ROOM BOOKING INFORMATION",
                    Left = 20,
                    Top = 20,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };

                Label roomInfoLabel = new Label
                {
                    Text = $"Room: {displayInfo} - Price: {room.getPrice():N0} VND/night",
                    Left = 20,
                    Top = 60,
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Blue
                };

                // Show amenities using safe method
                Label amenitiesLabel = new Label
                {
                    Text = $"Amenities: {amenitiesText}",
                    Left = 20,
                    Top = 80,
                    AutoSize = true,
                    Width = 400,
                    Font = new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = Color.DarkBlue
                };

                // Date range section
                Label dateRangeLabel = new Label
                {
                    Text = "Booking Date Range:",
                    Left = 20,
                    Top = 110,
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.DarkGreen
                };

                // Check-in date
                Label checkInLabel = new Label { Text = "Check-in date:", Left = 20, Top = 140, AutoSize = true };
                DateTimePicker checkInDatePicker = new DateTimePicker
                {
                    Left = 150,
                    Top = 140,
                    Width = 200,
                    Format = DateTimePickerFormat.Short,
                    MinDate = DateTime.Today, // Cannot book past dates
                    Value = DateTime.Today
                };

                // Check-out date
                Label checkOutLabel = new Label { Text = "Check-out date:", Left = 20, Top = 170, AutoSize = true };
                DateTimePicker checkOutDatePicker = new DateTimePicker
                {
                    Left = 150,
                    Top = 170,
                    Width = 200,
                    Format = DateTimePickerFormat.Short,
                    MinDate = DateTime.Today.AddDays(1), // Minimum 1 day booking
                    Value = DateTime.Today.AddDays(1)
                };

                // Total nights label
                Label totalNightsLabel = new Label
                {
                    Text = "Total nights: 1",
                    Left = 20,
                    Top = 200,
                    AutoSize = true,
                    Font = new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = Color.DarkRed
                };

                // Guest name
                Label nameLabel = new Label { Text = "Guest name:", Left = 20, Top = 230, AutoSize = true };
                TextBox nameTextBox = new TextBox { Left = 150, Top = 230, Width = 300 };

                // Age
                Label ageLabel = new Label { Text = "Age:", Left = 20, Top = 270, AutoSize = true };
                NumericUpDown ageNumeric = new NumericUpDown
                {
                    Left = 150,
                    Top = 270,
                    Width = 100,
                    Minimum = 18,
                    Maximum = 100,
                    Value = 25
                };

                // CCCD
                Label cccdLabel = new Label { Text = "CCCD:", Left = 20, Top = 310, AutoSize = true };
                TextBox cccdTextBox = new TextBox { Left = 150, Top = 310, Width = 300, MaxLength = 12 };

                // Birth date
                Label birthLabel = new Label { Text = "Birth date:", Left = 20, Top = 350, AutoSize = true };
                DateTimePicker birthDatePicker = new DateTimePicker
                {
                    Left = 150,
                    Top = 350,
                    Width = 200,
                    Format = DateTimePickerFormat.Short,
                    MaxDate = DateTime.Today.AddYears(-18),
                    MinDate = DateTime.Today.AddYears(-100)
                };

                // Gender
                Label genderLabel = new Label { Text = "Gender:", Left = 20, Top = 390, AutoSize = true };
                ComboBox genderComboBox = new ComboBox
                {
                    Left = 150,
                    Top = 390,
                    Width = 100,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                genderComboBox.Items.AddRange(new string[] { "Male", "Female" });
                genderComboBox.SelectedIndex = 0;

                // Number of guests
                Label guestCountLabel = new Label { Text = "Number of guests:", Left = 20, Top = 430, AutoSize = true };
                NumericUpDown guestCountNumeric = new NumericUpDown
                {
                    Left = 150,
                    Top = 430,
                    Width = 100,
                    Minimum = 1,
                    Maximum = room.getMaxPersons(),
                    Value = 1
                };

                // Price info
                Label priceInfoLabel = new Label
                {
                    Text = isDeposit ? $"Deposit amount: 100,000 VND" : $"Total cost: {room.getPrice():N0} VND/night × 1 night = {room.getPrice():N0} VND",
                    Left = 20,
                    Top = 470,
                    AutoSize = true,
                    Width = 450,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = isDeposit ? Color.Green : Color.Red
                };

                // Buttons
                Button confirmButton = new Button
                {
                    Text = isDeposit ? "Confirm Deposit" : "Confirm Booking",
                    Left = 100,
                    Top = 560,
                    Width = 120,
                    Height = 50,
                    BackColor = Color.LightGreen
                };
                Button cancelButton = new Button
                {
                    Text = "Cancel",
                    Left = 250,
                    Top = 560,
                    Width = 120,
                    Height = 50,
                    BackColor = Color.LightCoral
                };

                // Pre-fill user info if available
                TryPreFillUserInfo(nameTextBox, cccdTextBox, birthDatePicker, ageNumeric);

                // Event handlers for date validation and price calculation
                EventHandler updateDateCalculations = (sender, e) =>
                {
                    try
                    {
                        if (checkOutDatePicker.Value <= checkInDatePicker.Value)
                        {
                            checkOutDatePicker.Value = checkInDatePicker.Value.AddDays(1);
                        }

                        int totalNights = (checkOutDatePicker.Value.Date - checkInDatePicker.Value.Date).Days;
                        totalNightsLabel.Text = $"Total nights: {totalNights}";

                        if (!isDeposit)
                        {
                            ulong totalCost = room.getPrice() * (ulong)totalNights;
                            priceInfoLabel.Text = $"Total cost: {room.getPrice():N0} VND/night × {totalNights} nights = {totalCost:N0} VND";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating date calculations: {ex.Message}");
                    }
                };

                checkInDatePicker.ValueChanged += updateDateCalculations;
                checkOutDatePicker.ValueChanged += updateDateCalculations;

                checkInDatePicker.ValueChanged += (sender, e) =>
                {
                    checkOutDatePicker.MinDate = checkInDatePicker.Value.AddDays(1);
                };

                // Validation for CCCD
                cccdTextBox.KeyPress += (sender, e) =>
                {
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                };

                // Sync age with birth date
                birthDatePicker.ValueChanged += (sender, e) =>
                {
                    try
                    {
                        int age = DateTime.Today.Year - birthDatePicker.Value.Year;
                        if (birthDatePicker.Value.Date > DateTime.Today.AddYears(-age)) age--;
                        ageNumeric.Value = Math.Max(18, age);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating age: {ex.Message}");
                    }
                };

                // Event handlers
                cancelButton.Click += (cancelSender, cancelArgs) => bookingForm.Close();

                confirmButton.Click += (confirmSender, confirmArgs) =>
                {
                    try
                    {
                        ProcessBookingWithDateRange(nameTextBox, ageNumeric, cccdTextBox, birthDatePicker,
                                      guestCountNumeric, genderComboBox, room, isDeposit, bookingForm,
                                      checkInDatePicker, checkOutDatePicker);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing booking: {ex.Message}");
                    }
                };

                // Add controls to form
                bookingForm.Controls.Add(titleLabel);
                bookingForm.Controls.Add(roomInfoLabel);
                bookingForm.Controls.Add(amenitiesLabel);
                bookingForm.Controls.Add(dateRangeLabel);
                bookingForm.Controls.Add(checkInLabel);
                bookingForm.Controls.Add(checkInDatePicker);
                bookingForm.Controls.Add(checkOutLabel);
                bookingForm.Controls.Add(checkOutDatePicker);
                bookingForm.Controls.Add(totalNightsLabel);
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
                MessageBox.Show($"Error displaying booking form: {ex.Message}");
            }
        }


        private void ShowRoomDetailInfo(Room roomData)
        {
            try
            {
                if (roomData == null)
                {
                    MessageBox.Show("Room data is null", "Error");
                    return;
                }

                string displayInfo;
                string amenitiesText;
                string roomTypeText;

                try
                {
                    displayInfo = roomData.getDisplayInfo() ?? $"Room {roomData.getID()}";
                }
                catch (Exception)
                {
                    displayInfo = $"Room {roomData.getID()}";
                }

                try
                {
                    amenitiesText = roomData.getAmenitiesText() ?? "Basic room";
                }
                catch (Exception)
                {
                    amenitiesText = GetBasicAmenitiesText(roomData);
                }

                // Guest count information display removed

                try
                {
                    roomTypeText = roomData.getRoomType().ToString();
                }
                catch (Exception)
                {
                    roomTypeText = "Standard";
                }

                Form roomInfoForm = new Form
                {
                    Text = $"More info - {displayInfo}",
                    Width = 450,
                    Height = 500,
                    StartPosition = FormStartPosition.CenterParent
                };

                string roomStatusText = getStateText(roomData.getState());

                // Create enhanced labels using safe polymorphic methods
                Label roomIdLabel = new Label { Text = $"Room ID: {roomData.getID()}", Left = 20, Top = 20, AutoSize = true };
                Label roomDisplayLabel = new Label { Text = $"Display: {displayInfo}", Left = 20, Top = 50, AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold) };
                Label roomStatusLabel = new Label { Text = $"Status: {roomStatusText}", Left = 20, Top = 80, AutoSize = true };
                Label roomPriceLabel = new Label { Text = $"Price: {roomData.getPrice():N0} VND/night", Left = 20, Top = 110, AutoSize = true };
                Label roomBedsLabel = new Label { Text = $"Number of beds: {roomData.getNumBeds()}", Left = 20, Top = 140, AutoSize = true };
                Label maxPersonsLabel = new Label { Text = $"Max capacity: {roomData.getMaxPersons()} people", Left = 20, Top = 170, AutoSize = true };
                Label amenitiesLabel = new Label { Text = $"Amenities: {amenitiesText}", Left = 20, Top = 200, AutoSize = true, Width = 400 };
                Label roomTypeLabel = new Label { Text = $"Room type: {roomTypeText}", Left = 20, Top = 230, AutoSize = true };

                // Create buttons
                Button closeButton = new Button { Text = "Close", Left = 50, Top = 360, Width = 100, Height = 50 };
                Button bookRoomButton = new Button { Text = "Book Room", Left = 175, Top = 360, Width = 100, Height = 50 };
                Button depositButton = new Button { Text = "Deposit", Left = 300, Top = 360, Width = 100, Height = 50 };

                // Check conditions for deposit button
                ulong roomPriceInVND = roomData.getPrice();
                if (roomPriceInVND <= 100000)
                {
                    depositButton.Visible = false;
                    depositButton.Enabled = false;
                }

                // Only allow booking if room is available
                if (roomData.getState() != 0)
                {
                    bookRoomButton.Enabled = false;
                    depositButton.Enabled = false;
                }

                // Event handlers
                closeButton.Click += (closeSender, closeArgs) => roomInfoForm.Close();

                bookRoomButton.Click += (bookSender, bookArgs) =>
                {
                    try
                    {
                        roomInfoForm.Close();
                        ShowBookingForm(roomData, false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening booking form: {ex.Message}");
                    }
                };

                depositButton.Click += (depositSender, depositArgs) =>
                {
                    try
                    {
                        roomInfoForm.Close();
                        ShowBookingForm(roomData, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening deposit form: {ex.Message}");
                    }
                };

                // Add controls to form
                roomInfoForm.Controls.Add(roomIdLabel);
                roomInfoForm.Controls.Add(roomDisplayLabel);
                roomInfoForm.Controls.Add(roomStatusLabel);
                roomInfoForm.Controls.Add(roomPriceLabel);
                roomInfoForm.Controls.Add(roomBedsLabel);
                roomInfoForm.Controls.Add(maxPersonsLabel);
                roomInfoForm.Controls.Add(amenitiesLabel);
                roomInfoForm.Controls.Add(roomTypeLabel);
                roomInfoForm.Controls.Add(closeButton);
                roomInfoForm.Controls.Add(bookRoomButton);
                roomInfoForm.Controls.Add(depositButton);

                roomInfoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room information: {ex.Message}");
            }
        }

        // Keep all the existing methods from TryPreFillUserInfo onwards exactly as they were...
        private void TryPreFillUserInfo(TextBox nameTextBox, TextBox cccdTextBox,
                                       DateTimePicker birthDatePicker, NumericUpDown ageNumeric)
        {
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
                        var parser = new FileIniDataParser();
                        IniData data = parser.ReadFile(iniPath);
                        var customerSection = data["Customer"];

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
                Console.WriteLine($"Cannot pre-fill information: {ex.Message}");
            }
        }

        private string ExtractRoomNumberFromDisplayText(string displayText)
        {
            try
            {
                if (displayText.StartsWith("Room "))
                {
                    int startIndex = "Room ".Length;
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
                MessageBox.Show($"Error filtering rooms: {ex.Message}");
            }
        }

        private void ProcessBookingWithDateRange(TextBox nameTextBox, NumericUpDown ageNumeric, TextBox cccdTextBox,
          DateTimePicker birthDatePicker, NumericUpDown guestCountNumeric, ComboBox genderComboBox,
          Room room, bool isDeposit, Form bookingForm, DateTimePicker checkInDatePicker, DateTimePicker checkOutDatePicker)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                {
                    MessageBox.Show("Please enter guest name.", "Missing Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(cccdTextBox.Text) || cccdTextBox.Text.Length != 12)
                {
                    MessageBox.Show("CCCD must be exactly 12 digits.", "Invalid Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cccdTextBox.Focus();
                    return;
                }

                if (!cccdTextBox.Text.All(char.IsDigit))
                {
                    MessageBox.Show("CCCD must contain only numbers.", "Invalid Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cccdTextBox.Focus();
                    return;
                }

                if (ageNumeric.Value < 18)
                {
                    MessageBox.Show("Guest must be at least 18 years old.", "Invalid Age",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (birthDatePicker.Value > DateTime.Today.AddYears(-18))
                {
                    MessageBox.Show("Invalid birth date. Guest must be at least 18 years old.", "Invalid Birth Date",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    birthDatePicker.Focus();
                    return;
                }

                if (genderComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select gender.", "Missing Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    genderComboBox.Focus();
                    return;
                }

                if (checkOutDatePicker.Value <= checkInDatePicker.Value)
                {
                    MessageBox.Show("Check-out date must be after check-in date.", "Invalid Date Range",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkOutDatePicker.Focus();
                    return;
                }

                // Check room availability for the selected date range
                if (!IsRoomAvailableForDateRange(room, checkInDatePicker.Value, checkOutDatePicker.Value))
                {
                    MessageBox.Show("Room is not available for the selected date range. Please choose different dates.",
                                    "Room Not Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create booking information using safe polymorphic method
                string displayInfo;
                try
                {
                    displayInfo = room.getDisplayInfo() ?? $"Room {room.getID()}";
                }
                catch (Exception)
                {
                    displayInfo = $"Room {room.getID()}";
                }

                int totalNights = (checkOutDatePicker.Value.Date - checkInDatePicker.Value.Date).Days;
                ulong totalCost = isDeposit ? 100000 : room.getPrice() * (ulong)totalNights;

                string bookingInfo = $"Room: {displayInfo}\n" +
                   $"Check-in: {checkInDatePicker.Value:dd/MM/yyyy}\n" +
                   $"Check-out: {checkOutDatePicker.Value:dd/MM/yyyy}\n" +
                   $"Total nights: {totalNights}\n" +
                   $"Guest name: {nameTextBox.Text}\n" +
                   $"Age: {ageNumeric.Value}\n" +
                   $"CCCD: {cccdTextBox.Text}\n" +
                   $"Birth date: {birthDatePicker.Value:dd/MM/yyyy}\n" +
                   $"Gender: {genderComboBox.SelectedItem}\n" +
                   $"Number of guests: {guestCountNumeric.Value}\n" +
                   $"Type: {(isDeposit ? "Deposit" : "Booking")}\n" +
                   $"Total amount: {totalCost:N0} VND";

                DialogResult result = MessageBox.Show($"Confirm information:\n\n{bookingInfo}",
                                                    "Confirm Booking",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Update room status using RoomList
                    uint newStatus = isDeposit ? (uint)2 : (uint)1;
                    uint roomId = uint.Parse(room.getID());
                    var roomFromList = roomList.getRoom(roomId);
                    roomFromList.setState(newStatus);

                    // Save booking information to both systems
                    bool isFemale = genderComboBox.SelectedItem.ToString() == "Female";

                    // Save to RoomList for current state
                    SaveBookingToRoomList(roomFromList, nameTextBox.Text, cccdTextBox.Text,
                                         birthDatePicker.Value, (uint)guestCountNumeric.Value, isFemale, isDeposit);

                    // Save to EventData.json for calendar synchronization
                    SaveBookingToEventData(roomId, nameTextBox.Text, cccdTextBox.Text, birthDatePicker.Value,
                                          (uint)guestCountNumeric.Value, isFemale, checkInDatePicker.Value,
                                          checkOutDatePicker.Value, isDeposit);

                    MessageBox.Show($"{(isDeposit ? "Room deposit" : "Room booking")} successful!\n\n{bookingInfo}",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    bookingForm.Close();
                    RefreshGuestMenu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing booking: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsRoomAvailableForDateRange(Room room, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                uint roomId = uint.Parse(room.getID());

                // Check each day in the range for availability
                for (DateTime date = checkIn.Date; date < checkOut.Date; date = date.AddDays(1))
                {
                    if (!roomList.isRoomAvailableForDate(roomId, date))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking room availability: {ex.Message}");
                return false;
            }
        }

        private void SaveBookingToEventData(uint roomId, string customerName, string cccd, DateTime birthDate,
                                            uint guestCount, bool isFemale, DateTime checkIn, DateTime checkOut, bool isDeposit)
        {
            try
            {
                string eventDataPath = @"./Data/Events/EVENTDATA.json";

                // Get additional user info for complete Person object
                string email = "";
                string phoneNumber = "";
                string address = "";

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

                // Read existing event data
                List<object> eventsList = new List<object>();

                if (File.Exists(eventDataPath))
                {
                    string existingJson = File.ReadAllText(eventDataPath);
                    using JsonDocument document = JsonDocument.Parse(existingJson);

                    if (document.RootElement.TryGetProperty("events", out JsonElement eventsArray))
                    {
                        foreach (JsonElement eventElement in eventsArray.EnumerateArray())
                        {
                            // Convert existing events to objects for preservation
                            var existingEvent = new
                            {
                                roomId = eventElement.GetProperty("roomId").GetInt32(),
                                startDate = new
                                {
                                    day = eventElement.GetProperty("startDate").GetProperty("day").GetInt32(),
                                    month = eventElement.GetProperty("startDate").GetProperty("month").GetInt32(),
                                    year = eventElement.GetProperty("startDate").GetProperty("year").GetInt32()
                                },
                                endDate = new
                                {
                                    day = eventElement.GetProperty("endDate").GetProperty("day").GetInt32(),
                                    month = eventElement.GetProperty("endDate").GetProperty("month").GetInt32(),
                                    year = eventElement.GetProperty("endDate").GetProperty("year").GetInt32()
                                },
                                guestInfo = eventElement.TryGetProperty("guestInfo", out JsonElement guestElement) ? new
                                {
                                    name = guestElement.TryGetProperty("name", out var nameEl) ? nameEl.GetString() : "",
                                    age = guestElement.TryGetProperty("age", out var ageEl) && ageEl.TryGetUInt32(out uint age) ? age : 0u,
                                    sex = guestElement.TryGetProperty("sex", out var sexEl) ? sexEl.GetBoolean() : false,
                                    mail = guestElement.TryGetProperty("mail", out var mailEl) ? mailEl.GetString() : "",
                                    CCCD = guestElement.TryGetProperty("CCCD", out var cccdEl) ? cccdEl.GetString() : "",
                                    phoneNumber = guestElement.TryGetProperty("phoneNumber", out var phoneEl) ? phoneEl.GetString() : "",
                                    address = guestElement.TryGetProperty("address", out var addressEl) ? addressEl.GetString() : ""
                                } : null,
                                additionalGuests = eventElement.TryGetProperty("additionalGuests", out JsonElement addGuestsElement) ?
                                    addGuestsElement.EnumerateArray().Cast<JsonElement>().Select(addGuest => (object)new
                                    {
                                        name = addGuest.TryGetProperty("name", out var addNameEl) ? addNameEl.GetString() : "",
                                        age = addGuest.TryGetProperty("age", out var addAgeEl) && addAgeEl.TryGetUInt32(out uint addAge) ? addAge : 0u,
                                        sex = addGuest.TryGetProperty("sex", out var addSexEl) ? addSexEl.GetBoolean() : false,
                                        mail = addGuest.TryGetProperty("mail", out var addMailEl) ? addMailEl.GetString() : "",
                                        CCCD = addGuest.TryGetProperty("CCCD", out var addCccdEl) ? addCccdEl.GetString() : "",
                                        phoneNumber = addGuest.TryGetProperty("phoneNumber", out var phoneEl) ? phoneEl.GetString() : "",
                                        address = addGuest.TryGetProperty("address", out var addAddressEl) ? addAddressEl.GetString() : ""
                                    }).ToList() : new List<object>()
                            };
                            eventsList.Add(existingEvent);
                        }
                    }
                }
                else
                {
                    // Create directory if it doesn't exist
                    Directory.CreateDirectory(Path.GetDirectoryName(eventDataPath));
                }

                // Create new event object
                var newEvent = new
                {
                    roomId = (int)roomId,
                    startDate = new
                    {
                        day = checkIn.Day,
                        month = checkIn.Month,
                        year = checkIn.Year
                    },
                    endDate = new
                    {
                        day = checkOut.AddDays(-1).Day, // End date is last day of stay
                        month = checkOut.AddDays(-1).Month,
                        year = checkOut.AddDays(-1).Year
                    },
                    guestInfo = new
                    {
                        name = customerName,
                        age = (uint)(DateTime.Today.Year - birthDate.Year),
                        sex = isFemale,
                        mail = email,
                        CCCD = cccd,
                        phoneNumber = phoneNumber,
                        address = address
                    },
                    additionalGuests = new List<object>() // For future expansion - additional guests without accounts
                };

                // Add new event to the list
                eventsList.Add(newEvent);

                // Create updated event data
                var updatedEventData = new { events = eventsList };

                // Save back to file using System.Text.Json
                var options = new JsonSerializerOptions { WriteIndented = true };
                string updatedJson = JsonSerializer.Serialize(updatedEventData, options);
                File.WriteAllText(eventDataPath, updatedJson);

                Console.WriteLine($"Saved booking event for room {roomId} from {checkIn:yyyy-MM-dd} to {checkOut:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving booking to event data: {ex.Message}");
            }
        }

        private void SaveBookingToRoomList(Room room, string customerName, string cccd,
             DateTime birthDate, uint guestCount, bool isFemale, bool isDeposit)
        {
            try
            {
                // Get additional user info for complete Person object
                string email = "";
                string phoneNumber = "";
                string address = "";

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
                            var parser = new FileIniDataParser();
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

                // Create primary guest (account owner)
                var primaryGuest = new Person
                {
                    name = customerName,
                    age = (uint)(DateTime.Today.Year - birthDate.Year),
                    sex = isFemale,
                    mail = email,
                    CCCD = cccd,
                    phoneNumber = phoneNumber,
                    address = address
                };

                // For now, just set the primary guest. Additional guests will be managed through events
                room.setCurGuest(new[] { primaryGuest });

                // Update ROOMDATA.json (without curGuest field for persistence)
                string jsonPath = @"./Data/Rooms/ROOMDATA.json";
                var allRooms = roomList.getAllRoomsDetailed().Values.ToList();

                // Create room data without curGuest field
                var roomsData = allRooms.Select(r => new
                {
                    ID = int.Parse(r.getID()),
                    numBeds = r.getNumBeds(),
                    haveBalcony = r.getHaveBalcony(),
                    haveKitchen = r.getHaveKitchen(),
                    haveBathtub = r.getHaveBathtub(),
                    capacity = r.getCapacity(),
                    price = r.getPrice(),
                    state = r.getState()
                    // Removed curGuest field
                }).ToList();

                var roomDataObj = new { rooms = roomsData };
                var options = new JsonSerializerOptions { WriteIndented = true };
                string roomJson = JsonSerializer.Serialize(roomDataObj, options);
                File.WriteAllText(jsonPath, roomJson);

                string displayInfo;
                try
                {
                    displayInfo = room.getDisplayInfo() ?? $"Room {room.getID()}";
                }
                catch (Exception)
                {
                    displayInfo = $"Room {room.getID()}";
                }

                Console.WriteLine($"Saved booking information for {displayInfo}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving booking information: {ex.Message}");
            }
        }
        private void RefreshGuestMenu()
        {
            try
            {
                // Clear preview panel first to avoid issues
                ClearPreviewPanel();

                bookRoom_flowLayoutPanel.Controls.Clear();

                LoadRoomPanels();
                UpdateRoomSelectionBox();

                if (room_selection_box.Items.Count > 0)
                {
                    room_selection_box.SelectedIndex = 0;
                }

                Console.WriteLine("Refreshed GuestMenu with updated data");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing menu: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public GuestMenu()
        {
            InitializeComponent();
            try
            {
                roomList = RoomList.Instance; // Use singleton
                LoadAllRoomsFromRoomList();
                LoadRoomPanels();
                UpdateRoomSelectionBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing GuestMenu: {ex.Message}", "Initialization Error");
            }
        }

        // Continue with rest of the methods exactly as they were...
        private void LoadAllRoomsFromRoomList()
        {
            try
            {
                // Load rooms from JSON to RoomList if not already loaded
                string jsonPath = "Data/Rooms/ROOMDATA.json";

                if (File.Exists(jsonPath))
                {
                    var rooms = Room.LoadRoomsFromJson(jsonPath);

                    foreach (var room in rooms)
                    {
                        try
                        {
                            uint roomId = uint.Parse(room.getID());

                            try
                            {
                                roomList.getRoom(roomId);
                            }
                            catch (ArgumentException)
                            {
                                // Room doesn't exist, add it
                                RoomType roomType = DetermineRoomType(room);
                                uint option = DetermineRoomOption(room);
                                roomList.addRoom(roomId, option, roomType);

                                var addedRoom = roomList.getRoom(roomId);
                                addedRoom.setState(room.getState());
                                addedRoom.setCurGuest(room.getCurGuest());
                                addedRoom.setPrice(room.getPrice());
                            }

                            displayedRoomIds.Add(roomId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing room {room.getID()}: {ex.Message}");
                        }
                    }
                }
                else
                {
                    CreateFallbackRoomData();
                }

                displayedRoomIds = displayedRoomIds.Distinct().OrderBy(id => id).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rooms: {ex.Message}");
                CreateFallbackRoomData();
            }
        }

        private RoomType DetermineRoomType(Room room)
        {
            try
            {
                if (room.getPrice() > 1000000)
                    return RoomType.Suite;
                else if (room.getPrice() > 600000 || (room.getHaveBalcony() && room.getHaveKitchen() && room.getHaveBathtub()))
                    return RoomType.Luxury;
                else
                    return RoomType.Standard;
            }
            catch (Exception)
            {
                return RoomType.Standard; // Default fallback
            }
        }

        private uint DetermineRoomOption(Room room)
        {
            try
            {
                if (room.getNumBeds() == 1 && room.getCapacity() == 1)
                    return 1;
                else if (room.getNumBeds() == 1 && room.getCapacity() == 2)
                    return 2;
                else if (room.getNumBeds() == 2 && room.getCapacity() >= 4)
                    return 3;
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0; // Default fallback
            }
        }

        private void CreateFallbackRoomData()
        {
            try
            {
                roomList.addRoom(101, 1, RoomType.Standard);
                roomList.addRoom(102, 2, RoomType.Standard);
                roomList.addRoom(103, 3, RoomType.Luxury);
                roomList.addRoom(104, 1, RoomType.Standard);

                var room1 = roomList.getRoom(101);
                room1.setState(0);
                room1.setPrice(500);

                var room2 = roomList.getRoom(102);
                room2.setState(1);
                room2.setPrice(800);

                var room3 = roomList.getRoom(103);
                room3.setState(0);
                room3.setPrice(1200);

                var room4 = roomList.getRoom(104);
                room4.setState(2);
                room4.setPrice(450);

                displayedRoomIds.AddRange(new uint[] { 101, 102, 103, 104 });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating fallback data: {ex.Message}");
            }
        }

        private void LoadRoomPanels()
        {
            try
            {
                bookRoom_flowLayoutPanel.Controls.Clear();

                foreach (uint roomId in displayedRoomIds)
                {
                    try
                    {
                        Room room = roomList.getRoom(roomId);
                        Panel panel = createRoomPanel(room);
                        if (panel != null)
                        {
                            bookRoom_flowLayoutPanel.Controls.Add(panel);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading panel for room {roomId}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room panels: {ex.Message}");
            }
        }

        private void UpdateRoomSelectionBox()
        {
            try
            {
                room_selection_box.Items.Clear();
                room_selection_box.Items.Add("All rooms");

                foreach (uint roomId in displayedRoomIds)
                {
                    try
                    {
                        Room room = roomList.getRoom(roomId);
                        string roomDisplayText = $"Room {room.getID()} - " +
                           $"{getStateText(room.getState())} - " +
                           $"{room.getPrice():N0} VND/night";

                        room_selection_box.Items.Add(roomDisplayText);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding room {roomId} to selection box: {ex.Message}");
                    }
                }

                if (room_selection_box.Items.Count > 0)
                {
                    room_selection_box.SelectedIndex = 0;
                }

                room_selection_box.SelectedIndexChanged -= Room_selection_box_SelectedIndexChanged;
                room_selection_box.SelectedIndexChanged += Room_selection_box_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating room list: {ex.Message}");
            }
        }

        private void Room_selection_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = sender as ComboBox;

                if (comboBox?.SelectedIndex == 0) // "All rooms"
                {
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
                    string selectedText = comboBox.SelectedItem.ToString();
                    string roomNumber = ExtractRoomNumberFromDisplayText(selectedText);
                    FilterRoomPanels(roomNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering rooms: {ex.Message}");
            }
        }

        private void returnMainMenu_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear images before navigating
                ClearPreviewPanel();

                var mainMenu_Form = (mainMenu)Tag;
                mainMenu_Form.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning to main menu: {ex.Message}");
            }
        }

        private void user_info_Click(object sender, EventArgs e)
        {
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
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Username not found.");
                    return;
                }

                string iniPath = $"Data/Users/{username}.ini";
                if (!System.IO.File.Exists(iniPath))
                {
                    MessageBox.Show("User information file not found.");
                    return;
                }

                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(iniPath);
                var customerSection = data["Customer"];

                GuestAccount guestAccount = new GuestAccount();
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

                guestAccount.SetAccountInfo(userInfo);

                string rank = customerSection["Rank"];
                string currentRoom = customerSection.ContainsKey("CurrentRoom") ? customerSection["CurrentRoom"] : "";

                Form infoForm = new Form
                {
                    Text = "User Information",
                    Width = 500,
                    Height = 400,
                    StartPosition = FormStartPosition.CenterParent
                };

                Label nameLabel = new Label { Text = $"Name: {userInfo.FullName}", Left = 20, Top = 20, AutoSize = true };
                Label birthLabel = new Label { Text = $"Birth year: {userInfo.BirthDay}", Left = 20, Top = 50, AutoSize = true };
                Label CCCDLabel = new Label { Text = $"CCCD: {userInfo.CCCD}", Left = 20, Top = 80, AutoSize = true };
                Label addressLabel = new Label { Text = $"Address: {userInfo.Address}", Left = 20, Top = 110, AutoSize = true };
                Label phoneLabel = new Label { Text = $"Phone: {userInfo.PhoneNumber}", Left = 20, Top = 140, AutoSize = true };
                Label emailLabel = new Label { Text = $"Email: {userInfo.Email}", Left = 20, Top = 170, AutoSize = true };
                Label roomLabel = new Label { Text = $"Current room: {currentRoom}", Left = 20, Top = 200, AutoSize = true };
                Label rankLabel = new Label { Text = $"Member rank: {rank} (View benefits)", Left = 20, Top = 230, AutoSize = true };

                Button editButton = new Button { Text = "Edit", Left = 60, Top = 280, Width = 100 };
                Button saveButton = new Button { Text = "Save", Left = 220, Top = 280, Width = 100 };

                saveButton.Click += (saveSender, saveArgs) =>
                {
                    MessageBox.Show("Information has been saved!");
                    infoForm.Close();
                };

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
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying user info: {ex.Message}");
            }
        }
    }
}