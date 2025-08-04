using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using Calendar;

namespace HomestayManagementSystem
{
    public partial class GeneralCalendar : UserControl
    {
        private Calendar.Calendar calendar;
        private List<Calendar.Event> events = new List<Calendar.Event>();
        private DateTime currentWeekStart;
        private Dictionary<int, int> roomStates = new Dictionary<int, int>();

        public GeneralCalendar()
        {
            InitializeComponent();
            calendar = Calendar.Calendar.GetInstance();
            currentWeekStart = GetStartOfWeek(DateTime.Now);
            LoadRoomStates();
            LoadEventsFromJson();
            PopulateCalendar();
        }

        private void LoadRoomStates()
        {
            try
            {
                string roomDataPath = @"./Data/Rooms/ROOMDATA.json";

                if (!File.Exists(roomDataPath))
                {
                    MessageBox.Show($"Room data file not found: {roomDataPath}");
                    return;
                }

                string jsonString = File.ReadAllText(roomDataPath);
                using JsonDocument document = JsonDocument.Parse(jsonString);
                JsonElement root = document.RootElement;

                if (root.TryGetProperty("rooms", out JsonElement roomsArray))
                {
                    foreach (JsonElement roomElement in roomsArray.EnumerateArray())
                    {
                        int roomId = roomElement.GetProperty("ID").GetInt32();
                        int state = roomElement.GetProperty("state").GetInt32();
                        roomStates[roomId] = state;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room states: {ex.Message}");
            }
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
                        int roomId = eventElement.GetProperty("roomId").GetInt32();

                        var startDateElement = eventElement.GetProperty("startDate");
                        int startDay = startDateElement.GetProperty("day").GetInt32();
                        int startMonth = startDateElement.GetProperty("month").GetInt32();
                        int startYear = startDateElement.GetProperty("year").GetInt32();
                        Calendar.Date startDate = new Calendar.Date(startDay, startMonth, startYear);

                        var endDateElement = eventElement.GetProperty("endDate");
                        int endDay = endDateElement.GetProperty("day").GetInt32();
                        int endMonth = endDateElement.GetProperty("month").GetInt32();
                        int endYear = endDateElement.GetProperty("year").GetInt32();
                        Calendar.Date endDate = new Calendar.Date(endDay, endMonth, endYear);

                        // Get the room state and convert to type
                        int roomState = roomStates.ContainsKey(roomId) ? roomStates[roomId] : 0;
                        string type = GetEventTypeFromState(roomState);

                        Calendar.Event eventObj = new Calendar.Event(type, roomId, startDate, endDate);
                        events.Add(eventObj);
                        calendar.AddEvent(eventObj);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}");
            }

            return events;
        }

        private string GetEventTypeFromState(int state)
        {
            return state switch
            {
                0 => "Free",
                1 => "Occupied",
                2 => "Deposited",
                3 => "Maintanance",
                _ => "Unknown status"
            };
        }

        private string GetVietnameseDayName(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Monday => "Monday",
                DayOfWeek.Tuesday => "Tuesday",
                DayOfWeek.Wednesday => "Wednesday",
                DayOfWeek.Thursday => "Thursday",
                DayOfWeek.Friday => "Friday",
                DayOfWeek.Saturday => "Saturday",
                DayOfWeek.Sunday => "Sunday",
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
                "Maintanance" => Color.LightBlue,
                _ => Color.LightGray
            };
        }
        private bool IsEventOnDate(Calendar.Event evt, DateTime date)
        {
            DateTime startDate = new DateTime(evt.startDate.year, evt.startDate.month, evt.startDate.day);
            DateTime endDate = new DateTime(evt.endDate.year, evt.endDate.month, evt.endDate.day);

            return date.Date >= startDate.Date && date.Date <= endDate.Date;
        }
        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private List<Calendar.Event> GetEventsForWeek(DateTime weekStart)
        {
            DateTime weekEnd = weekStart.AddDays(6);

            return events.Where(e =>
            {
                DateTime eventStart = new DateTime(e.startDate.year, e.startDate.month, e.startDate.day);
                DateTime eventEnd = new DateTime(e.endDate.year, e.endDate.month, e.endDate.day);

                return (eventStart <= weekEnd && eventEnd >= weekStart);
            }).ToList();
        }

        private void PopulateCalendar()
        {
            // Clear existing rows except header
            while(weekCalendar_table.RowCount > 1)
            {
                weekCalendar_table.RowCount--;
            }

            // Clear all controls except header row
            for(int i = weekCalendar_table.Controls.Count - 1; i >= 0; i--)
            {
                Control control = weekCalendar_table.Controls[i];
                var position = weekCalendar_table.GetPositionFromControl(control);
                if(position.Row > 0) // Keep header row (row 0)
                {
                    weekCalendar_table.Controls.Remove(control);
                }
            }

            // Update day labels with current week dates
            for(int i = 0; i < 7; i++)
            {
                DateTime dayDate = currentWeekStart.AddDays(i);
                Label? dayLabel = weekCalendar_table.GetControlFromPosition(i, 0) as Label;
                if(dayLabel != null)
                {
                    dayLabel.Text = $"{GetVietnameseDayName(dayDate.DayOfWeek)}\n{dayDate:dd/MM}";
                    dayLabel.TextAlign = ContentAlignment.MiddleCenter;
                    dayLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }

            var weekEvents = GetEventsForWeek(currentWeekStart);

            // Group events by room
            var eventsByRoom = weekEvents.GroupBy(e => e.roomId).ToList();

            if (eventsByRoom.Any())
            {
                weekCalendar_table.RowCount = eventsByRoom.Count + 1;
                weekCalendar_table.RowStyles.Clear();

                // Header row - fixed height
                // Event rows - equal distribution
                weekCalendar_table.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
                float rowHeight = 100f / eventsByRoom.Count; // 100 is fixed
                for (int i = 0; i < eventsByRoom.Count; i++)
                {
                    weekCalendar_table.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));
                }

                // Populate event cells
                for (int roomIndex = 0; roomIndex < eventsByRoom.Count; roomIndex++)
                {
                    var roomEvents = eventsByRoom[roomIndex];

                    for (int day = 0; day < 7; day++)
                    {
                        DateTime currentDay = currentWeekStart.AddDays(day);
                        var dayEvents = roomEvents.Where(e => IsEventOnDate(e, currentDay)).ToList();

                        Panel eventPanel = CreateEventPanel(dayEvents, roomEvents.Key);
                        weekCalendar_table.Controls.Add(eventPanel, day, roomIndex + 1);
                    }
                }
            }

            // Update room list
            UpdateRoomList();
        }

        private Panel CreateEventPanel(List<Calendar.Event> dayEvents, int roomId)
        {
            Panel wrapPanel = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(1)
            };

            if (dayEvents.Any())
            {
                Panel panel = new Panel
                {
                    Dock = DockStyle.None,
                    AutoScroll = false,
                    Size = new Size(wrapPanel.Width - 10, wrapPanel.Height - 10),
                    Location = new Point(2, 2),
                    Margin = new Padding(5),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
                };

                foreach (var evt in dayEvents)
                {
                    Label eventLabel = new Label
                    {
                        Text = $"{evt.type}\nRoom {evt.roomId}",
                        BackColor = GetEventColor(evt.type),
                        ForeColor = Color.Black,
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(panel.Width / 2 + 20, panel.Height + 20),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        AutoSize = false
                    };

                    panel.Controls.Add(eventLabel);
                }

                wrapPanel.Controls.Add(panel);
            }
            else
            {
                // Show room number for empty cells
                Label roomLabel = new Label
                {
                    Text = $"Room {roomId}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    ForeColor = Color.Gray,
                    Padding = new Padding(5)
                };
                wrapPanel.Controls.Add(roomLabel);
            }

            return wrapPanel;
        }

        private void UpdateRoomList()
        {
            room_flowPanel.Controls.Clear();

            var roomsWithEvents = events.Select(e => e.roomId).Distinct().OrderBy(r => r);

            foreach (int roomId in roomsWithEvents)
            {
                var roomEvent = events.FirstOrDefault(e => e.roomId == roomId);
                Color roomColor = roomEvent != null ? GetEventColor(roomEvent.type) : Color.LightGray;

                Label roomLabel = new Label
                {
                    Text = $"Room {roomId}",
                    Width = room_flowPanel.Width - 10,
                    Height = 30,
                    BackColor = roomColor,
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    Margin = new Padding(2)
                };

                room_flowPanel.Controls.Add(roomLabel);
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
            // Paint event handled by PopulateCalendar method
        }
    }
}