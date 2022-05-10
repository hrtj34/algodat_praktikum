using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashDiv : IHashFunction, IMiniUpdateable
    {
        protected int tabsize;

        /// <summary>
        /// Constructs object capable of creating hashkeys with the division method.
        /// </summary>
        /// <param name="Tabsize">Size of the table</param>
        public HashDiv (int Tabsize) => tabsize = Tabsize;

        /// <summary>
        /// Hashes the key with the division method.
        /// </summary>
        /// <param name="key">Key to be hashed</param>
        /// <returns>Hashed key</returns>
        public int HashFunction(int key)
        {
            return key % tabsize;
        }

        /// <summary>
        /// Updates the tablesize to include more or less slots in the hashing.
        /// </summary>
        /// <param name="Tabsize">New tablesize</param>
        public void Update(int Tabsize) => tabsize = Tabsize;
    }
}
