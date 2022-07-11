using System;
using System.Collections.Generic;

namespace api
{
    public class User : BaseModel{
        public string Name { get; set; }
    }
    public class Movie : BaseModel{
        public string Name { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
    public class Theater : BaseModel{
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }
    public class Show : BaseModel{
        public Theater  Theater { get; set; }
        public Movie Movie { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int RemainingSeats { get; set; }
        public HashSet<int> AvailableSeats { get; set; }
    }
    public enum BookingStatus{
        Selected,
        Booked,
        Canceled
    }
    public class Booking : BaseModel{
        public Show  Show { get; set; }
        public User  User { get; set; }
        public bool PaymentStatus { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public HashSet<int> BookedSeats { get; set; }
    }

    public class BaseModel{
        public string  Id { get; set; } = Guid.NewGuid().ToString();
    }
}