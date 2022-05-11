using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class MultiSetSortedArray : BaseArray, IMultiSetSorted
    {
        int pos = 0;
        public MultiSetSortedArray(int al):base(al){}
        protected int binsearch(int elem)
        {
            int l = 0;
            int r = arrayl;
            int i = 0;
            do
            {
                i = (int)(l + r) / 2;
                if (array[i] < elem)
                    l = i + 1;
                else r = i - 1;
            }
            while (array[i] != elem && l <= r);
            pos = i;
            if (array[i] == elem)
                return i;
            else return -1;
        }
        public bool search(int elem)
        {
            if (binsearch(elem) == -1)
                return false;
            else
                return true;
        }
        public bool insert(int elem)
        {
            if (arrayl == array.Length)
                return false;
            else if (arrayl == 0)
            {
                array[0] = elem;
                arrayl++;
                return true;
            }
            else
            {
                binsearch(elem);
                if (array[pos] >= elem || array[pos] == 0)
                {
                    for (int i = arrayl - 1; i >= pos; i--)
                    {
                        array[i + 1] = array[i];
                    }
                    array[pos] = elem;
                }
                else
                {
                    for (int i = arrayl - 1; i > pos; i--)
                    {
                        array[i + 1] = array[i];
                    }
                    array[pos + 1] = elem;
                }
                arrayl++;
                return true;
            }
        }
        public bool delete(int elem)
        {
            int delel = binsearch(elem);
            if (delel == -1)
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
