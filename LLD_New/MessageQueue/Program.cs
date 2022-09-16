using System;
using System.Collections.Generic;

namespace MessageQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Driver driver = new Driver();
            driver.test();
        }
    }
    public class Driver{
      public void test(){
        ITopicManager topicManager = new TopicManager();
        ISubscriber subscriber = new Subscriber1();
        topicManager.addTopic("topic1");
        topicManager.subscribe("topic1",subscriber);
        topicManager.notify("topic1","puslish to topic 1");
        return;
      }
    }
    public interface ISubscriber{
        public void update(string message);
    }
    public class Subscriber1: ISubscriber{
        public void update(string message){
          Console.WriteLine("update received - "+message);
        }
    }
    public interface IPublisher{
        public void publish(string message);
    }
    public interface ITopicManager{
      public string subscribe(string topicName, ISubscriber subscriber);
      public string unsubscribe(string topicName, ISubscriber subscriber);
      public void notify(string topicName, string message);
      public void addTopic(string topicName);
    }
    public class TopicManager : ITopicManager{
        private Dictionary<string,List<ISubscriber>> subscribers = new Dictionary<string, List<ISubscriber>>();
        public void addTopic(string topicName){
          subscribers.Add(topicName,new List<ISubscriber>());
        }
        public void notify(string topicName, string message)
        {
            foreach(var elem in subscribers[topicName]){
              elem.update("messaeg - "+message);
            }
        }

        public string subscribe(string topicName, ISubscriber subscriber)
        {
            subscribers[topicName].Add(subscriber);
            return "subscriber added";
        }

        public string unsubscribe(string topicName, ISubscriber subscriber)
        {
            subscribers[topicName].Remove(subscriber);
            return "subscriber removed";
        }
    }

}
