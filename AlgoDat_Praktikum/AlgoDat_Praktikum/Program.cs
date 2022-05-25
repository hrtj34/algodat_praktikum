using System;

namespace AlgoDat_Praktikum
{
    class Program
    {
        static bool[] checkQuestions()
        {
            bool[] ans = new bool[2];
            Console.WriteLine("Do you want to be able to store the same element more than once? (y/n)?");
            ans[0] = Console.ReadLine() == "y";
            Console.WriteLine("Do you want your data to be sorted? (y/n)");
            ans[1] = Console.ReadLine() == "n";
            return ans;
        } 
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            // HashUserInterface a = new HashUserInterface();
            // a.ConsoleStartScreen();

            #region Treap Tests
            //Treap myTreap = new Treap();

            //myTreap.insert(7);
            //myTreap.insert(5);
            //myTreap.insert(3);
            //myTreap.insert(6);
            //myTreap.insert(1);
            //myTreap.insert(11);
            //myTreap.insert(14);
            //myTreap.insert(8);
            //myTreap.insert(15);
            //myTreap.insert(12);
            //myTreap.printHorizontal();
            //myTreap.insert(13);

            //Console.WriteLine();

            //myTreap.delete(13);

            //myTreap.printHorizontal();
            #endregion

            Console.WriteLine("Welcome to our programm! It lets you store your data in all our sorts of different structures.");
            Console.WriteLine("You can choose from the following:\n");
            Console.WriteLine("1 Array");
            Console.WriteLine("2 List");
            Console.WriteLine("3 Hash Table");
            Console.WriteLine("4 Binary Search Tree");
            Console.WriteLine("5 AVL - Tree");
            Console.WriteLine("6 Treap\n");
            IDictionary structure = null;
            string res;

            while (true)
            //keeps trying until no error has occured
            {
                try
                {
                    do
                    {
                        Console.Write("Please enter the number of your choice: ");
                        res = Console.ReadLine();
                        switch (res)
                        {
                            case "1":
                                bool[] ans = checkQuestions();
                                Console.WriteLine("How long do you want your array to be? ");
                                int length = Convert.ToInt32(Console.ReadLine());
                                if (ans[0])
                                {
                                    if (ans[1])
                                    {
                                        structure = new MultiSetUnsortedArray(length);
                                        Console.WriteLine("You chose an unsorted array multi set.");
                                    }
                                    else
                                    {
                                        structure = new MultiSetSortedArray(length);
                                        Console.WriteLine("You chose a sorted array multi set.");
                                    }
                                }
                                else
                                {
                                    if (ans[1])
                                    {
                                        structure = new SetUnsortedArray(length);
                                        Console.WriteLine("You chose an unsorted array set.");
                                    }
                                    else
                                    {
                                        structure = new SetSortedArray(length);
                                        Console.WriteLine("You chose an sorted array set.");
                                    }
                                }
                                break;
                            case "2":
                                ans = checkQuestions();
                                if (ans[0])
                                {
                                    if (ans[1])
                                    {
                                        structure = new MultiSetUnsortedLinkedList();
                                        Console.WriteLine("You chose an unsorted linked list multi set.");
                                    }
                                    else
                                    {
                                        structure = new MultiSetSortedLinkedList();
                                        Console.WriteLine("You chose a sorted linked list mulit set.");
                                    }
                                }
                                else
                                {
                                    if (ans[1])
                                    {
                                        structure = new SetUnsortedLinkedList();
                                        Console.WriteLine("You chose an unsorted linked list set.");
                                    }
                                    else
                                    {
                                        structure = new MultiSetSortedLinkedList();
                                        Console.WriteLine("You chose a sorted linked list set.");
                                    }
                                }
                                break;
                            case "3":
                                Console.Write("For QuadProb, please press Q. For SepChain, please press S: ");
                                if (Console.ReadLine().ToLower() == "q")
                                {
                                    Console.Write("\nFor Div, please press D. For Mult, please press M: ");
                                    if (Console.ReadLine().ToLower() == "d")
                                    {
                                        structure = new HashTabQuadProb();
                                    }
                                    else
                                    {
                                        structure = new HashTabQuadProb(10, new HashMult(50,10));
                                    }
                                }
                                else
                                {
                                    Console.Write("\nFor Div, please press D. For Mult, please press M: ");
                                    if (Console.ReadLine().ToLower() == "d")
                                    {
                                        structure = new HashTabSepChain<SetUnsortedLinkedList>();
                                    }
                                    else
                                    {

                                        //I don't know which hash type this is
                                    }
                                }
                                Console.WriteLine();
                                Console.WriteLine("You chose a Hash table.");
                                break;
                            case "4":
                                structure = new BinSearchTree();
                                Console.WriteLine("You chose a Binary Search Tree.");
                                break;
                            case "5":
                                structure = new AVLTree();
                                Console.WriteLine("You chose an AVL-Tree.");
                                break;
                            case "6":
                                structure = new Treap();
                                Console.WriteLine("You chose a Treap.");
                                break;
                            default:
                                Console.WriteLine("Sorry, we didn't recognize that input. Please try again!");
                                res = "y";
                                break;
                        }
                        if (res != "y")
                        {
                            Console.Write("Do you want to change your choice? (y/n) ");
                            res = Console.ReadLine();
                        }
                    } while (res != "n");
                    break;
                }
                catch
                {
                    Console.WriteLine("An unforeseen error has occured. Please try again!");
                }
            }

            Console.WriteLine("Wonderful! Now you can work with your chosen data structure.");
            Console.WriteLine("Press enter to continue.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ; //waits for enter to be pressed without displaying input
            Console.Clear();

            bool proceed = true;
            int data;
            string command;
            while (true)
            //keeps trying until no error has occured
            {
                try
                {

                    do
                    {
                        Console.WriteLine("\nYou can insert or delete data or you can search for data you already stored.");
                        Console.WriteLine("To do this, please first enter which command you wish to use. Then enter your data in the next line.");
                        Console.WriteLine("To exit the programm, please enter exit.");
                        Console.Write("\nPlease enter a command: ");
                        command = Console.ReadLine().ToLower();

                        switch (command)
                        {
                            case "insert":
                                Console.Write("Please enter your data: ");
                                data = Convert.ToInt32(Console.ReadLine());
                                if (!structure.insert(data))
                                {
                                    Console.WriteLine("This key could not be stored.");
                                }
                                break;
                            case "delete":
                                Console.Write("Please enter your data: ");
                                data = Convert.ToInt32(Console.ReadLine());
                                if (!structure.delete(data))
                                {
                                    Console.WriteLine("The key was not found and therefore could not be deleted.");
                                }
                                break;
                            case "search":
                                Console.Write("Please enter your data: ");
                                data = Convert.ToInt32(Console.ReadLine());
                                if (!structure.search(data))
                                {
                                    structure.insert(data);
                                    Console.WriteLine("This key was not yet part of your data structure. It was inserted into it now.");
                                }
                                break;
                            case "exit":
                                proceed = false;
                                break;
                            default:
                                Console.Write("Sorry, we didn't recognize that command. Please try again!");
                                break;
                        }

                        Console.Clear();
                        Console.WriteLine("\nYour current structure:\n");
                        structure.print();
                        Console.WriteLine();
                    } while (proceed);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("That didn't work. It seems an unforeseen error has occured. Please try again." + ex.Message); ;
                }
            }

            Console.WriteLine("Thank you for using our programm!");
        }
    }
}
