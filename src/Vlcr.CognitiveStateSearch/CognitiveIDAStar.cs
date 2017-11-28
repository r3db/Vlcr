using System;
using System.Collections.Generic;
using Vlcr.Agent;
using Vlcr.Core.Collections;

namespace Vlcr.CognitiveStateSearch
{
    // Done!
    public sealed class IDAStar<T> : ICognitiveStateSearchable<T> where T : class, ICognitiveLayout<T>
    {
        // Done!
        #region Internal Data

        private readonly CognitiveState<T> start;
        private readonly CognitiveState<T> goal;
        private readonly AgentCharacter agentCharacter;

        private CognitiveState<T> result;
        private int cutOff;

        #endregion

        // Done!
        #region .Ctor

        public IDAStar(T start, T goal, AgentCharacter agentCharacter)
        {
            this.start = new CognitiveState<T>(start, agentCharacter);
            this.goal = new CognitiveState<T>(goal, agentCharacter);
            this.agentCharacter = agentCharacter;

            // Vamos definir um nivel de corte!
            cutOff = (int)Math.Ceiling(start.GetHeuristic(start, goal));

        }

        #endregion

        // Done!
        #region Methods

        private bool SearchIDAStar()
        {
            PriorityQueue<CognitiveState<T>> open = new PriorityQueue<CognitiveState<T>>();
            PriorityQueue<CognitiveState<T>> frontier = new PriorityQueue<CognitiveState<T>>();

            // Reinicializar as variáveis
            result = null;

            // Vamos começar a expandir a partir deste estado!
            open.Enqueue(0, start);
            bool found;

            do
            {
                found = SearchAStar(open, frontier);
                open = frontier;
                frontier = new PriorityQueue<CognitiveState<T>>();
            } while (found == false || frontier.Count != 0);

            return true;
        }

        private bool SearchAStar(PriorityQueue<CognitiveState<T>> open, PriorityQueue<CognitiveState<T>> frontier)
        {
            IDictionary<int, CognitiveState<T>> closed = new Dictionary<int, CognitiveState<T>>(500);

            while (open.Count > 0)
            {
                // Apanhar o "melhor" estado possivel!
                CognitiveState<T> node = open.Dequeue();

                // Adicionar à lista de fechados! <<extension>>
                ((ICollection<CognitiveState<T>>) closed).Add(node);

                // Verificar se chegamos à solução!
                if (node.IsGoal(goal))
                {
                    result = node;
                    return true;
                }

                // Adicionar filhos à lista de abertos!
                // Depois de os filtramos é claro!
                open.FilterAdd(closed, node, goal, this.agentCharacter);

            }

            cutOff += (int)Math.Ceiling(frontier.Peek().Layout.GetHeuristic(frontier.Peek().Layout, goal.Layout));

            return false;
        }

        #endregion

        // Done!
        #region Properties

        public bool HasSolution { get; private set; }

        public void FindPath()
        {
            HasSolution = SearchIDAStar();
        }

        public IList<T> Path
        {
            get
            {
                IList<T> path = new List<T>();

                CognitiveState<T> currentState = result;
                while (currentState != null)
                {
                    path.Add(currentState.Layout);
                    currentState = currentState.Parent;
                }

                return path;
            }
        }

        public float MinimumCost
        {
            get
            {
                return result.Cost;
            }
        }

        public T Start
        {
            get { return goal.Layout; }
        }

        public T Goal
        {
            get { return goal.Layout; }
        }

        #endregion

    }
}
