using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class MultiSetSortedLinkedList : BaseList, IMultiSetSorted
    {

        public bool insert(int elem)
        {
            runner = root;

            ListNode remember;

            ListNode neu = new ListNode(elem);

            

            if (root == null)
            {
                root = neu;
                return true;
            }

            if (root.key > elem)
            {
                neu.next = root;
                root = neu;
                return true;
            }

            while (runner.next != null)
            {
                if (runner.next.key >= elem)
                {
                    remember = runner.next;
                    runner.next = neu;
                    neu.next = remember;
                    return true;
                }

                runner = runner.next;

            }

            runner.next = neu;
            return true;

        }

    }
}
