using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XtraDiagram.CustomDiagramStorage {
    public class DiagramSelector {
        public static string SelectDiagramToOpen() {
            var selector = new DiagramOpenDialog() { Text = "Choose a diagram to open" };
            return ShowDialogCore(selector);
        }

        public static string SelectDiagramToSave() {
            var selector = new DiagramSaveDialog() { Text = "Choose a save location" };
            return ShowDialogCore(selector);
        }

        protected static string ShowDialogCore(DiagramOpenDialog dialog) {
            dialog.ShowDialog();
            return dialog.DialogResult == DialogResult.OK ? dialog.SelectedItem : null;
        }
    }
}
