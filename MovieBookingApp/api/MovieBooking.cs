using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;

public class MovieBooking : IMovieBooking
{
    private Dictionary<string,User> users;
    private Dictionary<string,Movie> movies;
    private Dictionary<string,Theater> theaters;
    private Dictionary<string,Show> shows;
    private Dictionary<string,Booking> bookings;

    public MovieBooking()
    {
        this.users = new Dictionary<string, User>();
        this.movies = new Dictionary<string, Movie>();
        this.theaters = new Dictionary<string, Theater>();
        this.shows = new Dictionary<string, Show>();
        this.bookings = new Dictionary<string, Booking>();

        //seed data
        User user = new User(){
            Name = "vikash"
        };
        this.users.Add(user.Id,user);
        Movie movie = new Movie(){
            Name = "matrix",
            Language="English",
            Genre = "Science Fiction",
            ReleaseDate = new DateTime(2022,03,04)
        };
        this.movies.Add(movie.Id,movie);
        Theater theater = new Theater(){
            Name = "spana",
            Location = "Patna",
            Capacity = 100
        };
        this.theaters.Add(theater.Id,theater);
        HashSet<int> availableSeats = new HashSet<int>();
        for(int i=1;i<=theater.Capacity;i++){
            availableSeats.Add(i);
        }
        Show show = new Show(){
            Theater = theater,
            Movie = movie,
            RemainingSeats = theater.Capacity,
            StartTime = new DateTime(2022,07,10,10,0,0),
            EndTime = new DateTime(2022,07,11,12,20,0),
            AvailableSeats = availableSeats
        };
        this.shows.Add(show.Id,show);
    }
    
    public List<int> availableSeats(string showId)
    {
        if(!this.shows.ContainsKey(showId)){
            Console.WriteLine("No Show Found");
            return null;
        }
        Show show = this.shows[showId];
        return show.AvailableSeats.ToList();
    }

    public string createBooking(string userId, string showId, HashSet<int> bookedSeats)
    {
        //validation for userId, showid

        //check if all booked seats are still available
        Show show =this.shows[showId];
        User user =this.users[userId];
        foreach (var item in bookedSeats)
        {
            if(!show.AvailableSeats.Contains(item)){
                return "BookedSeats not available , try again";
            }
        }
        Booking booking = new Booking(){
            Show = show,
            User = user,
            BookedSeats = bookedSeats,
            BookingStatus = BookingStatus.Selected,
            PaymentStatus = false
        };
        this.bookings.Add(booking.Id,booking);
        return "Booking Done";
    }

    public string GetUser(string name)
    {
        return this.users.ToArray().Where((x)=> x.Value.Name == name).FirstOrDefault().Value.Id;
    }

    public List<string> listShows(string movieName)
    {
        List<string> shows = new List<string>();
        foreach (var item in this.shows)
        {
            if(item.Value.Movie.Name == movieName){
                shows.Add(item.Value.Id);
            }
        }
        return  shows;
    }

    public string updatePayment(string bookingId, bool paymentStatus)
    {
        Booking booking = this.bookings[bookingId];
        Show show = booking.Show;
        if(!paymentStatus){
            foreach (var item in booking.BookedSeats)
            {
                show.AvailableSeats.Add(item);
            }
            return "Payment Status Update as Failed";
        }
        else{
            booking.PaymentStatus = true;
        }
        return "";
    }
}