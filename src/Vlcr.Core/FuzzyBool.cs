using System;

namespace Vlcr.Core
{
    // Done!
    public struct FuzzyBool
    {
        // Done!
        #region Internal Instance Data

        private readonly Random r;
        private readonly float value;

        #endregion

        // Done!
        #region Automatic Properties

        // Done!
        public bool IsTrue
        {
            get
            {
                return value >= 0.5f + GetRandomDelta();
            }
        }

        // Done!
        public bool IsFalse
        {
            get
            {
                return !IsTrue;
            }
        }

        // Done!
        public bool IsUnknown { 
            get
            {
                var t = IsTrue;
                var f = IsFalse;
                return (t && f) || (!t && !f);
            }
        }

        // Done!
        public float Value
        {
            get { return this.value; }
        }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        public FuzzyBool(float value)
        {
            this.value = value;
            this.r = new Random();
        }

        #endregion

        // Done!
        #region Helpers

        // Done!
        private double GetRandomDelta()
        {
            return ((r.NextDouble() >= 0.5) == true) ? (-1 * r.NextDouble() / 50f) : (r.NextDouble() / 50f);
        }

        #endregion
    }
}
