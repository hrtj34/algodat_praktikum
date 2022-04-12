using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    interface IDictionary
    {
        bool search(int elem);
        bool insert(int elem);
        bool delete(int elem);
        void print();
    }
}
