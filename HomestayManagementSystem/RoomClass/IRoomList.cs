using System;

public interface IRoomList
{
    Room getRoom(uint id);
    int getTotalRoom();
    void addRoom(uint id, uint option);
    void deleteRoom(uint id);
    void editRoomBasePrice(uint id, ulong price);
}