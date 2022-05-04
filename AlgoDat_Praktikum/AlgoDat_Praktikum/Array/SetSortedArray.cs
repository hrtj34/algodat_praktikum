using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class SetSortedArray : MultiSetSortedArray
    {
        public SetSortedArray(int al) : base(al) { }

        new public bool insert(int elem)
        {
            if (search(elem))
                return false;
            else
                return base.insert(elem);
        }
    }
}
