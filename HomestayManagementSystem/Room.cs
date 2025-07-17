using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HomestayManagementSystem
{
    internal class Room
    {
        private uint roomID;
        private string roomNumber;
        private uint roomType;
        private uint roomStatus; // 0: Available, 1: Occupied, 2: Under Maintenance
        private uint numBeds;
        private bool hasBalcony;
        private uint maxPersons;
        private uint price;

        public Room(uint roomID, string roomNumber, uint roomType, uint roomStatus, uint numBeds, bool hasBalcony, uint maxPersons, uint price)
        {
            this.roomID = roomID;
            this.roomNumber = roomNumber;
            this.roomType = roomType;
            this.roomStatus = roomStatus;
            this.numBeds = numBeds;
            this.hasBalcony = hasBalcony;
            this.maxPersons = maxPersons;
            this.price = price;
        }
        // Getters and Setters
        public uint getRoomID() => roomID;
        public void setRoomID(uint roomID) => this.roomID = roomID;
        public string getRoomNumber() => roomNumber;
        public void setRoomNumber(string roomNumber) => this.roomNumber = roomNumber;
        public uint getRoomType() => roomType;
        public void setRoomType(uint roomType) => this.roomType = roomType;
        public uint getRoomStatus() => roomStatus;
        public void setRoomStatus(uint roomStatus) => this.roomStatus = roomStatus;
        public uint getNumBeds() => numBeds;
        public void setNumBeds(uint numBeds) => this.numBeds = numBeds;
        public bool getHasBalcony() => hasBalcony;
        public void setHasBalcony(bool hasBalcony) => this.hasBalcony = hasBalcony;
        public uint getMaxPersons() => maxPersons;
        public void setMaxPersons(uint maxPersons) => this.maxPersons = maxPersons;
        public uint getPrice() => price;
        public void setPrice(uint price) => this.price = price;

        public override string ToString()
        {
            return $"Room ID: {roomID}, Room Number: {roomNumber}, Room Type: {roomType}, Room Status: {roomStatus}, Beds: {numBeds}, Balcony: {hasBalcony}, Max Persons: {maxPersons}, Price: {price}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Room otherRoom)
            {
                return roomID == otherRoom.roomID &&
                       roomNumber == otherRoom.roomNumber &&
                       roomType == otherRoom.roomType &&
                       roomStatus == otherRoom.roomStatus &&
                       numBeds == otherRoom.numBeds &&
                       hasBalcony == otherRoom.hasBalcony &&
                       maxPersons == otherRoom.maxPersons &&
                       price == otherRoom.price;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(roomID, roomNumber, roomType, roomStatus, numBeds, hasBalcony, maxPersons, price);
        }
    }
}
