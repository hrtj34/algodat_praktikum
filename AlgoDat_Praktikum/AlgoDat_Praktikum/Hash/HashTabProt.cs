using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    abstract class HashTabProt : HashToolbox, ISetUnsorted
    {
        protected const int TABSIZE = 10;
        protected int tabsize;
        protected IHashFunction hashFunction;


        /// <summary>
        /// Inserts one element into the hash table.
        /// </summary>
        /// <param name="elem">Key of the element to be added.</param>
        /// <returns>True for success, false for failure.</returns>
        public abstract bool insert(int elem);

        /// <summary>
        /// Deletes one element from the hash table.
        /// </summary>
        /// <param name="elem">Key of element to be deleted</param>
        /// <returns>True for successful deletion, false if element was not found.</returns>
        public abstract bool delete(int elem);

        /// <summary>
        /// Searches one element in hash table. Returns upon the first hit.
        /// </summary>
        /// <param name="elem">Key of element being searched for</param>
        /// <returns>True if element was found, false if not.</returns>
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

        /// <summary>
        /// Prints all keys in the hash table.
        /// </summary>
        public abstract void print();
    }
}
