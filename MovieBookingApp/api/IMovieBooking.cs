
using System.Collections.Generic;

namespace api
{
    public interface IMovieBooking
    {
        public List<string> listShows(string movieName);
        public List<int> availableSeats(string showId);
        public string createBooking(string userId, string showId, HashSet<int> bookedSeats);
        public string updatePayment(string bookingId, bool paymentStatus);
        public string GetUser(string name);
    }
}