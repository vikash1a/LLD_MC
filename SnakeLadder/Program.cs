using System;
using System.Collections.Generic;
/*
next
    class for board and dice may be created for more flexiblity
*/
namespace SnakeLadder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ISnakeLadder snakeLadder = new SnakeLadder();
            snakeLadder.addPlayer(new Player(){Name = "vikash"});
            snakeLadder.addPlayer(new Player(){Name = "tom"});
            snakeLadder.start();
        }
    }
    public interface ISnakeLadder
    {
        public void start();
        public bool addPlayer(Player player);
    }
    public class SnakeLadder : ISnakeLadder{

        private Dictionary<string,Player> players;
        private Dictionary<int,Snake> snakes;
        private Dictionary<int,Stair> stairs;
        private State state;
        public SnakeLadder()
        {
            this.state = State.Free;
            this.players = new Dictionary<string,Player>();
            this.initialize();
        }
        private void initialize(){
            this.snakes = new Dictionary<int,Snake>();
            this.stairs = new Dictionary<int,Stair>();
            snakes.Add(4,new Snake(){StartPosition = 4, EndPosition=20});
            snakes.Add(30,new Snake(){StartPosition = 30, EndPosition=80});
            stairs.Add(43,new Stair(){StartPosition = 43, EndPosition=62});
            stairs.Add(14,new Stair(){StartPosition = 14, EndPosition=24});
            return;
        }
        
        public void start(){
            if(this.state != State.InProgress){
                this.state = State.InProgress;
                while(true){
                    int count = 0;
                    foreach (var elem in players)
                    {   
                        Player player = elem.Value;
                        if(player.Position != 100){
                            count++;
                            int val = new Random().Next(1,6);
                            if(player.Position + val > 100)continue;
                            player.Position +=val;
                            Console.WriteLine("palyer "+player.Name+" inital value "+player.Position);
                            if(snakes.ContainsKey(player.Position)){
                                player.Position = snakes[player.Position].EndPosition;
                            }
                            else if(stairs.ContainsKey(player.Position)){
                                player.Position = stairs[player.Position].EndPosition;
                            }
                            Console.WriteLine("palyer "+player.Name+" final value "+player.Position);
                            if(player.Position == 100){
                                Console.WriteLine("player "+player.Name+" won");
                            }
                        }
                    }
                    if(count<=1){
                        Console.WriteLine("game over");
                        this.state = State.Free;
                        players.Clear();
                        return;
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
                players.Add(player.Id, player);
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
        public int Position { get; set; } = 0;
    }
    public class Stair : Position{
       
    }
    public class Snake : Position{
       
    }
    public class Position{
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
    }
}
