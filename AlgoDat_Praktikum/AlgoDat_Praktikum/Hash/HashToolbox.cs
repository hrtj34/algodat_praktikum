using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashToolbox
    {
        /// <summary>
        /// Searches for primenumbers from a given number with given steps.
        /// </summary>
        /// <param name="num">Starting point</param>
        /// <param name="step">Size of the steps</param>
        /// <returns>First primenumber found.</returns>
        protected static int PrimeMaker(int num, int step = 1)
        {
            for (; !PrimeCheck(num); num += step) {}
            return num;
            
        }

        /// <summary>
        /// Checks if a number is a primenumber.
        /// </summary>
        /// <param name="num">Number to be checked</param>
        /// <returns>True if the number is a primenumber, false otherwise.</returns>
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

        /// <summary>
        /// Sets all array entries to -2 in the given array from including the start value to excluding the stop value.
        /// </summary>
        /// <param name="start">Start value</param>
        /// <param name="stop">Stop value</param>
        /// <param name="array">Array to be altered</param>
        protected static void AddMinusToArray(int start, int stop, ref int[] array)
        {
            for (int i = start; i < stop; i++)
            {
                array[i] = -2;
            }
        }

        /// <summary>
        /// Creates an Array filled with -2 with the given length.
        /// </summary>
        /// <param name="length">Length of the arry</param>
        /// <returns>Array of given length filled with -2.</returns>
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
