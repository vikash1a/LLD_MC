// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Driver driver = new Driver();
// driver.play(3,new List<string>(){"2 2","1 3","1 1","1 2","2 2","3 3"});
driver.play(3,new List<string>(){"2 3","1 2","2 2","2 1","1 1","3 3","3 2","3 1","1 3"});
public class Driver{
    public Driver()
    {
        
    }
    public bool play(int size, List<string> moves){
        Board board = new Board(size);
        char c = 'X';
        foreach(string s in moves){
            string [] sl = s.Split(' ');
            int i = int.Parse(sl[0])-1, j = int.Parse(sl[1])-1;
            c = c=='X'?'o':'X';
            if(!board.playNext(i,j,c))return true;
        }
        Console.WriteLine("Input Exhausted, Game Over");
        return true;
    }
}
// public class Player{
//     public string Name { get; set; }
//     public char CharIdentifier { get; set; }
// }
public class Board{
    List<List<char>> boardValues;
    int size;
    int count = 0;
    public Board(int size)
    {
        boardValues = new List<List<char>>();
        for(int i=0;i<=size-1;i++){
            List<char> list = new List<char>();
            for(int j=0;j<=size-1;j++){
                list.Add(' ');
            }
            boardValues.Add(list);
        }
        this.size = size;
    }
    public bool playNext(int i,int j, char c){
        count++;
        bool result = fill(i,j,c);
        print();
        if(!result){
            Console.WriteLine("Inavlid Input");
            return true;
        }
        if(ifPlayerWin(i,j,c)){
            Console.WriteLine("Player with idetifier "+c+" Won, Game Over");
            return false;
        }
        if(count == (size*size)){
            Console.WriteLine("Board Completely Filled , Game Over");
            return false;
        }
        return true;
    }
    private void print(){
        for(int i=0;i<=size-1;i++){
            for(int j=0;j<=size-1;j++){
                Console.Write(boardValues[i][j]+"-");
            }
            Console.WriteLine();
        }
        Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ");
    }
    private bool fill(int i,int j, char c){
        if(!validateInput(i,j))return false;
        boardValues[i][j] = c;
        return true;
    }
    private bool validateInput(int i, int j){
        if(i<0 || i>size-1 || j<0 || j>size-1 || boardValues[i][j]!=' ')return false;
        return true;
    }
    private bool ifPlayerWin(int i,int j,char c){
        for(int j1 = 0;j1<=size-1;j1++){
            if(boardValues[i][j1]!=c)break;
            if(j1==size-1)return true;
        }
        for(int i1 = 0;i1<=size-1;i1++){
            if(boardValues[i][i1]!=c)break;
            if(i1==size-1)return true;
        }
        if(i == j){
            int k =0;
            while(k<=size-1){
                if(boardValues[k][k]!=c)break;
                if(k==size-1)return true;
                k++;
            }
        }
        if(i == size-1-j){
            int k =0;
            while(k<=size-1){
                if(boardValues[k][size-1-k]!=c)break;
                if(k==size-1)return true;
                k++;
            }
        }
        return false;
    }
}
