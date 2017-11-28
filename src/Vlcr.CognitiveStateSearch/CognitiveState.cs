using System;
using System.Collections.Generic;
using Vlcr.Agent;

namespace Vlcr.CognitiveStateSearch
{
    internal sealed class CognitiveState<T> where T : class, ICognitiveLayout<T>
    {
        // Done!
        #region Internal Data

        public T                    Layout  { get; private set; }
        public CognitiveState<T>    Parent  { get; private set; }
        public float                Cost    { get; private set; }   // Custo Real

        #endregion

        // Done!
        #region .Ctor

        // Normal and most used ctor
        public CognitiveState(T layout, CognitiveState<T> parent, AgentCharacter agentCharacter)
        {
            Parent = parent;
            Layout = layout;
            Cost = parent == null ? 0 : parent.Cost + parent.Layout.GetCost(layout, agentCharacter);
        }

        // State without a parent
        public CognitiveState(T layout, AgentCharacter agentCharacter)
            : this(layout, null, agentCharacter)
        { }

        // Don't remember!
        public CognitiveState(CognitiveState<T> state)
        {
            Parent = state.Parent;
            Layout = state.Layout;
        }

        #endregion

        // Done!
        #region Methods

        public bool IsGoal(CognitiveState<T> value)
        {
            return Layout.IsGoal(value.Layout);
        }

        public IList<T> Children(CognitiveState<T> node, AgentCharacter ac)
        {
            return Layout.Children(ac);
        }

        public static float Estimate(CognitiveState<T> node, T c, CognitiveState<T> goal)
        {
            return c.GetHeuristic(c, goal.Layout);
        }

        #endregion

        // Done!
        #region Base Methods

        public override string ToString()
        {
            return string.Format("({0}, {1})", Layout, Cost);
        }

        #endregion

    }
}
