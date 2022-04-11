using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfMoverTests
{
    internal class SaveLoadTests
    {
        [Test]
        public static void Test_SaveLoad_AreIdentical()
        {
            string jsonFilePath = System.IO.Path.GetFullPath("rec1.json");
            Console.WriteLine(jsonFilePath);

            AbfMover.DailyRecord rec1 = new();
            rec1.Cells.Add(new AbfMover.CellRecord("id1", "color1", "comment1", "group1"));
            rec1.Cells.Add(new AbfMover.CellRecord("id2", "color2", "comment2", "group2"));
            rec1.Cells.Add(new AbfMover.CellRecord("id3", "color3", "comment3", "group3"));
            rec1.Operator = "test operator";
            rec1.Animal = "test animal";
            rec1.Bath = "test bath";
            rec1.Internal = "test internal";
            rec1.Drugs = "test drugs";
            rec1.Notes = "test notes";
            rec1.SaveJson(jsonFilePath);

            AbfMover.DailyRecord rec2 = AbfMover.DailyRecord.FromJson(jsonFilePath);

            AssertAreEqual(rec1, rec2);
        }

        private static void AssertAreEqual(AbfMover.DailyRecord rec1, AbfMover.DailyRecord rec2)
        {
            Assert.AreEqual(rec1.Version, rec2.Version);
            Assert.AreEqual(rec1.Operator, rec2.Operator);
            Assert.AreEqual(rec1.Animal, rec2.Animal);
            Assert.AreEqual(rec1.Bath, rec2.Bath);
            Assert.AreEqual(rec1.Internal, rec2.Internal);
            Assert.AreEqual(rec1.Drugs, rec2.Drugs);
            Assert.AreEqual(rec1.Notes, rec2.Notes);

            Assert.AreEqual(rec1.Count, rec2.Count);
            for (int i = 0; i < rec1.Count; i++)
            {
                AssertAreEqual(rec1.Cells[i], rec2.Cells[i]);
            }
        }

        private static void AssertAreEqual(AbfMover.CellRecord cell1, AbfMover.CellRecord cell2)
        {
            Assert.AreEqual(cell1.AbfID, cell2.AbfID);
            Assert.AreEqual(cell1.Color, cell2.Color);
            Assert.AreEqual(cell1.Comment, cell2.Comment);
            Assert.AreEqual(cell1.Group, cell2.Group);
        }
    }
}
