namespace api
{
    public class User{
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Movie{
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Theater{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }
    public class Show{
        public int Id { get; set; }
        public int  TheaterId { get; set; }
        public int MovieId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int RemainingSeats { get; set; }
    }
    public class Ticket{
        public int Id { get; set; }
        public int  ShowId { get; set; }
        public int  UserId { get; set; }
    }
}