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
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == elem)
                    return i;
            }
            return array.Length;
        }
        public void print()
        {
            Console.Write("| ");
            for (int i = 0; i < length; i++)
            {

            }
            foreach (int elem in array)
                Console.Write(elem + " | ");
        }
    }
}
