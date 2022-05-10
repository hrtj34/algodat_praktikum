using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashDiv : IHashFunction, IMiniUpdateable
    {
        protected int tabsize;

        public HashDiv (int Tabsize) => tabsize = Tabsize;
        public int HashFunction(int key)
        {
            return key % tabsize;
        }

        public void Update(int Tabsize) => tabsize = Tabsize;
    }
}
