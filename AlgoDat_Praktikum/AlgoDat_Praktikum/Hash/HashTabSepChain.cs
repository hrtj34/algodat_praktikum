using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashTabSepChain<Dictionary> : HashTabProt where Dictionary : IDictionary
    {
        const int TABSIZE = 50;
        Dictionary[] tab;

        public HashTabSepChain(int[] Tab, IHashFunction HashFunction)
        {
            tabsize = Tab.Length;
            InsertTab(Tab, true);
            hashFunction = HashFunction;
        }

        public HashTabSepChain(int Tabsize, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            hashFunction = HashFunction;
        }

        public HashTabSepChain() : this(TABSIZE, new HashDiv(TABSIZE)) { }
        public HashTabSepChain(int[] Tab) : this(Tab, new HashDiv(TABSIZE)) { }

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

        public override void print()
        {
            throw new NotImplementedException();
        }

        public override bool search(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            return tab[aux].search(elem);
        }

        private void InsertTab(int[] Tab, bool clean = false)
        {
            if (clean) tab = CreateNullArray<Dictionary>(tabsize);

            for (int i = 0; i < Tab.Length; i++)
            {
                insert(Tab[i]);
            }
        }

        public override void InsertTab(int[] Tab)
        {

            for (int i = 0; i < Tab.Length; i++)
            {
                insert(Tab[i]);
            }
        }

        public override void DeleteTab(int[] Tab)
        {

            for (int i = 0; i < Tab.Length; i++)
            {
                if (!delete(Tab[i])) throw new Exception();
            }
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
