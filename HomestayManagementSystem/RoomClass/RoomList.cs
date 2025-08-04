public class RoomList : IRoomList
{
	// Singleton
	private static readonly RoomList _instance = new RoomList();
	private RoomList() { }
	public static RoomList Instance
	{
		get
		{
			return _instance;
		}
	}

	protected uint totalRoom;
	protected ulong basePrice;
	protected Dictionary<uint, Room> rooms = new Dictionary<uint, Room>();

	public Room getRoom(uint id)
	{
		if (rooms.ContainsKey(id))
			return rooms[id];
		else
			throw new ArgumentException("Room ID not found.");
	}

	public int getTotalRoom()
	{
		return rooms.Count;
	}

	public void addRoom(uint id, uint option)
	{
		if (rooms.ContainsKey(id))
			throw new ArgumentException("Room ID already exists.");

		// Use builder pattern to create new room
		ConcreteBuilder builder = new ConcreteBuilder();
		RoomDirector director = new RoomDirector(builder);

		// Choose construction type based on option parameter
		switch (option)
		{
			case 1:
				director.ConstructPersonalRoom(); // Small bedroom, no bathtub, 200,000
				break;
			case 2:
				director.ConstructCoupleRoom(); // Medium bedroom, kitchen, bathtub, 500,000
				break;
			case 3:
				director.ConstructQuadroRoom(); // Large bedroom, kitchen, balcony, no bathtub, 800,000
				break;
			default:
				director.customConstruct(1, 1, 1, 1, 300000); // Custom room with all features
				break;
		}

		Room newRoom = builder.getResult();
		newRoom.setID(id.ToString()); // Set the ID after creation

		rooms[id] = newRoom;
	}

	public void deleteRoom(uint id)
	{
		if (!rooms.ContainsKey(id))
			throw new ArgumentException("Room ID not found.");

		rooms.Remove(id);
	}

	public void editRoomBasePrice(uint id, ulong price)
	{
		if (!rooms.ContainsKey(id))
			throw new ArgumentException("Room ID not found.");

		rooms[id].setPrice(price);
	}
}