using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
abstract class BaseArray
    {
        protected int[] array;
        protected int arrayl = 0;
        protected BaseArray(int al)
        {
            array = new int[al];
        }
        protected int sequsearch (int elem)
        {
            int i = 0;
            while (array[i] != elem)
                i++;
            return i;
        }
        public void print()
        {
            Console.Write("| ");
            foreach (int elem in array)
                Console.Write(elem + " | ");
        }
    }
}