using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashMult : IHashFunction
    {
        const uint C = 3586947843;
        uint c;
        int wordsize;
        int tabsize;

        /// <summary>
        /// Constructs an Object capable of generating Hashkeys with the multiplication method.
        /// </summary>
        /// <param name="Wordsize">Accepted size of the key as powers of two</param>
        /// <param name="Tabsize">Size of table as powers of two</param>
        public HashMult(int Wordsize, int Tabsize)
        {
            wordsize = Wordsize;
            tabsize = Tabsize;
            c = C >> 32 - wordsize;
        }

        /// <summary>
        /// Generates a hashvalue with a key.
        /// </summary>
        /// <param name="Key">Key to be hashed</param>
        /// <returns>Corresponding hashvalue to the key</returns>
        public int HashFunction(int Key)
        {
            uint aux = (uint)((Key * c) % (long)Math.Pow(2,32));
            aux = aux >> 32 - tabsize;
            return (int)aux;
        }
    }
}
