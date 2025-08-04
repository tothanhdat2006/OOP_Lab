namespace Calendar
{
    public class Date{
        public int day, month, year;
        public Date(int day = 1, int month = 1, int year = 1){
            this.day = day;
            this.month = month;
            this.year = year;
        }
        public override string ToString(){
            return $"{day}/{month}/{year}";
        }
    }
}