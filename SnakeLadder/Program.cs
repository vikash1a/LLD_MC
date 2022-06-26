using System;
using System.Math;
using System.Collections.Generic;

namespace SnakeLadder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    public interface ISnakeLadder
    {
        public void start();
        public bool addPlayer(Player player);
    }
    public class SnakeLadder : ISnakeLadder{

        private Dictionary<string,Player> players;
        private Dictionary<string,Snake> snakes;
        private Dictionary<string,Stair> stairs;
        private State state;
        public SnakeLadder(int numberOfPlayer)
        {
            this.state = State.Free;
            this.players = new Dictionary<string,Player>();
            this.initialize();
        }
        private void initialize(){
            this.snakes = new Dictionary<string,Snake>();
            this.stairs = new Dictionary<string,Stair>();
            snakes.Add(4,new Snake(){StartPosition = 4, EndPosition=20});
            snakes.Add(30,new Snake(){StartPosition = 30, EndPosition=80});
            stairs.Add(43,new Stair(){StartPosition = 43, EndPosition=62});
            stairs.Add(14,new Stair(){StartPosition = 14, EndPosition=24});
            return;
        }
        
        public void start(){
            if(this.state != State.InProgress){
                this.State = State.InProgress;
                while(1){
                    int count = 0;
                    foreach (Player player in players)
                    {
                        if(player.Position != 100){
                            count++;
                            int val = new Random().Next(1,6);
                            player.Position +=val;
                            
                        }
                    }
                    if(count<=1){
                        Console.WriteLine("game over");
                    }
                }
            }
            else{
                Console.WriteLine("try later");
                return;
            }
        }
        public bool addPlayer(Player player){
            if(this.state == State.InProgress){
                Console.WriteLine("game already in progress, can't add player now");
                return false;
            }
            else{
                this.state = State.Initiated;
                players.Add(player.id, Player);
                return true;
            }
        }
    }
    //Model
    public enum State
    {
        InProgress,
        Free,
        Initiated
    }
    public class BaseClass{
        public string  Id { get; set; } = Guid.NewGuid().ToString();
    }
    public class Player : BaseClass{
        public string Name { get; set; }
        public int Position { get; set; }
    }
    public class Stair : Position{
       
    }
    public class Snake : Position{
       
    }
    public class Positions{
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
    }
}
