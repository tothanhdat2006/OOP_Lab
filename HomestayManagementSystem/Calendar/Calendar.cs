using System;
namespace Calendar
{
    public class Calendar
    {
        private static Calendar? ins = null;
        public DoublyLinkedList<Event> eventList = new DoublyLinkedList<Event>();
        public Date currentDate;
        private Calendar()
        {
            DateTime now = DateTime.Now;
            currentDate = new Date(now.Day, now.Month, now.Year);
        }
        public static Calendar GetInstance()
        {
            if (ins == null) ins = new Calendar();
            return ins;
        }
        public void AddEvent(Event e)
        {
            eventList.AddLast(e);
        }
        public void ListEvent()
        {
            eventList.PrintAll();
        }
        public void RemoveEventAt(int idx)
        {
            Node<Event>? node = eventList.GetAt(idx);
            eventList.Remove(node);
        }

        public void EditEvent(int idx, Event newEvent)
        {
            eventList.EditAt(idx, newEvent);
        }
        public Date GetCurrentDate()
        {
            return currentDate;
        }
    }
}