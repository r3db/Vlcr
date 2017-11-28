using System;

namespace Vlcr.Agent
{
    [Serializable]
    public sealed class AgentCharacter
    {
        // Done!
        #region Automatic Properties

        // Time!
        public float Memory         { get; set; }
        public float Explore        { get; set; }
        public float Greedy         { get; set; }
        public float Bold           { get; set; }
        public float Temperamental  { get; set; }

        #endregion

        // Done!
        #region Static Properties

        public static AgentCharacter Default
        {
            get
            {
                return new AgentCharacter
                {
                    Memory          = 1,
                    Explore         = 1,
                    Greedy          = 1,
                    Bold            = 1,
                    Temperamental   = 1,
                };
            }
        }

        #endregion
    }
}
