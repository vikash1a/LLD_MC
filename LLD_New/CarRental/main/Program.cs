// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

ICarRental carRental = new CarRental();
var userId1 = carRental.RegisterUser(new User(){
    Name = "Vikash Sinha",
    MobileNumber = "1234567890",
    Email = "v@gmail.com",
    Address = "Patna",
    UserType = UserType.Admin
});
Console.WriteLine("userId1 - "+userId1);
var vehicleId = carRental.RegisterVehicle(userId1,new Vehicle(){
    Location = new Location(){
        Latitude = "1",Longitude = "2",State = "MH"
    },
    VehicleType = VehicleType.Suv,
    VehicleStatus = VehicleStatus.Available
});
Console.WriteLine("vehicleId - "+vehicleId);
var bookingId = carRental.BookVehicle(userId1,vehicleId,false,new BookingDto(){
    StartTime = DateTime.Now.AddDays(2),
    EndTime = DateTime.Now.AddDays(4),
    Payment = new Payment(){
        Mode = "Online",
        Status   = true,
        Amount = 1000
    },
    Services = new List<AddOnService>(){
        AddOnService.Ac,AddOnService.music
    }
});
Console.WriteLine("bookingId - "+bookingId);

//Model
public class Base{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
public class User : Base{
    public string Name { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public UserType UserType { get; set; }
}
public enum UserType{
    User,
    Admin,
    Driver
}
public class Vehicle : Base{
    public Location Location { get; set; }
    public VehicleType VehicleType { get; set; }
    public VehicleStatus VehicleStatus { get; set; }
    
}
public enum VehicleType{
    Bike ,
    Suv,
    Xuv,
    Sedan,
    HatchBack
}
public enum VehicleStatus{
    Booked,
    InMaintenance,
    InUse,
    Available
}
public class Booking : Base{
    public User User { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public User Driver { get; set; }
    public Payment Payment { get; set; }
    public BookingStatus Status { get; set; }
    public List<AddOnService> Services { get; set; }
}
public enum BookingStatus{
    Booked,
    Cancelled,
    Picked,
    Returned
}
public enum AddOnService{
    Ac,
    music
}
public class Location: Base{
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string State { get; set; }
}
public class Payment : Base{
    public string Mode { get; set; }
    public bool Status { get; set; }
    public int Amount { get; set; }
}
public class QrCodeMapping{
    public string QrCode { get; set; }
    public Vehicle Vehicle { get; set; }
}

public class BookingDto{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Payment Payment { get; set; }
    public List<AddOnService> Services { get; set; }
}
//main logic
public interface ICarRental{
    public string RegisterUser(User user);
    public string RegisterVehicle(string userId, Vehicle vehicle);
    public string BookVehicle(string userId, string id,bool IsQrId,BookingDto bookingDto);
    public string  PickVehicle (string vehicleId);
    public string  CancelVehicle (string vehicleId);
    public string  ReturnVehicle (string vehicleId);
}
public class CarRental : ICarRental
{
    Dictionary<string,Vehicle> vehicles = new Dictionary<string,Vehicle>();
    Dictionary<string,User> users = new Dictionary<string,User>();
    Dictionary<string,Booking> bookings = new Dictionary<string,Booking>();
    Dictionary<string,Location> locations = new Dictionary<string,Location>();
    Dictionary<string,Payment> payments = new Dictionary<string,Payment>();
    Dictionary<string,QrCodeMapping> qrCodeMappings = new Dictionary<string,QrCodeMapping>();
    public string BookVehicle(string userId, string id, bool IsQrId,BookingDto bookingDto)
    {
        Vehicle vehicle;
        User user = users[userId];
        if(IsQrId)vehicle = qrCodeMappings[id].Vehicle;
        else vehicle = vehicles[id];
        Booking booking = new Booking(){
            User = user,
            Vehicle = vehicle,
            StartTime = bookingDto.StartTime,
            EndTime = bookingDto.EndTime,
            Status = BookingStatus.Booked,
            Payment = bookingDto.Payment,
            Services = bookingDto.Services,
            Driver = user
        };
        bookings.Add(booking.Id,booking);
        return booking.Id;
    }

    public string CancelVehicle(string vehicleId)
    {
        throw new NotImplementedException();
    }

    public string PickVehicle(string vehicleId)
    {
        throw new NotImplementedException();
    }

    public string RegisterUser(User user)
    {
        users.Add(user.Id,user);
        return user.Id;
    }

    public string RegisterVehicle(string userId, Vehicle vehicle)
    {
        User user = users[userId];
        if(user.UserType != UserType.Admin)return "Permisson deinied for registering vehicle";
        vehicles.Add(vehicle.Id,vehicle);
        return vehicle.Id;
    }

    public string ReturnVehicle(string vehicleId)
    {
        throw new NotImplementedException();
    }
}