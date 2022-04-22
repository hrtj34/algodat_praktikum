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

            public override string ToString() => $"({key}, {prio})";
        }

        
        //Rotation Funktionen ebenfalls moeglicherweiser aus BinTree
        void leftRotation(TreapNode currentNode)
        {
            if(currentNode.prev == root)
            {
                currentNode.prev.right = currentNode.left;
                if (currentNode.left != null)
                    currentNode.left = currentNode.prev;
                TreeNode temp = currentNode.prev;   //evt. falscher Typ
                currentNode.prev = currentNode.prev.prev;
                temp.prev = currentNode;
                root = currentNode;
            }
            else
            {
                currentNode.prev.right = currentNode.left;
                if (currentNode.left != null)
                    currentNode.left.prev = currentNode.prev;
                currentNode.left = currentNode.prev;
                if(currentNode.prev == currentNode.prev.prev.left)
                    currentNode.prev.prev.left = currentNode;
                else
                    currentNode.prev.prev.right = currentNode;
                TreeNode temp = currentNode.prev;   //evt. falscher Typ
                currentNode.prev = currentNode.prev.prev;
                temp.prev = currentNode;
            }
        }
        void rightRotiation(TreapNode currentNode)
        {
            if (currentNode.prev == root)
            {
                currentNode.prev.left = currentNode.right;
                if (currentNode.right != null)
                    currentNode.right.prev = currentNode.prev;
                currentNode.right = currentNode.prev;
                TreeNode temp = currentNode.prev;   //evt. falscher Typ
                currentNode.prev = currentNode.prev.prev;
                temp.prev = currentNode;
                root = currentNode;
            }
            else
            {
                currentNode.prev.left = currentNode.right;
                if(currentNode.right != null)
                    currentNode.right.prev = currentNode.prev;
                currentNode.right = currentNode.prev;
                if (currentNode.prev.prev.right == currentNode.prev)
                    currentNode.prev.prev.right = currentNode;
                else
                    currentNode.prev.prev.left = currentNode;
                TreeNode temp = currentNode.prev;   //evt. falscher Typ
                currentNode.prev = currentNode.prev.prev;
                temp.prev = currentNode;
            }
        }
        void heapRotation(TreapNode currentNode)
        {
            if(currentNode.prev == root && currentNode.prio < (root as TreapNode).prio)
            {
                TreeNode temp = root;
                if(root.right == currentNode)
                {
                    root = currentNode;
                    root.left = temp;
                    root.prev = null;
                    temp.prev = root;
                    temp.right = null;
                }
                else
                {
                    root = currentNode;
                    root.right = temp;
                    root.prev = null;
                    temp.prev = root;
                    temp.left = null;
                }
            }
            else
            {
                while((currentNode.prev as TreapNode).prio > currentNode.prio)
                {
                    if (currentNode.prev.right == currentNode)
                        leftRotation(currentNode);
                    else
                        rightRotiation(currentNode);
                }
            }
        }

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
                    heapRotation(newNode as TreapNode);
                    return true;
                }
                return false;
            }
        }
    }
}
