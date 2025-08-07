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

namespace HomestayManagementSystem
{
    public partial class Admin_AllRoomMenu : UserControl
    {
        private RoomList roomList; // Use RoomList instead of List<Room>
        private List<uint> displayedRoomIds = new List<uint>(); // Track displayed rooms

        private string getStateText(uint state)
        {
            return state switch
            {
                0 => "Free",
                1 => "Occupied",
                2 => "Deposited",
                3 => "Maintenance",
                _ => "Unknown status"
            };
        }

        private Color getStateColor(uint status)
        {
            return status switch
            {
                0 => Color.LightGreen,
                1 => Color.LightCoral,
                2 => Color.LightYellow,
                3 => Color.LightBlue,
                _ => Color.LightGray
            };
        }

        private void LoadRoomsFromJsonToRoomList(string filePath)
        {
            try
            {
                // Load rooms from JSON using existing Room class method
                var rooms = Room.LoadRoomsFromJson(filePath);

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

                            // Room exists, update its data (excluding curGuest)
                            var existingRoom = roomList.getRoom(roomId);
                            existingRoom.setState(room.getState());
                            existingRoom.setPrice(room.getPrice());
                            existingRoom.setNumBeds(room.getNumBeds());
                            existingRoom.setHaveBalcony(room.getHaveBalcony());
                            existingRoom.setHaveKitchen(room.getHaveKitchen());
                            existingRoom.setHaveBathtub(room.getHaveBathtub());
                            existingRoom.setCapacity(room.getCapacity());
                            // Remove setCurGuest since guest info is now in events
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

                        displayedRoomIds.Add(roomId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing room {room.getID()}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room data from JSON: {ex.Message}");
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

        private void ShowRoomDetailInfo(Room roomData)
        {
            try
            {
                // Use the Room object directly with enhanced OOP methods
                Form roomInfoForm = new Form
                {
                    Text = $"More info - {roomData.getDisplayInfo()}", // Use polymorphic method
                    Width = 450,
                    Height = 600, // Increased height for event information
                    StartPosition = FormStartPosition.CenterParent
                };

                string roomStatusText = getStateText(roomData.getState());

                // Create labels with enhanced information
                Label roomIdLabel = new Label { Text = $"Room ID: {roomData.getID()}", Left = 20, Top = 20, AutoSize = true };
                Label roomDisplayLabel = new Label { Text = $"Display: {roomData.getDisplayInfo()}", Left = 20, Top = 50, AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold) };
                Label roomStatusLabel = new Label { Text = $"Status: {roomStatusText}", Left = 20, Top = 80, AutoSize = true };
                Label roomPriceLabel = new Label { Text = $"Price: {roomData.getPrice():N0} VND/night", Left = 20, Top = 110, AutoSize = true };
                Label roomBedsLabel = new Label { Text = $"Number of beds: {roomData.getNumBeds()}", Left = 20, Top = 140, AutoSize = true };
                Label maxPersonsLabel = new Label { Text = $"Max capacity: {roomData.getMaxPersons()} people", Left = 20, Top = 170, AutoSize = true };
                Label amenitiesLabel = new Label { Text = $"Amenities: {roomData.getAmenitiesText()}", Left = 20, Top = 200, AutoSize = true, Width = 400 };

                // Get guest information from events instead of room curGuest
                var roomAvailability = roomList.getRoomAvailabilityInfo(uint.Parse(roomData.getID()), DateTime.Today, DateTime.Today);

                Label guestCountLabel = new Label { Text = $"Current guests: {roomAvailability.GuestCount}", Left = 20, Top = 230, AutoSize = true };

                // Guest names with proper handling from events
                Label guestNamesLabel = new Label
                {
                    Text = $"Guest names: {(string.IsNullOrEmpty(roomAvailability.GuestNames) ? "None" : roomAvailability.GuestNames)}",
                    Left = 20,
                    Top = 260,
                    AutoSize = true,
                    Width = 400
                };

                // Room type information
                Label roomTypeLabel = new Label { Text = $"Room type: {roomData.getRoomType()}", Left = 20, Top = 290, AutoSize = true };

                // Current event information - Fixed to use the room's actual state instead of availability info
                Label eventInfoLabel = new Label
                {
                    Text = $"Booking status: {roomStatusText}",  // Use the same status text as the main display
                    Left = 20,
                    Top = 320,
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = roomData.getState() == 0 ? Color.Green : Color.Red  // Use room's actual state
                };

                // Create buttons
                Button closeButton = new Button { Text = "Close", Left = 50, Top = 480, Width = 100, Height = 50 };
                Button editButton = new Button { Text = "Edit Room", Left = 175, Top = 480, Width = 100, Height = 50 };
                Button deleteButton = new Button { Text = "Delete Room", Left = 300, Top = 480, Width = 100, Height = 50, BackColor = Color.LightCoral };

                // Event handlers
                closeButton.Click += (closeSender, closeArgs) => roomInfoForm.Close();

                editButton.Click += (editSender, editArgs) =>
                {
                    // TODO: Implement room editing functionality
                    MessageBox.Show("Room editing functionality will be implemented here.", "Feature Coming Soon");
                };

                deleteButton.Click += (deleteSender, deleteArgs) =>
                {
                    var result = MessageBox.Show($"Are you sure you want to delete {roomData.getDisplayInfo()}?",
                                               "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            uint roomId = uint.Parse(roomData.getID());
                            roomList.deleteRoom(roomId);
                            roomInfoForm.Close();
                            RefreshRoomDisplay();
                            MessageBox.Show("Room deleted successfully!", "Success");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting room: {ex.Message}", "Error");
                        }
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
                roomInfoForm.Controls.Add(guestCountLabel);
                roomInfoForm.Controls.Add(guestNamesLabel);
                roomInfoForm.Controls.Add(roomTypeLabel);
                roomInfoForm.Controls.Add(eventInfoLabel);
                roomInfoForm.Controls.Add(closeButton);
                roomInfoForm.Controls.Add(editButton);
                roomInfoForm.Controls.Add(deleteButton);

                roomInfoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room information: {ex.Message}");
            }
        }
        private Panel createRoomPanel(Room data)
        {
            string roomId = data.getID();
            uint roomStatus = data.getState();

            Panel panel = new Panel
            {
                Width = bookRoom_flowLayoutPanel.Width - 20,
                Height = 120, // Increased height for more info
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            // Header using polymorphic method
            Label roomName_label = new Label
            {
                Text = data.getDisplayInfo(), // Use polymorphic method
                BackColor = getStateColor(roomStatus),
                ForeColor = Color.Black,
                AutoSize = false,
                Height = 45,
                Dock = DockStyle.Top,
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(3)
            };

            // Amenities using polymorphic method
            Label amenitiesLabel = new Label
            {
                Text = data.getAmenitiesText(), // Use polymorphic method
                AutoSize = false,
                Height = 25,
                Dock = DockStyle.Top,
                Font = new Font("Arial", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.DarkBlue
            };

            // Guest info from availability info (events)
            var roomAvailability = roomList.getRoomAvailabilityInfo(uint.Parse(roomId), DateTime.Today, DateTime.Today);
            Label guestLabel = new Label
            {
                Text = $"Guests: {roomAvailability.GuestCount} | {roomAvailability.GuestNames}",
                AutoSize = false,
                Height = 20,
                Dock = DockStyle.Top,
                Font = new Font("Arial", 7, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.DarkGreen
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
                ShowRoomDetailInfo(data);
            };

            panel.Controls.Add(roomName_label);
            panel.Controls.Add(amenitiesLabel);
            panel.Controls.Add(guestLabel);
            panel.Controls.Add(moreInfo_button);
            return panel;
        }

        public Admin_AllRoomMenu()
        {
            InitializeComponent();
            roomList = RoomList.Instance; // Use singleton
            displayedRoomIds.Clear();

            string jsonFilePath = @"./Data/Rooms/ROOMDATA.json";
           
            // Try JSON first, then fall back to dummy data
            if (System.IO.File.Exists(jsonFilePath))
            {
                LoadRoomsFromJsonToRoomList(jsonFilePath);
            }
            else
            {
                MessageBox.Show("No ROOMDATA.json found. Using fallback data.");
                CreateFallbackData();
            }

            LoadRoomPanels();
        }

        private void CreateFallbackData()
        {
            try
            {
                // Create fallback rooms using RoomList
                roomList.addRoom(101, 2, RoomType.Standard);
                roomList.addRoom(102, 2, RoomType.Standard);
                roomList.addRoom(201, 3, RoomType.Luxury);

                var room1 = roomList.getRoom(101);
                room1.setState(0);
                room1.setPrice(500000);

                var room2 = roomList.getRoom(102);
                room2.setState(1);
                room2.setPrice(500000);

                var room3 = roomList.getRoom(201);
                room3.setState(2);
                room3.setPrice(800000);

                displayedRoomIds.AddRange(new uint[] { 101, 102, 201 });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating fallback data: {ex.Message}");
            }
        }

        private void LoadRoomPanels()
        {
            bookRoom_flowLayoutPanel.Controls.Clear();
            
            foreach (uint roomId in displayedRoomIds)
            {
                try
                {
                    Room room = roomList.getRoom(roomId);
                    Panel panel = createRoomPanel(room);
                    bookRoom_flowLayoutPanel.Controls.Add(panel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading panel for room {roomId}: {ex.Message}");
                }
            }
        }

        private void RefreshRoomDisplay()
        {
            displayedRoomIds.Clear();
            var allRooms = roomList.getAllRoomsDetailed();
            displayedRoomIds.AddRange(allRooms.Keys.OrderBy(k => k));
            LoadRoomPanels();
        }

        private void returnAdminMenu_button_Click(object sender, EventArgs e)
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

        private void filterCheckedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool[] includeStatus = { false, false, false }; // [available, occupied, deposited]

            // Determine which statuses to include based on checked items
            for (int i = 0; i < filterCheckedList.Items.Count; i++)
            {
                if (filterCheckedList.GetItemChecked(i))
                {
                    switch (i)
                    {
                        case 0: includeStatus[0] = true; break; // Show available rooms
                        case 1: includeStatus[1] = true; break; // Show occupied rooms
                        case 2: includeStatus[2] = true; break; // Show deposited rooms
                    }
                }
            }

            // Filter rooms based on selection using RoomList
            var filteredRoomIds = new List<uint>();

            if (!includeStatus[0] && !includeStatus[1] && !includeStatus[2])
            {
                // Show all rooms if no filters applied
                RefreshRoomDisplay();
                return;
            }

            if (includeStatus[0])
            {
                var freeRooms = roomList.getRoomsByState(0);
                filteredRoomIds.AddRange(freeRooms.Select(r => uint.Parse(r.getID())));
            }

            if (includeStatus[1])
            {
                var occupiedRooms = roomList.getRoomsByState(1);
                filteredRoomIds.AddRange(occupiedRooms.Select(r => uint.Parse(r.getID())));
            }

            if (includeStatus[2])
            {
                var depositedRooms = roomList.getRoomsByState(2);
                filteredRoomIds.AddRange(depositedRooms.Select(r => uint.Parse(r.getID())));
            }

            // Update displayed rooms
            displayedRoomIds = filteredRoomIds.Distinct().OrderBy(id => id).ToList();
            LoadRoomPanels();
        }
    }
}