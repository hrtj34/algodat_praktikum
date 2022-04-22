using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoDat_Praktikum
{
    class BinSearchTree : ISetSorted
    {
        protected class TreeNode : BaseNode
        {
            public TreeNode left;
            public TreeNode right;
            public TreeNode prev;

            public TreeNode(int Key) : base(Key) {}

        }
        
        protected TreeNode root;

        public bool delete(int elem)
        {
            throw new NotImplementedException();
        }

        public virtual bool insert(int elem)
        {
            throw new NotImplementedException();
        }

        //private Insert-Funktion zu Verrerbungszwecken
        protected bool binInsert(ref TreeNode newNode, int elem)
        {
            TreeNode currentNode = root;
            if (!search(ref currentNode, elem))
            {
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

        // Horizontale Ausgabe eines Trees mit Einrückungen (Code aus Übung)
        public void print()
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

        public bool search(int elem)
        {
            throw new NotImplementedException();
        }

        //private Such-Funktion, die auch Einfüg bzw. Lösch-Position zurückgibt
        private bool search(ref TreeNode node, int elem)
        {
            int count = 0;
            while (true)
            {
                if (node.key == elem)
                {
                    return true;
                }
                else if (node.key < elem)
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
    }
}
