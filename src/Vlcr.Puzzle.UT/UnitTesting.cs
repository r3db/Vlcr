using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Vlcr.StateSearch;

namespace Vlcr.Puzzle.UT
{
    [TestFixture]
    public sealed class UnitTesting
    {
        // Internal
        // -------------------------------------------------------------------------------------

        [Test]
        public void Moves()
        {
            MovesHelper("123405678", new List<string> { "103425678", "123045678", "123450678", "123475608" });
            MovesHelper("012345678", new List<string> { "102345678", "312045678" });
            MovesHelper("123456780", new List<string> { "123450786", "123456708" });
            MovesHelper("123456708", new List<string> { "123406758", "123456078", "123456780" });
            MovesHelper("120345678", new List<string> { "102345678", "125340678" });
        }

        // Done!
        private static void MovesHelper(string parent, ICollection<string> childreen)
        {
            EightPuzzle pParent = EightPuzzleFactory.Create(parent);

            IList<EightPuzzle> pChildreen = pParent.Children();

            Assert.AreEqual(childreen.Count, pChildreen.Count);

            var q1 = from x in pChildreen
                     orderby x.ToString()
                     select x;

            var q2 = (from x in childreen
                      orderby x
                      select x).ToList();

            int i = 0;

            foreach (var item in q1)
            {
                Assert.AreEqual(q2[i++], item.ToString());
            }
        }

        [Test]
        public void Heuristic()
        {
            HeuristicHelper("123405678", "123405678", 0);
            HeuristicHelper("123405678", "103425678", 1);
            HeuristicHelper("123450786", "123456708", 2);
            HeuristicHelper("312045678", "123450786", 10);
            HeuristicHelper("123450786", "123405678", 5);
        }

        // Done!
        private static void HeuristicHelper(string current, string goal, float value)
        {
            EightPuzzle pCurrent = EightPuzzleFactory.Create(current);
            EightPuzzle pGoal = EightPuzzleFactory.Create(goal);

            float h = pCurrent.GetHeuristic(pCurrent, pGoal);

            Assert.AreEqual(value, h);

        }

        // Puzzle/Algorithm
        // -------------------------------------------------------------------------------------

        [Test]
        public void PuzzleTesterAStar()
        {
            PuzzleTesterAStarHelper("586073421", "586073421", 0);
            PuzzleTesterAStarHelper("283164705", "023184765", 3);
            PuzzleTesterAStarHelper("123840765", "123804765", 1);
            PuzzleTesterAStarHelper("123456780", "436718520", 12);
            PuzzleTesterAStarHelper("863451702", "123804765", 17);
            PuzzleTesterAStarHelper("863412705", "123804765", 19);
            PuzzleTesterAStarHelper("603841752", "123804765", 19);
            PuzzleTesterAStarHelper("803461275", "123804765", 19);
            PuzzleTesterAStarHelper("286703541", "123804765", 20);
            PuzzleTesterAStarHelper("863521074", "123804765", 20);
            PuzzleTesterAStarHelper("863512407", "123804765", 21);
            PuzzleTesterAStarHelper("603827541", "123804765", 21);
            PuzzleTesterAStarHelper("873065421", "123804765", 21);
            PuzzleTesterAStarHelper("603725841", "123804765", 21);
            PuzzleTesterAStarHelper("763102845", "123804765", 22);
            PuzzleTesterAStarHelper("763804512", "123804765", 22);
            PuzzleTesterAStarHelper("860473251", "123804765", 22);
            PuzzleTesterAStarHelper("876520413", "123804765", 23);
            PuzzleTesterAStarHelper("763084251", "123804765", 23);
            PuzzleTesterAStarHelper("763082415", "123804765", 23);
            PuzzleTesterAStarHelper("726840513", "123804765", 23);
            PuzzleTesterAStarHelper("726403851", "123804765", 24);
            PuzzleTesterAStarHelper("876203541", "123804765", 24);
            PuzzleTesterAStarHelper("760843251", "123804765", 24);
            PuzzleTesterAStarHelper("763802541", "123804765", 24);
            PuzzleTesterAStarHelper("760824513", "123804765", 24);
            PuzzleTesterAStarHelper("583067421", "123804765", 25);
            PuzzleTesterAStarHelper("586073421", "123804765", 25);
            PuzzleTesterAStarHelper("876043251", "123804765", 25);
            PuzzleTesterAStarHelper("087526413", "123804765", 26);
            PuzzleTesterAStarHelper("076824513", "123804765", 26);
            PuzzleTesterAStarHelper("763042851", "123804765", 27);
            PuzzleTesterAStarHelper("123456780", "087654321", 28);
            PuzzleTesterAStarHelper("876105234", "123804765", 28);
            PuzzleTesterAStarHelper("012345678", "087654321", 30);
        }

        private static void PuzzleTesterAStarHelper(String start, String goal, int value)
        {
            IStateSearchable<EightPuzzle> iss = new AStar<EightPuzzle>(EightPuzzleFactory.Create(start), EightPuzzleFactory.Create(goal));
            iss.FindPath();
            Assert.AreEqual(true, iss.HasSolution);
            Assert.AreEqual(value, iss.MinimumCost);
        }

        [Test]
        public void PuzzleTesterIDAStar()
        {
            PuzzleTesterIDAStarHelper("586073421", "586073421", 0);
            PuzzleTesterIDAStarHelper("283164705", "023184765", 3);
            PuzzleTesterIDAStarHelper("123840765", "123804765", 1);
            PuzzleTesterIDAStarHelper("123456780", "436718520", 12);
            PuzzleTesterIDAStarHelper("863451702", "123804765", 17);
            PuzzleTesterIDAStarHelper("863412705", "123804765", 19);
            PuzzleTesterIDAStarHelper("603841752", "123804765", 19);
            PuzzleTesterIDAStarHelper("803461275", "123804765", 19);
            PuzzleTesterIDAStarHelper("286703541", "123804765", 20);
            PuzzleTesterIDAStarHelper("863521074", "123804765", 20);
            PuzzleTesterIDAStarHelper("863512407", "123804765", 21);
            PuzzleTesterIDAStarHelper("603827541", "123804765", 21);
            PuzzleTesterIDAStarHelper("873065421", "123804765", 21);
            PuzzleTesterIDAStarHelper("603725841", "123804765", 21);
            PuzzleTesterIDAStarHelper("763102845", "123804765", 22);
            PuzzleTesterIDAStarHelper("763804512", "123804765", 22);
            PuzzleTesterIDAStarHelper("860473251", "123804765", 22);
            PuzzleTesterIDAStarHelper("876520413", "123804765", 23);
            PuzzleTesterIDAStarHelper("763084251", "123804765", 23);
            PuzzleTesterIDAStarHelper("763082415", "123804765", 23);
            PuzzleTesterIDAStarHelper("726840513", "123804765", 23);
            PuzzleTesterIDAStarHelper("726403851", "123804765", 24);
            PuzzleTesterIDAStarHelper("876203541", "123804765", 24);
            PuzzleTesterIDAStarHelper("760843251", "123804765", 24);
            PuzzleTesterIDAStarHelper("763802541", "123804765", 24);
            PuzzleTesterIDAStarHelper("760824513", "123804765", 24);
            PuzzleTesterIDAStarHelper("583067421", "123804765", 25);
            PuzzleTesterIDAStarHelper("586073421", "123804765", 25);
            PuzzleTesterIDAStarHelper("876043251", "123804765", 25);
            PuzzleTesterIDAStarHelper("087526413", "123804765", 26);
            PuzzleTesterIDAStarHelper("076824513", "123804765", 26);
            PuzzleTesterIDAStarHelper("763042851", "123804765", 27);
            PuzzleTesterIDAStarHelper("123456780", "087654321", 28);
            PuzzleTesterIDAStarHelper("876105234", "123804765", 28);
            PuzzleTesterIDAStarHelper("012345678", "087654321", 30);
        }

        private static void PuzzleTesterIDAStarHelper(String start, String goal, int value)
        {
            IStateSearchable<EightPuzzle> iss = new IDAStar<EightPuzzle>(EightPuzzleFactory.Create(start), EightPuzzleFactory.Create(goal));
            iss.FindPath();
            Assert.AreEqual(true, iss.HasSolution);
            Assert.AreEqual(value, iss.MinimumCost);
        }

        // Other
        // -------------------------------------------------------------------------------------

        [Test]
        public void PuzzleMovesTester()
        {
            PuzzleMovesTesterHelper(new[] { "123456780", "123456708", "123406758" }, new[] { new Move(new Point(1, 2), new Point(2, 2)), new Move(new Point(1, 1), new Point(1, 2)) });
            PuzzleMovesTesterHelper(new[] { "103425678", "013425678", "413025678", "413205678" }, new[] { new Move(new Point(0, 0), new Point(1, 0)), new Move(new Point(0, 1), new Point(0, 0)), new Move(new Point(1, 1), new Point(0, 1)) });
        }

        private static void PuzzleMovesTesterHelper(IEnumerable<string> s, Move[] m)
        {
            List<EightPuzzle> p = s.Select(EightPuzzleFactory.Create).ToList();

            IList<Move> moves = Moves(p);

            Assert.AreEqual(moves.Count, m.Length);

            Assert.AreEqual(moves.Count, m.Length);

            for (int i = 0; i < moves.Count; ++i)
            {
                Assert.AreEqual(moves[i], m[i]);
            }

        }

        private struct Move
        {
            private Point From { get; set; }
            private Point To { get; set; }

            public Move(Point from, Point to)
                : this()
            {
                this.From = from;
                this.To = to;
            }

            public override bool Equals(object obj)
            {
                Move m = (Move)obj;
                //Console.WriteLine("{0}", this);
                //Console.WriteLine("{0}", m);
                //Console.WriteLine("---------------------");
                return From.X == m.From.X && From.Y == m.From.Y && To.X == m.To.X && To.Y == m.To.Y;
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            public override string ToString()
            {
                return string.Format("{0} {1}", From, To);
            }

        }

        private static IList<Move> Moves<T>(IList<T> data) where T : class, ILayout<T>
        {
            IList<Move> moves = new List<Move>();

            for (int i = 0; i < data.Count - 1; ++i)
            {
                string current = data[i].ToString();
                string next = data[i + 1].ToString();

                int spaceCurrent = current.IndexOf("0");
                int spaceNext = next.IndexOf("0");

                Point currentPiece = ConvertToPoint(spaceNext);
                Point nextPiece = ConvertToPoint(spaceCurrent);

                moves.Add(new Move(currentPiece, nextPiece));
            }

            return moves;

        }

        private static Point ConvertToPoint(int index)
        {
            switch (index)
            {
                case 0: return new Point(0, 0);
                case 1: return new Point(1, 0);
                case 2: return new Point(2, 0);
                case 3: return new Point(0, 1);
                case 4: return new Point(1, 1);
                case 5: return new Point(2, 1);
                case 6: return new Point(0, 2);
                case 7: return new Point(1, 2);
                case 8: return new Point(2, 2);
            }

            throw new ArgumentException();
        }

    }
}
