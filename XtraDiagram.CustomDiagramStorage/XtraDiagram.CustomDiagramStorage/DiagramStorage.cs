using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XtraDiagram.CustomDiagramStorage {
    public class DiagramStorage : DbContext {
        public DbSet<DiagramData> DiagramData { get; set; }
    }
    public class DiagramData {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
    public class DiagramRepository {
        public static IEnumerable<string> GetDiagramNames() {
            var storage = new DiagramStorage();
            return storage.DiagramData.Select(x => x.Name).ToList();
        }

        public static byte[] GetDiagramData(string diagramName) {
            var storage = new DiagramStorage();
            return storage.DiagramData.FirstOrDefault(x => x.Name == diagramName).Data;
        }
        public static void SaveDiagramData(string diagramName, byte[] diagramData) {
            var storage = new DiagramStorage();
            var diagramInfo = storage.DiagramData.FirstOrDefault(x => x.Name == diagramName);
            if (diagramInfo == null) {
                diagramInfo = new DiagramData() { Name = diagramName };
                storage.DiagramData.Add(diagramInfo);
            }
            diagramInfo.Data = diagramData;
            storage.SaveChanges();
        }
    }
}
