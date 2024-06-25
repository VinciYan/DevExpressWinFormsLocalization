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
    public partial class DiagramOpenDialog : DevExpress.XtraEditors.XtraForm {
        public string SelectedItem { get; set; }

        public DiagramOpenDialog() {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            PopulateListBox();
        }

        private void PopulateListBox() {
            if (!DesignMode)
                listBoxControl1.DataSource = DiagramRepository.GetDiagramNames();
        }

        private void listBoxControl1_MouseDoubleClick(object sender, MouseEventArgs e) {
            var itemIndex = listBoxControl1.IndexFromPoint(e.Location);
            if (itemIndex > -1) {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e) {
            OnSelectedItemChanged();
        }

        protected virtual void OnSelectedItemChanged() {
            SelectedItem = listBoxControl1.SelectedItem as string;
        }
    }
}