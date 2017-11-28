using System;
using System.Collections.Generic;
using Vlcr.Core.Collections;

namespace Vlcr.StateSearch
{
    // Done!
    internal static class CollectionExtension
    {
        // Done!
        public static void Add<T>(this IDictionary<int, State<T>> dic, State<T> e) where T : class, ILayout<T>
        {
            int key = e.Layout.GetHashCode();
            if (dic.ContainsKey(key) == false)
            {
                dic.Add(key, e);
            }
        }

        public static void FilterAdd<T>(this PriorityQueue<State<T>> list, IDictionary<int, State<T>> other, State<T> node, State<T> goal) where T : class, ILayout<T>
        {
            // Expandir o estado actual!
            IList<T> childreen = node.Children(node);

            int count = childreen.Count;
            for (int i = 0; i < count; ++i)
            {
                T c = childreen[i];
                // Adicionar à lista somente se não existir na lista de fechados.
                if (other.ContainsKey(c.GetHashCode())) continue;

                State<T> s = new State<T>(c, node);
                list.Enqueue(s.Cost + State<T>.Estimate(node, c, goal), s);
            }
        }

    }
}
