using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Vlcr.Core;
using Vlcr.Map;

namespace Vlcr.VisualMap
{
    // Done!
    [Serializable]
    public sealed class ConcreteVisualMap : List<VisualMapNode>
    {
        // Done!
        #region Static Methods

        // Done!
        public static ConcreteVisualMap LoadFrom(ConcreteMap cm)
        {
            if(cm == null || cm.Count == 0)
            {
                return new ConcreteVisualMap();
            }

            var cvm = new ConcreteVisualMap();
            for (int i = 0; i < cm.Count; ++i)
            {
                float hx = float.MaxValue;
                float hy = float.MaxValue;
                float lx = float.MinValue;
                float ly = float.MinValue;

                if(cm[i].Geometry.Count > 0)
                {
                    hx = cm[i].Geometry.Min(x => x.X);
                    hy = cm[i].Geometry.Min(x => x.Y);
                    lx = cm[i].Geometry.Max(x => x.X);
                    ly = cm[i].Geometry.Max(x => x.Y);
                }

                cvm.Add(new VisualMapNode
                {
                    ConcreteNode    = cm[i],
                    FillShape       = false,
                    FillShapePath   = false,
                    IsLocked        = false,
                    IsVisible       = true,
                    Visuals         = new MapVisuals { Geometry = Pens.Blue },
                    Min             = new Vector(hx, hy),
                    Max             = new Vector(lx, ly),
                });
            }
            return cvm;
        }

        #endregion

        // Done!
        #region Methods

        // Done!
        public VisualMapNode FindByName(string name)
        {
            for (int i = 0; i < this.Count; ++i)
            {
                var node = this[i];
                if (Helpers.StringCompare(node.ConcreteNode.Name, name))
                {
                    return node;
                }
            }
            return null;
        }

        // Done!
        public void Remove(VisualMapNode node, bool removeLinks)
        {
            base.Remove(node);

            if (node == null)
            {
                return;
            }

            if (removeLinks == false)
            {
                return;
            }

            for (int i = 0; i < this.Count; ++i)
            {
                var vmn = this[i];
                for (int k = 0; k < vmn.ConcreteNode.Exits.Count; ++k)
                {
                    var e = vmn.ConcreteNode.Exits[k];
                    if (e != null && e.Exits.Count > 0 && e.Exits[0] == node.ConcreteNode)
                    {
                        e.Exits.Clear();
                    }
                }
            }

        }

        // Done!
        public ConcreteMap ToConcreteMap()
        {
            ConcreteMap cm = new ConcreteMap(false);
            for (int i = 0; i < this.Count; ++i)
            {
                var node = this[i].ConcreteNode;
                cm.Add(node);
            }
            return cm;
        }

        // Done!
        public void SendToBack(VisualMapNode node)
        {
            var list = new ConcreteVisualMap { node };
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
        public void BringToFront(VisualMapNode node)
        {
            var list = new ConcreteVisualMap();
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

        // Done!
        public void ResetMemory()
        {
            for (int i = 0; i < this.Count; ++i)
            {
                var node = this[i];
                node.ConcreteNode.Constraints.Memory.Value = 0;
            }
        }

        // Done!
        public void ResetSpeed()
        {
            for (int i = 0; i < this.Count; ++i)
            {
                var node = this[i];
                node.ConcreteNode.Constraints.Speed.Value = 0;
            }
        }

        // Done!
        public void ResetTransitable()
        {
            for (int i = 0; i < this.Count; ++i)
            {
                var node = this[i];
                node.ConcreteNode.Constraints.Transitable.Value = 0;
            }
        }

        #endregion
    }
}
