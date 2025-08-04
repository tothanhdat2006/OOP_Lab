namespace Calendar
{
    public class Event
    {
        public string type;
        public int roomId;
        public Date startDate;
        public Date endDate;
        public Event(string type, int roomId, Date start, Date end)
        {
            this.type = type;
            this.roomId = roomId;
            this.startDate = start;
            this.endDate = end;
        }
        public override string ToString()
        {
            return $"[{type}] Room: {roomId}, From {startDate} to {endDate}";
        }
    }
}