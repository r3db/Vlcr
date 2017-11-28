using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using Vlcr.Core;
using Vlcr.Map;

namespace Vlcr.IO
{
    // Done!
    public static class MapFile
    {
        // Done!
        #region Helper Classes

        private class XmlNodeReference
        {
            public string Parent    { get; set; }
            public string Pointer   { get; set; }
            public Vector Location  { get; set; }
        }

        #endregion

        // Done!
        #region Binary Raw

        // Done!
        public static void BinaryRawSave(string path, ConcreteMap map)
        {
            File.WriteAllBytes(path, Helpers.ToByteArray(map));
        }

        // Done!
        public static ConcreteMap BinaryRawOpen(string path)
        {
            return Helpers.FromByteArray<ConcreteMap>(File.ReadAllBytes(path));
        }

        #endregion

        // Done!
        #region Xml

        // Done!
        #region Open Helpers

        // Done!
        private static Vector ParseXmlVector(XPathNavigator nav)
        {
            var x = float.Parse(nav.GetAttribute("X", string.Empty), CultureInfo.InvariantCulture);
            var y = float.Parse(nav.GetAttribute("Y", string.Empty), CultureInfo.InvariantCulture);
            var z = float.Parse(nav.GetAttribute("Z", string.Empty), CultureInfo.InvariantCulture);
            return new Vector(x, y, z);
        }

        // Done!
        private static XmlNodeReference ParseXmlExit(XPathNavigator nav, MapNode node)
        {
            nav.MoveToFirstChild();
            Vector v = ParseXmlVector(nav);
            nav.MoveToNext();
            var name = nav.GetAttribute("Name", string.Empty);
            return new XmlNodeReference { Location = v, Parent = node.Name, Pointer = name };
        }

        // Done!
        private static void RemakeConnectors(ConcreteMap map, IList<XmlNodeReference> con)
        {
            for (int i = 0; i < con.Count; ++i)
            {
                var xnr = con[i];
                var parentNode = map.FindByName(xnr.Parent);
                var exit = new MapNode(NodeType.Exit) { Location = xnr.Location, Parent = parentNode };
                exit.Exits.Add(map.FindByName(xnr.Pointer));
                parentNode.Exits.Add(exit);
            }
        }

        // Done!
        private static MapConstraint ParseConstraints(XPathNavigator cNode, bool generate, Random r)
        {
            if(generate)
            {
                return new MapConstraint
                {
                    Memory      = { Value = (float)r.NextDouble() * ((r.NextDouble() >= 0.2) ? 1 : -1) },
                    Speed       = { Value = (float)r.NextDouble() },
                    Transitable = { Value = (float)r.NextDouble() }
                };
            }
            return new MapConstraint
            {
                Memory      = { Value = float.Parse(cNode.GetAttribute("M", string.Empty), CultureInfo.InvariantCulture) },
                Speed       = { Value = float.Parse(cNode.GetAttribute("S", string.Empty), CultureInfo.InvariantCulture) },
                Transitable = { Value = float.Parse(cNode.GetAttribute("T", string.Empty), CultureInfo.InvariantCulture) }
            };
        }

        #endregion

        // Done!
        #region Save Helpers

        // Done!
        private static XElement ToXmlVector(Vector v)
        {
            return new XElement("Vector", new XAttribute("X", v.X), new XAttribute("Y", v.Y), new XAttribute("Z", v.Z));
        }

        #endregion

        // Done!
        #region Open & Save

        // Done!
        public static ConcreteMap XmlRawOpen(string path)
        {
            XPathDocument doc = new XPathDocument(path);
            XPathNavigator nav = doc.CreateNavigator();

            var xmlNodes = nav.Select("Nodes");

            if(xmlNodes.MoveNext())
            {
                var cNode = xmlNodes.Current;
                var v = int.Parse(cNode.GetAttribute("Version", string.Empty));
                switch (v)
                {
                    case 1: return XmlRawOpen1(path);
                    case 2: return XmlRawOpen2(path);
                }
            }

            throw new FileLoadException("Invalid Version");
        }

        // Done!
        public static ConcreteMap XmlRawOpen1(string path)
        {
            ConcreteMap map = new ConcreteMap();
            IList<XmlNodeReference> con = new List<XmlNodeReference>();

            Random r = new Random();
            XPathDocument doc = new XPathDocument(path);
            XPathNavigator nav = doc.CreateNavigator();

            var xmlNodes = nav.Select("Nodes/Node");
            while (xmlNodes.MoveNext())
            {
                var cNode = xmlNodes.Current;

                // Add Node!
                var node = new MapNode(cNode.GetAttribute("Name", string.Empty), NodeType.Geometry) { Constraints = ParseConstraints(cNode, true, r) };
                map.Add(node);

                // Add Geometry
                var xmlGeometry = cNode.Select("Vector");
                while (xmlGeometry.MoveNext())
                {
                    node.Geometry.Add(ParseXmlVector(xmlGeometry.Current));
                }

                // Add Exits
                var xmlExits = cNode.Select("Exit");
                while (xmlExits.MoveNext())
                {
                    con.Add(ParseXmlExit(xmlExits.Current, node));
                }
            }

            // Remake Connectors
            RemakeConnectors(map, con);

            return map;
        }

        // Done!
        public static ConcreteMap XmlRawOpen2(string path)
        {
            ConcreteMap map = new ConcreteMap();
            IList<XmlNodeReference> con = new List<XmlNodeReference>();

            XPathDocument doc = new XPathDocument(path);
            XPathNavigator nav = doc.CreateNavigator();

            var xmlNodes = nav.Select("Nodes/Node");
            while (xmlNodes.MoveNext())
            {
                var cNode = xmlNodes.Current;

                // Add Node!
                var node = new MapNode(cNode.GetAttribute("Name", string.Empty), NodeType.Geometry) { Constraints = ParseConstraints(cNode, false, null) };
                map.Add(node);

                // Add Geometry
                var xmlGeometry = cNode.Select("Vector");
                while (xmlGeometry.MoveNext())
                {
                    node.Geometry.Add(ParseXmlVector(xmlGeometry.Current));
                }

                // Add Exits
                var xmlExits = cNode.Select("Exit");
                while (xmlExits.MoveNext())
                {
                    con.Add(ParseXmlExit(xmlExits.Current, node));
                }
            }

            // Remake Connectors
            RemakeConnectors(map, con);

            return map;
        }

        // Done!
        public static void XmlRawSave(string path, ConcreteMap map, int version = 2)
        {
            switch (version)
            {
                case 1: XmlRawSaveV1(path, map); return;
                case 2: XmlRawSaveV2(path, map); return;
            }
            throw new FileLoadException("Invalid Version: " + version);
        }

        // Done!
        private static void XmlRawSaveV1(string path, ConcreteMap map)
        {
            XElement xNodes = new XElement("Nodes", new XAttribute("Version", "1"));
            for (int i = 0; i < map.Count; ++i)
            {
                var node = map[i];
                XElement xNode = new XElement("Node", new XAttribute("Name", node.Name));
                foreach (var vector in node.Geometry)
                {
                    xNode.Add(ToXmlVector(vector));
                }
                xNodes.Add(xNode);
                foreach (var exit in node.Exits)
                {
                    var e = exit.Exits;
                    if (e != null && e.Count > 0)
                    {
                        xNode.Add(new XElement("Exit", ToXmlVector(exit.Location), new XElement("Node", new XAttribute("Name", e[0].Name))));
                    }
                }
            }

            xNodes.Save(path, SaveOptions.None);
        }

        // Done!
        private static void XmlRawSaveV2(string path, ConcreteMap map)
        {
            XElement xNodes = new XElement("Nodes", new XAttribute("Version", "2"));
            for (int i = 0; i < map.Count; ++i)
            {
                var node = map[i];
                XElement xNode = new XElement("Node",
                    new XAttribute("Name", node.Name),
                    new XAttribute("M", node.Constraints.Memory.Value.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("S", node.Constraints.Speed.Value.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("T", node.Constraints.Transitable.Value.ToString(CultureInfo.InvariantCulture)));
                foreach (var vector in node.Geometry)
                {
                    xNode.Add(ToXmlVector(vector));
                }
                xNodes.Add(xNode);
                foreach (var exit in node.Exits)
                {
                    var e = exit.Exits;
                    if (e != null && e.Count > 0)
                    {
                        xNode.Add(new XElement("Exit", ToXmlVector(exit.Location), new XElement("Node", new XAttribute("Name", e[0].Name))));
                    }
                }
            }

            xNodes.Save(path, SaveOptions.None);
        }

        #endregion

        #endregion

        // Done!
        #region General

        // Done!
        public static void Save(string path, ConcreteMap map)
        {
            var fi = new FileInfo(path);
            if(fi.Extension == Constants.RawXmlMapExtension)
            {
                BinaryRawSave(path, map);
            }
            else if(fi.Extension == Constants.RawBinaryMapExtension)
            {
                XmlRawSave(path, map);
            }
        }

        // Done!
        public static ConcreteMap Open(string path)
        {
            var fi = new FileInfo(path);
            if (fi.Extension == Constants.RawXmlMapExtension)
            {
                return XmlRawOpen(path);
            }
            if (fi.Extension == Constants.RawBinaryMapExtension)
            {
                return BinaryRawOpen(path);
            }
            return null;
        }

        #endregion

    }
}
