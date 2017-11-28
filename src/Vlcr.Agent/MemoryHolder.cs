using System;
using Vlcr.Core;

namespace Vlcr.Agent
{
    [Serializable]
    public sealed class MemoryHolder
    {
        // Done!
        #region Internal Instance Data

        private readonly FuzzyFloat value;

        #endregion

        // Done!
        #region Properties

        public float Value
        {
            get { return this.value.Value; }
            set { this.value.Value = value; }
        }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        public MemoryHolder()
        {
            this.value = new FuzzyFloat(x => (float)(Math.Tanh(0.02*x)), x => (1f/Helpers.GetWeeks(x)), 0.8f);
        }

        #endregion

        // Done!
        #region Methods

        // Done!
        public void Increment()
        {
            this.value.Increment();
        }

        // Done!
        public void Decrement()
        {
            this.value.Decrement();
        }

        #endregion

    }
}