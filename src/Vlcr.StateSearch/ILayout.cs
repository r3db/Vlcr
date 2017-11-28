using System;
using System.Collections.Generic;

namespace Vlcr.StateSearch
{
    // Done!
    public interface ILayout<T> where T : class
    {
        IList<T> Children();
        bool IsGoal(T layout);
        float GetCost(T destination);
        float GetHeuristic(T source, T destination);
    }
}
