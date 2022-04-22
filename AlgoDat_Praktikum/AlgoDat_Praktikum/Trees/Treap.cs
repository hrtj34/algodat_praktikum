using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class Treap : BinSearchTree
    {
        class Node
        {
            public int elem;
            public int priority;
            //public int level;
            Random zufall = new Random();
            public Node left;
            public Node right;
            public Node prev;
            public Node(int elem)
            {
                this.elem = elem;
                priority = zufall.Next(100);
            }

            public Node(int elem, int priority)
            {
                this.elem = elem;
                this.priority = priority;
            }

            public override string ToString() => $"({elem}, {priority})";
        }

        Node root;
        //int maxLevel = 0;

        bool binInsert(Node newNode) //zukuenftig aus Klasse BinTree
        {
            Node elem = root;
            bool done = false;
            //int count = 1;

            while (!done)
            {
                //newNode.level = count++;
                if(elem.elem == newNode.elem)
                {
                    break;
                }
                else if(elem.elem < newNode.elem)
                {
                    if (elem.right == null)
                    {
                        elem.right = newNode;
                        newNode.prev = elem;
                        done = true;
                    }
                    else
                        elem = elem.right;
                }
                else
                {
                    if (elem.left == null)
                    {
                        elem.left = newNode;
                        newNode.prev = elem;
                        done = true;
                    }
                    else
                        elem = elem.left;
                }
            }
            return done;
        }

//Rotation Funktionen ebenfalls moeglicherweiser aus BinTree
        void leftRotation(Node currentNode)
        {
            if(currentNode.prev == root)
            {
                currentNode.prev.right = currentNode.left;
                if (currentNode.left != null)
                    currentNode.left = currentNode.prev;
                //if (currentNode.prev == currentNode.prev.prev.left)
                //    currentNode.prev.prev.left = currentNode;
                //else
                //    currentNode.prev.prev.right = currentNode;
                Node temp = currentNode.prev;
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
                Node temp = currentNode.prev;
                currentNode.prev = currentNode.prev.prev;
                temp.prev = currentNode;
            }
        }
        void rightRotiation(Node currentNode)
        {
            if (currentNode.prev == root)
            {
                currentNode.prev.left = currentNode.right;
                if (currentNode.right != null)
                    currentNode.right.prev = currentNode.prev;
                currentNode.right = currentNode.prev;
                //if (currentNode.prev.prev.right == currentNode.prev)
                //    currentNode.prev.prev.right = currentNode;
                //else
                //    currentNode.prev.prev.left = currentNode;
                Node temp = currentNode.prev;
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
                Node temp = currentNode.prev;
                currentNode.prev = currentNode.prev.prev;
                temp.prev = currentNode;
            }
        }
        void heapRotation(Node currentNode)
        {
            if(currentNode.prev == root && currentNode.priority < root.priority)
            {
                Node temp = root;
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
                while(currentNode.prev.priority > currentNode.priority)
                {
                    if (currentNode.prev.right == currentNode)
                        leftRotation(currentNode);
                    else
                        rightRotiation(currentNode);
                }
            }
        }

        public bool insert(int elem)
        {
            if(root == null)
            {
                root = new Node(elem);
                //root.level = 0;
                return true;
            }
            else
            {
                Node newNode = new Node(elem);
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

        string PrintHorizontal(Node current, int n)
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

        public override string ToString()
        {
            string res = "";
            res += PrintHorizontal(root, 0);
            return res;
        }
    }
}
