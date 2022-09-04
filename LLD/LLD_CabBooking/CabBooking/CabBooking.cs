using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace CabBooking
{
    //main app
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
        private readonly IDriverRepository driverRepository;
        private readonly IRiderRepository riderRepository;
        private readonly ILocationRepository locationRepository;
        private readonly ICabBookingStrategy cabBookingStrategy;
        private readonly ITripRepository tripRepository;

        public CabBookingApp(IDriverRepository driverRepository,IRiderRepository riderRepository,
        ILocationRepository locationRepository,ICabBookingStrategy cabBookingStrategy,ITripRepository tripRepository)
        {
            this.driverRepository = driverRepository;
            this.riderRepository = riderRepository;
            this.locationRepository = locationRepository;
            this.cabBookingStrategy = cabBookingStrategy;
            this.tripRepository = tripRepository;
        }

        public string bookCab(string riderId)
        {
            string selectedDriverId = cabBookingStrategy.pickDriver(riderId);
            if(selectedDriverId==null){
                Console.WriteLine("No Driver avaialble , please try again later");
                return null;
            }
            Driver driver = driverRepository.GetDriver(selectedDriverId);
            driver.IsAvailable = false;
            Trip trip = new Trip(){
                RiderId = riderId,
                DriverId = selectedDriverId
            };
            tripRepository.CreateTrip(trip);
            Console.WriteLine("Selected Driver name - "+driver.Name);
            return selectedDriverId;
        }

        public void endTrip(string tripId)
        {
            throw new NotImplementedException();
        }

        public List<Trip> fetchHistory(string riderId)
        {
            throw new NotImplementedException();
        }

        public string registerDriver(Driver driver, Location location)
        {
            throw new NotImplementedException();
        }

        public string registerRider(Rider rider, Location location)
        {
            throw new NotImplementedException();
        }

        public void updateAvailability(string driverId, bool isAvailable)
        {
            throw new NotImplementedException();
        }

        public void updateCabLocation(string driverId, int locationX, int locationY)
        {
            throw new NotImplementedException();
        }
    }
    //Startegy
    public interface ICabBookingStrategy{
        public string pickDriver(string riderId);
    }
    public class DefaultCabBookingStrategy : ICabBookingStrategy
    {
        private readonly IRiderRepository riderRepository;
        private readonly IDriverRepository driverRepository;
        private readonly ILocationRepository locationRepository;

        public DefaultCabBookingStrategy(IRiderRepository riderRepository,
        IDriverRepository driverRepository,ILocationRepository locationRepository)
        {
            this.riderRepository = riderRepository;
            this.driverRepository = driverRepository;
            this.locationRepository = locationRepository;
        }
        public string pickDriver(string riderId)
        {
            string selectedDriverId = null;

            Rider rider = riderRepository.GetRider(riderId);
            Location location = locationRepository.GetLocation(rider.LocatoinId);

            int x1 = location.LocationX, y1 = location.LocationY;
            double minDistance =Double.MaxValue;
            List<Driver> driversList = driverRepository.GetDriverList();
            foreach(Driver driver in driversList){
                if(!driver.IsAvailable)continue;
                Location driverLocation  = locationRepository.GetLocation(driver.LocatoinId);
                int x2 = driverLocation.LocationX, y2 = driverLocation.LocationY;
                double distance = Sqrt(Pow(x2-x1,2)+Pow(y2-y1,2));
                if(distance < minDistance){
                    distance = minDistance;
                    selectedDriverId = driver.Id;
                }
            }
            return selectedDriverId;
        }
    }
    //Repository
    public interface IRiderRepository{
        public string CreateRider(Rider rider);
        public Rider GetRider(string riderId);

    }
    public interface IDriverRepository{
        public string CreateDriver(Driver driver);
        public Driver GetDriver(string driverId);
        public List<Driver> GetDriverList();
        public string UpdateLocation(string driverId,Location location);
    }
    public interface ITripRepository{
        public string CreateTrip(Trip trip);
        public Trip GetTrip(string tripId);
    }
    public interface ILocationRepository{
        public string CreateLocation(Location location);
        public string UpdateLocation(Location location);
        public Location GetLocation(string locationId);
    }
    //Model
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string LocatoinId { get; set; }
    }
    public class Rider : User
    {
    }
    public class Driver : User
    {
        public bool IsAvailable { get; set; }
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