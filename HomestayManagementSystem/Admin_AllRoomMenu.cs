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
        private List<Room> roomData = new List<Room>();

        private string GetStatusText(uint state)
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

        private Color GetStatusColor(uint status)
        {
            return status switch
            {
                0 => Color.LightGreen,
                1 => Color.LightCoral,
                2 => Color.LightYellow,
                _ => Color.LightGray
            };
        }

        private List<Room> LoadRoomsFromJson(string filePath)
        {
            var rooms = new List<Room>();
            try
            {
                string jsonString = File.ReadAllText(filePath);

                using JsonDocument document = JsonDocument.Parse(jsonString);
                JsonElement root = document.RootElement;

                if (root.TryGetProperty("rooms", out JsonElement roomsArray))
                {
                    foreach (JsonElement roomElement in roomsArray.EnumerateArray())
                    {
                        Room room = new Room();

                        // Parse room properties
                        if (roomElement.TryGetProperty("ID", out JsonElement idElement) &&
                            idElement.TryGetUInt32(out uint id))
                            room.setID(id);

                        if (roomElement.TryGetProperty("numBeds", out JsonElement numBedsElement) &&
                            numBedsElement.TryGetUInt32(out uint numBeds))
                            room.setNumBeds(numBeds);

                        if (roomElement.TryGetProperty("haveBalcony", out JsonElement haveBalconyElement))
                            room.setHaveBalcony(haveBalconyElement.GetBoolean());

                        if (roomElement.TryGetProperty("haveKitchen", out JsonElement haveKitchenElement))
                            room.setHaveKitchen(haveKitchenElement.GetBoolean());

                        if (roomElement.TryGetProperty("haveBathtub", out JsonElement haveBathtubElement))
                            room.setHaveBathtub(haveBathtubElement.GetBoolean());

                        if (roomElement.TryGetProperty("capacity", out JsonElement capacityElement) &&
                            capacityElement.TryGetUInt32(out uint capacity))
                            room.setCapacity(capacity);

                        if (roomElement.TryGetProperty("price", out JsonElement priceElement) &&
                            priceElement.TryGetUInt64(out ulong price))
                            room.setPrice(price);

                        if (roomElement.TryGetProperty("state", out JsonElement stateElement) &&
                            stateElement.TryGetUInt32(out uint state))
                            room.setState(state);

                        // Parse curGuest array
                        if (roomElement.TryGetProperty("curGuest", out JsonElement guestsArray))
                        {
                            var guestsList = new List<Person>();

                            foreach (JsonElement guestElement in guestsArray.EnumerateArray())
                            {
                                Person guest = new Person();

                                if (guestElement.TryGetProperty("name", out JsonElement nameElement))
                                    guest.name = nameElement.GetString() ?? "";

                                if (guestElement.TryGetProperty("age", out JsonElement ageElement) &&
                                    ageElement.TryGetUInt32(out uint age))
                                    guest.age = age;

                                if (guestElement.TryGetProperty("sex", out JsonElement sexElement))
                                    guest.sex = sexElement.GetBoolean();

                                if (guestElement.TryGetProperty("mail", out JsonElement mailElement))
                                    guest.mail = mailElement.GetString() ?? "";

                                if (guestElement.TryGetProperty("CCCD", out JsonElement cccdElement))
                                    guest.CCCD = cccdElement.GetString() ?? "";

                                if (guestElement.TryGetProperty("phoneNumber", out JsonElement phoneElement))
                                    guest.phoneNumber = phoneElement.GetString() ?? "";

                                if (guestElement.TryGetProperty("address", out JsonElement addressElement))
                                    guest.address = addressElement.GetString() ?? "";

                                guestsList.Add(guest);
                            }

                            room.setCurGuest(guestsList.ToArray());
                        }
                        else
                        {
                            room.setCurGuest(Array.Empty<Person>());
                        }

                        rooms.Add(room);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room data from JSON: {ex.Message}");
            }

            return rooms;
        }

        private Panel createRoomPanel(Room data)
        {
            // Fixed method calls to match actual Room class
            string roomId = data.getID().ToString();
            uint roomStatus = data.getState(); // Using getState() instead of getRoomStatus()

            Panel panel = new Panel
            {
                Width = bookRoom_flowLayoutPanel.Width - 20,
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            Label roomName_label = new Label
            {
                Text = "Room " + roomId + "\nStatus: " + GetStatusText(roomStatus),
                BackColor = GetStatusColor(roomStatus),
                ForeColor = Color.Black,
                AutoSize = false,
                Height = 65,
                Dock = DockStyle.Top,
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(3)
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

            panel.Controls.Add(roomName_label);
            panel.Controls.Add(moreInfo_button);
            return panel;
        }

        public Admin_AllRoomMenu()
        {
            InitializeComponent();

            string jsonFilePath = @"./Data/Rooms/ROOMDATA.json";
           
            // Try JSON first, then fall back to dummy data
            if (System.IO.File.Exists(jsonFilePath))
            {
                roomData = LoadRoomsFromJson(jsonFilePath);
            }
            else
            {
                MessageBox.Show("No ROOMDATA.json found. Using fallback data.");
                roomData = CreateFallbackData();
            }

            LoadRoomPanels(roomData);
        }

        private List<Room> CreateFallbackData()
        {
            var fallbackRooms = new List<Room>();

            // Create rooms with proper constructor calls
            var room1 = new Room();
            room1.setID(101);
            room1.setNumBeds(2);
            room1.setHaveBalcony(true);
            room1.setHaveKitchen(false);
            room1.setHaveBathtub(true);
            room1.setCapacity(4);
            room1.setPrice(500000);
            room1.setState(0);
            room1.setCurGuest(Array.Empty<Person>());

            var room2 = new Room();
            room2.setID(102);
            room2.setNumBeds(2);
            room2.setHaveBalcony(true);
            room2.setHaveKitchen(false);
            room2.setHaveBathtub(true);
            room2.setCapacity(4);
            room2.setPrice(500000);
            room2.setState(1);
            room2.setCurGuest(Array.Empty<Person>());

            var room3 = new Room();
            room3.setID(201);
            room3.setNumBeds(2);
            room3.setHaveBalcony(true);
            room3.setHaveKitchen(false);
            room3.setHaveBathtub(true);
            room3.setCapacity(4);
            room3.setPrice(500000);
            room3.setState(2);
            room3.setCurGuest(Array.Empty<Person>());

            fallbackRooms.AddRange(new[] { room1, room2, room3 });
            return fallbackRooms;
        }

        private void LoadRoomPanels(List<Room> rooms)
        {
            bookRoom_flowLayoutPanel.Controls.Clear();
            foreach (Room room in rooms)
            {
                Panel panel = createRoomPanel(room);
                bookRoom_flowLayoutPanel.Controls.Add(panel);
            }
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

            // Determine which statuses to exclude based on checked items
            for (int i = 0; i < filterCheckedList.Items.Count; i++)
            {
                if (filterCheckedList.GetItemChecked(i))
                {
                    switch (i)
                    {
                        case 0: includeStatus[0] = true; break; // Hide available rooms
                        case 1: includeStatus[1] = true; break; // Hide occupied rooms
                        case 2: includeStatus[2] = true; break; // Hide deposited rooms
                    }
                }
            }

            // Filter rooms based on selection
            List<Room> filteredRooms = new List<Room>();

            if (!includeStatus[0] && !includeStatus[1] && !includeStatus[2])
            {
                // Show all rooms if no filters applied
                filteredRooms = roomData;
                LoadRoomPanels(filteredRooms);
                return;
            }

            if( includeStatus[0] )
            {
                filteredRooms.AddRange(roomData.Where(r => r.getState() == 0));
            }

            if(includeStatus[1])
            {
                filteredRooms.AddRange(roomData.Where(r => r.getState() == 1));
            }

            if(includeStatus[2])
            {
                filteredRooms.AddRange(roomData.Where(r => r.getState() == 2));
            }   

            LoadRoomPanels(filteredRooms);
        }
    }
}