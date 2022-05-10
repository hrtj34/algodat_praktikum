using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    abstract class HashTabProt : HashToolbox, ISetUnsorted
    {
        protected int tabsize;
        protected IHashFunction hashFunction;

      
        public abstract bool delete(int elem);
        public abstract bool insert(int elem);
        public abstract void print();
        public abstract bool search(int elem);

        public abstract void InsertTab(int[] Tab);
        public abstract void DeleteTab(int[] Tab);
    }
}
