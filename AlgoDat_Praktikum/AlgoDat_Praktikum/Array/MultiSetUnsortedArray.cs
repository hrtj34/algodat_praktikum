using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class MultiSetUnsortedArray : BaseArray, IMultiSetUnsorted
    {
        public MultiSetUnsortedArray(int al):base(al) { }

        public bool search(int elem)
        {
            if (base.sequsearch(elem) == array.Length)
                return false;
            else
                return true;
        }

        public bool insert(int elem)
        {
            if (arrayl == array.Length)
                return false;
            else
            {
                array[arrayl] = elem;
                arrayl++;
                return true;
            }
        }

        public bool delete(int elem)
        {
            int delel = sequsearch(elem);
            if (delel == array.Length)
                return false;
            else
            {
                for (int i = delel; i < array.Length - 1; i++)
                    array[i] = array[i + 1];
                array[array.Length - 1] = 0;
                arrayl--;
                return true;
            }
        }
    }
}
