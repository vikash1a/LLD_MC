using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders.Testing;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            test();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static void test(){
            try{
                IMovieBooking movieBooking = new MovieBooking();
                var shows = movieBooking.listShows("matrix");
                Console.WriteLine("showId-"+shows[0]);
                var list = movieBooking.availableSeats(shows[0]);
                HashSet<int> bookedSeat = new HashSet<int>();
                bookedSeat.Add(1);
                bookedSeat.Add(2);
                bookedSeat.Add(3);
                var userId = movieBooking.GetUser("vikash");
                var resp = movieBooking.createBooking(userId,shows[0],bookedSeat);
                Console.WriteLine("bookingId-"+resp);
                return;
            } 
            catch(Exception e){
                Console.WriteLine(e.StackTrace);
            }
            return;
            
        }
    }
}
