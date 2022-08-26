using System;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");

//Orchestrator
public class ExpenseManager{
    public bool AddExpense(){
        
        return true;
    }
}
//Service

public class ExpenseService{

}

//Model
public class BaseModel{
    public string  Id  { get; set; } =  Guid.NewGuid().ToString();
}
public class User : BaseModel{
    public string Name { get; set; }
    public int TotalDues { get; set; }
}
public class Group : BaseModel{
    public string Name { get; set; }
}
public class UserGroupJunction : BaseModel{
    public User User { get; set; }
    public Group Group { get; set; }
}
public class Split : BaseModel{
    public User User { get; set; }
}
public class ExactSplit : Split{
    public int Amount { get; set; }
}
public class EqualSplit : Split{
}
public class PercentageSplit : Split{
    public Double Percentage { get; set; }
}
public class Expense : BaseModel{
    public User PaidByUser { get; set; }
    public int Amount { get; set; }
    public List<Split> Splits { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public bool Validate(){
        switch (ExpenseType)
        {
            case ExpenseType.EXACT:
                int tempSum = 0;
                foreach(Split split in  Splits){
                    ExactSplit exactSplit = (ExactSplit)split;
                    tempSum+= exactSplit.Amount;
                }
                if(tempSum!=Amount)return false;
                break;
            case ExpenseType.PERCENTAGE:
                double tempPercentage = 0;
                foreach(Split split in  Splits){
                    PercentageSplit percentageSplit = (PercentageSplit)split;
                    tempPercentage += percentageSplit.Percentage;
                }
                if(tempPercentage!=100)return false;
                break;
            case ExpenseType.EQUAL:
                return true;
        }
        return true;
    }
}
public enum ExpenseType{
    EXACT,
    EQUAL,
    PERCENTAGE
}
