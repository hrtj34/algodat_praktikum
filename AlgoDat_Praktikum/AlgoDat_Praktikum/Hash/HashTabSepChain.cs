using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashTabSepChain<Dictionary> : HashTabProt where Dictionary : IDictionary
    {
        Dictionary[] tab;

        public HashTabSepChain(int Tabsize, int[] Tab, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            hashFunction = HashFunction;

            if (tabsize < Tab.Length)
                tabsize = Tab.Length;

            HashFunctionUpdater(ref hashFunction, tabsize);

            InsertTab(Tab, true);
            
        }

        public HashTabSepChain(int Tabsize, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            hashFunction = HashFunction;

            HashFunctionUpdater(ref hashFunction, tabsize);
        }

        public HashTabSepChain() : this(TABSIZE, new HashDiv(TABSIZE)) { }
        public HashTabSepChain(int[] Tab) : this(TABSIZE, Tab, new HashDiv(TABSIZE)) { }

        public override bool delete(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            return tab[aux].delete(elem);
        }

        public override bool insert(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            return tab[aux].insert(elem);
        }

        /// <summary>
        /// Prints all keys in the hash table.
        /// </summary>
        public override void print()
        {
            foreach (Dictionary key in tab)
            {
                Console.Write("-----------");
                key.print();
            }
        }

        public override bool search(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            return tab[aux].search(elem);
        }

        private void InsertTab(int[] Tab, bool clean)
        {
            if (clean) tab = CreateNullArray<Dictionary>(tabsize);

            InsertTab(Tab);
        }

       

        

        /* private void InsertTab(Dictionary[] Tab, bool clean = false)
        {
            if (clean) tab = CreateNullArray<Dictionary>(tabsize);

            for (int i = 0; i < Tab.Length; i++)
            {
                insert(Tab[i]);
            }
        } */
    }
}
