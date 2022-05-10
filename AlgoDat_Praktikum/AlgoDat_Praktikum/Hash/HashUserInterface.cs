using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashUserInterface
    {
        HashTabProt HashStructure;

        public void ConsoleStartScreen()
        {
            ConsoleKeyInfo key;
            Header();
            do
            {
                Console.Write("Für QuadProb drücken Sie Q; Für SepChain drücken Sie S... ");
                key = Console.ReadKey();
                Console.WriteLine();

            } while (key.Key != ConsoleKey.Q && key.Key != ConsoleKey.S && key.Key != ConsoleKey.Escape);
            if (key.Key == ConsoleKey.Escape) return;
            else if (key.Key == ConsoleKey.Q) ConsoleHashFunction(true);
            else if (key.Key == ConsoleKey.S) ConsoleHashFunction(false);
            else return;
        }

        private void ConsoleHashFunction(bool probing)
        {
            ConsoleKeyInfo key;
            Header();
            do
            {
                Console.Write("Für Div drücken Sie D; Für Mult drücken Sie M... ");
                key = Console.ReadKey();
                Console.WriteLine();

            } while (key.Key != ConsoleKey.D && key.Key != ConsoleKey.M && key.Key != ConsoleKey.Escape);
            if (key.Key == ConsoleKey.Escape) ConsoleStartScreen();
            else if (probing && key.Key == ConsoleKey.D) ConsoleQuadProbDivExe();
            else if (probing && key.Key == ConsoleKey.M) ConsoleStartScreen();
            else if (!probing && key.Key == ConsoleKey.D) ConsoleSepChainExe();
            else if (!probing && key.Key == ConsoleKey.M) ConsoleStartScreen();
            else ConsoleStartScreen();
        }

        private void ConsoleQuadProbDivExe()
        {
            HashStructure = new HashTabQuadProb();
            Header();
            KeyInterpreter();
            ConsoleStartScreen();
        }

        private void ConsoleSepChainExe()
        {
            HashStructure = new HashTabSepChain<SetUnsortedLinkedList>();
            Header();
            KeyInterpreter();
            ConsoleStartScreen();
        }

        public ConsoleKeyInfo KeyInterpreter()
        {
            ConsoleKeyInfo key;
            int[] convertedInput;
            int convertedNumber;
            do
            {
                do
                {
                    Console.WriteLine("Bitte geben Sie an, welche Operation Sie ausführen wollen.");
                    Console.Write("Für Einfügen drücken Sie E; Für Löschen drücken Sie L; Für Suchen drücken Sie S; Für Ausdrucken drücken Sie A... ");
                    key = Console.ReadKey();
                    Console.WriteLine();
                } while (key.Key != ConsoleKey.E && key.Key != ConsoleKey.L && key.Key != ConsoleKey.S && key.Key != ConsoleKey.A && key.Key != ConsoleKey.Escape);

                if (key.Key == ConsoleKey.Escape) ConsoleStartScreen();
                else if (key.Key == ConsoleKey.E)
                {
                    do
                    {
                        Console.WriteLine("Bitte geben Sie Komma und/oder Leerzeichengetrennt alle Schlüssel an, welche Sie einfügen wollen. Drücken Sie zum Bestätigen Enter.");

                        try
                        {
                            convertedInput = ConvertInputToIntArray();
                        }
                        catch
                        {
                            Console.WriteLine("Ihre Eingabe konnte nicht konvertiert werden. Bitte versuchen Sie es noch einmal.");
                            continue;
                        };
                        try
                        {
                            for (int i = 0; i < convertedInput.Length; i++)
                            {
                                Console.WriteLine(convertedInput[i]);
                            }
                            
                            HashStructure.InsertTab(convertedInput);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Ihre Eingabe konnte nicht eingefügt werden.");
                            continue;
                        }
                        HashStructure.print();
                    } while (false);
                }
                else if (key.Key == ConsoleKey.L)
                {
                    do
                    {
                        Console.WriteLine("Bitte geben Sie Komma und/oder Leerzeichengetrennt alle Schlüssel an, welche Sie löschen wollen. Drücken Sie zum Bestätigen Enter.");

                        try
                        {
                            convertedInput = ConvertInputToIntArray();
                        }
                        catch
                        {
                            Console.WriteLine("Ihre Eingabe konnte nicht konvertiert werden. Bitte versuchen Sie es noch einmal.");
                            continue;
                        };
                        try
                        {
                            HashStructure.DeleteTab(convertedInput);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Ihre Eingabe konnte nicht gelöscht werden.");
                            continue;
                        }

                    } while (false);
                }
                else if (key.Key == ConsoleKey.S)
                {
                    do
                    {
                        Console.WriteLine("Bitte geben Sie den Schlüssel an, welchen Sie finden wollen. Drücken Sie zum Bestätigen Enter.");

                        try
                        {
                            convertedNumber = ConvertInputToInt();
                        }
                        catch
                        {
                            Console.WriteLine("Ihre Eingabe konnte nicht konvertiert werden. Bitte versuchen Sie es noch einmal.");
                            continue;
                        };
                        if (HashStructure.search(convertedNumber))
                            Console.WriteLine("Der Schlüssel wurde gefunden.");
                        else Console.WriteLine("Der Schlüssel wurde nicht gefunden.");

                    } while (false);
                }
                else if (key.Key == ConsoleKey.A) HashStructure.print();
            } while (key.Key != ConsoleKey.Escape);
            return default;
        }

        private int[] ConvertInputToIntArray()
        {
            string inputString;
            string[] separatedInputStrings;
            int[] convertedInput;

            inputString = Console.ReadLine();
            inputString = inputString.Trim();
            separatedInputStrings = inputString.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            convertedInput = new int[separatedInputStrings.Length];

            for (int i = 0; i < separatedInputStrings.Length; i++)
            {
                if (!Int32.TryParse(separatedInputStrings[i], out convertedInput[i])) throw new ArgumentException();
            }

            return convertedInput;
        }

        private int ConvertInputToInt()
        {
            string inputString;
            int convertedInput;

            inputString = Console.ReadLine();
            inputString = inputString.Trim();

            if (!Int32.TryParse(inputString, out convertedInput)) throw new ArgumentException();

            return convertedInput;
        }

        private void Header()
        {
            Console.Clear();
            Console.WriteLine("Hash Testumgebung");
            Console.WriteLine("Um die Testumgebung zu verlassen, drücken Sie Escape (Esc)");
        }
    }
}
