using System;
using System.Collections.Generic;

namespace Atm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IAtm atm = new Atm();
            var res = atm.insertCard("123");
            Console.WriteLine("res - "+res);
            res = atm.insertPin(321);
            Console.WriteLine("res - "+res);
            res = atm.checkBalance();
            Console.WriteLine("res - "+res);
            res = atm.withdrawMoney(700);
            Console.WriteLine("res - "+res);
            res = atm.cancel();
            Console.WriteLine("res - "+res);
        }
    }
    public interface IAtm{
        public string  insertCard(string cardNo);
        public string  insertPin(int pin);
        public string  checkBalance();
        public string  withdrawMoney(int amount);
        public string  cancel();
    }
    public class Atm : IAtm
    {
        private State state;
        private bool  isLoggedIn = false;
        private string cardNumber = null;
        private Dictionary<string,User> users = new Dictionary<string, User>();
        private CashBox cashBox = new CashBox();
        private List<Note> notes = new List<Note>();
        public Atm(){
            this.state = State.Available;
            User user1 = new User(){
                CardNumber = "123",Balance = 1000,Pin=321
            };
            User user2 = new User(){
                CardNumber = "234",Balance = 2000,Pin= 432
            };
            users.Add(user1.CardNumber,user1);
            users.Add(user2.CardNumber,user2);
            Note note1 = new Note(){
                Value = 100,Count=10
            };
             Note note2 = new Note(){
                Value = 500,Count=20
            };
            notes.Add(note1);
            notes.Add(note2);
            cashBox.Notes = notes;

        }
        public string checkBalance()
        {
            if(state == State.InUse){
                return users[this.cardNumber].Balance.ToString();
            }
            else return "Invalid State";
        }

        public string insertCard(string cardNo)
        {
            if(state == State.Available){
                if(users.ContainsKey(cardNo)){
                    this.cardNumber = cardNo;
                    this.state = State.InUse;
                    return "Card Inserted Successfully";
                }
                return "Inavlid Card";
            }
            else return "Invalid State";
        }

        public string insertPin(int pin)
        {
            if(state == State.InUse){
                if(users[cardNumber].Pin == pin){
                    this.isLoggedIn = true;
                    return "PIN Validated Successfully";
                }
                return "Inavlid Pin";
            }
            else return "Invalid State";
        }
        

        public string withdrawMoney(int amount)
        {
            if(state == State.InUse){
                User user = users[this.cardNumber];
                foreach (var item in notes)
                {
                    int tempCount = amount/item.Value;
                    if(tempCount > item.Count){
                        tempCount = item.Count;
                    }
                    amount -= item.Value*tempCount;
                    item.Count-=tempCount;
                }
                if(amount > 0)return "Insufficient Balalnce";
                return "Collect your cash";
                
            }
            else return "Invalid State";
        }
        public string  cancel(){
            this.state = State.Available;
            return "Seesion Cancelled";
        }
    }


    //Model
    public enum State{
        InUse,Available,OutOfService
    }
    public class Base{
        public string Id {get;set;} = Guid.NewGuid().ToString();
    }
    public class User{
        public string  CardNumber { get; set; }
        public int Balance { get; set; }
        public int Pin { get; set; }
    }
    public class CashBox {
        public List<Note> Notes { get; set; }
        public int TotalAmount { get; set; }
    }
    public class Note{
        public int Value { get; set; }
        public int Count { get; set; }
    }
}
