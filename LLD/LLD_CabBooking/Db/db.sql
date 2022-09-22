create database cabbooking

create table user(
    id uuid primary key,
    name varchar(50) not null,
    locationid varchar references location(id)
);

create table location(
    id serial primary key,
    locationX varchar(50) not null,
    locationY varchar(50) not null
);


/*
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
    */