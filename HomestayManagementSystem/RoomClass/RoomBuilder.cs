using System;
using System.Collections.Generic;

public interface IBuilder
{
    void reset();
    void buildSmallBedroom();
    void buildMediumBedroom();
    void buildLargeBedroom();
    void buildBalcony();
    void buildKitchen();
    void buildToiletHaveBathtub();
    void buildToiletNoBathtub();
    void setPrice(ulong price);
    void setRoomType(RoomType type); // New method for room types
}

// Enhanced Room types using inheritance (Polymorphism)
public enum RoomType
{
    Standard = 0,
    Luxury = 1,
    Suite = 2
}

public class ConcreteBuilder : IBuilder
{
    private Room room = new Room();

    public ConcreteBuilder()
    {
        reset();
    }

    public void reset() 
    {
        room = new Room();
    }

    public void buildSmallBedroom()
    {
        room.setNumBeds(1);
        room.setCapacity(1);
    }

    public void buildMediumBedroom()
    {
        room.setNumBeds(1);
        room.setCapacity(2);
    }

    public void buildLargeBedroom()
    {
        room.setNumBeds(2);
        room.setCapacity(4);
    }

    public void buildBalcony()
    {
        room.setHaveBalcony(true);
    }

    public void buildKitchen()
    {
        room.setHaveKitchen(true);
    }

    public void buildToiletHaveBathtub()
    {
        room.setHaveBathtub(true);
    }

    public void buildToiletNoBathtub()
    {
        room.setHaveBathtub(false);
    }

    public void setPrice(ulong price)
    {
        room.setPrice(price);
    }

    public void setRoomType(RoomType type)
    {
        room.setRoomType(type);
    }

    public Room getResult()
    {
        Room constructed = room;
        reset();
        return constructed;
    }
}

public class RoomDirector
{
    private ConcreteBuilder builder;

    public RoomDirector(ConcreteBuilder builder)
    {
        this.builder = builder;
    }

    public void customConstruct(int bedRoomOpt, int balconyOpt, int kitchenOpt, int bathTub, ulong price, RoomType roomType = RoomType.Standard)
    {
        builder.reset();
        builder.setRoomType(roomType);

        // param1 for bedroom option
        if (bedRoomOpt == 0) 
            builder.buildSmallBedroom();
        else if (bedRoomOpt == 1) 
            builder.buildMediumBedroom();
        else if (bedRoomOpt == 2)
            builder.buildLargeBedroom();

        // param2 for balcony option
        if (balconyOpt == 1)
            builder.buildBalcony();

        // param3 for kitchen option
        if (kitchenOpt == 1)
            builder.buildKitchen();

        // param4 for bathtub option
        if (bathTub == 0)
            builder.buildToiletNoBathtub();
        else 
            builder.buildToiletHaveBathtub();

        // param5 for setting price
        builder.setPrice(price);
    }
    
    public void ConstructPersonalRoom()
    {
        builder.reset();
        builder.setRoomType(RoomType.Standard);
        builder.buildSmallBedroom();
        builder.buildToiletNoBathtub();
        builder.setPrice(200000);
    }

    public void ConstructCoupleRoom() 
    {
        builder.reset();
        builder.setRoomType(RoomType.Standard);
        builder.buildMediumBedroom();
        builder.buildKitchen();
        builder.buildToiletHaveBathtub();
        builder.setPrice(500000);
    }

    public void ConstructQuadroRoom()
    {
        builder.reset();
        builder.setRoomType(RoomType.Luxury);
        builder.buildLargeBedroom();
        builder.buildKitchen();
        builder.buildToiletNoBathtub();
        builder.buildBalcony();
        builder.setPrice(800000);
    }

    public void ConstructLuxurySuite()
    {
        builder.reset();
        builder.setRoomType(RoomType.Suite);
        builder.buildLargeBedroom();
        builder.buildKitchen();
        builder.buildBalcony();
        builder.buildToiletHaveBathtub();
        builder.setPrice(1500000);
    }
}