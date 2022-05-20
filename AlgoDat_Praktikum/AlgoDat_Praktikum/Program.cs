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
            ans[1] = Console.ReadLine() == "y";
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
            Console.WriteLine("1. Array");
            Console.WriteLine("2. List");
            Console.WriteLine("3 Hash Table");
            Console.WriteLine("4 Binary Search Tree");
            Console.WriteLine("5 AVL - Tree");
            Console.WriteLine("6 Treap\n");
            IDictionary structure = null;
            string res;

            do
            {
                Console.Write("Your Choice: ");
                res = Console.ReadLine();
                switch (res)
                {
                    case "1":
                        bool[] ans = checkQuestions();
                        //if (ans[0])
                        //{
                        //    if (ans[1])
                        //        structure = new MultiSetUnsortedArray();
                        //          Console.WriteLine("You chose an unsorted array multi set.");
                        //    else
                        //        structure = new MultiSetSortedArray();
                        //          Console.WriteLine("You chose a sorted array multi set.");
                        //}
                        //else
                        //{
                        //    if (ans[1])
                        //        structure = new SetUnsortedArray();
                        //Console.WriteLine("You chose an unsorted array set.");
                        //    else

                        //        structure = new SetSortedArray();
                        //Console.WriteLine("You chose an sorted array set.");
                        //}
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
                        // Lukas Hash Implementierung
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

            Console.WriteLine("Wonderful! Now you can work with your chosen data structure.");
            Console.WriteLine("Press Enter.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ; //waits for enter to be pressed without displaying input
            
            bool proceed = true;
            int data;
            do
            {
                Console.Clear();
                Console.WriteLine("Your current structure: ");
                structure.print();
                Console.WriteLine("\nYou can insert or delete data or you can search for data you already stored.");
                Console.WriteLine("To do this, please first enter which command you wish to use. Then enter your data in the next line.");
                Console.WriteLine("To exit the programm , please enter exit.");
                Console.Write("\nPlease enter a command: ");
                string command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "insert":
                        Console.Write("Please enter your data: ");
                        data = Convert.ToInt32(Console.ReadLine());
                        structure.insert(data);
                        break;
                    case "delete":
                        Console.Write("Please enter your data: ");
                        data = Convert.ToInt32(Console.ReadLine());
                        structure.delete(data);
                        break;
                    case "search":
                        Console.Write("Please enter your data: ");
                        data = Convert.ToInt32(Console.ReadLine());
                        structure.search(data);
                        break;
                    case "exit":
                        proceed = false;
                        break;
                    default:
                        Console.WriteLine("Sorry, we didn't recognize that command. Please try again!");
                        break;
                }
            } while (proceed);

            Console.WriteLine("Thank you for using our programm!");
        }
    }
}
