using System;
using System.Collections.Generic;

namespace Chess
{
    public class Driver{
        public void test(){
            IChessApp chessApp = new ChessApp();
            chessApp.start();
        }
    }
    public interface IChessApp{
        public void start();
    }
    public class ChessApp : IChessApp{
        IDbManager dbManager;
        public ChessApp(){
            dbManager = new DbManager();
            intialize();
        }   
        public void start(){
            Console.WriteLine("Let's Play.");
            while(true){
                if(!processMove("Player1"))break;
                if(!processMove("Player2"))break;
            }
        }
        bool processMove(string player){
            KeyValuePair<int,int> initialPosition, finalPosition;
            Console.WriteLine(player+", Enter your move - ");
            string move = Console.ReadLine();
            string[] movesList= move.Split(" ");
            initialPosition = new KeyValuePair<int, int>(int.Parse(movesList[0]),int.Parse(movesList[1]));
            finalPosition = new KeyValuePair<int, int>(int.Parse(movesList[2]),int.Parse(movesList[3]));
            if(!dbManager.checkMoveAllowed(initialPosition,finalPosition,player)){
                Console.WriteLine("This move not allowed.");
                return processMove(player);
            }
            dbManager.updatePiecePosition(initialPosition,finalPosition);
            if(isKingCheck()){
                Console.WriteLine("Your king is check.");
            }
            if(isKingCheckMate()){
                Console.WriteLine("Your king is checkMate,"+ player+" Won");
                return false;
            }
            return true;
        }
        void intialize(){
            //add all pieces with initial position
        }
        bool isKingCheck(){
            throw new NotImplementedException();
        }
        bool isKingCheckMate(){
            throw new NotImplementedException();
        }
    }
    //Db
    public interface IDbManager{
        public string addPiece(KeyValuePair<int, int> kvp, IPiece piece);
        public string removePiece(KeyValuePair<int, int> kvp);
        public string updatePiecePosition(KeyValuePair<int, int> initalPosition, KeyValuePair<int, int> finalPosition);
        public KeyValuePair<int, int> getPiecePosition(KeyValuePair<int, int> kvp);
        public bool checkMoveAllowed(KeyValuePair<int, int> initalPosition, KeyValuePair<int, int> finalPosition,string Player);
    }

    public class DbManager : IDbManager{
        Dictionary<KeyValuePair<int,int>,IPiece> pieceDict;
        public DbManager(){
            pieceDict = new Dictionary<KeyValuePair<int, int>, IPiece>();
        }

        public string addPiece(KeyValuePair<int, int> kvp, IPiece piece)
        {
            throw new NotImplementedException();
        }

        public bool checkMoveAllowed(KeyValuePair<int, int> initalPosition, KeyValuePair<int, int> finalPosition, string Player)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<int, int> getPiecePosition(KeyValuePair<int, int> kvp)
        {
            throw new NotImplementedException();
        }

        public string removePiece(KeyValuePair<int, int> kvp)
        {
            throw new NotImplementedException();
        }

        public string updatePiecePosition(KeyValuePair<int, int> initalPosition, KeyValuePair<int, int> finalPosition)
        {
            throw new NotImplementedException();
        }
    }
    //Model
    public interface IPiece{
        public bool isAllowed(KeyValuePair<int, int> position);
        public bool updatePosition(KeyValuePair<int, int> position);
    }
    public class Knight : IPiece
    {
        KeyValuePair<int, int> currentPosition;

        public bool isAllowed(KeyValuePair<int, int> position)
        {
            throw new NotImplementedException();
        }

        public bool updatePosition(KeyValuePair<int, int> position)
        {
            throw new NotImplementedException();
        }
    }
}
