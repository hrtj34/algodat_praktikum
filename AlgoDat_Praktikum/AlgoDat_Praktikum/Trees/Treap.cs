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

            public TreapNode(int elem, int priority) : base(elem) // Konstruktor zum Debuggen
            {
                this.prio = priority;
            }

            public override string ToString() => $"({key}, {prio})";
        }

        TreapNode root;
        //int maxLevel = 0;
        private bool search(ref TreeNode node, int elem)
        {
            while (true)
            {
                //newNode.level = count++;
                if(node.key == elem)
                {
                    return true;
                }
                else if(node.key < elem)
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
            }
        }

        protected bool binInsert(ref TreeNode currentNode, int elem) //zukuenftig aus Klasse BinTree
        {
            //int count = 1;
            if(!search(ref currentNode,elem))
            {
                TreapNode newNode = new TreapNode(elem);
                if (currentNode.key < elem)
                {
                    currentNode.right = newNode;
                    newNode.prev = currentNode;
                    currentNode = currentNode.right;
                }
                else
                {
                    currentNode.left = newNode;
                    newNode.prev = currentNode;
                    currentNode = currentNode.left;
                }
                return true;
            }
            else
                return false;
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
            if(currentNode.prev == root && currentNode.prio < root.prio)
            {
                TreapNode temp = root;
                if(root.left == currentNode)
                {
                    root = currentNode;
                    root.left = temp;
                    root.prev = null;
                    temp.prev = root;
                }
                else
                {
                    root = currentNode;
                    root.right = temp;
                    temp.prev = root;
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

        public new bool insert(int elem)
        {
            if(root == null)
            {
                root = new TreapNode(elem);
                //root.level = 0;
                return true;
            }
            else
            {
                TreapNode newNode = new TreapNode(elem);
                bool inserted = binInsert(newNode);
                if(inserted)
                    heapRotation(newNode);
                //if (newNode.level > maxLevel)
                    //maxLevel = newNode.level;
                return inserted;
            }
        }

        public bool search(int elem) //ebenfalls aus BinTree erben
        {
            return true;
        }

        public bool delete(int elem)
        {
            return true;
        }

        // Horizontale Ausgabe eines Treaps mit Einrückungen
        public void Print()
        {
            PrintHorizontal(root, 0);
        }

        string PrintHorizontal(TreeNode current, int n)
        {
            string res = "";
            if(current != null)
            {
                
                res += PrintHorizontal(current.right, n + 1);
                res += "\n";
                for (int i = 0; i < n; i++)
                    res += "\t";
                res += $"--{current}";
                res += PrintHorizontal(current.left, n + 1);
            }
            return res;
        }
    }
}
