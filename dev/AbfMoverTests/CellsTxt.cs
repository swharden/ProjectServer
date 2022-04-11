using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AbfMoverTests
{
    internal class CellsTxt
    {
        [Test]
        public static void Test_CellsTxt_CanLoad()
        {
            AbfMover.CellRecord[] cells = AbfMover.CellsTxt.GetCellRecords("data/cells.txt");
            Assert.AreEqual(82, cells.Length);
            foreach (AbfMover.CellRecord cell in cells)
                Console.WriteLine(cell);
        }

        [Test]
        public static void Test_CellsTxt_SubFolderName()
        {
            Assert.AreEqual("1234-56-78", AbfMover.CellsTxt.GetSubfolderName("1234_56_78_9999"));
            Assert.AreEqual("12o45", AbfMover.CellsTxt.GetSubfolderName("12o45678"));
        }
    }
}
