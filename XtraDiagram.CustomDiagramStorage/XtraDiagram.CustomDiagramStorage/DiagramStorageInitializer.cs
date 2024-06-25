using DevExpress.Diagram.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.XtraDiagram;

namespace XtraDiagram.CustomDiagramStorage {
    class DiagramStorageInitializer : DropCreateDatabaseIfModelChanges<DiagramStorage> {
        protected override void Seed(DiagramStorage storage) {
            base.Seed(storage);
            var diagram = new DiagramControl();
            for(int i = 0; i < 5; i++) {
                diagram.Items.Add(new DiagramShape() {
                    Position = new DevExpress.Utils.PointFloat(200, 100 + i * 100),
                    Width = 100,
                    Height = 50,
                    Content = "Item " + (i + 1).ToString(),
                });
                if(i == 0)
                    continue;
                diagram.SelectedStencils = new StencilCollection(new string[] {
                DiagramToolboxRegistrator.Stencils.ElementAt(i).Id
                });
                using(var stream = new MemoryStream()) {
                    diagram.SaveDocument(stream);
                    var diagramData = new DiagramData() {
                        Name = (i + 1).ToString() + " items",
                        Data = stream.ToArray(),
                    };
                    storage.DiagramData.Add(diagramData);
                }
            }
            storage.SaveChanges();
        }
    }
}
