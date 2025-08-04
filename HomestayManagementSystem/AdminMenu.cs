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
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
        }
        public void RestoreMenu()
        {
            this.Controls.Clear();
            InitializeComponent();
        }

        private void updateRoomInfo_button_Click(object sender, EventArgs e)
        {
            Admin_AllRoomMenu admin_AllRoomMenu_form = new Admin_AllRoomMenu();
            admin_AllRoomMenu_form.Dock = DockStyle.Fill;
            admin_AllRoomMenu_form.BringToFront();
            admin_AllRoomMenu_form.Parent = this;
            this.Controls.Clear();
            this.Controls.Add(admin_AllRoomMenu_form);
        }

        private void showCalendar_button_Click(object sender, EventArgs e)
        {

            GeneralCalendar generalCalendar = new GeneralCalendar();
            generalCalendar.Dock = DockStyle.Fill;
            generalCalendar.BringToFront();
            generalCalendar.Parent = this;
            this.Controls.Clear();
            this.Controls.Add(generalCalendar);
        }

        private void returnMainMenu_button_Click(object sender, EventArgs e)
        {
            var mainMenu_Form = Tag as mainMenu;
            mainMenu_Form.Show();
            Close();
        }

        private void AdminMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            var mainMenu_Form = Tag as mainMenu;
            if (mainMenu_Form != null)
            {
                mainMenu_Form.Show();
            }
            else
            {
                Application.Exit(); // If mainMenu_Form is null, exit the application
            }
        }
    }
}
