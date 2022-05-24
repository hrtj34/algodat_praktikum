namespace AlgoDat_Praktikum
{
    
        
    class BaseList
    {
        public class ListNode : BaseNode
        {

            public ListNode next = null;
            public ListNode(int Key) : base(Key) { }
    
        }

        public ListNode root = null;
        public ListNode runner = null;

        protected bool SearchNode(int elem)
        {
            runner = root;

            if (root == null)
                return false;


            while (runner.next != null)
            {

                if (runner.next.key == elem)
                    return true;

                runner = runner.next;
            }


            return false;
        }
    }




}
