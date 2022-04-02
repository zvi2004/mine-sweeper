using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/
namespace project
{

    class Program
    {
        public static void Main(string[] args)
        {
            int arraySize = 0, mineAmount = 0; //Initial lines
            Panel[,] myArray;
            bool over = false;

            Console.Write("enter board size: ");//getting the size of the board and the amount of bombs 
            arraySize = int.Parse(Console.ReadLine());
            Console.Write("enter bomb amount: ");
            mineAmount = int.Parse(Console.ReadLine());
            myArray = new Panel[arraySize + 2, arraySize + 2];

            generateBoard(myArray, arraySize, mineAmount);// generating the board with the size and the random bombs
            while (!over)
            {
                printBoard(myArray, arraySize);//print the board
                over = turn(myArray); //a turn in the game(let you choce the spot and what to do) and checking if the player lost
                if (!over)
                    over = ifOver(myArray, mineAmount, arraySize);//checking if the game is over by if the player won
            }
            mineAmount = int.Parse(Console.ReadLine());
        }

        /*
        summery:the function creat a board 
        input:Panel[,] myArray, int size, int bombAmount
        output: board
        */
        public static void generateBoard(Panel[,] myArray, int size, int bombAmount)
        {
            for (int i = 0; i < size + 2; i++)
            {
                for (int j = 0; j < size + 2; j++)
                {
                    if (i == 0 || i == size + 1 || j == 0 || j == size + 1)
                    {
                        myArray[i, j] = new Panel(-20); //the board has edge that is not part of the game (is there for the zero func)
                    }
                    else
                    {
                        myArray[i, j] = new Panel(0);//reset the spot to 0
                    }

                }
            }
            generateMines(myArray, size, bombAmount); //generating the bombs randomly 
            generateNumbers(myArray, size); //generating the numbers arrond the bombs
        }
        /*
        summery:the function get the panel place of the turn from user
        input:Panel[,] myArray
        output: true if choose number and false if not and none if use flag or remove flag
        */
        public static bool turn(Panel[,] myArray)
        {
            int x = 0, y = 0, FOR = 0;//FOR is flag or reveal

            Console.Write("enter x: ");//getting the spot on the board
            x = int.Parse(Console.ReadLine());
            Console.Write("enter y: ");
            y = int.Parse(Console.ReadLine());

            Console.Write("1 to reveal\n2 to add flag\n3 to delete flag\nany other number to cancel X and y");
            FOR = int.Parse(Console.ReadLine()); //chocing what to do

            if (FOR == 1)// if the player wants to reveal a spot 
            {
                return checkPlace(myArray, x, y); //return itsOver if the player lose
            }
            else if (FOR == 2 && !myArray[x, y].getIsRevealed())// if the player wants to add a flag
            {
                myArray[x, y].setIsFlagged(true);
            }
            else if (FOR == 3)// if the player wants to remove a flag
            {
                myArray[x, y].setIsFlagged(false);
            }
            return false; // the game is not over
        }

        /*
        summery:the function check if the player revealed all the numbers
        input:Panel[,] myArray, int minesNum,int size
        output: if won print wining message and return true
        */
        public static bool ifOver(Panel[,] myArray, int minesNum, int size)
        {
            int count = 0;
            for (int i = 1; i < size + 1; i++)
            {
                for (int j = 1; j < size + 1; j++)
                {
                    if (myArray[i, j].getIsRevealed() == true && myArray[i, j].getNum() != -1)
                    {
                        count++; //if the spot is not a bomb and reveld so it is counting that
                    }
                }
            }
            if (count + minesNum == size * size) //if all non bombs spot was reveld 
            {
                Console.WriteLine("you won");
                return true;//the game is over
            }
            return false;// the game is not over

        }
        /*
        summery:the function generate numbers around bombs
        input:Panel[,] myArray, int size
        output: add 1 around the block of bomb(not including another bomb if there is)
        */
        public static void generateNumbers(Panel[,] myArray, int size)
        {
            for (int i = 1; i < size + 1; i++) // go over all spots
            {
                for (int j = 1; j < size + 1; j++)
                {
                    if (myArray[i, j].getNum() == -1) //if the spot is over (-1 = bomb)
                    {
                        if (myArray[i - 1, j].getNum() != -1)//checking if the spot is a bomb if not adding 1 to the bomb around
                        {
                            myArray[i - 1, j].setNum(myArray[i - 1, j].getNum() + 1);
                        }
                        if (myArray[i + 1, j].getNum() != -1)//checking if the spot is a bomb if not adding 1 to the bomb around
                        {
                            myArray[i + 1, j].setNum(myArray[i + 1, j].getNum() + 1);
                        }
                        if (myArray[i, j - 1].getNum() != -1)//checking if the spot is a bomb if not adding 1 to the bomb around
                        {
                            myArray[i, j - 1].setNum(myArray[i, j - 1].getNum() + 1);
                        }
                        if (myArray[i, j + 1].getNum() != -1)//checking if the spot is a bomb if not adding 1 to the bomb around
                        {
                            myArray[i, j + 1].setNum(myArray[i, j + 1].getNum() + 1);
                        }
                        if (myArray[i - 1, j + 1].getNum() != -1)//checking if the spot is a bomb if not adding 1 to the bomb around
                        {
                            myArray[i - 1, j + 1].setNum(myArray[i - 1, j + 1].getNum() + 1);
                        }
                        if (myArray[i - 1, j - 1].getNum() != -1)//checking if the spot is a bomb if not adding 1 to the bomb around
                        {
                            myArray[i - 1, j - 1].setNum(myArray[i - 1, j - 1].getNum() + 1);
                        }
                        if (myArray[i + 1, j + 1].getNum() != -1)//checking if the spot is a bomb if not adding 1 to the bomb around
                        {
                            myArray[i + 1, j + 1].setNum(myArray[i + 1, j + 1].getNum() + 1);
                        }
                        if (myArray[i + 1, j - 1].getNum() != -1)//checking if the spot is a bomb if not adding 1 to the bomb around
                        {
                            myArray[i + 1, j - 1].setNum(myArray[i + 1, j - 1].getNum() + 1);
                        }
                    }
                }
            }
        }
        /*
        summery:the function generate mines on different places on board
        input:Panel[,] myArray, int size, int bombAmount
        output: place mines on board
        */
        public static void generateMines(Panel[,] myArray, int size, int bombAmount)
        {
            int x = 0, y = 0;
            Random rnd = new Random();
            for (int i = 0; i < bombAmount; i++)//can change based on bonus
            {
                do
                {
                    x = rnd.Next(1, size + 1);//random x
                    y = rnd.Next(1, size + 1);//random y
                } while (myArray[x, y].getNum() == -1); //if it is not allready a bomb
                myArray[x, y].setNum(-1); //its now a bomb
            }
        }
        /*
        summery:the function check if panel is zero and if it is reveal all around him and check the others 0 around
        input:Panel[,] myArray, int x ,int y
        output:reveal all the connected 0
        */
        public static void zero(Panel[,] myArray, int x, int y, bool isFrist)
        {
            if (myArray[x, y].getNum() == 0 && (!myArray[x, y].getIsRevealed() || isFrist))//1
            {
                myArray[x - 1, y].setIsRevealed(true);//set panel next to zero to be revealed                
                myArray[x + 1, y].setIsRevealed(true);//set panel next to zero to be revealed                
                myArray[x, y - 1].setIsRevealed(true);//set panel next to zero to be revealed               
                myArray[x, y + 1].setIsRevealed(true);//set panel next to zero to be revealed                
                myArray[x - 1, y + 1].setIsRevealed(true);//set panel next to zero to be revealed               
                myArray[x - 1, y - 1].setIsRevealed(true);//set panel next to zero to be revealed                
                myArray[x + 1, y + 1].setIsRevealed(true);//set panel next to zero to be revealed               
                myArray[x + 1, y - 1].setIsRevealed(true);//set panel next to zero to be revealed
                zero(myArray, x - 1, y, false);//call func zero to check if panel is zero and to reveal it
                zero(myArray, x + 1, y, false);//call func zero to check if panel is zero and to reveal it
                zero(myArray, x, y - 1, false);//call func zero to check if panel is zero and to reveal it
                zero(myArray, x, y + 1, false);//call func zero to check if panel is zero and to reveal it
                zero(myArray, x - 1, y + 1, false);//call func zero to check if panel is zero and to reveal it
                zero(myArray, x - 1, y - 1, false);//call func zero to check if panel is zero and to reveal it
                zero(myArray, x + 1, y + 1, false);//call func zero to check if panel is zero and to reveal it
                zero(myArray, x + 1, y - 1, false);//call func zero to check if panel is zero and to reveal it
            }
        }
        /*
        summery:check what the choosen number is
        input:Panel[,] myArray, int i, int j
        output: end game if bomb and reveal the selected panel
        */
        public static bool checkPlace(Panel[,] myArray, int i, int j)
        {
            if (myArray[i, j].getNum() == -1 && myArray[i, j].getIsFlagged() == false) //if it is a bomb and its not flaged
            {
                myArray[i, j].setIsRevealed(true);
                Console.WriteLine("you are a failure"); //the player lost
                return true; //the game is over
            }
            else if (myArray[i, j].getNum() != -1 && myArray[i, j].getIsFlagged() == false)//if it is not a bomb and its not a flaged
            {
                myArray[i, j].setIsRevealed(true);//the spot is revealed
                zero(myArray, i, j, true);//checking if it is zero to reveal all of the 0
            }
            else if (myArray[i, j].getIsFlagged() == true) //if it is flaged
            {
                Console.WriteLine("flaged");//do not do anything
            }
            else if (myArray[i, j].getIsRevealed() == true)//if it is already revealed
            {
                Console.WriteLine("already revealed");//do not do anything
            }
            return false;//the game is not over
        }
        /*
        summery:the function print the board where IsFlagged==true
        input: Panel[,] myArray,size
        output: print board
        */
        public static void printBoard(Panel[,] myArray, int size)
        {
            for (int x = 1; x < size + 1; x++)
            {
                for (int y = 1; y < size + 1; y++)
                {
                    if (myArray[x, y].getNum() == 1 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==1
                    {
                        Console.Write("|1|");
                    }
                    else if (myArray[x, y].getNum() == 2 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==2
                    {
                        Console.Write("|2|");
                    }
                    else if (myArray[x, y].getNum() == 3 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==3
                    {
                        Console.Write("|3|");
                    }
                    else if (myArray[x, y].getNum() == 4 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==4
                    {
                        Console.Write("|4|");
                    }
                    else if (myArray[x, y].getNum() == 5 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==5
                    {
                        Console.Write("|5|");
                    }
                    else if (myArray[x, y].getNum() == 6 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==6
                    {
                        Console.Write("|6|");
                    }
                    else if (myArray[x, y].getNum() == 7 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==7
                    {
                        Console.Write("|7|");
                    }
                    else if (myArray[x, y].getNum() == 8 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==8
                    {
                        Console.Write("|8|");
                    }
                    else if (myArray[x, y].getNum() == -1 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==bomb
                    {
                        Console.Write("|*|");
                    }
                    else if (myArray[x, y].getIsFlagged() == true)//if num==flag
                    {
                        Console.Write("|f|");
                    }
                    else if (myArray[x, y].getNum() == 0 && myArray[x, y].getIsFlagged() == false && myArray[x, y].getIsRevealed())//if num==bomb
                    {
                        Console.Write("|0|");
                    }
                    else
                    {
                        Console.Write("| |");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("------------------------------------");
            }
        }


    }
}


