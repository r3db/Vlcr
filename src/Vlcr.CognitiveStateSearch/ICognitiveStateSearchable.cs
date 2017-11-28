using System;
using System.Collections.Generic;

namespace Vlcr.CognitiveStateSearch
{
    // Done!
    public interface ICognitiveStateSearchable<T> where T : class, ICognitiveLayout<T>
    {
        void FindPath();
        IList<T> Path { get; }
        float MinimumCost { get; }
        T Start { get; }
        T Goal { get; }
        bool HasSolution { get; }
    }
}
