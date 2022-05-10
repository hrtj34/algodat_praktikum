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

        public HashMult(int Wordsize, int Tabsize)
        {
            wordsize = Wordsize;
            tabsize = Tabsize;
            c = C >> 32 - wordsize;
        }
        public int HashFunction(int Key)
        {
            uint aux = (uint)((Key * c) % (long)Math.Pow(2,32));
            aux = aux >> 32 - tabsize;
            return (int)aux;
        }
    }
}
