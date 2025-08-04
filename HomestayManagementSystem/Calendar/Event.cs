namespace Calendar
{
    public class Event
    {
        public string type;
        public int roomId;
        public Date startDate;
        public Date endDate;
        public Person? guestInfo;

        public Event(string type, int roomId, Date start, Date end)
        {
            this.type = type;
            this.roomId = roomId;
            this.startDate = start;
            this.endDate = end;
            this.guestInfo = null;
        }

        public Event(string type, int roomId, Date start, Date end, Person guest)
        {
            this.type = type;
            this.roomId = roomId;
            this.startDate = start;
            this.endDate = end;
            this.guestInfo = guest;
        }

        public override string ToString()
        {
            string guestName = guestInfo?.name ?? "Unknown";
            return $"[{type}] Room: {roomId}, Guest: {guestName}, From {startDate} to {endDate}";
        }
    }
}