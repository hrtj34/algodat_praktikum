using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    abstract class HashTabProt : HashToolbox, ISetUnsorted
    {
        protected const int TABSIZE = 50;
        protected int tabsize;
        protected IHashFunction hashFunction;

      
        public abstract bool delete(int elem);
        public abstract bool insert(int elem);
        public abstract void print();
        public abstract bool search(int elem);

        /// <summary>
        /// Inserts all values from a table into the hash table.
        /// </summary>
        /// <param name="Tab">Table whose values shall be inserted.</param>
        /// <exception cref="Exception"></exception>
        public void InsertTab(int[] Tab)
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
        public void DeleteTab(int[] Tab)
        {

            for (int i = 0; i < Tab.Length; i++)
            {
                if (!delete(Tab[i])) throw new Exception();
            }
        }
    }
}
