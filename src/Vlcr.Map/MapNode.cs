using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Vlcr.Agent;
using Vlcr.CognitiveStateSearch;
using Vlcr.Core;
using Vlcr.Core.Collections;
using Vlcr.StateSearch;

namespace Vlcr.Map
{
    // Done!
    [Serializable]
    public sealed class MapNode : ILayout<MapNode>, ICognitiveLayout<MapNode>, ICloneable<MapNode>
    {
        private readonly Random r = new Random();

        // Done!
        #region Automatic Properties

        public NodeType         NodeType    { get; set; }
        public GeometryList     Geometry    { get; private set; }
        public Vector           Location    { get; set; }
        public ConcreteMap      Exits       { get; private set; }
        public string           Name        { get; set; }
        public int              Level       { get; set; }
        public MapNode          Parent      { get; set; }
        public MapConstraint    Constraints { get; set; }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        #region Helpers

        // Done!
        private static bool HasName(string name)
        {
            return string.IsNullOrEmpty(name) == false;
        }

        // Done!
        private static bool NotGeometry(NodeType nodeType)
        {
            return nodeType != NodeType.Geometry;
        }

        #endregion

        // Done!
        #region .Ctors

        // Done!
        private MapNode()
        {
        }

        // Done!
        public MapNode(string name, NodeType nodeType)
        {
            if (NotGeometry(nodeType) && HasName(name))
            {
                throw new ArgumentException("nodeType");
            }

            this.NodeType = nodeType;
            this.Geometry = new GeometryList();
            this.Exits = new ConcreteMap((this.NodeType == NodeType.Exit) ? true : false);
            this.Name = name;
            this.Constraints = new MapConstraint();
        }

        // Done!
        public MapNode(NodeType nodeType)
            : this(string.Empty, nodeType)
        {
        }

        #endregion

        #endregion

        // Done!
        #region ICloneable<MapNode> Members

        // Done!
        public MapNode Clone()
        {
            return new MapNode
            {
                NodeType    = this.NodeType,
                Geometry    = Helpers.Clone(this.Geometry),
                Location    = Helpers.Clone(this.Location),
                Exits       = Helpers.Clone(this.Exits),
                Name        = Helpers.Clone(Name),
                Level       = this.Level,
                Parent      = Helpers.Clone(this.Parent),
                Constraints = Helpers.Clone(this.Constraints),
            };
        }

        #endregion

        // Done!
        #region ILayout<MapNode> Members

        // Done!
        public IList<MapNode> Children()
        {
            if (this.NodeType == NodeType.Exit)
            {
                return this.Exits[0].Children();
            }

            IList<MapNode> data = new List<MapNode>();
            for (int i = 0; i < this.Exits.Count; ++i)
            {
                data.Add(this.Exits[i]);
            }
            return data;
        }

        // Done!
        public bool IsGoal(MapNode layout)
        {
            MapNode n = this.NodeType == NodeType.Exit ? this.Exits[0] : this;
            return Helpers.StringCompare(n.Name, layout.Name) && (n.NodeType == layout.NodeType);
        }

        // Done!
        public float GetCost(MapNode destination)
        {
            return Vector.Distance(this.Location, destination.Location);
        }

        // Done!
        public float GetHeuristic(MapNode source, MapNode destination)
        {
            return Vector.Distance(source.Location, destination.Location);
        }

        #endregion

        // Done!
        #region ICognitiveLayout<MapNode> Members

        // Done!
        public IList<MapNode> Children(AgentCharacter ac)
        {
            if (this.NodeType == NodeType.Exit)
            {
                return this.Exits[0].Children();
            }

            IList<MapNode> data = new List<MapNode>();
            for (int i = 0; i < this.Exits.Count; ++i)
            {
                //var x = this.Character.Bold*this.Exits[i].Exits[0].Passable;
                data.Add(this.Exits[i]);
            }
            return data;
        }

        // Done!
        public float GetCost(MapNode destination, AgentCharacter ac)
        {
            var c = destination.Exits[0].Constraints;

            const float factor = 4.0f;

            var d = GetCost(destination);
            var m = (c.Memory.Value*ac.Memory*d/factor);                                                            // Apply Time!
            var t = (r.NextDouble() * ac.Temperamental * d / factor);                                               // Apply Time!
            var e = -(Math.Abs(c.Memory.Value) * ac.Explore * d / (2*(c.Memory.Value > 0 ? 2 : 1.3)));              // Apply Time!
            var g = (c.Speed.Value * ac.Greedy * d / factor);                                                       // Apply Time!
            var p = ((1 - c.Transitable.Value) * ac.Bold * d / factor);                                             // Apply Time!
            var nd = d - m - t - e - p - g;
            return (float)nd;
        }

        #endregion

        // Done!
        #region Static Methods

        // Done!
        public static MapNode ToVirtual(MapNode mn, Vector location)
        {
            MapNode node = new MapNode(NodeType.Virtual) { Location = location, Level = mn.Level, Parent = mn };
            for (int i = 0; i < mn.Exits.Count; ++i)
            {
                node.Exits.Add(mn.Exits[i]);
            }
            return node;
        }

        #endregion

        // Done!
        #region Base Methods

        // Done!
        public override string ToString()
        {
            switch (this.NodeType)
            {
                case NodeType.Exit:     return string.Format(CultureInfo.InvariantCulture, "From: {0}; To: {1}; Location: {2}", this.Parent.Name, this.Exits[0].Name, this.Location);
                case NodeType.Geometry: return string.Format(CultureInfo.InvariantCulture, "Name: {0}", this.Name);
                case NodeType.Virtual:  return string.Format(CultureInfo.InvariantCulture, "Parent: {0}; Location: {1};", this.Parent, this.Location);
                case NodeType.Goal:     return string.Format(CultureInfo.InvariantCulture, "Name: {0}; Location: {1};", this.Name, this.Location);
            }

            throw new Exception();
        }

        // Done!
        public override int GetHashCode()
        {
            //return this.Name.GetHashCode();
            return this.NodeType == NodeType.Exit ? this.Parent.GetHashCode() : this.ToString().GetHashCode();
        }

        // Todo: Implement?
        public override bool Equals(object obj)
        {
            var node = obj as MapNode;

            if(node == null)
            {
                return false;
            }

            // Simple Comparison!
            return (this.Location == node.Location) && (Helpers.StringCompare(this.Name, node.Name));
        }

        #endregion

        // Done!
        #region Methods

        // Done!
        public void Translate(float dx, float dy, float dz, float scale)
        {
            Parallel.For(0, this.Geometry.Count, i => this.Geometry[i].Translate(dx, dy, dz, scale));
            Parallel.For(0, this.Exits.Count, i => this.Exits[i].Translate(dx, dy, dz, scale));

            //for (int i = 0; i < this.Geometry.Count; ++i)
            //{
            //    this.Geometry[i].Translate(dx, dy, dz, scale);
            //}

            //for (int i = 0; i < this.Exits.Count; ++i)
            //{
            //    this.Exits[i].Location.Translate(dx, dy, dz, scale);
            //}
        }

        #endregion

    }
}