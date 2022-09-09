using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

//below implementaion is for sliding window logs

namespace RateLimiter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RateLimiter rateLimiter = new RateLimiter();
            DateTime dt = DateTime.Now;
            Console.WriteLine(rateLimiter.check("1",dt));
            Console.WriteLine(rateLimiter.check("1",dt.AddSeconds(1)));
            Console.WriteLine(rateLimiter.check("1",dt.AddSeconds(2)));
            Console.WriteLine(rateLimiter.check("2",dt.AddSeconds(3)));
            Thread.Sleep(4000);
            Console.WriteLine(rateLimiter.check("1",dt.AddSeconds(4)));
        }
    }
    public class RateLimiter{
        private Dictionary<string,Data> dataDict;
        private int maxCount;
        public RateLimiter()
        {
            this.dataDict = new Dictionary<string, Data>();
            this.maxCount = 2;
        }
        public bool check(string id, DateTime dt){
            LinkedList<DateTime> ll;
            if(!dataDict.ContainsKey(id)){
                ll = new LinkedList<DateTime>();
                ll.AddLast(new LinkedListNode<DateTime>(dt));
                dataDict.Add(id,new Data(){Id = id, TimeStampList = ll});
            }
            else{
                ll = dataDict[id].TimeStampList;
                update(ll,dt);
                if(ll.Count >= this.maxCount)return false;
                ll.AddLast(new LinkedListNode<DateTime>(dt));
            }
            return true;
        }
        private void update(LinkedList<DateTime> ll, DateTime dt){
            if(ll.Count == 0)return;
            while(true){
                var diff = dt.Subtract(ll.First.Value);
                if(diff > new TimeSpan(0,0,3)){
                    ll.RemoveFirst();
                }
                else break;
            }
            return;
        }
    }
    public class Data{
        public string Id { get; set; }
        public LinkedList<DateTime> TimeStampList { get; set; }
    }
}
