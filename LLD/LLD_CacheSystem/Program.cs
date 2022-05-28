using System;
using System.Collections.Generic;

namespace LLD_CacheSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ICacheSystem cacheSystem = new CacheSystem(2);
            cacheSystem.putValue("1","one");
            
            cacheSystem.putValue("2","two");
            cacheSystem.putValue("3","three");
            Console.WriteLine("1 value - "+cacheSystem.getValue("1"));
            Console.WriteLine("2 value - "+cacheSystem.getValue("2"));
            Console.WriteLine("3 value - "+cacheSystem.getValue("3"));
        }
    }
    public interface ICacheSystem
    {
        public void putValue(string key, string value);
        public string getValue(string key);
    }
    public class CacheSystem : ICacheSystem
    {
        private IEvictionPolicy evictionPolicy;
        private ICacheDataRepo cacheDataRepo;
        public CacheSystem(int capacity)
        {
            this.evictionPolicy = new EvictionPolicy();
            this.cacheDataRepo = new CacheDataRepo(capacity);
        }
        public string getValue(string key)
        {
            try{
                string resp = cacheDataRepo.getItem(key);
                evictionPolicy.keyAccessed(key);
                return resp;
            }
            catch(Exception e){
                Console.WriteLine("Exception - "+e.Message);
                return null;
            }
            
        }

        public void putValue(string key, string value)
        {
            try{
                if(cacheDataRepo.addItem(key,value)){
                    evictionPolicy.keyAccessed(key);
                    return;
                }
                else{
                    string evcictedKey = evictionPolicy.evict();
                    cacheDataRepo.removeItem(evcictedKey);
                    Console.WriteLine("Evicted Key - "+evcictedKey);
                    cacheDataRepo.addItem(key,value);
                }
            }
            catch(Exception e){
                Console.WriteLine("Exception - "+e.Message);
                return;
            }

        }
    }
    public interface IEvictionPolicy
    {
        public void keyAccessed(string key);
        public string evict();
    }
    public class EvictionPolicy : IEvictionPolicy
    {
        private LinkedList<string> linkedListNode = new LinkedList<string>();
        private Dictionary<string,LinkedListNode<string>> linkedListNodeMap = new Dictionary<string, LinkedListNode<string>>();
        public string evict()
        {
            LinkedListNode<string> linkedListNodeElem;
            linkedListNodeElem = linkedListNode.First;
            if(linkedListNodeElem == null)return null;
            linkedListNode.Remove(linkedListNodeElem);
            linkedListNodeMap.Remove(linkedListNodeElem.Value);
            return linkedListNodeElem.Value;

        }

        public void keyAccessed(string key)
        {
            LinkedListNode<string> linkedListNodeElem;
            if(!linkedListNodeMap.ContainsKey(key)){
                linkedListNodeElem =linkedListNode.AddLast(key);
                linkedListNodeMap.Add(key,linkedListNodeElem);
            }
            else{
                linkedListNode.Remove(linkedListNodeMap[key]);
                linkedListNodeElem =linkedListNode.AddLast(key);
                linkedListNodeMap[key]=linkedListNodeElem;
            }
            return;
        }
    }
    public interface ICacheDataRepo
    {
        public bool addItem(string key, string value);
        public string getItem(string key);
        public void removeItem(string key);
    }
    public class CacheDataRepo : ICacheDataRepo
    {
        private Dictionary<string,string> cacheDatas = new Dictionary<string, string>();
        private int capacity;
        public CacheDataRepo(int capacity)
        {
            this.capacity = capacity;
        }
        public bool addItem(string key, string value)
        {
            Console.WriteLine("count - "+cacheDatas.Count);
            if(cacheDatas.Count >= capacity)return false;
            if(!cacheDatas.ContainsKey(key)) cacheDatas.Add(key,value);
            else cacheDatas[key] = value;   
            return true;  
        }

        public string getItem(string key)
        {
            if(!cacheDatas.ContainsKey(key)) throw new KeyNotFoundException(key+" does not exist");
            else return cacheDatas[key];  
        }
        public void removeItem(string key){
             if(!cacheDatas.ContainsKey(key)) throw new KeyNotFoundException(key+" does not exist");
            else cacheDatas.Remove(key);
        }
    }
    // //model
    // public class CacheData
    // {
    //     public string  key { get; set; }
    //     public string  value { get; set; }
    // }
}
