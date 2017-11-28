using System;
using System.Collections.Generic;
using Vlcr.Agent;
using Vlcr.Core.Collections;

namespace Vlcr.CognitiveStateSearch
{
    // Done!
    public sealed class CognitiveAStar<T> : ICognitiveStateSearchable<T> where T : class, ICognitiveLayout<T>
    {
        // Done!
        #region Internal Data

        private readonly CognitiveState<T> start;
        private readonly CognitiveState<T> goal;
        private readonly AgentCharacter agentCharacter;

        private CognitiveState<T> result;

        #endregion

        // Done!
        #region .Ctor

        public CognitiveAStar(T start, T goal, AgentCharacter agentCharacter)
        {
            this.start = new CognitiveState<T>(start, agentCharacter);
            this.goal = new CognitiveState<T>(goal, agentCharacter);
            this.agentCharacter = agentCharacter;
        }

        #endregion

        // Done!
        #region Methods

        private bool Search()
        {
            PriorityQueue<CognitiveState<T>> open = new PriorityQueue<CognitiveState<T>>();
            IDictionary<int, CognitiveState<T>> closed = new Dictionary<int, CognitiveState<T>>(500);

            // Reinicializar as variáveis
            result = null;
            
            // Vamos começar a expandir a partir deste estado!
            open.Enqueue(0, start);

            while (open.Count > 0)
            {
                // Apanhar o "melhor" estado possivel!
                CognitiveState<T> node = open.Dequeue();

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
                open.FilterAdd(closed, node, goal, this.agentCharacter);
            }

            return false;
        }

        public void FindPath()
        {
            HasSolution = Search();
        }

        #endregion

        // Done!
        #region Properties

        public bool HasSolution { get; private set; }

        public IList<T> Path
        {
            get
            {
                List<T> path = new List<T>();
                CognitiveState<T> currentState = result;
                
                while (currentState != null)
                {
                    path.Add(currentState.Layout);
                    currentState = currentState.Parent;
                }
                path.Reverse();

                return path;
            }
        }

        public float MinimumCost
        {
            get
            {
                if(HasSolution == false)
                {
                    throw new ArgumentException();
                }
                return result.Cost;
            }
        }

        public T Start
        {
            get { return start.Layout; }
        }

        public T Goal
        {
            get { return goal.Layout; }
        }

        #endregion

    }
}