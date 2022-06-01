using System;

namespace AlgoDat_Praktikum
{


    abstract class BaseList
    {
        public class ListNode : BaseNode
        {

            public ListNode next = null;
            public ListNode(int Key) : base(Key) { }

        }

        public ListNode root = null;
        public ListNode runner = null;

        public void print()
        {

            if (root != null)
            { runner = root; Console.Write("| "); }
            else
                return;

            while (runner != null)
            {
                Console.Write(runner.key + " | ");
                runner = runner.next;
            }

            return;
        }

        public bool search(int elem)
        {
            runner = root;

            if (root == null)
                return false;

            if (root.key == elem)
                return true;

            while (runner.next != null)
            {

                if (runner.next.key == elem)
                    return true;

                runner = runner.next;
            }


            return false;

            //Nach Search is runner = Element vor gefundenem
            //Falls nicht gefunden ist runner das letzte Element
        }

        public bool delete(int elem)
        {
            if (!search(elem))
                return false;

            if (root.key == elem)
            {
                root = root.next; return true;
            }
            else
                runner.next = runner.next.next;
            return true;

            //while (runner.next != null)
            //{
            //    if (runner.next.key == elem)
            //    {
            //        runner.next = runner.next.next;
            //        return true;
            //    }
            //    runner = runner.next;
            //}

            //return false; 

        }

    }




}
