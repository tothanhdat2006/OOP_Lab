using Calendar;
using System;
using System.Data;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HomestayManagementSystem
{
    public partial class GeneralCalendar : UserControl
    {
        private Calendar.Calendar calendar;
        private List<Calendar.Event> events = new List<Calendar.Event>();
        private DateTime currentWeekStart;
        private RoomList roomList; // Use RoomList instead of dictionaries
        private System.Windows.Forms.ToolTip toolTip;

        public GeneralCalendar()
        {
            InitializeComponent();
            calendar = Calendar.Calendar.GetInstance();
            toolTip = new System.Windows.Forms.ToolTip();
            roomList = RoomList.Instance; // Use singleton
            currentWeekStart = GetStartOfWeek(DateTime.Now);
            LoadRoomDetails();
            LoadEventsFromJson();
            PopulateCalendar();
        }

        private void LoadRoomDetails()
        {
            try
            {
                string roomDataPath = @"./Data/Rooms/ROOMDATA.json";

                if (!File.Exists(roomDataPath))
                {
                    MessageBox.Show($"Room data file not found: {roomDataPath}");
                    return;
                }

                // Load rooms using existing Room.LoadRoomsFromJson method
                var rooms = Room.LoadRoomsFromJson(roomDataPath);

                // Add rooms to RoomList if they don't exist
                foreach (var room in rooms)
                {
                    try
                    {
                        uint roomId = uint.Parse(room.getID());
                        // Try to get existing room, if not found, add it
                        try
                        {
                            roomList.getRoom(roomId);
                        }
                        catch (ArgumentException)
                        {
                            // Room doesn't exist, determine type and add it
                            RoomType roomType = DetermineRoomType(room);
                            uint option = DetermineRoomOption(room);
                            roomList.addRoom(roomId, option, roomType);

                            // Update the room with loaded data (excluding curGuest)
                            var addedRoom = roomList.getRoom(roomId);
                            addedRoom.setState(room.getState());
                            addedRoom.setPrice(room.getPrice());
                            // Remove setCurGuest since guest info is now in events
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing room {room.getID()}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room details: {ex.Message}");
            }
        }

        private RoomType DetermineRoomType(Room room)
        {
            // Logic to determine room type based on amenities and price
            if (room.getPrice() > 1000000)
                return RoomType.Suite;
            else if (room.getPrice() > 600000 || (room.getHaveBalcony() && room.getHaveKitchen() && room.getHaveBathtub()))
                return RoomType.Luxury;
            else
                return RoomType.Standard;
        }

        private uint DetermineRoomOption(Room room)
        {
            // Determine option based on room configuration
            if (room.getNumBeds() == 1 && room.getCapacity() == 1)
                return 1; // Personal room
            else if (room.getNumBeds() == 1 && room.getCapacity() == 2)
                return 2; // Couple room
            else if (room.getNumBeds() == 2 && room.getCapacity() >= 4)
                return 3; // Quadro room
            else
                return 0; // Custom
        }

        private List<Calendar.Event> LoadEventsFromJson()
        {
            try
            {
                string jsonFilePath = @"./Data/Events/EVENTDATA.json";

                if (!File.Exists(jsonFilePath))
                {
                    MessageBox.Show($"Event data file not found: {jsonFilePath}");
                    return new List<Calendar.Event>();
                }

                string jsonString = File.ReadAllText(jsonFilePath);
                using JsonDocument document = JsonDocument.Parse(jsonString);
                JsonElement root = document.RootElement;

                if (root.TryGetProperty("events", out JsonElement eventsArray))
                {
                    foreach (JsonElement eventElement in eventsArray.EnumerateArray())
                    {
                        try
                        {
                            // Safely get room ID
                            if (!eventElement.TryGetProperty("roomId", out JsonElement roomIdElement))
                                continue;
                            int roomId = roomIdElement.GetInt32();

                            // Safely get start date
                            if (!eventElement.TryGetProperty("startDate", out JsonElement startDateElement))
                                continue;

                            if (!startDateElement.TryGetProperty("day", out JsonElement startDayElement) ||
                                !startDateElement.TryGetProperty("month", out JsonElement startMonthElement) ||
                                !startDateElement.TryGetProperty("year", out JsonElement startYearElement))
                                continue;

                            int startDay = startDayElement.GetInt32();
                            int startMonth = startMonthElement.GetInt32();
                            int startYear = startYearElement.GetInt32();
                            Calendar.Date startDate = new Calendar.Date(startDay, startMonth, startYear);

                            // Safely get end date
                            if (!eventElement.TryGetProperty("endDate", out JsonElement endDateElement))
                                continue;

                            if (!endDateElement.TryGetProperty("day", out JsonElement endDayElement) ||
                                !endDateElement.TryGetProperty("month", out JsonElement endMonthElement) ||
                                !endDateElement.TryGetProperty("year", out JsonElement endYearElement))
                                continue;

                            int endDay = endDayElement.GetInt32();
                            int endMonth = endMonthElement.GetInt32();
                            int endYear = endYearElement.GetInt32();
                            Calendar.Date endDate = new Calendar.Date(endDay, endMonth, endYear);

                            string type = "Occupied"; // Default for events

                            Calendar.Event eventObj;

                            // Load primary guest information and additional guests
                            if (eventElement.TryGetProperty("guestInfo", out JsonElement guestElement) &&
                                guestElement.ValueKind != JsonValueKind.Null)
                            {
                                Person primaryGuest = new Person();

                                if (guestElement.TryGetProperty("name", out JsonElement nameElement) &&
                                    nameElement.ValueKind != JsonValueKind.Null)
                                    primaryGuest.name = nameElement.GetString() ?? "";

                                if (guestElement.TryGetProperty("age", out JsonElement ageElement) &&
                                    ageElement.ValueKind != JsonValueKind.Null &&
                                    ageElement.TryGetUInt32(out uint age))
                                    primaryGuest.age = age;

                                if (guestElement.TryGetProperty("sex", out JsonElement sexElement) &&
                                    sexElement.ValueKind != JsonValueKind.Null)
                                    primaryGuest.sex = sexElement.GetBoolean();

                                if (guestElement.TryGetProperty("mail", out JsonElement mailElement) &&
                                    mailElement.ValueKind != JsonValueKind.Null)
                                    primaryGuest.mail = mailElement.GetString() ?? "";

                                if (guestElement.TryGetProperty("CCCD", out JsonElement cccdElement) &&
                                    cccdElement.ValueKind != JsonValueKind.Null)
                                    primaryGuest.CCCD = cccdElement.GetString() ?? "";

                                if (guestElement.TryGetProperty("phoneNumber", out JsonElement phoneElement) &&
                                    phoneElement.ValueKind != JsonValueKind.Null)
                                    primaryGuest.phoneNumber = phoneElement.GetString() ?? "";

                                if (guestElement.TryGetProperty("address", out JsonElement addressElement) &&
                                    addressElement.ValueKind != JsonValueKind.Null)
                                    primaryGuest.address = addressElement.GetString() ?? "";

                                // Load additional guests if available
                                var allGuests = new List<Person> { primaryGuest };

                                if (eventElement.TryGetProperty("additionalGuests", out JsonElement additionalGuestsArray) &&
                                    additionalGuestsArray.ValueKind == JsonValueKind.Array)
                                {
                                    foreach (JsonElement additionalGuestElement in additionalGuestsArray.EnumerateArray())
                                    {
                                        if (additionalGuestElement.ValueKind == JsonValueKind.Null)
                                            continue;

                                        Person additionalGuest = new Person();

                                        if (additionalGuestElement.TryGetProperty("name", out JsonElement addNameElement) &&
                                            addNameElement.ValueKind != JsonValueKind.Null)
                                            additionalGuest.name = addNameElement.GetString() ?? "";

                                        if (additionalGuestElement.TryGetProperty("age", out JsonElement addAgeElement) &&
                                            addAgeElement.ValueKind != JsonValueKind.Null &&
                                            addAgeElement.TryGetUInt32(out uint addAge))
                                            additionalGuest.age = addAge;

                                        if (additionalGuestElement.TryGetProperty("sex", out JsonElement addSexElement) &&
                                            addSexElement.ValueKind != JsonValueKind.Null)
                                            additionalGuest.sex = addSexElement.GetBoolean();

                                        if (additionalGuestElement.TryGetProperty("mail", out JsonElement addMailElement) &&
                                            addMailElement.ValueKind != JsonValueKind.Null)
                                            additionalGuest.mail = addMailElement.GetString() ?? "";

                                        if (additionalGuestElement.TryGetProperty("CCCD", out JsonElement addCccdElement) &&
                                            addCccdElement.ValueKind != JsonValueKind.Null)
                                            additionalGuest.CCCD = addCccdElement.GetString() ?? "";

                                        if (additionalGuestElement.TryGetProperty("phoneNumber", out JsonElement addPhoneElement) &&
                                            addPhoneElement.ValueKind != JsonValueKind.Null)
                                            additionalGuest.phoneNumber = addPhoneElement.GetString() ?? "";

                                        if (additionalGuestElement.TryGetProperty("address", out JsonElement addAddressElement) &&
                                            addAddressElement.ValueKind != JsonValueKind.Null)
                                            additionalGuest.address = addAddressElement.GetString() ?? "";

                                        allGuests.Add(additionalGuest);
                                    }
                                }

                                eventObj = new Calendar.Event(type, roomId, startDate, endDate, primaryGuest);

                                // Store all guests in the room's current guest list
                                if (roomList != null)
                                {
                                    try
                                    {
                                        var room = roomList.getRoom((uint)roomId);
                                        room.setCurGuest(allGuests.ToArray());
                                    }
                                    catch (ArgumentException)
                                    {
                                        // Room not found in RoomList, skip guest assignment
                                        Console.WriteLine($"Room {roomId} not found in RoomList, skipping guest assignment");
                                    }
                                }
                            }
                            else
                            {
                                eventObj = new Calendar.Event(type, roomId, startDate, endDate);
                            }

                            events.Add(eventObj);
                            calendar.AddEvent(eventObj);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing event: {ex.Message}");
                            // Continue with next event instead of failing completely
                            continue;
                        }
                    }
                }

                // Update room availability information in RoomList
                var allRooms = roomList.getAllRoomsDetailed();
                foreach (var roomPair in allRooms)
                {
                    roomList.updateRoomAvailability(roomPair.Key, events);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}");
            }

            return events;
        }

        private string getDayName(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Monday => "Mon",
                DayOfWeek.Tuesday => "Tue",
                DayOfWeek.Wednesday => "Wed",
                DayOfWeek.Thursday => "Thu",
                DayOfWeek.Friday => "Fri",
                DayOfWeek.Saturday => "Sat",
                DayOfWeek.Sunday => "Sun",
                _ => ""
            };
        }

        private Color GetEventColor(string eventType)
        {
            return eventType switch
            {
                "Free" => Color.LightGreen,
                "Occupied" => Color.LightCoral,
                "Deposited" => Color.LightYellow,
                "Maintenance" => Color.LightBlue,
                _ => Color.LightGray
            };
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private void PopulateCalendar()
        {
            weekCalendar_table.SuspendLayout();

            try
            {
                var controlsToRemove = new List<Control>();
                foreach (Control control in weekCalendar_table.Controls)
                {
                    var position = weekCalendar_table.GetPositionFromControl(control);
                    if (position.Row > 0)
                    {
                        controlsToRemove.Add(control);
                    }
                }

                foreach (Control control in controlsToRemove)
                {
                    weekCalendar_table.Controls.Remove(control);
                    control.Dispose();
                }

                weekCalendar_table.RowCount = 1;
                weekCalendar_table.RowStyles.Clear();
                weekCalendar_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

                for (int i = 0; i < 7; i++)
                {
                    DateTime dayDate = currentWeekStart.AddDays(i);
                    Label? dayLabel = weekCalendar_table.GetControlFromPosition(i, 0) as Label;
                    if (dayLabel != null)
                    {
                        dayLabel.Text = $"{getDayName(dayDate.DayOfWeek)}\n{dayDate:dd/MM}";
                        dayLabel.TextAlign = ContentAlignment.MiddleCenter;
                        dayLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                }

                var allRooms = roomList.getAllRoomsDetailed();
                var sortedRooms = allRooms.Keys.OrderBy(r => r).ToList();

                if (sortedRooms.Count > 0)
                {
                    weekCalendar_table.RowCount = sortedRooms.Count + 1;

                    float rowHeight = 100f / sortedRooms.Count;
                    for (int i = 0; i < sortedRooms.Count; i++)
                    {
                        weekCalendar_table.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));
                    }

                    for (int roomIndex = 0; roomIndex < sortedRooms.Count; roomIndex++)
                    {
                        uint roomId = sortedRooms[roomIndex];

                        for (int day = 0; day < 7; day++)
                        {
                            DateTime currentDay = currentWeekStart.AddDays(day);
                            Panel eventPanel = CreateEventPanel(roomId, currentDay);
                            weekCalendar_table.Controls.Add(eventPanel, day, roomIndex + 1);
                        }
                    }
                }
                else
                {
                    weekCalendar_table.RowCount = 2;
                    weekCalendar_table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                    for (int day = 0; day < 7; day++)
                    {
                        Panel emptyPanel = CreateEmptyPanel();
                        weekCalendar_table.Controls.Add(emptyPanel, day, 1);
                    }
                }
            }
            finally
            {
                weekCalendar_table.ResumeLayout(true);
            }

            UpdateRoomList();
        }

        private Panel CreateEventPanel(uint roomId, DateTime currentDay)
        {
            Panel wrapPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(1)
            };

            try
            {
                var availabilityInfo = roomList.getRoomAvailabilityInfo(roomId, currentDay, currentDay);

                if (!availabilityInfo.IsAvailable)
                {
                    Label eventLabel = new Label
                    {
                        Text = availabilityInfo.StateText,
                        BackColor = GetEventColor(availabilityInfo.StateText),
                        ForeColor = Color.Black,
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.None,
                        AutoSize = false
                    };

                    string tooltipText = $"Room {roomId}\nState: {availabilityInfo.StateText}\nGuests: {availabilityInfo.GuestCount}\nDate: {currentDay:dd/MM/yyyy}";
                    if (!string.IsNullOrEmpty(availabilityInfo.GuestNames))
                    {
                        tooltipText += $"\nGuest Names: {availabilityInfo.GuestNames}";
                    }

                    toolTip.SetToolTip(eventLabel, tooltipText);
                    wrapPanel.Controls.Add(eventLabel);
                }
                else
                {
                    Label roomLabel = new Label
                    {
                        Text = "Free",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        ForeColor = Color.Black,
                        BackColor = GetEventColor("Free"),
                        BorderStyle = BorderStyle.None,
                        AutoSize = false
                    };

                    toolTip.SetToolTip(roomLabel, $"Room {roomId}\nState: Free\nDate: {currentDay:dd/MM/yyyy}");
                    wrapPanel.Controls.Add(roomLabel);
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = new Label
                {
                    Text = $"Room {roomId}\nError",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    ForeColor = Color.Red,
                    BackColor = Color.White
                };
                wrapPanel.Controls.Add(errorLabel);
                Console.WriteLine($"Error creating event panel for room {roomId}: {ex.Message}");
            }

            return wrapPanel;
        }

        private Panel CreateEmptyPanel()
        {
            Panel emptyPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label emptyLabel = new Label
            {
                Text = "No rooms",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            emptyPanel.Controls.Add(emptyLabel);
            return emptyPanel;
        }

        private void UpdateRoomList()
        {
            room_flowPanel.Controls.Clear();

            var allRooms = roomList.getAllRoomsDetailed();
            var sortedRooms = allRooms.Keys.OrderBy(r => r).ToList();

            foreach (uint roomId in sortedRooms)
            {
                Panel roomPanel = CreateDetailedRoomPanel(roomId);
                room_flowPanel.Controls.Add(roomPanel);
            }
        }

        private Panel CreateDetailedRoomPanel(uint roomId)
        {
            try
            {
                var room = roomList.getRoom(roomId);
                var availabilityInfo = roomList.getRoomAvailabilityInfo(roomId, currentWeekStart, currentWeekStart.AddDays(6));

                Color roomColor = GetEventColor(availabilityInfo.StateText);

                Panel roomPanel = new Panel
                {
                    Width = room_flowPanel.Width - 15,
                    Height = 120,
                    BackColor = roomColor,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    Margin = new Padding(3)
                };

                // Header with polymorphic method call
                Label headerLabel = new Label
                {
                    Text = room.getDisplayInfo(), // Polymorphic method
                    Location = new Point(5, 5),
                    Size = new Size(roomPanel.Width - 10, 20),
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent
                };

                // Room details using polymorphic method
                Label detailsLabel = new Label
                {
                    Text = $"💰 {room.getPrice():N0} VND/night\n🛏️ {room.getNumBeds()} beds | 👥 {room.getCapacity()} max\n{room.getAmenitiesText()}", // Polymorphic method
                    Location = new Point(5, 25),
                    Size = new Size(roomPanel.Width - 10, 45),
                    Font = new Font("Segoe UI", 7, FontStyle.Regular),
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent
                };

                // Current occupancy using polymorphic methods
                string occupancyText = availabilityInfo.GuestCount == 0
                    ? "👤 No current guests"
                    : $"👥 {availabilityInfo.GuestCount} guest(s)\n{room.getGuestNames()}"; // Polymorphic method

                Label occupancyLabel = new Label
                {
                    Text = occupancyText,
                    Location = new Point(5, 75),
                    Size = new Size(roomPanel.Width - 10, 40),
                    Font = new Font("Segoe UI", 7, FontStyle.Regular),
                    ForeColor = Color.DarkBlue,
                    BackColor = Color.Transparent
                };

                roomPanel.Controls.Add(headerLabel);
                roomPanel.Controls.Add(detailsLabel);
                roomPanel.Controls.Add(occupancyLabel);

                // Enhanced tooltip
                string tooltipText = $"Room {roomId} Details:\n" +
                                   $"State: {availabilityInfo.StateText}\n" +
                                   $"Price: {room.getPrice():N0} VND/night\n" +
                                   $"Beds: {room.getNumBeds()} | Capacity: {room.getCapacity()}\n" +
                                   $"Balcony: {(room.getHaveBalcony() ? "Yes" : "No")}\n" +
                                   $"Kitchen: {(room.getHaveKitchen() ? "Yes" : "No")}\n" +
                                   $"Bathtub: {(room.getHaveBathtub() ? "Yes" : "No")}\n" +
                                   $"Current Guests: {availabilityInfo.GuestCount}\n" +
                                   (!string.IsNullOrEmpty(availabilityInfo.GuestNames) ? $"Guest Names: {availabilityInfo.GuestNames}" : "");

                toolTip.SetToolTip(roomPanel, tooltipText);

                return roomPanel;
            }
            catch (Exception ex)
            {
                // Return error panel if room not found
                Panel errorPanel = new Panel
                {
                    Width = room_flowPanel.Width - 15,
                    Height = 120,
                    BackColor = Color.LightGray,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label errorLabel = new Label
                {
                    Text = $"Room {roomId}\nError: {ex.Message}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Red
                };

                errorPanel.Controls.Add(errorLabel);
                return errorPanel;
            }
        }

        public void returnAdminMenu_button_Click(object sender, EventArgs e)
        {
            if (this.Parent is AdminMenu adminMenu)
            {
                adminMenu.RestoreMenu();
            }
            else
            {
                MessageBox.Show("Parent control is not an AdminMenu instance.");
            }
        }

        private void weekCalendar_table_Paint(object sender, PaintEventArgs e)
        {
        }

        private void nextWeek_button_Click(object sender, EventArgs e)
        {
            currentWeekStart = currentWeekStart.AddDays(7);
            PopulateCalendar();
        }

        private void prevWeek_button_Click(object sender, EventArgs e)
        {
            currentWeekStart = currentWeekStart.AddDays(-7);
            PopulateCalendar();
        }
    }
}