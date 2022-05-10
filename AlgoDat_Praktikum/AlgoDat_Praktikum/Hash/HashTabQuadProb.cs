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
                InsertTab(Tab, true);
            }
            else
            {
                sondsize = MakeQuadProbeable(tabsize);
                InsertTab(Tab, true);
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
            if (HashFunctionUpdater(ref HashFunction, sondsize))
                tabsize = sondsize;
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
                int index = (aux + iSquare) % sondsize;

                if (MatchFinder(index, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);
                

                if (MatchFinder(index, ref vacantMemoriser, elem, ref matchMemoriser))
                    return (matchMemoriser, vacantMemoriser);
            }
            return (matchMemoriser, vacantMemoriser);
        } 

        public bool MatchFinder(int index, ref int vacantMemoriser, int elem, ref int matchMemoriser)
        {
            if(tab[index] == elem)
            {
                matchMemoriser = index;
                return true;
            }

            if (vacantMemoriser == -1)
            {
                if (tab[index] < 0)
                {
                    vacantMemoriser = index;
                    if (tab[index] < -1) return true;
                }
               
            }
            else if (tab[index] < -1) return true;
            return false;
        }

        private static bool QuadProbeable(int num) => num % 4 == 3 && PrimeCheck(num);
        private static int MakeQuadProbeable(int num)
        {
            int auxNum = num + 3 - num % 4;
            return PrimeMaker(auxNum, 4);
        }

        

        

        

        private void InsertTab(int[] Tab, bool clean = false)
        {
            if (clean) tab = CreateMinusArray(sondsize);

            for (int i = 0; i < Tab.Length; i++)
            {
                insert(Tab[i]);
            }
        }

        public override void InsertTab(int[] Tab)
        {
            for (int i = 0; i < Tab.Length; i++)
            {
                if(!insert(Tab[i])) throw new Exception();
            }
        }

        public override void DeleteTab(int[] Tab)
        {

            for (int i = 0; i < Tab.Length; i++)
            {
                if (!delete(Tab[i])) throw new Exception();
            }
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
            foreach (int key in tab)
            {
                Console.Write("| " + key);
            }

            
        }

        

       
    }
}
