using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OFPSEngine.Rendering;
using OFPSEngine.ResourceManagement;
using SharpDX;
using Matrix = SharpDX.Matrix;

namespace OFPSGame
{
    public partial class FormGame : Form
    {
        ControlViewport control;
        private Stopwatch gameLoopWatch;
        private Timer gameTimer;
        private Model3DResource model;

        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            var resolver = new GenericFileResolver("GameContent");
            var resourceManager = new ResourceManager(resolver);
            resourceManager.RegisterLoader(new Model3DLoader(), ".fbx");
            
            model = resourceManager.Load<Model3DResource>("stairs01_ph.fbx");

            control = new ControlViewport() {Dock = DockStyle.Fill};
            control.CustomRender += ControlOnCustomRender;
            Controls.Add(control);

            gameLoopWatch = Stopwatch.StartNew();
            gameTimer = new Timer();
            gameTimer.Interval = 15;
            gameTimer.Tick += GameTimerOnTick;
            gameTimer.Start();
        }

        private void ControlOnCustomRender()
        {
            var info = new DrawInfo();
            info.World = Matrix.Identity;
            info.Projection = Matrix.PerspectiveFovLH(1, control.ClientSize.Width/(float) ClientSize.Height, 0.1f, 1000);
            info.View = Matrix.LookAtLH(Vector3.BackwardLH*-10f + Vector3.Left*4, Vector3.Zero, Vector3.Up);
            Renderer.Current.DrawModel3D(model, info);
        }

        private void GameTimerOnTick(object sender, EventArgs eventArgs)
        {
            control.Render();
        }

        private void FormGame_Shown(object sender, EventArgs e)
        {
            if (DesignMode) return;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            //control.Render();
        }
    }
}
