using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashMult : IHashFunction
    {
        const uint c = 3586947843;
        int tabsize2Pow;

        /// <summary>
        /// Constructs an Object capable of generating Hashkeys with the multiplication method.
        /// </summary>
        /// <param name="Tabsize">Size of table as powers of two</param>
        public HashMult(int Tabsize)
        {
            tabsize2Pow = (int)Math.Floor(Math.Log2(Tabsize));
        }

        /// <summary>
        /// Generates a hashvalue with a key.
        /// </summary>
        /// <param name="Key">Key to be hashed</param>
        /// <returns>Corresponding hashvalue to the key</returns>
        public int HashFunction(int Key)
        {
            uint aux = (uint)((Key * c) % (long)Math.Pow(2,32));
            aux = aux >> 32 - tabsize2Pow;
            return (int)aux;
        }
    }
}
