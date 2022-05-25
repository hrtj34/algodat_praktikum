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

        /// <summary>
        /// Updates the tablesize of the HashFunction if it takes updates.
        /// </summary>
        /// <param name="HashFunction">HashFunction object to be updated.</param>
        /// <param name="size">Tablesize to update to.</param>
        /// <returns>True if the function could be updated, false if not.</returns>
        protected static bool HashFunctionUpdater(ref IHashFunction HashFunction, int size)
        {
            if (HashFunction == null)
            {
                HashFunction = new HashDiv(size);
                return true;
            }
            if (HashFunction is IMiniUpdateable)
            {
                (HashFunction as IMiniUpdateable).Update(size);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks for a given index, if the element occupies the slot or if the slot is vacant. If no vacant slot has been found before, and the slot is vacant, it is memorised.
        /// </summary>
        /// <param name="tab">Hashtable to be checked</param>
        /// <param name="index">Index of the hash table being looked at</param>
        /// <param name="vacantMemoriser">Memorises the first vacant slot found</param>
        /// <param name="elem">Key of the element being looked for</param>
        /// <param name="matchMemoriser">Memorises the slot where the element was found</param>
        /// <returns>True, if a match has been found. True, if a vacant slot that was never occupied has been found. False otherwise.</returns>
        protected static bool MatchFinder(int[] tab, int index, ref int vacantMemoriser, int elem, ref int matchMemoriser)
        {
            if (tab[index] == elem)
            {
                matchMemoriser = index;
                return true;
            }

            if (vacantMemoriser == -1)
            {
                if (tab[index] < 0)
                {
                    vacantMemoriser = index;
                    if (tab[index] < -1) return true;       //-2 stands for never occupied, -1 stands for empty but once occupied
                }

            }
            else if (tab[index] < -1) return true;
            return false;
        }

        
    }
}
