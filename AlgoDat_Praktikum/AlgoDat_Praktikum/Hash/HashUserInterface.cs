using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashUserInterface : HashToolbox
    {
        HashTabProt HashStructure;

        public void ConsoleStartScreen()
        {
            ConsoleKeyInfo key;
            Header();

            do
            {
              do
                {
                    Console.Write("Für QuadProb drücken Sie Q; Für SepChain drücken Sie S... ");
                    key = Console.ReadKey();
                    Console.WriteLine();

                } while (key.Key != ConsoleKey.Q && key.Key != ConsoleKey.S && key.Key != ConsoleKey.Escape);
                if (key.Key == ConsoleKey.Escape) return;
                else if (key.Key == ConsoleKey.Q) ConsoleHashFunction(true);
                else if (key.Key == ConsoleKey.S) ConsoleHashFunction(false);
            } while (true);
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
            if (key.Key == ConsoleKey.Escape) return;
            else if (probing && key.Key == ConsoleKey.D) ConsoleQuadProbDivExe();
            else if (probing && key.Key == ConsoleKey.M) ConsoleStartScreen();
            else if (!probing && key.Key == ConsoleKey.D) ConsoleSepChainDivExe();
            else if (!probing && key.Key == ConsoleKey.M) ConsoleStartScreen();
        }

        private void ConsoleQuadProbDivExe()
        {
            ConsoleKeyInfo key;
            int convertedNumber;
            Header();

            do
            {
                convertedNumber = AskTablesize();

                if (!HashTabQuadProb.QuadProbeable(convertedNumber))
                {
                    convertedNumber = HashTabQuadProb.MakeQuadProbeable(convertedNumber);
                    do
                    {
                        Console.WriteLine("Ihre gewünschte Tabellengröße ist nicht quadratisch sondierbar. Wenn sie fortfahren wird die Tabellengröße automatisch auf den nächstgrößeren sondierbaren Wert (" + convertedNumber + ") gesetzt. Fortfahren? ");

                        Console.Write("Zum Fortfahren drücken Sie J; Zum Abbrechen N... ");
                        key = Console.ReadKey();
                        Console.WriteLine();

                    } while (key.Key != ConsoleKey.J && key.Key != ConsoleKey.N && key.Key != ConsoleKey.Escape);
                    if (key.Key == ConsoleKey.Escape) return;
                    else if (key.Key == ConsoleKey.J) HashStructure = new HashTabQuadProb(convertedNumber, new HashDiv(convertedNumber));
                    else if (key.Key == ConsoleKey.N) continue;
                }
                else
                {
                    HashStructure = new HashTabQuadProb();
                }
            } while (false);


            KeyInterpreter();
        }

        private void ConsoleSepChainDivExe()
        {
            ConsoleKeyInfo key;
            int convertedNumber;
            int suggestedNumber;
            Header();

            
                convertedNumber = AskTablesize();

                if (!PrimeCheck(convertedNumber))
                {
                    suggestedNumber = PrimeMaker(convertedNumber);
                    do
                    {
                        Console.WriteLine("Ihre gewünschte Tabellengröße ist keine Primzahl. Dies führt bei der gewählten Hashfunktion zu vielen Kollissionen und nur teilweiser Ausschöpfung des vorhandenen Speichers,\nwodurch es zu Verzögerungen kommen kann. Wollen Sie stattdessen die nächstgrößere Primzahl (" + suggestedNumber + ") nutzen? ");

                        Console.Write("Zum Zustimmen drücken Sie J; Zum Ablehnen N... ");
                        key = Console.ReadKey();
                        Console.WriteLine();

                    } while (key.Key != ConsoleKey.J && key.Key != ConsoleKey.N && key.Key != ConsoleKey.Escape);
                    if (key.Key == ConsoleKey.Escape) return;
                    else if (key.Key == ConsoleKey.J) HashStructure = new HashTabSepChain<SetUnsortedLinkedList>(suggestedNumber, new HashDiv(suggestedNumber));
                    else if (key.Key == ConsoleKey.N) HashStructure = new HashTabSepChain<SetUnsortedLinkedList>(convertedNumber, new HashDiv(convertedNumber));
                }
                else
                {
                    new HashTabSepChain<SetUnsortedLinkedList>(convertedNumber, new HashDiv(convertedNumber));
                }

            KeyInterpreter();
        }

        public void KeyInterpreter()
        {
            ConsoleKeyInfo key;
            int[] convertedInput;
            int convertedNumber;

            Header();
            do
            {
                do
                {
                    Console.WriteLine("Bitte geben Sie an, welche Operation Sie ausführen wollen.");
                    Console.Write("Für Einfügen drücken Sie E; Für Löschen drücken Sie L; Für Suchen drücken Sie S; Für Ausdrucken drücken Sie A... ");
                    key = Console.ReadKey();
                    Console.WriteLine();
                } while (key.Key != ConsoleKey.E && key.Key != ConsoleKey.L && key.Key != ConsoleKey.S && key.Key != ConsoleKey.A && key.Key != ConsoleKey.Escape);

                if (key.Key == ConsoleKey.Escape) return;
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

        private int AskTablesize()
        {
            int convertedNumber = -1;
            do
            {
                Console.WriteLine("Bitte geben Sie die gewünschte Tabellengröße an.");

                try
                {
                    convertedNumber = ConvertInputToInt();
                }
                catch
                {
                    Console.WriteLine("Ihre Eingabe konnte nicht konvertiert werden. Bitte versuchen Sie es noch einmal.");
                    continue;
                };
            } while (false);
            return convertedNumber;
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
