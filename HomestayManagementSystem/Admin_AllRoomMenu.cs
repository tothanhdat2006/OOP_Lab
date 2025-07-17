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
    public partial class Admin_AllRoomMenu : UserControl
    {
        private Panel createRoomPanel(Room data)
        {
            // "Room 101, 0"
            string roomName = data.getRoomNumber();
            uint roomStatus = data.getRoomStatus(); // Assuming 0 for available, 1 for occupied, etc.
            Panel panel = new Panel
            {
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
        public Admin_AllRoomMenu()
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

    }
}
