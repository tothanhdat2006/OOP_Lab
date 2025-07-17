using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomestayManagementSystem
{
    public partial class GuestMenu : Form
    {
        private Panel createRoomPreviewInfo(Room data)
        {
            // "Room 101, 0"
            string roomName = data.getRoomNumber();
            uint roomStatus = data.getRoomStatus(); // Assuming 0 for available, 1 for occupied, etc.
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
                Text = "Phòng " + roomName + "\nTình trạng: " + (roomStatus == 0 ? "Trống" : "Đang ở"),
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
                Text = "Tình trạng: " + (roomStatus == 0 ? "Trống" : "Đang ở"),
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
                Text = "Giá: " + data.getPrice() + " VNĐ/đêm",
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
            string roomName = data.getRoomNumber();
            uint roomStatus = data.getRoomStatus(); // Assuming 0 for available, 1 for occupied, etc.
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
                Text = "Phòng " + roomName + "\nTình trạng: " + (roomStatus == 0 ? "Trống" : "Đang ở"),
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
                // Clear previous preview panels
                preview_flowLayoutPanel.Controls.Clear();
                // Create and add the preview panel for the clicked room
                Panel previewPanel = createRoomPreviewInfo(data);
                preview_flowLayoutPanel.Controls.Add(previewPanel);
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
            panel.Controls.Add(roomName_label);
            panel.Controls.Add(moreInfo_button);
            return panel;
        }

        public GuestMenu()
        {
            InitializeComponent(); 
            List<Room> dummy_data = new List<Room>
            {
                new Room(1, "101", 0, 0, 2, true, 4, 100),
                new Room(2, "102", 1, 1, 1, false, 2, 80),
                new Room(3, "103", 0, 0, 3, true, 5, 120),
                new Room(4, "104", 1, 1, 2, false, 4, 90)
            };

            for (int i = 0; i < dummy_data.Count; i++)
            {
                Panel panel = createRoomPanel(dummy_data[i]);
                bookRoom_flowLayoutPanel.Controls.Add(panel);
            }

        }

        private void returnMainMenu_button_Click(object sender, EventArgs e)
        {
            var mainMenu_Form = (mainMenu)Tag;
            mainMenu_Form.Show();
            Close();
        }
    }
}
