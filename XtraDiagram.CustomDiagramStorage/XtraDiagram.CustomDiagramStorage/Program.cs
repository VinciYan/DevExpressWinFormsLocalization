using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Diagram.Core.Localization;
using DevExpress.Internal;
using DevExpress.Utils.Localization;

namespace XtraDiagram.CustomDiagramStorage
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hans");

            DiagramControlLocalizer.QueryLocalizedString += DiagramControlLocalizer_QueryLocalizedString;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DbEngineDetector.PatchConnectionStringsAndConfigureEntityFrameworkDefaultConnectionFactory();
            Database.SetInitializer(new DiagramStorageInitializer());

            Application.Run(new Form1());
        }

        private static void DiagramControlLocalizer_QueryLocalizedString(object sender, XtraLocalizer.QueryLocalizedStringEventArgs e)
        {
            if (e.StringIDType == typeof(DiagramControlStringId))
            {
                if ((DiagramControlStringId)e.StringID == DiagramControlStringId.PrintPreview_BestFit)
                    e.Value = "适应页面";
                if ((DiagramControlStringId)e.StringID == DiagramControlStringId.DiagramCommand_QuickPrint)
                    e.Value = "快速打印";
                if ((DiagramControlStringId)e.StringID == DiagramControlStringId.DiagramCommand_SetPageParameters_PageSize_Header)
                    e.Value = "页面设置";
            }           
        }
    }
}
