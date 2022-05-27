using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashTabSepChain<Dictionary> : HashTabProt where Dictionary : IDictionary, new()
    {
        Dictionary[] tab;


        /// <summary>
        /// Creates structure for a hash table with separate chaining.
        /// </summary>
        /// <param name="Tabsize">Minimum desired tablesize</param>
        /// <param name="Tab">Tab to be inserted into the hash table</param>
        /// <param name="HashFunction">Hashfunction to determine hashvalues</param>
        public HashTabSepChain(int Tabsize, int[] Tab, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            hashFunction = HashFunction;

            if (tabsize < Tab.Length)
                tabsize = Tab.Length;

            HashFunctionUpdater(ref hashFunction, tabsize);

            InsertTab(Tab, true);

            foreach (Dictionary item in tab)
            {

            }
        }

        /// <summary>
        /// Creates structure for a hash table with separate chaining.
        /// </summary>
        /// <param name="Tabsize">Minimum desired tablesize</param>
        /// <param name="HashFunction">Hashfunction to determine hashvalues</param>
        public HashTabSepChain(int Tabsize, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            hashFunction = HashFunction;

            HashFunctionUpdater(ref hashFunction, tabsize);

            tab = CreateInitialisedArray<Dictionary>(tabsize);
        }

        /// <summary>
        /// Creates structure for a hash table with table size 10 with separate chaining using the division method for hashing.
        /// </summary>
        /// <param name="Tab">Tab to be inserted into the hash table</param>
        public HashTabSepChain(int[] Tab) : this(TABSIZE, Tab, new HashDiv(TABSIZE)) { }

        /// <summary>
        /// Creates structure for a hash table with table size 10 with separate chaining using the division method for hashing.
        /// </summary>
        public HashTabSepChain() : this(TABSIZE, new HashDiv(TABSIZE)) { }


        public override bool insert(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            return tab[aux].insert(elem);
        }

        public override bool delete(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            return tab[aux].delete(elem);
        }

        public override bool search(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            return tab[aux].search(elem);
        }


        /// <summary>
        /// Inserts all values from a table into the hash table. Allows wiping the table beforehand by setting clean to true.
        /// </summary>
        /// <param name="Tab">Table whose values shall be inserted.</param>
        /// <param name="clean">Optional argument to wipe the hash table beforehand.</param>
        private void InsertTab(int[] Tab, bool clean)
        {
            if (clean) tab = CreateInitialisedArray<Dictionary>(tabsize);

            InsertTab(Tab);
        }

        public override void print()
        {
            foreach (Dictionary key in tab)
            {
                Console.Write("-----------");
                key.print();
            }
        }
    }
}
