//Improved Solution
// next step 
/*
create a new model - vending machine-> state,cash,name,id
*/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LLD_CacheSystem
{
    class Program{
        static void Main(){
            Console.WriteLine("Program is working");
            DiContainer diContainer = new DiContainer();
            IVendingMachine vendingMachine = diContainer.vendingMachine;
            //IVendingMachine vendingMachine = new VendingMachine();
            vendingMachine.SelectItem(1);
            vendingMachine.SelectItem(1);
            return;
        }
        private async Task<string> callApiAsync(){
            var client = new HttpClient();
            //
            var url = "";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,url);
            httpRequestMessage.Headers.Add("k","v");
            var resp = await client.SendAsync(httpRequestMessage);
            var mes = resp.Content.ReadAsStringAsync();

            //post
              url = "";
             httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,url);
             StringContent stringContent  =  new StringContent("",Encoding.UTF8,"application/json");
             httpRequestMessage.Content = stringContent;
            httpRequestMessage.Headers.Add("k","v");
             resp = await client.SendAsync(httpRequestMessage);
             mes = resp.Content.ReadAsStringAsync();

            return "";


        }
    }

    //
    public class DiContainer{
        public  IItemRepo itemRepo ;
        public  ISlotRepo slotRepo ;
        public  IPaymentRepo paymentRepo;
        public  IVendingMachine vendingMachine ;
        public DiContainer(){
            itemRepo = new ItemRepo();
            slotRepo = new SlotRepo();
            paymentRepo = new PaymentRepo();
            vendingMachine = new VendingMachine(itemRepo,slotRepo,paymentRepo);
        }

    }
    //
    public enum State{
        Free ,
        ItemSelected ,
        PaymentDone 
    }

    //interface
    public interface IVendingMachine
    {
        public void SelectItem(int slotId);
        public void RecordPayment(Payment payment);
        public void Checkout();
        public void Reset();
    }

    //implementaion
    public class VendingMachine : IVendingMachine
    {
        
        private  IItemRepo itemRepo;
        private  ISlotRepo slotRepo;
        private  IPaymentRepo paymentRepo;
        private State state ;
        private Item selectedItem = null;
        
        public VendingMachine (IItemRepo itemRepo,ISlotRepo slotRepo,IPaymentRepo paymentRepo){
            this.itemRepo = itemRepo;
            this.slotRepo = slotRepo;
            this.paymentRepo = paymentRepo;
            this.seedData();
            this.state = State.Free;
        }
        private void seedData(){
            itemRepo.AddItem(new Item(){id =1,price = 20,name="coke"});
            itemRepo.AddItem(new Item(){id =2,price = 30,name="chips"});
            slotRepo.AddSlot(new Slot(){id =1,itemId=1,isEmpty=false});
            slotRepo.AddSlot(new Slot(){id =2,itemId=2,isEmpty=false});
            return;
        }
        public void SelectItem(int slotId){
            if(state == State.Free){
                Slot slot = slotRepo.GetSlot(slotId);
                if(slot.isEmpty){
                    Console.WriteLine("Slot is Empty");return;
                }
                else{
                    selectedItem = itemRepo.GetItem(slot.itemId);
                    slot.isEmpty=true;
                    itemRepo.RemoveItem(slot.itemId);
                    Console.WriteLine("item selected - "+slot.itemId);
                    return;
                }

            }
            else{
                Console.WriteLine("Vedning machine is not free");
            }
            return;
        }
        public void RecordPayment(Payment payment){
            return;
        }
        public void Checkout(){
            return;
        }
        public void Reset(){
            return;
        }
        
    }

    //repository
    public interface IItemRepo{
        public void AddItem(Item item);
        public void RemoveItem(int itemId);
        public Item GetItem(int itemId);
    }
    public class ItemRepo : IItemRepo{
        private Dictionary<int,Item> items = new Dictionary<int, Item>();
        public void AddItem(Item item){
            items.Add(item.id,item);return;
        }
        public void RemoveItem(int itemId){
            items.Remove(itemId);return;
        }
        public Item GetItem(int itemId){
            return items[itemId];
        }
    }


    public interface ISlotRepo{
        public void AddSlot(Slot slot);
        public void RemoveSlot(int slotId);
        public Slot GetSlot(int slotId);
    }
    public class SlotRepo : ISlotRepo{
        private Dictionary<int,Slot> slots = new Dictionary<int, Slot>();
        public void AddSlot(Slot slot){
            slots.Add(slot.id,slot);return;
        }
        public void RemoveSlot(int slotId){
            slots.Remove(slotId);return;
        }
        public Slot GetSlot(int slotId){
            return slots[slotId];
        }
    }

    public interface IPaymentRepo{
        public void AddPayment(Payment payment);
        public void RemovePayment(int paymentId);
    }
    public class PaymentRepo : IPaymentRepo{
        private Dictionary<int,Payment> payments = new Dictionary<int, Payment>();
        public void AddPayment(Payment payment){
            payments.Add(payment.id,payment);return;
        }
        public void RemovePayment(int paymentId){
            payments.Remove(paymentId);return;
        }
    }


    //models
    //item, payment, Slot
    public class Item{
        public int id {get;set;}
        public string name {get;set;}
        public int price {get;set;}
    }
    public class Slot{
        public int id {get;set;}
        public int itemId {get;set;}
        public bool isEmpty {get;set;}
    }
    public class Payment{
        public int id {get;set;}
        public int amount {get;set;}
        public string payemntMethod {get;set;}
    }
}




