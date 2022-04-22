using System;

namespace AlgoDat_Praktikum
{
    abstract class BaseNode
    {
        public int key;
        public BaseNode(int Key)
        {
            key = Key;
        }
        public override string ToString()
        {
            return key.ToString();
        }
    }
}