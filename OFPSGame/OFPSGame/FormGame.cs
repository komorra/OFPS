using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OFPSEngine.ResourceManagement;

namespace OFPSGame
{
    public partial class FormGame : Form
    {
        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            var resolver = new GenericFileResolver("GameContent");
            var resourceManager = new ResourceManager(resolver);
            resourceManager.RegisterLoader(new Model3DLoader(), ".fbx");
            var model = resourceManager.Load<Model3DResource>("stairs01_ph.fbx");
        }
    }
}
