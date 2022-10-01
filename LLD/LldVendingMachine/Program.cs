using System;
using System.Collections.Generic;

namespace LLD_CacheSystem
{
    class Program{
        static void Main(){
            Console.WriteLine("Program is working");
            DiContainer diContainer = new DiContainer();
            IVendingMachine vendingMachine = diContainer.vendingMachine;
            vendingMachine.SelectItem(1);
            vendingMachine.RecordPayment(new Payment(){id = 1, amount=100,payemntMethod="online"});
            vendingMachine.Checkout();
            // vendingMachine.SelectItem(1);
            return;
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
    //interface
    public interface IVendingMachine
    {
        public Item SelectItem(int slotId);
        public void RecordPayment(Payment payment);
        public void Checkout();
        public void Reset();
    }
    public class VendingMachineFreeState : IVendingMachine
    {
        private readonly ISlotRepo slotRepo;
        private readonly IItemRepo itemRepo;

        public VendingMachineFreeState(ISlotRepo slotRepo,IItemRepo itemRepo)
        {
            this.slotRepo = slotRepo;
            this.itemRepo = itemRepo;
        }
        public void Checkout()
        {
            throw new NotImplementedException();
        }

        public void RecordPayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Item SelectItem(int slotId)
        {
            Item selectedItem = null;
            Slot slot = slotRepo.GetSlot(slotId);
            if(slot.isEmpty){
                Console.WriteLine("Slot is Empty");
                return selectedItem;
            }
            else{
                selectedItem = itemRepo.GetItem(slot.itemId);
                slot.isEmpty=true;
                itemRepo.RemoveItem(slot.itemId);
                Console.WriteLine("item selected - "+slot.itemId);
                return selectedItem;
            }
        }
    }
    public class VendingMachineSelectedState : IVendingMachine
    {
        private readonly IPaymentRepo paymentRepo;

        public VendingMachineSelectedState(IPaymentRepo paymentRepo)
        {
            this.paymentRepo = paymentRepo;
        }
        public void Checkout()
        {
            throw new NotImplementedException();
        }

        public void RecordPayment(Payment payment)
        {
            paymentRepo.AddPayment(payment);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Item SelectItem(int slotId)
        {
            throw new NotImplementedException();
        }
    }
    public class VendingMachinePaymentDoneState : IVendingMachine
    {
        private readonly ISlotRepo slotRepo;
        private readonly int slotId;

        public VendingMachinePaymentDoneState(ISlotRepo slotRepo, int slotId)
        {
            this.slotRepo = slotRepo;
            this.slotId = slotId;
        }
        public void Checkout()
        {
            slotRepo.RemoveSlot(slotId);
        }

        public void RecordPayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Item SelectItem(int slotId)
        {
            throw new NotImplementedException();
        }
    }
    //implementaion
    public class VendingMachine : IVendingMachine
    {
        
        private  IItemRepo itemRepo;
        private  ISlotRepo slotRepo;
        private  IPaymentRepo paymentRepo;
        private IVendingMachine vendingMachineState;
        private int slotId;
        
        public VendingMachine (IItemRepo itemRepo,ISlotRepo slotRepo,IPaymentRepo paymentRepo){
            this.itemRepo = itemRepo;
            this.slotRepo = slotRepo;
            this.paymentRepo = paymentRepo;
            this.seedData();
            this.vendingMachineState = new VendingMachineFreeState(slotRepo,itemRepo);
        }
        private void seedData(){
            itemRepo.AddItem(new Item(){id =1,price = 20,name="coke"});
            itemRepo.AddItem(new Item(){id =2,price = 30,name="chips"});
            slotRepo.AddSlot(new Slot(){id =1,itemId=1,isEmpty=false});
            slotRepo.AddSlot(new Slot(){id =2,itemId=2,isEmpty=false});
            return;
        }
        public Item SelectItem(int slotId){
            this.slotId = slotId;
            var result =  this.vendingMachineState.SelectItem(slotId);
            this.vendingMachineState = new VendingMachineSelectedState(paymentRepo);
            return result;
            
        }
        public void RecordPayment(Payment payment){
            this.vendingMachineState.RecordPayment(payment);
            Console.WriteLine("payment done");
            this.vendingMachineState = new VendingMachinePaymentDoneState(slotRepo,slotId);
        }
        public void Checkout(){
            this.vendingMachineState.Checkout();
            Console.WriteLine("checkout done");
            this.Reset();
            this.vendingMachineState = new VendingMachineFreeState(slotRepo,itemRepo);
        }
        public void Reset(){
            slotId = 0;
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
