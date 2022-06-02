using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class Treap : BinSearchTree
    {
        class TreapNode : TreeNode
        {
            public int prio;
            //public int level;
            Random zufall = new Random();
            public TreapNode(int elem) : base(elem) //eigentlicher Konstruktor 
            {
                prio = zufall.Next(100);
            }

            public TreapNode(int elem, int prio) : base(elem)
            {
                this.prio = prio;
            }
            public override string ToString() => $"({key};{prio})";
        }

        //Rotation Funktionen ebenfalls moeglicherweiser aus BinTree
        /// <summary>
        /// private function that rotates a node left
        /// </summary>
        /// <param name="currentNode"></param>
        void leftRotation(TreapNode currentNode)
        {
            TreeNode temp = currentNode.prev;
            currentNode.prev.right = currentNode.left;
            if (currentNode.left != null)
                currentNode.left.prev = currentNode.prev;
            currentNode.left = currentNode.prev;

            if (currentNode.prev != root)
            {
                if (currentNode.prev == currentNode.prev.prev.left)
                    currentNode.prev.prev.left = currentNode;
                else
                    currentNode.prev.prev.right = currentNode;
            }
            else
                root = currentNode;

            currentNode.prev = currentNode.prev.prev;
            temp.prev = currentNode;
        }

        /// <summary>
        /// private function that rotates a node right
        /// </summary>
        /// <param name="currentNode"></param>
        void rightRotation(TreapNode currentNode)
        {
            TreeNode temp = currentNode.prev;
            currentNode.prev.left = currentNode.right;
            if (currentNode.right != null)
                currentNode.right.prev = currentNode.prev;
            currentNode.right = currentNode.prev;

            if (currentNode.prev != root)
            {
                if (currentNode.prev.prev.right == currentNode.prev)
                    currentNode.prev.prev.right = currentNode;
                else
                    currentNode.prev.prev.left = currentNode;
            }
            else
                root = currentNode;

            currentNode.prev = currentNode.prev.prev;
            temp.prev = currentNode;
        }
        
        
        /// <summary>
        /// public insert function for treap, sends integer element to correct position in treap
        /// calls on private functions binInsert from BinSearchTree and rotation
        /// </summary>
        /// <param name="elem"> integer value, data of coresponding TreapNode </param>
        /// <returns> returns boolean value, 
        /// true = value was inserted into treap, false = value was already in treap </returns>
        public override bool insert(int elem)
        {
            if(root == null)
            {
                root = new TreapNode(elem);
                return true;
            }
            else
            {
                TreeNode newNode = new TreapNode(elem);
                if (binInsert(ref newNode, elem))
                {
                    TreapNode currentNode = newNode as TreapNode;
                    while (currentNode != root && (currentNode.prev as TreapNode).prio > currentNode.prio)
                    {
                        TreeNode temp = currentNode.prev;
                        if (currentNode.prev.right == currentNode)
                            leftRotation(currentNode);
                        else
                            rightRotation(currentNode);
                    }
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Deletes Node of given key from Treap
        /// </summary>
        /// <param name="elem">key of element</param>
        /// <returns>Return if deletion took place/ if key was in treap at all</returns>
        public override bool delete(int elem)
        {
            if (root == null)
                return false;
            if (elem == root.key && root.left == null && root.right == null)
            {
                root = null;
                return true;
            }
            TreapNode delNode = SearchNode(elem) as TreapNode;

            if (delNode != null)
            {
                //rotates delNode down the treap until it becomes a leaf
                while (!(delNode.left == null && delNode.right == null))
                {
                    if (delNode.left != null && delNode.right != null)
                    {
                        if ((delNode.left as TreapNode).prio < (delNode.right as TreapNode).prio)
                            rightRotation(delNode.left as TreapNode);
                        else
                            leftRotation(delNode.right as TreapNode);
                    }
                    else if (delNode.left != null)
                        rightRotation(delNode.left as TreapNode);
                    else if (delNode.right != null)
                        leftRotation(delNode.right as TreapNode);
                }

                //disconnects delNode from the rest of the treap
                if (delNode.prev.right == delNode)
                    delNode.prev.right = null;
                else
                    delNode.prev.left = null;
                delNode.prev = null;

                return true;
            }
            else
                return false;

        }

    }
}
