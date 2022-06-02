using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashTabQuadProb : HashTabProt
    {
        
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
            hashFunction = HashFunction;

            if (tabsize < Tab.Length)
                tabsize = Tab.Length;
           
            sondsize = tabsize;

            if (QuadProbeable(tabsize))
            {
                HashFunctionUpdater(ref hashFunction, sondsize);
                InsertTab(Tab, true);
            }
            else
            {
                sondsize = MakeQuadProbeable(tabsize);
                if (HashFunctionUpdater(ref hashFunction, sondsize))
                    tabsize = sondsize;
                InsertTab(Tab, true);
            }
            
        }

        /// <summary>
        /// Creates the structure for a hash table with quadratic probing.
        /// </summary>
        /// <param name="Tabsize">Minimum table size</param>
        /// <param name="HashFunction">Hashfunction to determine hashvalues</param>
        public HashTabQuadProb(int Tabsize, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            hashFunction = HashFunction;
            sondsize = tabsize;

            if (QuadProbeable(tabsize))
            {
                tab = CreateMinusArray(sondsize);               
            }
            else
            {
                sondsize = MakeQuadProbeable(tabsize);
                if (HashFunctionUpdater(ref hashFunction, sondsize))
                    tabsize = sondsize;
                tab = CreateMinusArray(sondsize);

            }
            
        }       

        /// <summary>
        /// Creates the structure for a hash table with quadratic probing using the division method for hashing.
        /// </summary>
        /// <param name="Tabsize">Minimum table size</param>
        /// <param name="Tab">Array with keys to be added. Also determines minimum table size.</param>
        public HashTabQuadProb(int Tabsize, int[] Tab) : this(Tabsize, Tab, new HashDiv(Tabsize)) { }

        /// <summary>
        /// Creates the structure for a hash table with quadratic probing using the division method for hashing.
        /// </summary>
        /// <param name="Tabsize">Minimum table size</param>
        public HashTabQuadProb(int Tabsize) : this(Tabsize, new HashDiv(Tabsize)) { }

        /// <summary>
        /// Creates the structure for a hash table with quadratic probing using the division method for hashing and a minimum table size of 10.
        /// </summary>
        public HashTabQuadProb() : this(TABSIZE, new HashDiv(TABSIZE)) { }


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
        private (int, int) QuadProbing(int elem)
        {
            int index;
            int aux = hashFunction.HashFunction(elem);
            int vacantMemoriser = -1;
            int matchMemoriser = -1;

            /* When the MatchFinder is true either the element, or a slot that was never occupied, has been found. The element cannot be beyond this spot 
             * as it would either already be in the table and thus not be inserted again (as only sets are accepted) or the element would have been in
             * the vacant slot as that would have been the next vacant slot in any case as it was never occupied.
             */
            if (MatchFinder(tab, aux, ref vacantMemoriser, elem, ref matchMemoriser))
                return (matchMemoriser, vacantMemoriser);

            int maxSondSteps = (tab.Length - 1) / 2;
            for (int i = 1; i <= maxSondSteps; i++)
            {
                int iSquare = i * i;
                index = (aux + iSquare) % sondsize;
                if(index < 0)
                    index += sondsize;

                if (MatchFinder(tab, index, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);

                index = (aux - iSquare) % sondsize;
                if (index < 0)
                    index += sondsize;

                if (MatchFinder(tab, index, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);
            }
            return (matchMemoriser, vacantMemoriser);
        }        

        /// <summary>
        /// Checks if a tablesize/ number is suitable for quadratic probeing.
        /// </summary>
        /// <param name="num">Number to be checked</param>
        /// <returns>True if it is suitable, false if it is not.</returns>
        public static bool QuadProbeable(int num) => num % 4 == 3 && PrimeCheck(num);

        /// <summary>
        /// Finds the next bigger tablesize/ number that is suitable for quadratic probeing.
        /// </summary>
        /// <param name="num">Number to be adjusted</param>
        /// <returns>A larger number that is suitable for quadratic probeing.</returns>
        public static int MakeQuadProbeable(int num)
        {
            int auxNum = num + 3 - num % 4;
            return PrimeMaker(auxNum, 4);
        }


        /// <summary>
        /// Inserts all values from a table into the hash table. Allows wiping the table beforehand by setting clean to true.
        /// </summary>
        /// <param name="Tab">Table whose values shall be inserted.</param>
        /// <param name="clean">Optional argument to wipe the hash table beforehand.</param>
        private void InsertTab(int[] Tab, bool clean)
        {
            if (clean) tab = CreateMinusArray(sondsize);

            InsertTab(Tab);
        }       

        
        public override void print()
        {
            foreach (int key in tab)
            {
                Console.Write("| " + key);
            }
        }
    }
}
