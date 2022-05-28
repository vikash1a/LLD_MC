using System;
using Xunit;
using LLD_CabBooking;

namespace CabBokking.Tests
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            
        }
        [Fact]
        public void TestCabBooking()
        {
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
            Assert.Contains("vikash",selectedDriverid);
        }

        [Fact]
        public void Test1()
        {

        }
        
        
    }
}
