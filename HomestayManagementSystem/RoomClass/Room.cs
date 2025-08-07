using System.Text.Json;

public struct Person // Changed accessibility from 'internal' to 'public'  
{
	public string name;
	public uint age;
	public bool sex;
	public string mail;
	public string CCCD;
	public string phoneNumber;
	public string address;
}

public class Room : IRoom
{
	protected string ID;
	protected uint numBeds;
	protected bool haveBalcony;
	protected bool haveKitchen;
	protected bool haveBathtub;
	protected uint capacity;
	protected ulong price;
	protected uint state;
	protected Person[] curGuest;
	protected string roomNumber;
	protected RoomType roomType = RoomType.Standard; // New field

	public Room()
	{
		ID = "0";
		numBeds = 0;
		haveBalcony = false;
		haveKitchen = false;
		haveBathtub = false;
		capacity = 0;
		price = 0;
		state = 0;
		curGuest = Array.Empty<Person>();
		roomType = RoomType.Standard;
	}

	public Room(string ID, uint roomStatus, uint numBeds, bool haveBalcony, bool haveKitchen, bool haveBathtub, uint maxPersons, ulong price)
	{
		this.ID = ID;
		this.state = roomStatus;
		this.numBeds = numBeds;
		this.haveBalcony = haveBalcony;
		this.haveKitchen = haveKitchen; // Fix: use parameter instead of hardcoded false
		this.haveBathtub = haveBathtub; // Fix: use parameter instead of hardcoded false
		this.capacity = maxPersons;
		this.price = price;
		this.curGuest = Array.Empty<Person>();
		this.roomType = RoomType.Standard;
	}

	// Interface methods with correct signatures
	public virtual void setID(uint id) => this.ID = id.ToString();
    public virtual void setID(string ID) => this.ID = ID;
    public virtual void setNumBeds(uint numBeds) => this.numBeds = numBeds;
	public virtual void setHaveBalcony(bool haveBalcony) => this.haveBalcony = haveBalcony;
	public virtual void setHaveKitchen(bool haveKitchen) => this.haveKitchen = haveKitchen;
	public virtual void setHaveBathtub(bool haveBathtub) => this.haveBathtub = haveBathtub;
	public virtual void setCapacity(uint capacity) => this.capacity = capacity;
	public virtual void setPrice(ulong price) => this.price = price;
	public virtual void setState(uint state) => this.state = (uint)state;
	public virtual void setCurGuest(Person[] curGuest) => this.curGuest = curGuest;
    public virtual void setRoomNumber(string roomNumber) => this.roomNumber = roomNumber;

    public virtual string getID() => this.ID;
    public virtual string getIDString() => this.ID;
    public virtual uint getNumBeds() => this.numBeds;
	public virtual bool getHaveBalcony() => this.haveBalcony;
	public virtual bool getHaveKitchen() => this.haveKitchen;
	public virtual bool getHaveBathtub() => this.haveBathtub;
	public virtual uint getCapacity() => this.capacity;
	public virtual ulong getPrice() => this.price;
	public virtual uint getState() => (uint)this.state;
	public virtual string getRoomNumber() => this.roomNumber;
	public virtual uint getRoomStatus() => this.state;
	public virtual uint getMaxPersons() => this.capacity;
	public virtual Person[] getCurGuest() => this.curGuest;


	// New enhanced methods (Polymorphism)
	public virtual void setRoomType(RoomType type) => this.roomType = type;
	public virtual RoomType getRoomType() => this.roomType;

	public virtual string getDisplayInfo()
	{
		string typePrefix = roomType switch
		{
			RoomType.Luxury => "Luxury ",
			RoomType.Suite => "Suite ",
			_ => ""
		};
		return $"{typePrefix}Room {ID} - {getStateText(state)}";
	}

	public virtual string getAmenitiesText()
	{
		var amenities = new List<string>();
		
		if (haveBalcony) amenities.Add("🏙️ Balcony");
		if (haveKitchen) amenities.Add("🍳 Kitchen");
		if (haveBathtub) amenities.Add("🛁 Bathtub");
		
		if (roomType == RoomType.Luxury)
			amenities.Add("⭐ Premium Service");
		else if (roomType == RoomType.Suite)
			amenities.Add("👑 Royal Suite");
		
		return amenities.Count > 0 ? string.Join(" | ", amenities) : "🏠 Basic room";
	}

	public virtual string getFullDescription()
	{
		return $"{getDisplayInfo()}\n{getAmenitiesText()}";
	}

	private string getStateText(uint state)
	{
		return state switch
		{
			0 => "Free",
			1 => "Occupied",
			2 => "Deposited",
			3 => "Maintenance",
			_ => "Unknown"
		};
	}

	public string getImagePath() => $"Data/Rooms/{ID}.jpg";
	public string getInfoPath() => $"Data/Rooms/{ID}.ini";

	// Keep existing static methods for backward compatibility
	public static Room LoadFromIni(string roomNumber)
	{
		string iniPath = $"Data/Rooms/{roomNumber}.ini";

		if (!File.Exists(iniPath))
		{
			throw new FileNotFoundException($"Không tìm thấy file thông tin phòng: {iniPath}");
		}

		var parser = new IniParser.FileIniDataParser();
		var data = parser.ReadFile(iniPath);
		var roomSection = data["RoomInfo"];

		// Tạo đối tượng Room từ thông tin INI theo cách OOP
		Room room = new Room(
			ID: roomSection["RoomID"],
			roomStatus: uint.Parse(roomSection["RoomStatus"]),
			numBeds: uint.Parse(roomSection["RoomBeds"]),
			haveBalcony: bool.Parse(roomSection["HasBalcony"]),
			maxPersons: uint.Parse(roomSection["MaxPersons"]),
			price: ulong.Parse(roomSection["RoomPrice"]) / 1000,
			haveKitchen: bool.Parse(roomSection["HaveKitchen"]),
			haveBathtub: bool.Parse(roomSection["HaveBathtub"])
		);

		return room;
	}

	public static List<Room> LoadRoomsFromJson(string jsonPath)
	{
		if (!File.Exists(jsonPath))
			throw new FileNotFoundException($"Không tìm thấy file: {jsonPath}");

		string json = File.ReadAllText(jsonPath);
		var doc = JsonDocument.Parse(json);
		var roomsElement = doc.RootElement.GetProperty("rooms");

		var rooms = new List<Room>();
		foreach (var roomElem in roomsElement.EnumerateArray())
		{
			var room = new Room
			{
				ID = roomElem.GetProperty("ID").ToString(),
				numBeds = roomElem.GetProperty("numBeds").GetUInt32(),
				haveBalcony = roomElem.GetProperty("haveBalcony").GetBoolean(),
				haveKitchen = roomElem.GetProperty("haveKitchen").GetBoolean(),
				haveBathtub = roomElem.GetProperty("haveBathtub").GetBoolean(),
				capacity = roomElem.GetProperty("capacity").GetUInt32(),
				price = roomElem.GetProperty("price").GetUInt64(),
				state = roomElem.GetProperty("state").GetUInt32()
			};

			if (roomElem.TryGetProperty("additionalGuests", out var guestsElem))
			{
				var guests = new List<Person>();
				foreach (var guestElem in guestsElem.EnumerateArray())
				{
					guests.Add(new Person
					{
						name = guestElem.GetProperty("name").GetString(),
						age = guestElem.GetProperty("age").GetUInt32(),
						sex = guestElem.GetProperty("sex").GetBoolean(),
						mail = guestElem.GetProperty("mail").GetString(),
						CCCD = guestElem.GetProperty("CCCD").GetString(),
						phoneNumber = guestElem.GetProperty("phoneNumber").GetString(),
						address = guestElem.GetProperty("address").GetString()
					});
				}
				room.curGuest = guests.ToArray();
			}
			rooms.Add(room);
		}
		return rooms;
	}

	public static Room LoadFromJson(string roomNumber, string jsonPath = "Data/Rooms/ROOMDATA.json")
	{
		if (!File.Exists(jsonPath))
		{
			throw new FileNotFoundException($"Không tìm thấy file JSON: {jsonPath}");
		}

		var rooms = LoadRoomsFromJson(jsonPath);
		var room = rooms.FirstOrDefault(r => r.getID() == roomNumber);

		if (room == null)
		{
			throw new InvalidOperationException($"Không tìm thấy phòng {roomNumber} trong file JSON");
		}

		return room;
	}

	// Add method to save rooms to JSON
	public static void SaveRoomsToJson(List<Room> rooms, string jsonPath = "Data/Rooms/ROOMDATA.json")
	{
		var roomsData = new
		{
			rooms = rooms.Select(r => new
			{
				ID = int.Parse(r.getID()),
				numBeds = r.getNumBeds(),
				haveBalcony = r.getHaveBalcony(),
				haveKitchen = r.getHaveKitchen(),
				haveBathtub = r.getHaveBathtub(),
				capacity = r.getCapacity(),
				price = r.getPrice(),
				state = r.getState(),
				curGuest = r.getCurGuest().Select(g => new
				{
					name = g.name,
					age = g.age,
					sex = g.sex,
					mail = g.mail,
					CCCD = g.CCCD,
					phoneNumber = g.phoneNumber,
					address = g.address
				}).ToArray()
			}).ToArray()
		};

		string json = JsonSerializer.Serialize(roomsData, new JsonSerializerOptions
		{
			WriteIndented = true,
			Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		});

		// Ensure directory exists
		Directory.CreateDirectory(Path.GetDirectoryName(jsonPath) ?? "");
		File.WriteAllText(jsonPath, json);
	}
}