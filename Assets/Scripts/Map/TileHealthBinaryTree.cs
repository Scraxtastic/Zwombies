using System;

namespace Map
{
    public class TileHealthBinaryTree : IComparable<TileHealthBinaryTree>
    {
        public BinaryTree<TileHealthIndex> tileHealthTree = null;
        public int index;
        
        public TileHealthBinaryTree(int index)
        {
            this.index = index;
            tileHealthTree = new BinaryTree<TileHealthIndex>();
        }

        public int CompareTo(TileHealthBinaryTree other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return index.CompareTo(other.index);
        }
    }
}