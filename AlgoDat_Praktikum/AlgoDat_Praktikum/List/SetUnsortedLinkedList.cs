using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class SetUnsortedLinkedList : BaseList, ISetUnsorted
    {
        

        public bool insert(int elem)
        {
            if (!search(elem))
            {
                ListNode neu = new ListNode(elem);

                if (root == null)
                {
                    root = neu;
                    return true;
                }


                runner.next = neu;
                return true;

               

            }

            else return false;


        }




    }
}
