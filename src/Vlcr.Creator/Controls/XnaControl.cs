using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vlcr.Creator.Controls
{
    public abstract class XnaControl : UserControl
    {
        // Done!
        #region Automatic Properties

        public GraphicsDevice   GraphicsDevice  { get; private set; }
        public BasicEffect      Effect          { get; private set;}
        public Stopwatch        Timer           { get; private set; }

        #endregion

        // Done!
        #region Initialization

        // Done!
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (DesignMode == true)
            {
                return;
            }

            this.CreateGraphicsDevice();
            this.Timer = Stopwatch.StartNew();
            this.Effect = new BasicEffect(GraphicsDevice) { VertexColorEnabled = true, Alpha = 1.0f };
            Application.Idle += delegate { Invalidate(); };

            this.Initialize();

        }

        // Done!
        private void CreateGraphicsDevice()
        {
            var presentation = new PresentationParameters
            {
                BackBufferWidth         = Math.Max(ClientSize.Width, 1),
                BackBufferHeight        = Math.Max(ClientSize.Height, 1),
                BackBufferFormat        = SurfaceFormat.Color,
                DepthStencilFormat      = DepthFormat.Depth24,
                DeviceWindowHandle      = this.Handle,
                PresentationInterval    = PresentInterval.Immediate,
                IsFullScreen            = false,
                MultiSampleCount        = 100
            };

            this.GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, presentation);
        }

        #endregion

        // Done!
        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Enabled == true && this.Visible == true && this.GraphicsDevice != null)
            {
                SetViewport();
                Draw();
                Present();
            }
            else
            {
                e.Graphics.Clear(this.BackColor);
            }
        }

        // Bug: Fix!
        private void SetViewport()
        {
            GraphicsDevice.Viewport = new Viewport
            {
                X           = 0,
                Y           = 0,
                Width       = ClientSize.Width,
                Height      = ClientSize.Height,
                MinDepth    = 0,
                MaxDepth    = 1
            };
        }

        // Done!
        private void Present()
        {
            GraphicsDevice.Present(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height), null, this.Handle);
        }

        // Done!
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {}


        #endregion

        // Done!
        #region Abstract Methods

        // Done!
        protected virtual void Initialize()
        {
            this.Effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -130), Vector3.Zero, Vector3.Down);
            this.Effect.Projection = Matrix.CreatePerspectiveFieldOfView(1, GraphicsDevice.Viewport.AspectRatio, 1, 1000);
            this.GraphicsDevice.RasterizerState  = RasterizerState.CullNone;
            this.Effect.GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        protected abstract void Draw();

        #endregion
    }
}
