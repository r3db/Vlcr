using System;
using System.Collections.Generic;

namespace Vlcr.StateSearch
{
    // Done!
    public interface IStateSearchable<T> where T : class, ILayout<T>
    {
        void FindPath();
        IList<T> Path { get; }
        float MinimumCost { get; }
        T Start { get; }
        T Goal { get; }
        bool HasSolution { get; }
    }
}
