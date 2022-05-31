using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    public delegate int StepWidth(int elem);

    class HashTabLinProb : HashTabProt
    {
        int[] tab;
        bool prime;
        StepWidth stepWidth;

        /// <summary>
        /// Creates structure for a hash table with variable linear probing.
        /// </summary>
        /// <param name="Tabsize">Minimum desired tablesize</param>
        /// <param name="Tab">Tab to be inserted into the hash table</param>
        /// <param name="StepWidth">Function determining the step width for each element</param>
        /// <param name="HashFunction">Hashfunction to determine hashvalues</param>
        public HashTabLinProb(int Tabsize, int[] Tab, StepWidth StepWidth, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            hashFunction = HashFunction;
            stepWidth = StepWidth;

            if (tabsize < Tab.Length)
                tabsize = Tab.Length;

            HashFunctionUpdater(ref hashFunction, tabsize);

            prime = PrimeCheck(tabsize);

            InsertTab(Tab, true);           
        }

        /// <summary>
        /// Creates structure for a hash table with variable linear probing.
        /// </summary>
        /// <param name="Tabsize">Minimum desired tablesize</param>
        /// <param name="StepWidth">Function determining the step width for each element</param>
        /// <param name="HashFunction">Hashfunction to determine hashvalues</param>
        public HashTabLinProb(int Tabsize,  StepWidth StepWidth, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            hashFunction = HashFunction;
            stepWidth = StepWidth;

            prime = PrimeCheck(tabsize);

            tab = CreateMinusArray(tabsize);
        }

        /// <summary>
        /// Creates structure for a hash table with standard linear probing using the division method for hashing.
        /// </summary>
        /// <param name="Tabsize">Minimum desired tablesize</param>
        /// <param name="Tab">Tab to be inserted into the hash table</param>
        public HashTabLinProb(int Tabsize, int[] Tab) : this(Tabsize, Tab, (int i) => 1, new HashDiv(Tabsize)) { }

        /// <summary>
        /// Creates structure for a hash table with standard linear probing using the division method for hashing.
        /// </summary>
        /// <param name="Tabsize">Minimum desired tablesize</param>
        public HashTabLinProb(int Tabsize) : this(Tabsize, (int i) => 1, new HashDiv(Tabsize)) { }

        /// <summary>
        /// Creates structure for a hash table with standard linear probing using the division method for hashing with tablesize 10.
        /// </summary>
        public HashTabLinProb() : this(TABSIZE, (int i) => 1, new HashDiv(TABSIZE)) { }


        public override bool insert(int elem)
        {
            int elemSlot;
            int vacantSlot;
            (elemSlot, vacantSlot) = LinProbing(elem);

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
            (elemSlot, vacantSlot) = LinProbing(elem);
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
            (elemSlot, vacantSlot) = LinProbing(elem);
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
        private (int, int) LinProbing(int elem)
        {
            int index;
            int aux = hashFunction.HashFunction(elem);
            int step = stepWidth(elem);
            int vacantMemoriser = -1;
            int matchMemoriser = -1;

            if (!(prime && step < 2 * tabsize) && step != 1)
            {
                for (; !DividerExtraneousCheck(tabsize, step); step--){}
            }               
                

            /* When the MatchFinder is true either the element, or a slot that was never occupied, has been found. The element cannot be beyond this spot 
             * as it would either already be in the table and thus not be inserted again (as only sets are accepted) or the element would have been in
             * the vacant slot as that would have been the next vacant slot in any case as it was never occupied.
             */
            if (MatchFinder(tab, aux, ref vacantMemoriser, elem, ref matchMemoriser))
                return (matchMemoriser, vacantMemoriser);

            int maxSondSteps = tabsize - 1;
            for (int i = 1; i < maxSondSteps; i++)
            {
                index = (aux + step * i) % tabsize;

                if (MatchFinder(tab, index, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);
            }
            return (matchMemoriser, vacantMemoriser);
        }

        /// <summary>
        /// Checks if two numbers are divider extraeous.
        /// </summary>
        /// <param name="num1">Number 1</param>
        /// <param name="num2">Number 2</param>
        /// <returns>True if the numbers are divider extraeous, false if not.</returns>
        private static bool DividerExtraneousCheck(int num1, int num2)
        {
            num1 = Math.Abs(num1);
            num2 = Math.Abs(num2);
            int min = Math.Min(num1, num2);

            if (min == 0) return false;
            if (min == 1) return true;
            if (num1 % 2 == 0 && num2 % 2 == 0) return false;
            if (num1 % min == 0 && num2 % min == 0) return false;

            for (int i = 3; i <= min / i; i += 2)
            {
                if (num1 % i == 0 && num2 % i == 0) return false;
            }

            return true;
        }


        /// <summary>
        /// Inserts all values from a table into the hash table. Allows wiping the table beforehand by setting clean to true.
        /// </summary>
        /// <param name="Tab">Table whose values shall be inserted.</param>
        /// <param name="clean">Optional argument to wipe the hash table beforehand.</param>
        private void InsertTab(int[] Tab, bool clean)
        {
            if (clean) tab = CreateMinusArray(tabsize);

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
