using System;

namespace LogisticSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    //Driver
    public interface ILogisiticSystem{
        public string order(Order order);
        public string registerUser(User user);
        public Location trackOrder(string orderId);
    }
    public class LogisticSystem : ILogisiticSystem{
        Dictionary<string,CustomerUser> customerUsers = new Dictionary<string, CustomerUser>();
        Dictionary<string,CustomerUser> staffUsers = new Dictionary<string, StaffUser>();
        Dictionary<string,Location> locations = new Dictionary<string, Location>();
        Dictionary<string,Payment> payments = new Dictionary<string, Payment>();
        Dictionary<string,Vehicle> vehicles = new Dictionary<string, Vehicle>();
        Dictionary<string,Order> orders = new Dictionary<string, Order>();
        public string order(Order order){
            order.OrderStatus = OrderStatus.Booked;
            orders.Add(order.id,order);
            return order.id;
        }
        public string registerUser(User user){
        }
        public Location trackOrder(string orderId){
            
        }
    }
    //Model
    public class Base{
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
    public class User : Base{
        public string  Name { get; set; }
        public string  Mobile { get; set; }
        public string  Email { get; set; }
        public Locaton AddressLocation { get; set; }
    }
    public class CustomerUser : User{
    }
    public class StaffUser : User{
    }
    public class Location : Base{
        public string  Address { get; set; }
        public string  City { get; set; }
        public string  State { get; set; }
        public string  PinCode { get; set; }

    }
    public class Payment : Base{
        public bool  Status { get; set; }
        public int?  Amount { get; set; }
        public string  Mode { get; set; }
    }
    public enum VehicleType{
        Bike,
        Truck
    }
    public class Vehicle: Base{
        public string  Name { get; set; }
        public bool  IsAvailable { get; set; }
        public Location  CurrentLocation { get; set; }
        public VehicleType VehicleType {get; set;}
    }
    public enum DeliveryType{
        Economy,
        Standard,
        Premium
    }
    public enum DeliveryTypeCharges{
        Economy = 100,
        Standard = 200,
        Premium= 300
    }
    public enum OrderStatus{
        Booked,
        Packed,
        Shipped,
        Delivered,
        Cancelled
    }
    public class Order{
        public List<Pair<string,int>> Items { get; set; }
        public Location SourceLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public User User { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Vehicle Vehicle { get; set; }
        public Payment Payment { get; set; }
    }
}
