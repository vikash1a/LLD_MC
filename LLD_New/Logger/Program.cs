using System;
using System.Collections.Generic;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ILogger logger = Logger.getSingeltonInstance();
            logger.subscribe(Severvity.Info,new ConsoleListenener());
            logger.subscribe(Severvity.Info,new DbListenener());
            logger.log(Severvity.Info,"Hello, This is info log");
        }
    }
    public interface ILogger{
        public void log(Severvity severvity,string message);
        public void subscribe(Severvity severvity, IListener listener);    
        public void unSubscribe(Severvity severvity, IListener listener);   
    }
    public class Logger : ILogger{
        Dictionary<Severvity, List<IListener>> dict;
        private static Logger loggerSingelton = new Logger();
        private Logger(){
            dict = new Dictionary<Severvity, List<IListener>>();
        }
        public static Logger getSingeltonInstance(){
            return loggerSingelton;
        }

        public void log(Severvity severvity,string message){
            foreach (var elem in dict[severvity]){
                elem.update(severvity,message);
            }
        }
        public void subscribe(Severvity severvity, IListener listener){
            if(dict.ContainsKey(severvity)){
                dict[severvity].Add(listener);
            }
            else dict.Add(severvity,new List<IListener>(){listener});
            
        }  
        public void unSubscribe(Severvity severvity, IListener listener){
            dict[severvity].Remove(listener);
        } 
    }
    public interface IListener{
        public void update(Severvity severvity, string message);
    }

    public class ConsoleListenener : IListener{
        public void update(Severvity severvity, string message){
            Console.WriteLine(severvity.ToString()+" - "+message);
        }
    }
    public class DbListenener : IListener{
        public void update(Severvity severvity, string message){
            Console.WriteLine(severvity.ToString()+" db- "+message);
        }
    }
    public enum Severvity{
        Info,
        Error,
        Debug
    }


}
