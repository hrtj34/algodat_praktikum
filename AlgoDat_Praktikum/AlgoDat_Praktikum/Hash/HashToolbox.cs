using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashToolbox
    {
        protected static int PrimeMaker(int num, int step = 1)
        {
            for (; PrimeCheck(num); num += step) {}
            return num;
            
        }

        protected static bool PrimeCheck(int num)
        {
            if (num <= 1) return false;
            if (num == 2) return true;
            if (num % 2 == 0) return false;

            for (int i = 3; i <= num / i; i += 2)
            {
                if (num % i == 0) return false;
            }

            return true;
        }

        protected static void AddMinusToArray(int start, int stop, ref int[] array)
        {
            for (int i = start; i < stop; i++)
            {
                array[i] = -2;
            }
        }
        protected static int[] CreateMinusArray(int length)
        {
            int[] array = new int[length];
            AddMinusToArray(0, length, ref array);
            return array;
        }

        protected static void AddNullToArray<Dictionary>(int start, int stop, ref Dictionary[] array) where Dictionary : IDictionary
        {
            for (int i = start; i < stop; i++)
            {
                array[i] = default;
            }
        }
        protected static Dictionary[] CreateNullArray<Dictionary>(int length) where Dictionary : IDictionary
        {
            Dictionary[] array = new Dictionary[length];
            return array;
        }
    }
}
