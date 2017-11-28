using System;
using System.Collections.Generic;
using Vlcr.Core;

namespace Vlcr.Map
{
    // Done!
    [Serializable]
    public sealed class ConcreteMap : List<MapNode>
    {
        // Done!
        #region Internal Instance Data

        public bool             Restrict        { get; set; }
        
        #endregion

        // Done!
        #region .Ctor

        // Done!
        public ConcreteMap(bool restrict = true)
        {
            this.Restrict = restrict;
        }

        #endregion

        // Done!
        #region Base Methods

        // Todo: Don't like the "new" keyword!
        public new void Add(MapNode node)
        {
            if(Restrict == true && node.NodeType == NodeType.Exit && this.Count == 1)
            {
                throw new ArgumentException("node");
            }
            base.Add(node);
        }

        #endregion

        // Done!
        #region Methods

        // Done!
        public MapNode FindByName(string name)
        {
            for (int i = 0; i < this.Count; ++i)
            {
                var node = this[i];
                if (Helpers.StringCompare(node.Name, name))
                {
                    return node;
                }
            }
            return null;
        }

        // Done!
        public void Remove(MapNode node, bool removeLinks)
        {
            base.Remove(node);

            if(node == null)
            {
                return;
            }

            if(removeLinks == false)
            {
                return;
            }

            for (int i = 0; i < this.Count; ++i)
            {
                var mn = this[i];
                for (int k = 0; k < mn.Exits.Count; ++k)
                {
                    var e = mn.Exits[k];
                    if(e != null && e.Exits[0] == node)
                    {
                        e.Exits.Clear();
                    }
                }
            }
            
        }

        // Done!
        public void SendToBack(MapNode node)
        {
            var list = new ConcreteMap { node };
            for (int i = 0; i < this.Count; ++i)
            {
                if (this[i] != node)
                {
                    list.Add(this[i]);
                }
            }
            this.Clear();
            for (int i = 0; i < list.Count; ++i)
            {
                this.Add(list[i]);
            }
        }

        // Done!
        public void BringToFront(MapNode node)
        {
            var list = new ConcreteMap();
            for (int i = 0; i < this.Count; ++i)
            {
                if (this[i] != node)
                {
                    list.Add(this[i]);
                }
            }
            list.Add(node);

            this.Clear();
            for (int i = 0; i < list.Count; ++i)
            {
                this.Add(list[i]);
            }
        }

        #endregion

        // Done!
        #region Static Methods

        // Done!
        public static MapNode AdaptNodeToGoal(MapNode mn, Vector location)
        {
            mn.Location = location;
            mn.NodeType = NodeType.Goal;
            return mn;
        }

        // Done!
        public static MapNode RestoreNodeFromGoal(MapNode mn)
        {
            mn.Location = null;
            mn.NodeType = NodeType.Geometry;
            return mn;
        }

        #endregion

    }
}
