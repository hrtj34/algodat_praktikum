using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class MultiSetUnsortedLinkedList : BaseList, IMultiSetUnsorted
    {

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


        
    }
}
