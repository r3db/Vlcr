using System;
using System.Collections.Generic;

namespace Vlcr.StateSearch
{
    // Done!
    internal sealed class State<T> where T : class, ILayout<T>
    {
        // Done!
        #region Internal Data

        public T Layout         { get; private set; }
        public State<T> Parent  { get; private set; }
        public float Cost       { get; private set; }   // Custo Real

        #endregion

        // Done!
        #region .Ctor

        // Normal and most used ctor
        public State(T layout, State<T> parent)
        {
            Parent = parent;
            Layout = layout;
            Cost = parent == null ? 0 : parent.Cost + parent.Layout.GetCost(layout);
        }

        // State without a parent
        public State(T layout) : this(layout, null)
        { }

        // Don't remember!
        public State(State<T> state)
        {
            Parent = state.Parent;
            Layout = state.Layout;
        }

        #endregion

        // Done!
        #region Methods

        public bool IsGoal(State<T> value)
        {
            return Layout.IsGoal(value.Layout);
        }

        public IList<T> Children(State<T> node)
        {
            return Layout.Children();
        }

        public static float Estimate(State<T> node, T c, State<T> goal)
        {
            float h = c.GetHeuristic(c, goal.Layout);
            //float g = node.Layout.GetCost(c);

            return h;
            //return g + h;
        }

        #endregion

        // Done!
        #region Base Methods

        public override string ToString()
        {
            return String.Format("({0}, {1})", Layout, Cost);
        }

        #endregion

    }
}
