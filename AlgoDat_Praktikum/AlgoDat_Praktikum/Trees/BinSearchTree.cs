using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    //// BinSearchTree test run
    //BinSearchTree a = new BinSearchTree();
    //a.insert(5);
    //        a.insert(2);
    //        a.insert(1);
    //        a.insert(7);
    //        a.insert(10);
    //        a.insert(13);
    //        a.insert(14);
    //        a.insert(3);
    //        a.insert(8);
    //        a.insert(6);
    //        a.print();
    //        Console.WriteLine();
    //        a.delete(2);
    //        a.print();
    //        a.delete(5);
    //        Console.WriteLine();
    //        a.print();
    //        a.delete(8);
    //        Console.WriteLine();
    //        a.print();
    //        a.delete(3);
    //        Console.WriteLine();
    //        a.print();

    //        Console.WriteLine("-------------");
    //        Console.WriteLine("-------------");
    //        Console.WriteLine("-------------");
    //        Console.WriteLine("-------------");
    //        BinSearchTree b = new BinSearchTree();
    //b.insert(14);
    //        b.insert(3);
    //        b.insert(1);
    //        b.insert(2);
    //        b.insert(8);
    //        b.insert(6);
    //        b.insert(5);
    //        b.insert(4);
    //        b.insert(7);
    //        b.insert(13);
    //        b.insert(12);
    //        b.insert(10);
    //        b.insert(9);
    //        b.insert(11);
    //        b.insert(55);
    //        b.insert(35);
    //        b.insert(20);
    //        b.insert(16);
    //        b.insert(21);
    //        b.insert(30);
    //        b.insert(45);
    //        b.insert(40);
    //        b.insert(68);
    //        b.insert(104);
    //        b.insert(99);
    //        b.insert(66);
    //        b.insert(60);
    //        b.insert(67);
    //        b.print();
    //        Console.WriteLine();
    //        b.delete(14);
    //        b.print();
    //        Console.WriteLine();
    //        b.delete(35);
    //        b.print();
    //        Console.WriteLine();
    //        b.delete(68);
    //        b.print();
    //        Console.WriteLine();
    //        b.delete(104);
    //        b.print();
    //        Console.WriteLine();
    //        b.delete(8);
    //        b.print();
    //        Console.WriteLine();
    //        b.delete(13);
    //        b.print();
    //        Console.WriteLine();
    /////////////////////////////////
    /// <summary>
    /// <para>
    /// Binary Search Tree public functions: .insert(int), .delete(int), .serarch(int) .print()
    /// </para>
    /// TreeNode sends its Key to BaseNode 
    /// <para>
    /// Adds left, right and prev;
    /// Has optional ToString function for TreeNodes for easier debugging
    /// </para>
    ///  ALTE FUNKTIONEN SIND UNTEN KOMMENTIERT!
    /// </summary>
    class BinSearchTree : ISetSorted
    {
        protected class TreeNode : BaseNode
        {
            public TreeNode left = null, right = null, prev = null;
            public TreeNode(int Key) : base(Key) {}

            /////////////////////////
            //// for debugging usage!
            /////////////////////////
            //public override string ToString()
            //{
            //    string lString = (left == null) ? "null" : left.key.ToString();
            //    string rString = (right == null) ? "null" : right.key.ToString();
            //    return $"left:{lString} <- {key} -> right:{rString}";
            //}
        }
        protected TreeNode root = null;

        #region Delete functions, with helper functions
        /// <summary>
        /// Public delete function, refers to private function DeleteNode
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public virtual bool delete(int elem)
        {
            return deleteNode(elem);
        }
        /// <summary>
        /// Private delete function, used by public bool delete
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private bool deleteNode(int elem)
        {
            // initiate some important variables needed for the different delete cases 
            TreeNode tempChild = null;
            TreeNode tempParent = null;
            TreeNode predNode = null;

            TreeNode tempDelete = SearchNode(elem); // search for the Node
            if (tempDelete != null && tempDelete.key == elem)
            {
                ///////---------LEAF-CASE---------///////
                if ((tempDelete.left == null) && (tempDelete.right == null))
                {
                    if (tempDelete == root) // if there is only 1 element (root), delete that single node
                    {
                        root = null;
                        return true;
                    }
                    tempParent = tempDelete.prev;
                    if (tempDelete == tempParent.left) // navigate to delnodes parent and set the coresponding pointer to null
                        tempParent.left = null;
                    else
                        tempParent.right = null;
                }


                ///////---------SINGLE-CHILD-CASE---------///////
                else if ((tempDelete.left == null) || (tempDelete.right == null)) // delnode has either Left orRight child
                {
                    tempChild = tempDelete.left == null ? tempDelete.right : tempDelete.left; // get delnodes child
                    if (tempDelete == root) // if delnode is root, set root to its child, since there is only one and set childs prev to null
                    {
                        root = tempChild;
                        tempChild.prev = null;
                    }
                    else
                        tempParent = tempDelete.prev; // otherwise get delnodes parent

                    if (tempParent != null) // if it DOES have a parent
                    {
                        if (tempDelete == tempParent.left) // make delNodes parent point to delNodes only child (depending on if its a right or left child)
                            tempParent.left = tempChild;
                        else
                            tempParent.right = tempChild;
                        tempChild.prev = tempParent; // set previous of delnodes child to delnodes parent (get rid of delnode completely!)
                    }
                }

                ///////---------MULTIPLE-CHILDREN-CASE---------///////
                else if ((tempDelete.left != null) && (tempDelete.right != null)) // delnode has both a left and a right child
                {
                    predNode = GetPredecessor(tempDelete);  // find its predecessor
                    tempDelete.key = predNode.key;
                    if (predNode.left != null) // predecessor node can only have a LEFT child or NO child
                    {
                        if (predNode.prev.left == predNode) // prednode is on left of its parent
                            predNode.prev.left = predNode.left; // rewire nodes accordingly
                        else // prednode is on right of its parent
                            predNode.prev.right = predNode.left;
                        predNode.left.prev = predNode.prev; // rewire nodes accordingly
                    }
                    else // predNode doesnt have a child ->  can be set to null
                    {
                        if (predNode == predNode.prev.left)
                            predNode.prev.left = null;
                        if (predNode == predNode.prev.right) // rewire nodes accordingly
                            predNode.prev.right = null;
                    }
                }
                return true; // node has been found and probably deleted, unless there is still smthing wrong with this code

            }
            else
                return false; // node couldnt be found!
        }
        
        /// <summary>
        /// Additional function for DeleteNode, find the biggest TreeNode of a (Sub-)Tree, used by DeleteNode
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private TreeNode FindBinSTreeMax(TreeNode root)
        {
            TreeNode current = root; 
            if (current.right == null) // recursively find biggest element of a (Sub-)Tree, if current has a left child
                return current;
            return FindBinSTreeMax(current.right);
        }
        /// <summary>
        /// additional function for DeleteNode to find the predecessor of a TreeNode, used by DeleteNode
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private TreeNode GetPredecessor(TreeNode element)
        {
            ////Get the Node object for an element
            TreeNode current = element;
            if (element != null)
            {
                if (current.left != null)
                    return FindBinSTreeMax(current.left);
                else
                    return null;
            }
            return null;
        }
        #endregion

        #region insert functions
        /// <summary>
        /// Public function to send an int to the private function binInsert
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public virtual bool insert(int elem)
        {
            TreeNode placeHolder = new TreeNode(elem);
            return binInsert(ref placeHolder, elem);
        }

        /// <summary>
        /// NEW binInsert function!!!
        /// works like the old one, just doesnt use searchNode yet
        /// Private binInsert inserts an int value into a Tree, used by public bool insert
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        protected bool binInsert(ref TreeNode newItem, int elem) // remove ref TreeNode
        {
            newItem.key = elem;
            // insert as root!
            if (root == null) // insert a new root into an empty tree
            {
                root = newItem;
                return true;
            }
            TreeNode insKey = SearchNode(elem);
            if (insKey.key == elem) // we found insKey in the tree => ITEM IS ALREADY IN THE TREE ==> FALSE BCS DIDNT INSERT
                return false;
            else // insKey is refering to parent key of where elem should go
            {
                if (elem < insKey.key) // elem is left child of the parent
                {
                    insKey.left = newItem;
                    insKey.left.prev = insKey;
                }
                else // elem is right child of the parent
                {
                    insKey.right = newItem;
                    insKey.right.prev = insKey;
                }
                return true;
            }
        }
        #endregion

        #region search functions
        /// <summary>
        /// Public search function, sends int to searchNode
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public bool search(int elem)
        {
            TreeNode output = SearchNode(elem);
            if (output != null && output.key == elem) // return true in case elem was found (!= null) AND searchNode didnt return its parent (output.key == elem)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Private search function, returns a TreeNode, used by public bool search
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        protected TreeNode SearchNode(int elem)
        {
            TreeNode item = root;
            TreeNode prevItem = null;
            while (item != null)
            {
                if (elem == item.key)
                    return item;

                prevItem = item; // get the previous item in case we need to refer to it for the case that elem is not in tree

                item = (elem < item.key) ? item.left : item.right; // progress through the tree
            }
            // element is not in tree => return the "location" where it should be!
            // prevItem is pointing at the parents location
            return prevItem;
        }
        #endregion

        #region print functions + misc.
        //public void print()
        //{
        //    print(root);
        //}
        private void print(TreeNode item)
        {   // kleiner -- selbst -- größer
            if (item != null)
            {
                Console.Write("( ");
                if (item.left != null)
                    print(item.left);
                // linke Elemente ausgeben
                Console.Write("  " + item.key + "  ");
                if (item.right != null)
                    print(item.right);
                // rechte Elemente ausgeben
                Console.Write(" )");
            }
        }

        // other stuff
        // ALTE binInsert und search
        //protected bool binInsert(ref TreeNode newNode, int elem)
        //{
        //    TreeNode currentNode = root;
        //    if (!search(ref currentNode, elem))
        //    {
        //        if (currentNode.key < elem)
        //        {
        //            currentNode.right = newNode;
        //            newNode.prev = currentNode;
        //            currentNode = currentNode.right;
        //        }
        //        else
        //        {
        //            currentNode.left = newNode;
        //            newNode.prev = currentNode;
        //            currentNode = currentNode.left;
        //        }
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        ////protected Such-Funktion, die auch Einfüg bzw. Lösch-Position zurückgibt
        protected bool search(ref TreeNode node, int elem)
        {
            int count = 0;
            while (true)
            {
                if (node.key == elem)
                {
                    return true;
                }
                else if (node.key<elem)
                {
                    if (node.right == null)
                    {
                        return false;
                    }
                    else
                        node = node.right;
                }
                else
                {
                    if (node.left == null)
                    {
                        return false;
                    }
                    else
        node = node.left;
                }
                count++;
            }
        }

        // Horizontale Ausgabe eines Trees mit Einrückungen (Code aus Übung)
        public void print()
        {
            int len;
            if (this is Treap)
                len = 9;
            else
                len = 4;
            Console.WriteLine(PrintHorizontal(root,0,len));
        }
        string PrintHorizontal(TreeNode current, int n,int len)
        {
            string res = "";
            if (current != null)
            {
                string buffer = "";
                for (int i = 0; i <= len; i++)
                {
                    buffer += " ";
                }

                res += PrintHorizontal(current.right, n + 1,len);
                res += "\n";
                for (int i = 0; i < n; i++)
                    res += buffer;
                res += $"--{current}";
                res += PrintHorizontal(current.left, n + 1,len);
            }
            return res;
        }

        //public void PreOrderPrint()
        //{
        //    PreOrderPrint(root);
        //}
        //private void PreOrderPrint(BItem item)
        //{   // kleiner -- selbst -- größer
        //if (item != null)
        //{
        //    Console.Write("( ");
        //    Console.Write("  " + item.number + "  ");

        //    if (item.left != null)
        //        PreOrderPrint(item.left);
        //    // linke Elemente ausgeben

        //    if (item.right != null)
        //        PreOrderPrint(item.right);
        //    // rechte Elemente ausgeben
        //    Console.Write(" )");
        //}
        //}

        // if needed enumerators
        //private IEnumerable<int> GetEnum(BItem item)
        //{
        //    if (item != null)
        //    {
        //        foreach (var item2 in GetEnum(item.left))
        //            yield return item2;

        //        yield return item.number;

        //        foreach (var item2 in GetEnum(item.right))
        //            yield return item2;
        //    }
        //}
        //public IEnumerator<int> GetEnumerator()
        //{
        //    foreach (var item in GetEnum(root))
        //        yield return item;
        //}
        #endregion
    }
}
