using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    abstract class HashTabProt : ISetUnsorted
    {
        protected int tabsize;
        protected IHashFunction hashFunction;
        protected Object[] HashNode;

      
        public abstract bool delete(int elem);
        public abstract bool insert(int elem);
        public abstract void print();
        public abstract bool search(int elem);
    }
}
