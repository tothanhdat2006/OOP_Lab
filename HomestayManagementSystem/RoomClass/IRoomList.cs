using System;
using System.Collections.Generic;

public interface IRoomList
{
    Room getRoom(uint id);
    int getTotalRoom();
    void addRoom(uint id, uint option);
    void addRoom(uint id, uint option, RoomType roomType); // New overload
    void deleteRoom(uint id);
    void editRoomBasePrice(uint id, ulong price);
    
    // New methods for enhanced functionality
    List<Room> getRoomsByState(uint state);
    List<Room> getRoomsByType(RoomType type);
    bool isRoomAvailableForDate(uint id, DateTime date);
    RoomAvailabilityInfo getRoomAvailabilityInfo(uint id, DateTime weekStart, DateTime weekEnd);
    void updateRoomAvailability(uint id, List<Calendar.Event> events);
    Dictionary<uint, Room> getAllRoomsDetailed();
}

// Data class for room availability information (Encapsulation)
public class RoomAvailabilityInfo
{
    public bool IsAvailable { get; set; }
    public DateTime? BookingStart { get; set; }
    public DateTime? BookingEnd { get; set; }
    public string GuestNames { get; set; } = "";
    public uint State { get; set; }
    public string StateText { get; set; } = "";
    public string DisplayInfo { get; set; } = "";
    public string AmenitiesText { get; set; } = "";
}