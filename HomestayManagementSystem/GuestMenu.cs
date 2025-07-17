namespace HomestayManagementSystem
{
	public partial class GuestMenu : Form
	{
		private string _username;

		public GuestMenu(string username)
		{
			InitializeComponent();
			this.AutoScaleMode = AutoScaleMode.Dpi;
			this.Load += GuestMenu_Load;
			_username = username;
			tableLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
			tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
			tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
			SetupPanel2TestDragDrop();
			SetupRoomSelectionPanelDocking();
		}

		private void SetupPanel2TestDragDrop()
		{
			// Enable drag and drop
			panel2_test.AllowDrop = true;

			// Subscribe to ControlAdded event to handle when controls are added
			panel2_test.ControlAdded += Panel2Test_ControlAdded;

			// Subscribe to drag events
			panel2_test.DragEnter += Panel2Test_DragEnter;
			panel2_test.DragDrop += Panel2Test_DragDrop;
		}

		private void Panel2Test_DragEnter(object sender, DragEventArgs e)
		{
			// Allow drag and drop of controls
			if (e.Data.GetDataPresent(typeof(Label)) || e.Data.GetDataPresent(typeof(LinkLabel)))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void Panel2Test_DragDrop(object sender, DragEventArgs e)
		{

		}

		private void Panel2Test_ControlAdded(object sender, ControlEventArgs e)
		{
			// Check if the added control is a Label or LinkLabel
			if (e.Control is Label || e.Control is LinkLabel)
			{
				AlignControlsInPanel2Test();
			}
		}

		private void AlignControlsInPanel2Test() //draw outline
		{
			// Get all Label and LinkLabel controls in panel2_test
			var labelsAndLinks = panel2_test.Controls.OfType<Control>()
				.Where(c => c is Label || c is LinkLabel)
				.OrderBy(c => c.Top) // Sort by current top position
				.ToList();

			int currentTop = 10; // Starting top margin
			int spacing = 5; // Space between controls

			foreach (Control control in labelsAndLinks)
			{
				// Set the position to top-middle alignment
				control.Location = new Point(
					(panel2_test.Width - control.Width) / 2, // Center horizontally
					currentTop
				);

				// Update top position for next control
				currentTop += control.Height + spacing;
			}
		}

		// Handle panel resize to maintain center alignment
		private void Panel2Test_Resize(object sender, EventArgs e)
		{
			AlignControlsInPanel2Test();
		}

		private void GuestMenu_Load(object sender, EventArgs e)
		{
			//this.WindowState = FormWindowState.Maximized;
			panel2_test.Resize += Panel2Test_Resize;

			// Align controls in panel2_test at startup
			AlignControlsInPanel2Test();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			//scale font theo chiều rộng form
			float scale = this.Width / 800f;
			//greetinđg_label.Font = new Font(greeting_label.Font.FontFamily, 16 * scale, greeting_label.Font.Style);
		}

		private void greeting_label_Click(object sender, EventArgs e)
		{

		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{
			var panel = tableLayoutPanel1;
			var g = e.Graphics;
			var pen = Pens.Gray; // You can use a custom Pen for color/thickness

			// Draw vertical lines
			for (int i = 1; i < panel.ColumnCount; i++)
			{
				int x = panel.GetColumnWidths().Take(i).Sum();
				g.DrawLine(pen, x, 0, x, panel.Height);
			}

			// Draw horizontal lines
			for (int i = 1; i < panel.RowCount; i++)
			{
				int y = panel.GetRowHeights().Take(i).Sum();
				g.DrawLine(pen, 0, y, panel.Width, y);
			}
		}

		private void bookingToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void room_selection_panel_Paint(object sender, PaintEventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint_2(object sender, PaintEventArgs e)
		{

		}

		private void room_selection_panel_Resize(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void room101_label_Click(object sender, EventArgs e)
		{

		}

		private void SetupRoomSelectionPanelDocking()
		{
			room_selection_panel.ControlAdded += RoomSelectionPanel_ControlAdded;
		}

		private void RoomSelectionPanel_ControlAdded(object sender, ControlEventArgs e)
		{
			if (e.Control is Panel panel)
			{
				panel.Dock = DockStyle.Top;
				panel.BringToFront();
			}
		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void label6_Click(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
		{
			var panel = tableLayoutPanel2;
			var g = e.Graphics;
			var pen = Pens.Gray;

			// Draw vertical lines
			for (int i = 1; i < panel.ColumnCount; i++)
			{
				int x = panel.GetColumnWidths().Take(i).Sum();
				g.DrawLine(pen, x, 0, x, panel.Height);
			}

			// Draw horizontal lines
			for (int i = 1; i < panel.RowCount; i++)
			{
				int y = panel.GetRowHeights().Take(i).Sum();
				g.DrawLine(pen, 0, y, panel.Width, y);
			}
		}
	}
}