using System;
using System.Collections.Generic;
using Vlcr.Core.Collections;

namespace Vlcr.StateSearch
{
    // Done!
    public sealed class IDAStar<T>  : IStateSearchable<T> where T : class, ILayout<T>
    {
        // Done!
        #region Internal Data

        private readonly State<T> start;
        private readonly State<T> goal;

        private State<T> result;
        private int cutOff;

        #endregion

        // Done!
        #region .Ctor

        public IDAStar(T start, T goal)
        {
            this.start = new State<T>(start);
            this.goal = new State<T>(goal);

            // Vamos definir um nivel de corte!
            cutOff = (int)Math.Ceiling(start.GetHeuristic(start, goal));

        }

        #endregion

        // Done!
        #region Methods

        private bool SearchIDAStar()
        {
            PriorityQueue<State<T>> open = new PriorityQueue<State<T>>();
            PriorityQueue<State<T>> frontier = new PriorityQueue<State<T>>();

            // Reinicializar as variáveis
            result = null;

            // Vamos começar a expandir a partir deste estado!
            open.Enqueue(0, start);
            bool found;

            do
            {
                found = SearchAStar(open, frontier);
                open = frontier;
                frontier = new PriorityQueue<State<T>>();
            } while (found == false || frontier.Count != 0);

            return true;
        }

        private bool SearchAStar(PriorityQueue<State<T>> open, PriorityQueue<State<T>> frontier)
        {
            IDictionary<int, State<T>> closed = new Dictionary<int, State<T>>(500);

            while (open.Count > 0)
            {
                // Apanhar o "melhor" estado possivel!
                State<T> node = open.Dequeue();

                // Adicionar à lista de fechados! <<extension>>
                closed.Add(node);

                // Verificar se chegamos à solução!
                if (node.IsGoal(goal))
                {
                    result = node;
                    return true;
                }

                // Adicionar filhos à lista de abertos!
                // Depois de os filtramos é claro!
                open.FilterAdd(closed, node, goal);

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

                State<T> currentState = result;
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
