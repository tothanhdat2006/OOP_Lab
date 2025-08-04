public interface IRoom
{
	void setID(string id);
	void setNumBeds(uint numBeds);
	void setHaveBalcony(bool haveBalcony);
	void setHaveKitchen(bool haveKitchen);
	void setHaveBathtub(bool haveBathtub);
	void setCapacity(uint capacity);
	void setPrice(ulong price);
	void setState(uint state);
	void setCurGuest(Person[] curGuest);


	string getID();
	uint getNumBeds();
	bool getHaveBalcony();
	bool getHaveKitchen();
	bool getHaveBathtub();
	uint getCapacity();
	ulong getPrice();
	uint getState();
	uint getMaxPersons();
	Person[] getCurGuest();
}