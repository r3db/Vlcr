using System;
using System.Collections.Generic;
using Vlcr.Agent;
using Vlcr.Core.Collections;

namespace Vlcr.CognitiveStateSearch
{
    // Done!
    internal static class CollectionExtension
    {
        // Done!
        public static void Add<T>(this IDictionary<int, CognitiveState<T>> dic, CognitiveState<T> e) where T : class, ICognitiveLayout<T>
        {
            int key = e.Layout.GetHashCode();
            if (dic.ContainsKey(key) == false)
            {
                dic.Add(key, e);
            }
        }

        // Done!
        public static void FilterAdd<T>(this PriorityQueue<CognitiveState<T>> list, IDictionary<int, CognitiveState<T>> other, CognitiveState<T> node, CognitiveState<T> goal, AgentCharacter agentCharacter) where T : class, ICognitiveLayout<T>
        {
            // Expandir o estado actual!
            IList<T> childreen = node.Children(node, agentCharacter);

            int count = childreen.Count;
            for (int i = 0; i < count; ++i)
            {
                T c = childreen[i];
                // Adicionar à lista somente se não existir na lista de fechados.
                if (other.ContainsKey(c.GetHashCode())) continue;

                CognitiveState<T> s = new CognitiveState<T>(c, node, agentCharacter);
                list.Enqueue(s.Cost + CognitiveState<T>.Estimate(node, c, goal), s);
            }
        }

    }
}
