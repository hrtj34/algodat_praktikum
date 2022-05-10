using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class HashTabQuadProb : HashTabProt
    {
        const int TABSIZE = 50;
        private int sondsize;
        private int[] tab;

        public HashTabQuadProb(int Tabsize, int[] Tab, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            if (QuadProbeable(tabsize))
            {
                tab = Tab;
            }
            else
            {
                sondsize = MakeQuadProbeable(tabsize);
                int[] tab = new int[sondsize];

                for (int i = 0; i < tabsize; i++)
                {
                    tab[i] = Tab[i];
                }
                AddMinusToArray(Tabsize, sondsize, ref tab);
            }
            if(HashFunctionUpdater(ref HashFunction, sondsize))
                tabsize = sondsize;
            hashFunction = HashFunction;
        }

        public HashTabQuadProb(int Tabsize, IHashFunction HashFunction)
        {
            tabsize = Tabsize;
            if (QuadProbeable(tabsize))
            {
                tab = CreateMinusArray(tabsize);
                sondsize = tabsize;
            }
            else
            {
                sondsize = MakeQuadProbeable(tabsize);
                tab = CreateMinusArray(sondsize);

            }
            hashFunction = HashFunction;
        }

        public HashTabQuadProb() : this(TABSIZE, new HashDiv(TABSIZE)) {}
        public HashTabQuadProb(int[] Tab) : this(Tab.Length, Tab, new HashDiv(Tab.Length)) {}

        public override bool insert(int elem)
        {
            int elemSlot;
            int vacantSlot;
            (elemSlot, vacantSlot) = QuadProbing(elem);

            if (elemSlot == -1 && vacantSlot != -1)
            {
                tab[vacantSlot] = elem;
                return true;
            }
            else return false;
        }

        public override bool delete(int elem)
        {
            int elemSlot;
            int vacantSlot;
            (elemSlot, vacantSlot) = QuadProbing(elem);
            if(elemSlot != -1)
            {
                tab[elemSlot] = -1;
                return true;
            }
            return false;
        }

        public override bool search(int elem)
        {
            int elemSlot;
            int vacantSlot;
            (elemSlot, vacantSlot) = QuadProbing(elem);
            if (elemSlot != -1)
            {
                return true;
            }
            return false;
        }

        public (int, int) QuadProbing(int elem)
        {
            int aux = hashFunction.HashFunction(elem);
            int vacantMemoriser = -1;
            int matchMemoriser = -1;

            if (MatchFinder(aux, ref vacantMemoriser, elem, ref matchMemoriser))
                return (matchMemoriser, vacantMemoriser);

            int maxSondSteps = (tab.Length - 1) / 2;
            for (int i = 1; i < maxSondSteps; i++)
            {
                int iSquare = i * i;

                if (MatchFinder(aux + iSquare, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);
                

                if (MatchFinder(aux - iSquare, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);
            }
            return (matchMemoriser, vacantMemoriser);
        } 

        public bool MatchFinder(int index, ref int vacantMemoriser, int elem, ref int matchMemoriser)
        {
            
            if (vacantMemoriser != -1 && tab[index] < 0)
            {
                vacantMemoriser = index;
                if (tab[index] < -1) return true;
            }
            else if (tab[index] == elem) 
            {
                matchMemoriser = index;
                return true;
            }
            return false;
        }

        private static bool QuadProbeable(int num) => num % 4 == 3 && PrimeCheck(num);
        private static int MakeQuadProbeable(int num)
        {
            int auxNum = num + 3 - num % 4;

            while (!QuadProbeable(auxNum))
            {
                auxNum += 4;
            }

            return auxNum;
        }

        private static bool PrimeCheck(int num)
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

        private static void AddMinusToArray(int start, int stop, ref int[] array)
        {
            for (int i = start; i < stop; i++)
            {
                array[i] = -2;
            }
        }
        private static int[] CreateMinusArray(int length)
        {
            int[] array = new int[length];
            AddMinusToArray(0, length, ref array);
            return array;
        }

        private static bool HashFunctionUpdater(ref IHashFunction HashFunction, int size)
        {
            if(HashFunction == null)
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
        

       

        

        public override void print()
        {
            throw new NotImplementedException();
        }

        

       
    }
}
