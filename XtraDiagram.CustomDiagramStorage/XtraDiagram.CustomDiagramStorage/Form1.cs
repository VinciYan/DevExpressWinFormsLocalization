using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using DevExpress.Diagram.Core;
using DevExpress.XtraDiagram;

namespace XtraDiagram.CustomDiagramStorage {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm {
        public Form1() {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            diagramControl1.InitializeRibbon(ribbonControl1);
            diagramControl1.OpenFile();
        }
        void OnShowingOpenDialog(object sender, DiagramShowingOpenDialogEventArgs e) {
            var diagramName = DiagramSelector.SelectDiagramToOpen();
            if(diagramName != null)
                e.DocumentSourceToOpen = diagramName;
            else
                e.Cancel = true;
        }
        void OnCustomLoadDocument(object sender, DiagramCustomLoadDocumentEventArgs e) {
            if(e.DocumentSource == null) {
                diagramControl1.NewDocument();
                Text = "(New Document)";
                return;
            }
            var diagramName = (string)e.DocumentSource;
            Text = diagramName;
            var diagramData = DiagramRepository.GetDiagramData(diagramName);
            if (diagramData != null)
                diagramControl1.LoadDocument(new MemoryStream(diagramData));
            e.Handled = true;
        }
        void OnShowingSaveDialog(object sender, DiagramShowingSaveDialogEventArgs e) {
            var diagramName = DiagramSelector.SelectDiagramToSave();
            if(diagramName != null)
                e.DocumentSourceToSave = diagramName;
            else
                e.Cancel = true;
        }
        void OnCustomSaveDocument(object sender, DiagramCustomSaveDocumentEventArgs e) {
            var diagramName = (string)e.DocumentSource;
            Text = diagramName;
            var stream = new MemoryStream();
            diagramControl1.SaveDocument(stream);
            var diagramData = stream.ToArray();
            DiagramRepository.SaveDiagramData(diagramName, diagramData);
            e.Handled = true;
        }
    }
}
