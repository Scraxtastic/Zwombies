using System;

namespace Map
{
    public class TileHealthIndex : IComparable<TileHealthIndex>
    {
        public int index;
        public TileHealth tileHealth;

        public TileHealthIndex(int index)
        {
            this.index = index;
        }

        public TileHealthIndex(int index, TileHealth tileHealth) : this(index)
        {
            this.tileHealth = tileHealth;
        }

        public int CompareTo(TileHealthIndex other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return index.CompareTo(other.index);
        }
    }
}