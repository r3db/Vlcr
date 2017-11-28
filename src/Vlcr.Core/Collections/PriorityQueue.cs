using System;
using System.Collections.Generic;

namespace Vlcr.Core.Collections
{
    // Done!
    [Serializable]
    public sealed class PriorityQueue<V>
    {
        // Done!
        #region Internal Instance Data

        private readonly Dictionary<float, Stack<V>> list = new Dictionary<float, Stack<V>>();
        private readonly SortedList<float, float> keys = new SortedList<float, float>();

        #endregion

        // Done!
        #region Methods

        public void Enqueue(float priority, V value)
        {
            if (list.ContainsKey(priority) == false)
            {
                keys.Add(priority, 0);
                list.Add(priority, new Stack<V>());
            }

            list[priority].Push(value);
            Count += 1;
        }

        public V Dequeue()
        {
            float k = keys.Keys[0];

            Stack<V> pair = list[k];
            V v = pair.Pop();
            if (pair.Count == 0)
            {
                list.Remove(k);
                keys.Remove(k);
            }
            Count -= 1;
            return v;
        }

        public V Peek()
        {
            return list[keys.Keys[0]].Peek();
        }

        #endregion

        // Done!
        #region Properties

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public int Count { get; private set; }

        #endregion
    }
}