using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashTabQuadProb : HashTabProt
    {
        const int TABSIZE = 50;
        private int sondsize;
        private int[] tab;

        /// <summary>
        /// Creates the structure for a hash table with quadratic probing.
        /// </summary>
        /// <param name="Tabsize">Minimum table size</param>
        /// <param name="Tab">Array of keys to be added to the hash table</param>
        /// <param name="HashFunction">Hashfunction to determine hashvalues</param>
        public HashTabQuadProb(int Tabsize, int[] Tab, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            if (QuadProbeable(tabsize))
            {
                InsertTab(Tab, true);
            }
            else
            {
                sondsize = MakeQuadProbeable(tabsize);
                InsertTab(Tab, true);
            }
            if (HashFunctionUpdater(ref HashFunction, sondsize))
                tabsize = sondsize;
            hashFunction = HashFunction;
        }

        /// <summary>
        /// Creates the structure for a hash table with quadratic probing.
        /// </summary>
        /// <param name="Tabsize">Minimum table size</param>
        /// <param name="HashFunction">Hashfunction to determine hashvalues</param>
        public HashTabQuadProb(int Tabsize, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            if (QuadProbeable(tabsize))
            {
                tab = CreateMinusArray(tabsize);
                sondsize = tabsize;
            }
            else
            {
                sondsize = MakeQuadProbeable(tabsize);
                tab = CreateMinusArray(sondsize);

            }
            if (HashFunctionUpdater(ref HashFunction, sondsize))
                tabsize = sondsize;
            hashFunction = HashFunction;
        }

        /// <summary>
        /// Creates the structure for a hash table with quadratic probing using the division method for hashing and a minimum table size of 50.
        /// </summary>
        public HashTabQuadProb() : this(TABSIZE, new HashDiv(TABSIZE)) { }

        /// <summary>
        /// Creates the structure for a hash table with quadratic probing using the division method for hashing.
        /// </summary>
        /// <param name="Tab">Array with keys to be added. Also determines minimum table size.</param>
        public HashTabQuadProb(int[] Tab) : this(Tab.Length, Tab, new HashDiv(Tab.Length)) { }


        /// <summary>
        /// Inserts one element into the hash table.
        /// </summary>
        /// <param name="elem">Key of the element to be added.</param>
        /// <returns>True for success, false for failure.</returns>
        public override bool insert(int elem)
        {
            int elemSlot;
            int vacantSlot;
            (elemSlot, vacantSlot) = QuadProbing(elem);

            if (elemSlot == -1 && vacantSlot != -1)
            {
                tab[vacantSlot] = elem;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Deletes one element from the hash table.
        /// </summary>
        /// <param name="elem">Key of element to be deleted</param>
        /// <returns>True for successful deletion, false if element was not found.</returns>
        public override bool delete(int elem)
        {
            int elemSlot;
            int vacantSlot;
            (elemSlot, vacantSlot) = QuadProbing(elem);
            if (elemSlot != -1)
            {
                tab[elemSlot] = -1;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches one element in hash table. Returns upon the first hit.
        /// </summary>
        /// <param name="elem">Key of element being searched for</param>
        /// <returns>True if element was found, false if not.</returns>
        public override bool search(int elem)
        {
            int elemSlot;
            int vacantSlot;
            (elemSlot, vacantSlot) = QuadProbing(elem);
            if (elemSlot != -1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches the table for vacant spots and the given element. Stops once it reaches a vacant spot that was never held by an element or once it finds the element.
        /// </summary>
        /// <param name="elem">Key of the element being looked for.</param>
        /// <returns>First int returns the index of the element if it was found, otherwise -1. Second int returns index of first vacant slot if one was found before stopping, otherwise -1.</returns>
        public (int, int) QuadProbing(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            int vacantMemoriser = -1;
            int matchMemoriser = -1;

            /* When the MatchFinder is true either the element or a slot that was never occupied has been found. The element cannot be beyond this spot 
             * as it would either already be in the table and thus not be inserted again (as only sets are accepted) or the element would have been in
             * the vacant slot as that would have been the next vacant slot in any case as it was never occupied.
             */
            if (MatchFinder(aux, ref vacantMemoriser, elem, ref matchMemoriser))
                return (matchMemoriser, vacantMemoriser);

            int maxSondSteps = (tab.Length - 1) / 2;
            for (int i = 1; i < maxSondSteps; i++)
            {
                int iSquare = i * i;
                int index = (aux + iSquare) % sondsize;

                if (MatchFinder(index, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);


                if (MatchFinder(index, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);
            }
            return (matchMemoriser, vacantMemoriser);
        }

        /// <summary>
        /// Checks for a given index, if the element occupies the slot or if the slot is vacant. If no vacant slot has been found before, and the slot is vacant, it is memorised.
        /// </summary>
        /// <param name="index">Index of the hash table being looked at</param>
        /// <param name="vacantMemoriser">Memorises the first vacant slot found</param>
        /// <param name="elem">Key of the element being looked for</param>
        /// <param name="matchMemoriser">Memorises the slot where the element was found</param>
        /// <returns>True, if a match has been found. True, if a vacant slot that was never occupied has been found. False otherwise.</returns>
        public bool MatchFinder(int index, ref int vacantMemoriser, int elem, ref int matchMemoriser)
        {
            if (tab[index] == elem)
            {
                matchMemoriser = index;
                return true;
            }

            if (vacantMemoriser == -1)
            {
                if (tab[index] < 0)
                {
                    vacantMemoriser = index;
                    if (tab[index] < -1) return true;       //-2 stands for never occupied, -1 stands for empty but once occupied
                }

            }
            else if (tab[index] < -1) return true;
            return false;
        }

        /// <summary>
        /// Checks if a tablesize/ number is suitable for quadratic probeing.
        /// </summary>
        /// <param name="num">Number to be checked</param>
        /// <returns>True if it is suitable, false if it is not.</returns>
        private static bool QuadProbeable(int num) => num % 4 == 3 && PrimeCheck(num);

        /// <summary>
        /// Finds the next bigger tablesize/ number that is suitable for quadratic probeing.
        /// </summary>
        /// <param name="num">Number to be adjusted</param>
        /// <returns>A larger number that is suitable for quadratic probeing.</returns>
        private static int MakeQuadProbeable(int num)
        {
            int auxNum = num + 3 - num % 4;
            return PrimeMaker(auxNum, 4);
        }


        /// <summary>
        /// Inserts all values from a table into the hash table. Allows wiping the table beforehand by setting clean to true.
        /// </summary>
        /// <param name="Tab">Table whose values shall be inserted.</param>
        /// <param name="clean">Optional argument to wipe the hash table beforehand.</param>
        private void InsertTab(int[] Tab, bool clean = false)
        {
            if (clean) tab = CreateMinusArray(sondsize);

            for (int i = 0; i < Tab.Length; i++)
            {
                insert(Tab[i]);
            }
        }

        /// <summary>
        /// Inserts all values from a table into the hash table.
        /// </summary>
        /// <param name="Tab">Table whose values shall be inserted.</param>
        /// <exception cref="Exception"></exception>
        public override void InsertTab(int[] Tab)
        {
            for (int i = 0; i < Tab.Length; i++)
            {
                if (!insert(Tab[i])) throw new Exception();
            }
        }

        /// <summary>
        /// Deletes all values from a table from the hash table.
        /// </summary>
        /// <param name="Tab">Table whose values shall be deleted.</param>
        /// <exception cref="Exception"></exception>
        public override void DeleteTab(int[] Tab)
        {

            for (int i = 0; i < Tab.Length; i++)
            {
                if (!delete(Tab[i])) throw new Exception();
            }
        }

        /// <summary>
        /// Updates the tablesize of the HashFunction if it takes updates.
        /// </summary>
        /// <param name="HashFunction">HashFunction object to be updated.</param>
        /// <param name="size">Tablesize to update to.</param>
        /// <returns>True if the function could be updated, false if not.</returns>
        private static bool HashFunctionUpdater(ref IHashFunction HashFunction, int size)
        {
            if (HashFunction == null)
            {
                HashFunction = new HashDiv(size);
                return true;
            }
            if (HashFunction is IMiniUpdateable)
            {
                (HashFunction as IMiniUpdateable).Update(size);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prints all keys in the hash table.
        /// </summary>
        public override void print()
        {
            foreach (int key in tab)
            {
                Console.Write("| " + key);
            }
        }
    }
}
