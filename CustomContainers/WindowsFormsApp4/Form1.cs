using DevExpress.Diagram.Core;
using DevExpress.Diagram.Core.Native;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace WindowsFormsApp4 {
    public partial class Form1 : Form {
        const string MyContainersStencilName = "MyContainers";
        static readonly ContainerShapeDescription[] containerDescriptions;
        static Form1()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApp4.CustomContainers.xml"))
            {
                containerDescriptions = ShapeDescriptions.LoadDescriptionsFromXml(stream).OfType<ContainerShapeDescription>().ToArray();
            }
            DiagramContainerGalleryRegistrator.RegisterContainerShapes(containerDescriptions);
        }
        public Form1() {
            InitializeComponent();
            var customContainersStencil = DiagramStencil.Create(MyContainersStencilName, "Custom Containers", containerDescriptions);
            diagramControl1.OptionsBehavior.Stencils = new DiagramStencilCollection(DiagramToolboxRegistrator.Stencils.Concat(new[] { customContainersStencil }));
            diagramControl1.SelectedStencils = new StencilCollection(MyContainersStencilName, BasicShapes.StencilId);
        }
    }
}
