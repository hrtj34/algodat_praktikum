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
            
            IDictionary structure = null;
            string res;
            bool proceed = true;

            while (true)
            //keeps trying until no error has occured
            {
                try
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome to our programm! It lets you store your data in all our sorts of different structures.");
                        Console.WriteLine("You can choose from the following:\n");
                        Console.WriteLine("1 Array");
                        Console.WriteLine("2 List");
                        Console.WriteLine("3 Hash Table");
                        Console.WriteLine("4 Binary Search Tree");
                        Console.WriteLine("5 AVL - Tree");
                        Console.WriteLine("6 Treap\n");
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
                                int size = 10;
                                Console.Write($"Do you want to pick the table size (Default: {size})? (y/n)? ");
                                if(Console.ReadLine() == "y")
                                {
                                    bool failed = true;
                                    do
                                    {
                                        Console.Write("Please enter your desired tablesize: ");
                                        failed = !int.TryParse(Console.ReadLine(), out size);
                                        if (size <= 0) failed = true;

                                        if (failed) Console.WriteLine("Your input is not an eligible table size. Please try again.");
                                    } while (failed);
                                    
                                }

                                Console.Write("For QuadProb, please press Q. For SepChain, please press S. For LinProb, please press L. For DoubHash, please press D: ");
                                string pressedKey = Console.ReadLine().ToLower();
                                if (pressedKey == "q")
                                {
                                    Console.Write("\nFor Div, please press D. For Mult, please press M: ");
                                    if (Console.ReadLine().ToLower() == "d")
                                    {
                                        structure = new HashTabQuadProb(size);
                                    }
                                    else
                                    {
                                        structure = new HashTabQuadProb(size, new HashMult(size));
                                    }
                                }
                                else if (pressedKey == "l")
                                {
                                    int step = 1;
                                    Console.Write($"Do you want to pick the probing step size (Default: {step})? (y/n)? ");
                                    if (Console.ReadLine() == "y")
                                    {
                                        bool failed = true;
                                        do
                                        {
                                            Console.Write("Please enter your desired step size: ");
                                            failed = !int.TryParse(Console.ReadLine(), out step);
                                            if (step <= 1) failed = true;

                                            if (failed) Console.WriteLine("Your input is not an eligible step size. Please try again.");
                                        } while (failed);

                                    }


                                    Console.Write("\nFor Div, please press D. For Mult, please press M: ");
                                    if (Console.ReadLine().ToLower() == "d")
                                    {
                                        structure = new HashTabLinProb(size, (int i) => step, new HashDiv(size));
                                    }
                                    else
                                    {
                                        structure = new HashTabLinProb(size, (int i) => step, new HashMult(size));
                                    }
                                }
                                else if (pressedKey == "d")
                                {
                                    Console.Write("\nFor Div, please press D. For Mult, please press M: ");
                                    if (Console.ReadLine().ToLower() == "d")
                                    {
                                        structure = new HashTabLinProb(size, new HashDiv(9).HashFunction, new HashDiv(size));
                                    }
                                    else
                                    {
                                        structure = new HashTabLinProb(size, new HashMult(size - 1).HashFunction, new HashMult(size));
                                    }
                                }
                                else
                                {
                                    Console.Write("\nFor Div, please press D. For Mult, please press M: ");
                                    if (Console.ReadLine().ToLower() == "d")
                                    {
                                        structure = new HashTabSepChain<SetUnsortedLinkedList>(size);
                                    }
                                    else
                                    {
                                        structure = new HashTabSepChain<SetUnsortedLinkedList>(size, new HashMult(size));
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

                        Console.WriteLine("If you want to continue, please press enter. If you want to chance your choise, please press escape.");
                        while (true) //waits for enter to be pressed without displaying input
                        {
                            ConsoleKey key;
                            if (Console.KeyAvailable)
                            {
                                key = Console.ReadKey(true).Key;
                                if (key == ConsoleKey.Enter)
                                {
                                    proceed = false;
                                    break;
                                }
                                else if (key == ConsoleKey.Escape)
                                {
                                    proceed = true;
                                    break;
                                }
                            }
                        }
                    } while (proceed);
                    break;
                }
                catch
                {
                    Console.WriteLine("An unforeseen error has occured. Please try again!");
                }
            }
            Console.Clear();

            proceed = true;
            int data;
            string command = "",input;
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

                        Console.WriteLine($"\nTo use your previous command {command} again, please press enter.");
                        Console.Write("Please enter a command: ");
                        input = Console.ReadLine().ToLower();
                        if (input != "")
                            command = input;
                        else
                        {
                            Console.SetCursorPosition(Console.CursorLeft,Console.CursorTop-1);
                            Console.WriteLine($"Please enter a command: {command}");
                        }

                        switch (command)
                        {
                            case "insert":
                                Console.Write("Please enter your data: ");
                                data = Convert.ToInt32(Console.ReadLine());
                                if (data < 0 || !structure.insert(data))
                                {
                                    throw new Exception("This key could not be stored.");
                                }
                                break;
                            case "delete":
                                Console.Write("Please enter your data: ");
                                data = Convert.ToInt32(Console.ReadLine());
                                if (!structure.delete(data))
                                {
                                    throw new Exception("The key was not found and therefore could not be deleted.");
                                }
                                else
                                {
                                    Console.WriteLine("Deletion succesful. This is your new data structure:\n");
                                    structure.print();
                                    Console.WriteLine("\nPress enter to continue.");
                                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
                                }
                                break;
                            case "search":
                                Console.Write("Please enter your data: ");
                                data = Convert.ToInt32(Console.ReadLine());
                                if (!structure.search(data))
                                {
                                    if (structure.insert(data))
                                        throw new Exception("This key is not yet part of your data structure. It was inserted into it now.");
                                    else
                                        throw new Exception("This key is not yet part of your data structure, but it was also not possible to insert it.");
                                }
                                else
                                {
                                    Console.WriteLine("Your data was found.");
                                    Console.WriteLine("Press enter to continue.");
                                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
                                }
                                break;
                            case "exit":
                                proceed = false;
                                break;
                            default:
                                command = "";
                                throw new Exception("Sorry, we didn't recognize that command. Please try again!");
                                break;
                        }

                        Console.Clear();
                        Console.WriteLine("\nYour current structure:\n");
                        structure.print();
                        Console.WriteLine();
                    } while (proceed);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press enter to continue.");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
                    Console.Clear();
                    Console.WriteLine("\nYour current structure:\n");
                    structure.print();
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Thank you for using our programm!");
        }
    }
}
