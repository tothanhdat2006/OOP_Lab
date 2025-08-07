using System;
using System.Collections.Generic;
using System.Linq;

public class RoomList : IRoomList
{
    // Singleton pattern (Encapsulation)
    private static readonly RoomList _instance = new RoomList();
    private RoomList() { }
    public static RoomList Instance => _instance;

    protected uint totalRoom;
    protected ulong basePrice;
    protected Dictionary<uint, Room> rooms = new Dictionary<uint, Room>();
    
    // Enhanced fields for booking management
    private Dictionary<uint, List<BookingPeriod>> roomBookings = new Dictionary<uint, List<BookingPeriod>>();

    // Nested class for booking periods (Encapsulation)
    private class BookingPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Person[] Guests { get; set; } = Array.Empty<Person>();
        public uint BookingState { get; set; } // 1 = Occupied, 2 = Deposited
    }

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
        addRoom(id, option, RoomType.Standard);
    }

    public void addRoom(uint id, uint option, RoomType roomType)
    {
        if (rooms.ContainsKey(id))
            throw new ArgumentException("Room ID already exists.");

        // Use builder pattern to create new room with enhanced functionality
        ConcreteBuilder builder = new ConcreteBuilder();
        RoomDirector director = new RoomDirector(builder);

        // Choose construction type based on option parameter
        switch (option)
        {
            case 1:
                director.ConstructPersonalRoom();
                break;
            case 2:
                director.ConstructCoupleRoom();
                break;
            case 3:
                director.ConstructQuadroRoom();
                break;
            case 4:
                director.ConstructLuxurySuite();
                break;
            default:
                director.customConstruct(1, 1, 1, 1, 300000, roomType);
                break;
        }

        Room newRoom = builder.getResult();
        newRoom.setID(id.ToString());
        
        rooms[id] = newRoom;
        roomBookings[id] = new List<BookingPeriod>();
    }

    public void deleteRoom(uint id)
    {
        if (!rooms.ContainsKey(id))
            throw new ArgumentException("Room ID not found.");

        rooms.Remove(id);
        roomBookings.Remove(id);
    }

    public void editRoomBasePrice(uint id, ulong price)
    {
        if (!rooms.ContainsKey(id))
            throw new ArgumentException("Room ID not found.");

        rooms[id].setPrice(price);
    }

    // Enhanced methods implementation
    public List<Room> getRoomsByState(uint state)
    {
        return rooms.Values.Where(r => r.getState() == state).ToList();
    }

    public List<Room> getRoomsByType(RoomType type)
    {
        return rooms.Values.Where(r => r.getRoomType() == type).ToList();
    }

    public bool isRoomAvailableForDate(uint id, DateTime date)
    {
        if (!roomBookings.ContainsKey(id))
            return true;

        return !roomBookings[id].Any(booking => 
            date.Date >= booking.StartDate.Date && date.Date <= booking.EndDate.Date);
    }

    public RoomAvailabilityInfo getRoomAvailabilityInfo(uint id, DateTime weekStart, DateTime weekEnd)
    {
        if (!rooms.ContainsKey(id))
        {
            return new RoomAvailabilityInfo
            {
                IsAvailable = false,
                StateText = "Room not found",
                DisplayInfo = $"Room {id} - Not Found"
            };
        }

        var room = rooms[id];
        var bookings = roomBookings.ContainsKey(id) ? roomBookings[id] : new List<BookingPeriod>();

        var activeBooking = bookings.FirstOrDefault(bp => 
            bp.StartDate.Date <= weekEnd.Date && bp.EndDate.Date >= weekStart.Date);

        if (activeBooking != null)
        {
            return new RoomAvailabilityInfo
            {
                IsAvailable = false,
                BookingStart = activeBooking.StartDate,
                BookingEnd = activeBooking.EndDate,
                GuestCount = activeBooking.Guests.Length,
                GuestNames = string.Join(", ", activeBooking.Guests.Select(g => g.name)),
                State = activeBooking.BookingState,
                StateText = GetStateText(activeBooking.BookingState),
                DisplayInfo = room.getDisplayInfo(),
                AmenitiesText = room.getAmenitiesText()
            };
        }

        return new RoomAvailabilityInfo
        {
            IsAvailable = true,
            State = 0,
            StateText = "Free",
            GuestCount = 0,
            GuestNames = "",
            DisplayInfo = room.getDisplayInfo(),
            AmenitiesText = room.getAmenitiesText()
        };
    }

    public void updateRoomAvailability(uint id, List<Calendar.Event> events)
    {
        if (!rooms.ContainsKey(id))
            return;

        if (!roomBookings.ContainsKey(id))
            roomBookings[id] = new List<BookingPeriod>();

        roomBookings[id].Clear();

        foreach (var evt in events.Where(e => e.roomId == (int)id))
        {
            // Get all guests for this event from the event data
            var allGuests = new List<Person>();

            if (evt.guestInfo.HasValue)
            {
                allGuests.Add(evt.guestInfo.Value);

                // Note: Additional guests would need to be loaded from the expanded event structure
                // This would require modifying the Event class to support multiple guests
                // For now, we'll work with the primary guest
            }

            var booking = new BookingPeriod
            {
                StartDate = new DateTime(evt.startDate.year, evt.startDate.month, evt.startDate.day),
                EndDate = new DateTime(evt.endDate.year, evt.endDate.month, evt.endDate.day),
                BookingState = evt.type == "Occupied" ? (uint)1 : (uint)2,
                Guests = allGuests.ToArray()
            };
            roomBookings[id].Add(booking);
        }

        // Update current state based on today's bookings
        UpdateCurrentRoomState(id);
    }

    public Dictionary<uint, Room> getAllRoomsDetailed()
    {
        return new Dictionary<uint, Room>(rooms);
    }

    // Helper methods
    private void UpdateCurrentRoomState(uint id)
    {
        if (!rooms.ContainsKey(id) || !roomBookings.ContainsKey(id))
            return;

        var todaysBooking = roomBookings[id].FirstOrDefault(bp => 
            DateTime.Today >= bp.StartDate.Date && DateTime.Today <= bp.EndDate.Date);

        if (todaysBooking != null)
        {
            rooms[id].setState(todaysBooking.BookingState);
            rooms[id].setCurGuest(todaysBooking.Guests);
        }
        else
        {
            rooms[id].setState(0); // Free
            rooms[id].setCurGuest(Array.Empty<Person>());
        }
    }

    private string GetStateText(uint state)
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
}