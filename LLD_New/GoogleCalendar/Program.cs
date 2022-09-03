using System;
using System.Collections.Generic;

namespace GoogleCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IGoogleCalendar googleCalendar = new GoogleCalendar();
            string usersId1 = googleCalendar.AddUser(new User(){Name="vikash",Email="v@gmail.com"});
            Console.WriteLine("userId1 - "+usersId1);
            string usersId2 = googleCalendar.AddUser(new User(){Name="ram",Email="r@gmail.com"});
            Console.WriteLine("userId2 - "+usersId2);
            string eventId = googleCalendar.AddEvent(new MeetingEvent(){
                Title = "My first event",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                OrganizerId = usersId1,
                EventUsers = new List<EventUser>(){
                    new EventUser(){UserId = usersId1},
                    new EventUser(){UserId=usersId2}
                },
                MeetingType = MeetingType.Online,
            });
            Console.WriteLine("eventId - "+eventId);
        }
    }

    //service
    public interface IGoogleCalendar{
        public string AddEvent(Event _event);
        public string AddUser(User user);
        public string UpdateStatus(string userId, string eventId);
        public List<Event>  GetCalendar(string userId);
        public Event GetEvent(string eventId);
    }
    public class GoogleCalendar : IGoogleCalendar{
        Dictionary<string,MeetingEvent> meetingEvents = new Dictionary<string, MeetingEvent>();
        Dictionary<string,HolidayEvent> holidayEvents = new Dictionary<string, HolidayEvent>();
        Dictionary<string,EventUser> eventUsers = new Dictionary<string, EventUser>();
        Dictionary<string,User> users = new Dictionary<string, User>();
        Dictionary<string,Location> locations = new Dictionary<string, Location>();
        public string AddEvent(Event _event){
            Console.WriteLine("event type - "+_event.GetType().ToString());
            if(_event.GetType().ToString() == "GoogleCalendar.MeetingEvent"){
                MeetingEvent meetingEvent = (MeetingEvent)_event;
                meetingEvents.Add(meetingEvent.Id,meetingEvent);
                if(meetingEvent.Location !=null)locations.Add(meetingEvent.Location.Id, meetingEvent.Location);
                foreach(var user in meetingEvent.EventUsers){
                    eventUsers.Add(user.Id,user);
                }
                return meetingEvent.Id;
            }
            else if(_event.GetType().ToString() == "GoogleCalendar.HolidayEvent"){
                HolidayEvent holidayEvent = (HolidayEvent)_event;
                holidayEvents.Add(holidayEvent.Id,holidayEvent);
                return holidayEvent.Id;
            }
            return "object type not found";
        }
        public string AddUser(User user){
            users.Add(user.Id,user);
            return user.Id;
        }
        public string UpdateStatus(string userId, string eventId){
            return "";
        }
        public List<Event>  GetCalendar(string userId){
            return null;
        }
        public Event GetEvent(string eventId){
            return null;
        }
    }

    //models
    public class Base{
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
    public class Event : Base{
        public string Title { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
    public class MeetingEvent : Event{
        public string OrganizerId { get; set; }
        public List<EventUser> EventUsers { get; set; }
        public MeetingType MeetingType { get; set; }
        public Location Location { get; set; }
    }
    public class HolidayEvent : Event{
    }
    public class EventUser : Base{
        public string EventId { get; set; }
        public string UserId { get; set; }
        public AcceptanceStatus AcceptanceStatus { get; set; }

    }
    public enum AcceptanceStatus{
        Accepted,
        Rejected,
        Tentative
    }
    public class User : Base{
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public enum MeetingType{
        Online,
        Offline
    }
    public class Location : Base{
        public string RoomNo { get; set; }
        public string Floor { get; set; }
        public string BuildingName { get; set; }
    }
}
