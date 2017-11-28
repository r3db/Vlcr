using System;
using System.Collections.Generic;
using Vlcr.Agent;

namespace Vlcr.CognitiveStateSearch
{
    // Done!
    public interface ICognitiveLayout<T> where T : class
    {
        IList<T> Children(AgentCharacter ac);
        bool IsGoal(T layout);
        float GetCost(T destination, AgentCharacter agentCharacter);
        float GetHeuristic(T source, T destination);
    }
}
