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

    public void customConstruct(int param1, int param2, int param3, int param4, ulong param5)
    {
        builder.reset();
        // param1 for bedroom option
        if (param1 == 0) 
            builder.buildSmallBedroom();
        else if (param1 == 1) 
            builder.buildMediumBedroom();
        else if (param1 == 2)
            builder.buildLargeBedroom();


        // param2 for balcony option
        if (param2 == 1)
            builder.buildBalcony();

        // param3 for kitchen option
        if (param3 == 1)
            builder.buildKitchen();

        // param4 for bathtub option
        if (param4 == 0)
            builder.buildToiletNoBathtub();
        else builder.buildToiletHaveBathtub();

        // param5 for setting price
        builder.setPrice(param5);
                    
    }
    
    public void ConstructPersonalRoom()
    {
        builder.reset();
        builder.buildSmallBedroom();
        builder.buildToiletNoBathtub();
        builder.setPrice(200000);
    }

    public void ConstructCoupleRoom() 
    {
        builder.reset();
        builder.buildMediumBedroom();
        builder.buildKitchen();
        builder.buildToiletHaveBathtub();
        builder.setPrice(500000);
    }

    public void ConstructQuadroRoom()
    {
        builder.reset();
        builder.buildLargeBedroom();
        builder.buildKitchen();
        builder.buildToiletNoBathtub();
        builder.buildBalcony();
        builder.setPrice(800000);
    }
}