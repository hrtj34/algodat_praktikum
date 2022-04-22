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

        public bool delete(int elem)
        {
            throw new NotImplementedException();
        }

        public bool insert(int elem)
        {
            throw new NotImplementedException();
        }

        public void print()
        {
            throw new NotImplementedException();
        }

        public bool search(int elem)
        {
            throw new NotImplementedException();
        }
    }
}
