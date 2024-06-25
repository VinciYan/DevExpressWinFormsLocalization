using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace XtraDiagram.CustomDiagramStorage {
    public partial class DiagramSaveDialog : DiagramOpenDialog {
        public DiagramSaveDialog() {
            InitializeComponent();
        }

        protected override void OnSelectedItemChanged() {
            base.OnSelectedItemChanged();
            textEdit1.Text = SelectedItem;
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            SelectedItem = textEdit1.Text;
        }
    }
}