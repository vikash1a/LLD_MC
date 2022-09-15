using System;
using System.Collections.Generic;

namespace  ParkingLot
{
    public class Driver{
        public void test(){
            IParkingLot parkingLot = new ParkingLot();
            Console.WriteLine(parkingLot.park("reg1","color1"));
            Console.WriteLine(parkingLot.park("reg2","color2"));
            Console.WriteLine(parkingLot.park("reg3","color3"));
            Console.WriteLine(parkingLot.park("reg3","color4"));
            Console.WriteLine(parkingLot.status());
            Console.WriteLine(parkingLot.leave("2"));
            Console.WriteLine(parkingLot.status());
            Console.WriteLine(parkingLot.park("reg3","color4"));
            Console.WriteLine(parkingLot.status());
        }
    }
    public interface IParkingLot{
        public string park(string regNo, string color);
        public string leave(string slotNo);
        public string status();
    }
    public class ParkingLot : IParkingLot
    {
        Dictionary<string,Ticket> tickets = new Dictionary<string, Ticket>();
        Dictionary<string,Car> cars = new Dictionary<string, Car>();
        Dictionary<string,Slot> slots = new Dictionary<string, Slot>();
        public ParkingLot(){
            this.seed();
        }
        public void seed(){
            Slot slot = new Slot(){Number=1,DistanceFromEntry=10,Status = SlotStatus.Available};
            slots.Add(slot.Id,slot);
            slot = new Slot(){Number=2,DistanceFromEntry=20,Status = SlotStatus.Available};
            slots.Add(slot.Id,slot);
            slot = new Slot(){Number=3,DistanceFromEntry=30,Status = SlotStatus.Available};
            slots.Add(slot.Id,slot);
        }
        public string park(string regNo, string color)
        {
            int nearestAvailableSlot = 0;
            int minDistance = int.MaxValue;
            Slot slot = null;
            foreach(var elem in slots){
                if(elem.Value.Status == SlotStatus.Available && minDistance > elem.Value.DistanceFromEntry){
                    nearestAvailableSlot = elem.Value.Number;
                    slot = elem.Value;
                    minDistance = elem.Value.DistanceFromEntry;
                } 
            }
            if(minDistance == int.MaxValue)return "No Available Slot";
            slot.Status = SlotStatus.Occupied;
            Car car = new Car(){
                RegNo = regNo,Color = color
            };
            Ticket ticket = new Ticket(){
                SlotId = slot.Id,
                CarId = car.Id,
                StartTime = DateTime.Now,
                Status = TicketStatus.Parked
            };
            return nearestAvailableSlot.ToString();
        }
        public string leave(string slotNo)
        {
            foreach(var elem in tickets){
                if(slots[elem.Value.SlotId].Number == int.Parse(slotNo)){
                    slots[elem.Value.SlotId].Status = SlotStatus.Available;
                    elem.Value.Status = TicketStatus.Left;
                    return slotNo +" is free.";
                }
            }
            return slotNo +" not found.";
        }
        public string status()
        {
            string result = "SlotNo RegNo Color\n";
            foreach(var elem in tickets){
                if(elem.Value.Status ==TicketStatus.Parked){
                    Slot slot = slots[elem.Value.SlotId];
                    Car car = cars[elem.Value.CarId];
                    result+= slot.Number.ToString()+" "+car.RegNo+" "+car.Color+"\n";
                }
            }
            return result;
        }
    }

    //Models
    public class Ticket : Base{
        public string CarId { get; set; }
        public string SlotId { get; set; }
        public DateTime StartTime { get; set; }
        public TicketStatus Status { get; set; }
    }
    public enum TicketStatus{
        Parked,
        Left
    }
    public class Car : Base{
        public string RegNo { get; set; }
        public string Color { get; set; }
    }
    public class Slot : Base{
        public int Number { get; set; }
        public int DistanceFromEntry { get; set; }
        public SlotStatus Status { get; set; }
    }
    public enum SlotStatus{
        Occupied,
        Available,
        NotAvailable
    }
    public class Base{
        public string Id {get;set;} = Guid.NewGuid().ToString();
    }

}
/*

*/