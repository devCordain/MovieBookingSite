namespace MovieBookingSite
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public bool Available { get; set; } = true;
    }
}