using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashMult : IHashFunction
    {
        const uint C = 3586947843;
        uint c;
        int wordsize2Pow;
        int tabsize2Pow;

        /// <summary>
        /// Constructs an Object capable of generating Hashkeys with the multiplication method.
        /// </summary>
        /// <param name="Wordsize">Accepted size of the key as powers of two</param>
        /// <param name="Tabsize">Size of table as powers of two</param>
        public HashMult(int Wordsize, int Tabsize)
        {
            wordsize2Pow = (int)Math.Ceiling(Math.Log2(Wordsize));
            tabsize2Pow = (int)Math.Ceiling(Math.Log2(Tabsize));
            c = C >> 32 - wordsize2Pow;
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
