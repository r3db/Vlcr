using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vlcr.Core;
using XnaColor = Microsoft.Xna.Framework.Color;

namespace Vlcr.Creator.Controls
{
    internal class XnaWorkArea : XnaControl
    {
        // Done!
        #region Internal Static Data

        private const int Norm = 1000;
        const float DeltaX = -52;
        const float DeltaY = -73;
        const float DeltaZ = 0;
        const int ScaleFactor = 80;
        const int H = 5;
        //const int TopLeft = 0;
        //const int TopRight = 1;
        //const int BottomRight = 2;
        //const int BottomLeft = 3;

        #endregion

        // Done!
        #region Internal Instance Data

        private VertexPositionColor[] geometryData;
        private VertexPositionColor[] nodeData;

        //private RasterizerState WIREFRAME_RASTERIZER_STATE = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.WireFrame };

        //private VertexPositionColorTexture[] vertexData;
        //private short[] indexData;

        #endregion

        // Done!
        #region Automatic Properties

        internal MainForm RealParent { get; private set; }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        internal XnaWorkArea() : this(null)
        {
        }

        // Done!
        internal XnaWorkArea(MainForm parent)
        {
            if(parent != null)
            {
                this.CtorHelper(parent);
            }
        }

        // Done!
        internal void CtorHelper(MainForm parent)
        {
            this.RealParent = parent;
            this.BuildData();
            this.RealParent.StateChanged += StateChanged;
        }

        private void BuildData()
        {
            if(this.RealParent== null || this.RealParent.VisualMap == null || this.RealParent.VisualMap.Count == 0)
            {
                return;
            }

            IList<VertexPositionColor> temp1 = new List<VertexPositionColor>();
            AddLayer(temp1, ScaleFactor, DeltaX, DeltaY, DeltaZ, 0);
            AddLayer(temp1, ScaleFactor, DeltaX, DeltaY, DeltaZ, H);
            ConnectLayers(temp1);
            this.geometryData = temp1.ToArray();

            IList<VertexPositionColor> temp2 = new List<VertexPositionColor>();
            AddConnectors(temp2, ScaleFactor, DeltaX, DeltaY, DeltaZ, H);
            this.nodeData = temp2.ToArray();

        }

        //private void SetUpVertices(float factor, float deltaX, float deltaY, float h)
        //{
        //    var color = XnaColor.White;
        //    float z = h + 1;

        //    var minX = (factor * (from x in this.RealParent.VisualMap select (from w in x.ConcreteNode.Geometry select w.Normalize(Norm, Norm, Norm).X).Min()).Min(x => x)) + deltaX;
        //    var minY = (factor * (from x in this.RealParent.VisualMap select (from w in x.ConcreteNode.Geometry select w.Normalize(Norm, Norm, Norm).Y).Min()).Min(x => x)) + deltaY;
        //    var maxX = (factor * (from x in this.RealParent.VisualMap select (from w in x.ConcreteNode.Geometry select w.Normalize(Norm, Norm, Norm).X).Max()).Max(x => x)) + deltaX;
        //    var maxY = (factor * (from x in this.RealParent.VisualMap select (from w in x.ConcreteNode.Geometry select w.Normalize(Norm, Norm, Norm).Y).Max()).Max(x => x)) + deltaY;

        //    vertexData = new VertexPositionColorTexture[4];
        //    vertexData[TopLeft]            = new VertexPositionColorTexture(new Vector3(minX, minY, z), color, new Vector2(0, 0));
        //    vertexData[TopRight]           = new VertexPositionColorTexture(new Vector3(maxX, minY, z), color, new Vector2(1, 0));
        //    vertexData[BottomRight]        = new VertexPositionColorTexture(new Vector3(maxX, maxY, z), color, new Vector2(1, 1));
        //    vertexData[BottomLeft]         = new VertexPositionColorTexture(new Vector3(minX, maxY, z), color, new Vector2(0, 1));
        //}

        //private void SetUpIndices()
        //{
        //    indexData = new short[6];
        //    indexData[0] = TopLeft;
        //    indexData[1] = BottomRight;
        //    indexData[2] = BottomLeft;

        //    indexData[3] = TopLeft;
        //    indexData[4] = TopRight;
        //    indexData[5] = BottomRight;
        //}

        //private void CreateLabels()
        //{
        //    Bitmap bmp = new Bitmap(256, 256);
        //    Graphics g = Graphics.FromImage(bmp);
        //    g.Clear(WinColor.Red);
        //    g.DrawString("123", new Font("Consolas", 90f), Brushes.Black, 0, 0);
        //    g.Save();

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        bmp.Save(ms, ImageFormat.Png);
        //        ms.Position = 0;
        //        Texture2D.FromStream(this.GraphicsDevice, ms, bmp.Width, bmp.Height, false);
        //    }

        //}

        // Done!
        private void AddLayer(IList<VertexPositionColor> temp, int factor, float deltaX, float deltaY, float deltaZ, float h)
        {
            for (int i = 0; i < this.RealParent.VisualMap.Count; i++)
            {
                var s = this.RealParent.VisualMap[i].ConcreteNode.Geometry;
                int start = 0;
                for (int k = 0; k < s.Count - 1; k++)
                {
                    if (k == 0)
                    {
                        start = temp.Count;
                    }
                    var vc = s[k + 0].Normalize(Norm, Norm, Norm);
                    var vn = s[k + 1].Normalize(Norm, Norm, Norm);
                    var c = new Vector3((factor * vc.X) + deltaX, (factor * vc.Y) + deltaY, (factor * vc.Z) + deltaZ + h);
                    var n = new Vector3((factor * vn.X) + deltaX, (factor * vn.Y) + deltaY, (factor * vn.Z) + deltaZ + h);
                    temp.Add(new VertexPositionColor(c, XnaColor.Blue));
                    temp.Add(new VertexPositionColor(n, XnaColor.Blue));
                }
                temp.Add(temp[temp.Count - 1]);
                temp.Add(temp[start]);
            }
        }

        // Done!
        private static void ConnectLayers(IList<VertexPositionColor> temp)
        {
            int count = temp.Count/2;
            IList<VertexPositionColor> data = new List<VertexPositionColor>();
            for (int i = 0; i < count; ++i)
            {
                data.Add(temp[i]);
                data.Add(temp[i + count]);
            }

            for (int i = 0; i < data.Count; ++i)
            {
                temp.Add(data[i]);
            }

        }

        // Done!
        private void AddConnectors(IList<VertexPositionColor> temp, int factor, float deltaX, float deltaY, float deltaZ, float h)
        {
            for (int i = 0; i < this.RealParent.VisualMap.Count; i++)
            {
                var cn = this.RealParent.VisualMap[i].ConcreteNode;
                var vc = Vector.Average(cn.Geometry).Normalize(1000, 1000, 1000);
                var c = new Vector3((factor * vc.X) + deltaX, (factor * vc.Y) + deltaY, (factor * vc.Z) + deltaZ + h);
                var s = cn.Exits;
                for (int k = 0; k < s.Count; k++)
                {
                    var vp = s[k].Location.Normalize(1000, 1000, 1000);
                    var p = new Vector3((factor * vp.X) + deltaX, (factor * vp.Y) + deltaY, (factor * vp.Z) + deltaZ + h);
                    temp.Add(new VertexPositionColor(c, XnaColor.Red));
                    temp.Add(new VertexPositionColor(p, XnaColor.Red));
                }
            }
            for (int i = 0; i < this.RealParent.VisualMap.Count; i++)
            {
                var cn = this.RealParent.VisualMap[i].ConcreteNode;
                var s = cn.Exits;
                for (int k = 0; k < s.Count; k++)
                {
                    var vp = s[k].Location.Normalize(1000, 1000, 1000);
                    var p = new Vector3((factor * vp.X) + deltaX, (factor * vp.Y) + deltaY, (factor * vp.Z) + deltaZ + h);
                    if (cn.Exits[k].Exits.Count > 0 && cn.Exits[k].Exits[0] != null)
                    {
                        var vn = Vector.Average(cn.Exits[k].Exits[0].Geometry).Normalize(1000, 1000, 1000);
                        var n = new Vector3((factor * vn.X) + deltaX, (factor * vn.Y) + deltaY, (factor * vn.Z) + deltaZ + h);
                        temp.Add(new VertexPositionColor(p, XnaColor.Green));
                        temp.Add(new VertexPositionColor(n, XnaColor.Green));
                    }
                }
            }
        }

        #endregion

        // Done!
        private void StateChanged(object sender, EventArgs e)
        {
            this.BuildData();
        }

        // Make it WAY more efficient!
        protected override void Draw()
        {
            GraphicsDevice.Clear(new XnaColor(this.BackColor.R, this.BackColor.G, this.BackColor.B, this.BackColor.A));

            if (geometryData == null || geometryData.Length == 0)
            {
                return;
            }

            //// Spin the triangle according to how much time has passed.
            float time = (float)base.Timer.Elapsed.TotalSeconds;
            //time = 0;
            const float factor = 0.1f;

            float yaw = time * factor;
            float pitch = time * factor;
            float roll = time * factor;

            Effect.World = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);

            //SetUpVertices(ScaleFactor, DeltaX, DeltaY, H);
            //SetUpIndices();
            //CreateLabels();

            // Draw textured box
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;  // vertex order doesn't matter
            //GraphicsDevice.BlendState = BlendState.NonPremultiplied;    // use alpha blending
            //GraphicsDevice.DepthStencilState = DepthStencilState.None;  // don't bother with the depth/stencil buffer

            //Effect.Texture = this.anchorLabels;
            //Effect.TextureEnabled = true;
            //// Draw the data
            //Effect.CurrentTechnique.Passes[0].Apply();

            // -------------------------------------------------

            //GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertexData, 0, 4, indexData, 0, 2);

            // -------------------------------------------------

            Effect.Texture = null;
            Effect.TextureEnabled = false;
            Effect.Alpha = 1;

            // Draw the data
            Effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, geometryData, 0, geometryData.Length/2);
            //GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, nodeData, 0, nodeData.Length / 2);

            // -------------------------------------------------

        }
    }
}
