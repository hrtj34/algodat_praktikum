using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class MultiSetUnsortedLinkedList : BaseList, IMultiSetUnsorted
    {

        public bool delete(int elem)
        {
            if (!SearchNode(elem))
                return false;
            if (root.key == elem)
            { 
                root = root.next; return true; 
            }
            else
                runner.next = runner.next.next;
            return true;
            
        }

        public bool insert(int elem)
        {
            runner = root;

            ListNode neu = new ListNode(elem);
            if (root == null)
            {
                root = neu;
                return true;
            }

            else
            {
                while (runner.next != null)
                {
                    runner = runner.next;
                }

                runner.next = neu;
                return true;

            }




        }

        public void print()
        {
            Console.WriteLine("hallo");
            return;
        }

        public bool search(int elem)
        {
            return SearchNode(elem);
        }

        
    }
}
