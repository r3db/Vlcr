using System;
using Vlcr.Agent;
using Vlcr.Core;

namespace Vlcr.Map
{
    [Serializable]
    public sealed class MapConstraint
    {
        // Done!
        #region Automatic Properties

        public MemoryHolder Memory          { get; private set; }
        public FuzzyFloat   Speed           { get; private set; }
        public FuzzyFloat   Transitable     { get; private set; }

        #endregion

        // Done!
        #region .Ctor

        // Todo: Change Speed and Transitable
        public MapConstraint()
        {
            this.Memory         = new MemoryHolder();
            this.Speed          = new FuzzyFloat(x => (float)(1/(1+Math.Pow(Math.E, -0.04*x))), null, 0.8f);
            this.Transitable    = new FuzzyFloat(x => (float)(1/(1+Math.Pow(Math.E, -0.04*x))), null, 0.8f);
        }

        #endregion
    }
}