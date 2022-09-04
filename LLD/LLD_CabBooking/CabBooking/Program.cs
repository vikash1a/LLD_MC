using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace LLD_CabBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ICabBookingApp cabBookingApp = new CabBookingApp();
            Location location3 = new Location(){
                LocationX = 3,
                LocationY = 1
            };
            string riderId = cabBookingApp.registerRider(new Rider(){
                Name = "vikash",LocatoinId = location3.Id
            },location3);
            string selectedDriverid = cabBookingApp.bookCab(riderId);
            Console.WriteLine("selectedDriverid - "+selectedDriverid);
            return;
        }
    }
    public interface ICabBookingApp
    {
        public string registerRider(Rider rider,Location location);
        public string registerDriver(Driver driver,Location location);
        public void updateCabLocation(string driverId, int locationX, int locationY);
        public void updateAvailability(string driverId,  bool isAvailable);
        public string bookCab(string riderId);
        public List<Trip> fetchHistory(string riderId);
        public void endTrip(string tripId);
    }
    public class CabBookingApp : ICabBookingApp
    {
        private Dictionary<string,Rider> riders = new Dictionary<string,Rider>();
        private Dictionary<string,Driver> drivers = new Dictionary<string,Driver>();
        private Dictionary<string,Trip> trips = new Dictionary<string,Trip>();
        private Dictionary<string,Location> locations = new Dictionary<string,Location>();
        public CabBookingApp()
        {
            this.seedData();   
        }
        private void seedData(){
            Location location = new Location(){
                LocationX = 1,
                LocationY = 2
            };
            this.registerDriver(new Driver(){
                Name = "vikash",IsAvailable=true,LocatoinId = location.Id
            },location);
            Location location2 = new Location(){
                LocationX = 2,
                LocationY = 3
            };
            this.registerRider(new Rider(){
                Name = "vikash",LocatoinId = location2.Id
            },location2);
            Location location4 = new Location(){
                LocationX = 3,
                LocationY = 1
            };
            this.registerDriver(new Driver(){
                Name = "vikashm3",LocatoinId = location4.Id
            },location4);
        }
        
        public string bookCab(string riderId)
        {
            Rider rider = riders[riderId];
            Location location = locations[rider.LocatoinId];
            int x1 = location.LocationX, y1 = location.LocationY;
            string selectedDriverId = null;
            double minDistance =Double.MaxValue;
            foreach(Driver driver in drivers.Values.ToList()){
                if(!driver.IsAvailable)continue;
                Location driverLocation  = locations[driver.LocatoinId];
                int x2 = driverLocation.LocationX, y2 = driverLocation.LocationY;
                double distance = Sqrt(Pow(x2-x1,2)+Pow(y2-y1,2));
                if(distance < minDistance){
                    distance = minDistance;
                    selectedDriverId = driver.Id;
                }
            }
            if(selectedDriverId==null){
                Console.WriteLine("No Driver avaialble , please try again later");
                return null;
            }
            drivers[selectedDriverId].IsAvailable = false;
            Trip trip = new Trip(){
                RiderId = riderId,
                DriverId = selectedDriverId
            };
            trips.Add(trip.Id,trip);
            Console.WriteLine("Selected Driver name - "+drivers[selectedDriverId].Name);
            return selectedDriverId;
        }

        public void endTrip(string tripId)
        {
            Trip trip = trips[tripId];
            trip.IsTripEnded = true;
            trip.EndTime = DateTime.Now;
            drivers[trip.DriverId].IsAvailable = true;
            trips[tripId]=trip;
        }

        public List<Trip> fetchHistory(string riderId)
        {
            List<Trip> tripsHistory = new List<Trip>();
            foreach(Trip trip in trips.Values){
                if(trip.RiderId == riderId){
                    tripsHistory.Add(trip);
                }
            }
            return tripsHistory;
        }

        public string registerDriver(Driver driver,Location location)
        {
            drivers.Add(driver.Id,driver);
            locations.Add(location.Id,location);
            locations[driver.LocatoinId].PersonId = driver.Id;
            return driver.Id;
        }

        public string registerRider(Rider rider,Location location)
        {
            riders.Add(rider.Id,rider);
            locations.Add(location.Id,location);
            locations[rider.LocatoinId].PersonId = rider.Id;
            return rider.Id;
        }

        public void updateAvailability(string driverId, bool isAvailable)
        {
            drivers[driverId].IsAvailable = isAvailable;
        }

        public void updateCabLocation(string driverId, int locationX, int locationY)
        {
            string locationId = drivers[driverId].LocatoinId;
            Location location = locations[locationId];
            location.LocationX = locationX;
            location.LocationY = locationY;
            locations[locationId] = location;
        }
    }
    //Model
    public class Rider : BaseModel
    {
        public string Name { get; set; }
        public string LocatoinId { get; set; }
    }
    public class Driver : BaseModel
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public string LocatoinId { get; set; }
    }
    public class Trip : BaseModel
    {
        public string RiderId { get; set; }
        public string DriverId { get; set; }
        public DateTime? StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }
        public bool IsTripEnded { get; set; } = false;
    }
    public class Location : BaseModel
    {
        public string PersonId { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
    }
    public class BaseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
    
}
