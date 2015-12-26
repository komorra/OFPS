using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace OFPSEngine.Rendering
{
    public partial class ControlViewport : UserControl
    {
        private SwapChain swapChain;
        private Texture2D backBufferTexture;
        private RenderTargetView backBufferView;
        private Texture2D depthStencilTexture;
        private DepthStencilView depthStencilView;

        public event Action CustomRender = delegate { }; 

        public ControlViewport()
        {
            InitializeComponent();
        }

        private void ControlViewport_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;                    

            SwapChainDescription swd = new SwapChainDescription();
            swd.BufferCount = 1;
            swd.Flags = SwapChainFlags.None;
            swd.IsWindowed = true;
            swd.ModeDescription.Format = Format.R8G8B8A8_UNorm;
            swd.ModeDescription.Width = ClientSize.Width;
            swd.ModeDescription.Height = ClientSize.Height;
            swd.ModeDescription.RefreshRate = new Rational(60, 1);
            swd.OutputHandle = Handle;
            swd.SampleDescription = new SampleDescription(8, 31);
            swd.SwapEffect = SwapEffect.Discard;
            swd.Usage = Usage.RenderTargetOutput;
            swapChain = new SwapChain(Renderer.Current.Factory, Renderer.Current.Device, swd);

            backBufferTexture = swapChain.GetBackBuffer<Texture2D>(0);
            backBufferView = new RenderTargetView(Renderer.Current.Device, backBufferTexture);

            RecreateDepthStencilView(swd);
        }

        private void RecreateDepthStencilView(SwapChainDescription swd)
        {
            Texture2DDescription td = new Texture2DDescription();
            td.Width = ClientSize.Width;
            td.Height = ClientSize.Height;
            td.ArraySize = 1;
            td.BindFlags = BindFlags.DepthStencil;
            td.CpuAccessFlags = CpuAccessFlags.None;
            td.Format = Format.D24_UNorm_S8_UInt;
            td.MipLevels = 1;
            td.OptionFlags = ResourceOptionFlags.None;
            td.SampleDescription = swd.SampleDescription;
            td.Usage = ResourceUsage.Default;
            depthStencilTexture = new Texture2D(Renderer.Current.Device, td);
            depthStencilView = new DepthStencilView(Renderer.Current.Device, depthStencilTexture);
        }

        public void Render()
        {
            if (DesignMode || backBufferView == null) return;

            var context = Renderer.Current.Context;
            context.ClearDepthStencilView(depthStencilView,
                DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1, 0);
            context.ClearRenderTargetView(backBufferView, Color4.Black);
            context.Rasterizer.SetViewport(0, 0, ClientSize.Width, ClientSize.Height);
            context.OutputMerger.SetRenderTargets(depthStencilView, backBufferView);

            CustomRender();

            swapChain.Present(0, PresentFlags.None);
        }

        private void ControlViewport_Resize(object sender, EventArgs e)
        {
            if (depthStencilView != null)
            {
                depthStencilTexture.Dispose();
                depthStencilTexture = null;
                backBufferTexture.Dispose();
                backBufferTexture = null;

                depthStencilView.Dispose();               
                depthStencilView = null;
                backBufferView.Dispose();
                backBufferView = null;                
            }

            if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;

            Renderer.Current.Context.ClearState();

            swapChain.ResizeBuffers(1, ClientSize.Width, ClientSize.Height, Format.R8G8B8A8_UNorm, SwapChainFlags.None);

            var md = new ModeDescription();
            md.Width = ClientSize.Width;
            md.Height = ClientSize.Height;
            md.Format = Format.R8G8B8A8_UNorm;
            md.RefreshRate = new Rational(60, 1);                      
            swapChain.ResizeTarget(ref md);

            backBufferTexture = swapChain.GetBackBuffer<Texture2D>(0);
            backBufferView = new RenderTargetView(Renderer.Current.Device, backBufferTexture);

            RecreateDepthStencilView(swapChain.Description);
        }
    }
}
