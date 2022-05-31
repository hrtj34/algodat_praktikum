using System;

namespace AlgoDat_Praktikum
{
    class AVLTree : BinSearchTree
    {
        class AVLNode : TreeNode
        {
            public int balance;
            public int hR;
            public int hL;
            public AVLNode(int Key) : base(Key) { hR = hL = 0; }
        }
        /// <summary>
        /// Functions which handle rotations within the tree. (The naming of variables
        /// corresponds to that of the examples given in the AlgoDat script)
        /// </summary>
        /// <param name="node"></param>
        private void RightRotation(AVLNode a)
        {
            AVLNode b = a.prev as AVLNode;
            AVLNode B = a.right as AVLNode;
            AVLNode predecessor = b.prev as AVLNode;

            if (predecessor != null)// vorgaenger wird festgelegt
            {
                if (a.key < predecessor.key)
                    predecessor.left = a;
                else
                    predecessor.right = a;
                a.prev = predecessor;
            }
            else //vorgaenger ist null, a ist root
            {
                a.prev = null;
                root = a;
            }
            a.right = b;
            b.prev = a;
            b.left = B;
            if (B != null)
            {
                B.prev = b;
                //Bestimme hL fuer b
                if (B.hL >= B.hR)
                    b.hL = B.hL + 1;
                else
                    b.hL = B.hR + 1;
            }
            else
                b.hL = 0;
            //Bestimme hR fuer a
            if (b.hL >= b.hR)
                a.hR = b.hL + 1;
            else
                a.hR = b.hR + 1;
            //Berechne balancen von a und b neu:
            a.balance = a.hR - a.hL;
            b.balance = b.hR - b.hL;
        }
        private void LeftRotation(AVLNode b)
        {
            AVLNode a = b.prev as AVLNode;
            AVLNode B = b.left as AVLNode;
            AVLNode predecessor = a.prev as AVLNode;
            if (predecessor != null)
            {
                if (b.key < predecessor.key)
                    predecessor.left = b;
                else
                    predecessor.right = b;
                b.prev = predecessor;
            }
            else
            {
                b.prev = null;
                root = b;
            }
            b.left = a;
            a.prev = b;
            a.right = B;
            if (B != null)
            {
                B.prev = a;
                //Bestimme hR fuer a
                if (B.hL >= B.hR)
                    a.hR = B.hL + 1;
                else
                    a.hR = B.hR + 1;
            }
            else
                a.hR = 0;
            //Bestimme hL fuer b
            if (a.hL >= a.hR)
                b.hL = a.hL + 1;
            else
                b.hL = a.hR + 1;

            //Berechne balancen von a und b neu:
            a.balance = a.hR - a.hL;
            b.balance = b.hR - b.hL;
        }

        /// <summary>
        /// Fixbalance uses rotations in order to fix the balance of a given node.
        /// </summary>
        /// <param name="node"></param>
        private void FixBalance(AVLNode node)
        {
            if (node.balance == -2 && ((node.left as AVLNode).balance == -1 || (node.left as AVLNode).balance == 0)) // Tree is leaning to the left
                RightRotation(node.left as AVLNode);
            else if (node.balance == -2 && (node.left as AVLNode).balance == 1)
            {
                LeftRotation(node.left.right as AVLNode);
                RightRotation(node.left as AVLNode);
            }
            else if (node.balance == 2 && ((node.right as AVLNode).balance == 1 || (node.right as AVLNode).balance == 0)) // Tree is leaning to the right 
                LeftRotation(node.right as AVLNode);
            else if (node.balance == 2 && (node.right as AVLNode).balance == -1)
            {
                RightRotation(node.right.left as AVLNode);
                LeftRotation(node.right as AVLNode);
            }
        }



        /// <summary>
        /// FixTree is able to take any search tree and turn it into an AVL Tree.
        /// It is a recursive function based on postorder traversing through the Tree.
        /// This function is less efficient but more generally aplicable than Delbalancing
        /// and Addbalancing, which is why it only is used if an element which is not a
        /// leaf is deleted.
        /// </summary>
        /// <param name="node"> The tree which is branching out from "node" is the tree 
        /// which is turned into an AVL tree</param>
        private void FixTree(AVLNode node)
        {
            if (node != null)
            {
                FixTree(node.left as AVLNode);
                FixTree(node.right as AVLNode);

                if (node.right == null)
                    node.hR = 0;
                else
                {
                    if ((node.right as AVLNode).hR >= (node.right as AVLNode).hL)
                        node.hR = (node.right as AVLNode).hR + 1;
                    else
                        node.hR = (node.right as AVLNode).hL + 1;
                }

                if (node.left == null)
                    node.hL = 0;
                else
                {
                    if ((node.left as AVLNode).hR >= (node.left as AVLNode).hL)
                        node.hL = (node.left as AVLNode).hR + 1;
                    else
                        node.hL = (node.left as AVLNode).hL + 1;
                }
                node.balance = node.hR - node.hL;
                // Gibts probleme? - rotiere
                FixBalance(node);
            }
        }

        /// <summary>
        /// Addbalancing recalculates hR, hL and balance after the inserting of a new element,
        /// or deleting of a leaf elemnt, and uses, if necessary, FixBalance to rebalance the Tree.
        /// </summary>
        /// <param name="node"></param>
        private void Addbalancing(AVLNode currentNode, int count, bool del = false)
        {          
            if (currentNode != null && (count != currentNode.hL || count != currentNode.hR))
            {
                AVLNode SafeNode = currentNode.prev as AVLNode;

                if (currentNode.right != null) 
                    if ((currentNode.right as AVLNode).hR >= (currentNode.right as AVLNode).hL)
                        currentNode.hR = (currentNode.right as AVLNode).hR + 1;
                    else
                        currentNode.hR = (currentNode.right as AVLNode).hL + 1;
                else
                    currentNode.hR = 0;
                if (currentNode.left != null)
                    if ((currentNode.left as AVLNode).hR >= (currentNode.left as AVLNode).hL)
                        currentNode.hL = (currentNode.left as AVLNode).hR + 1;
                    else
                        currentNode.hL = (currentNode.left as AVLNode).hL + 1;
                else
                    currentNode.hL = 0;

                currentNode.balance = currentNode.hR - currentNode.hL;//da
                if (currentNode.balance != 2 && currentNode.balance != -2) // If the node is not inbalanced -> try the previous node
                    Addbalancing(SafeNode, ++count);
                else
                {
                    FixBalance(currentNode);
                    if (del)
                        Addbalancing(SafeNode,++count);
                }
            }
        }
        
        /// <summary>
        /// insert simply inserts a key, "elem", as a new node into the tree. 
        /// </summary>
        /// <param name="elem">key of a new to be inserted node</param>
        /// <returns>returns boolean value, true if the value was inserted into tree, 
        /// false if the value was already in tree</returns>
        public override bool insert(int elem)
        {
            if (root == null)
            {
                root = new AVLNode(elem);
                return true;
            }
            else
            {
                TreeNode newNode = new AVLNode(elem);
                if (binInsert(ref newNode, elem))
                {
                    Addbalancing(newNode.prev as AVLNode, 1);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// if the tree contains a node with elem as key, that node is deleted
        /// </summary>
        /// <param name="elem"></param>
        /// <returns>returns boolean value, true if a node with the key "elem" was deleted from tree, 
        /// false if the tree contains no such node</returns>
        public override bool delete(int elem)
        {
            TreeNode tempNode = root;
            search(ref tempNode, elem);
            bool tempBool;
            if (tempNode == root)
                return base.delete(elem);
            else if (tempNode.right == null && tempNode.left == null) // a leaf is being deleted
            {
                tempNode = tempNode.prev;
                tempBool = base.delete(elem);
                if (tempBool)
                    Addbalancing(tempNode as AVLNode,0,true);
            }
            else // the to be deleted node is not a leaf
            {
                tempBool = base.delete(elem);
                FixTree(root as AVLNode);

            }
            return tempBool;
        }


        // Zur Testausgabe:

        //public void inorderVis()
        //{
        //    Console.WriteLine("\n_____________________________________________________\nBaum:");
        //    inorderVis(root as AVLNode, 0);
        //}
        //private void inorderVis(AVLNode node, int order)
        //{
        //    if (node != null)
        //    {
        //        inorderVis(node.left as AVLNode, ++order);
        //        for (int i = 0; i < order; i++)
        //            Console.Write("         ");
        //        Console.WriteLine("------" + node.key);
        //        inorderVis(node.right as AVLNode, order);
        //    }
        //}
    }
}
