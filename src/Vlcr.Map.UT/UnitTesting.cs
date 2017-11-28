using System;
using System.Collections.Generic;
using NUnit.Framework;
using Vlcr.Agent;
using Vlcr.CognitiveStateSearch;
using Vlcr.Core;
using Vlcr.IO;
using Vlcr.StateSearch;

namespace Vlcr.Map.UT
{
    [TestFixture]
    public sealed class UnitTesting
    {
        private static ConcreteMap GetMap()
        {
            // Load from file!
            return MapFile.XmlRawOpen(@"C:\Users\redb\Desktop\Vlcr\Resources\Map.rxmf");
        }

        [Test]
        public void TestPath()
        {
            ConcreteMap map = GetMap();
            TestPathHelper(map, false, "Hall Section:U", "Hall Section:S", true, 3);
            TestPathHelper(map, true,  "Hall Section:U", "Hall Section:S", true, 3);
        }

        // Done!
        private static void TestPathHelper(ConcreteMap map, bool cognitive, string startNode, string closeNode, bool solution, int count)
        {
            MapNode start = map.FindByName(startNode);
            MapNode close = map.FindByName(closeNode);

            var startStub = MapNode.ToVirtual(start, Vector.Average(start.Geometry));
            var closeStub = ConcreteMap.AdaptNodeToGoal(close, Vector.Average(close.Geometry));

            Assert.AreNotEqual(start, null);
            Assert.AreNotEqual(close, null);

            Assert.AreNotEqual(startStub, null);
            Assert.AreNotEqual(closeStub, null);

            IList<MapNode> path;
            if (cognitive == true)
            {
                ICognitiveStateSearchable<MapNode> iss = new CognitiveAStar<MapNode>(startStub, closeStub, AgentCharacter.Default);
                iss.FindPath();
                Assert.AreEqual(solution, iss.HasSolution);
                Assert.AreEqual(count, iss.Path.Count);
                path = iss.Path;
            }
            else
            {
                IStateSearchable<MapNode> iss = new AStar<MapNode>(startStub, closeStub);
                iss.FindPath();
                Assert.AreEqual(solution, iss.HasSolution);
                Assert.AreEqual(count, iss.Path.Count); 
                path = iss.Path;
            }

            path.Add(closeStub);
            foreach (var item in path)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine(new string('-', 70));

            ConcreteMap.RestoreNodeFromGoal(close);
        }
    }
}
