using System;
using Vlcr.Core;

namespace Vlcr.Puzzle
{
    // Done!
    internal sealed class EightPuzzleCell : ICloneable<EightPuzzleCell>
    {
        // Done!
        #region Internal Data

        public int Line    { get; private set; }
        public int Column  { get; private set; }

        #endregion

        // Done!
        #region .Ctor

        public EightPuzzleCell(int line, int column)
        {
            //CtorContract(line, column);
            this.Line = line;
            this.Column = column;
        }

        //[Conditional(Constants.ConditionalDebug)]
        //private static void CtorContract(int line, int column)
        //{
        //    (line >= EightPuzzle.Min && line < EightPuzzle.Max).Assert();
        //    (column >= EightPuzzle.Min && column < EightPuzzle.Max).Assert();
        //}

        #endregion

        // Done!
        #region ICloneable<EightPuzzleCell> Members

        public EightPuzzleCell Clone()
        {
            return (EightPuzzleCell)MemberwiseClone();
        }

        #endregion
    }
}
