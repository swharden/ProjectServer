using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServer.Tests
{
    internal class AbfInfoTests
    {
        [Test]
        public void Test_AbfInfo_ReadFile()
        {
            string abfPath = @"X:/Data/SD/ACh-preliminary/abfs/2019_01_18_DIC2__0006.abf";
            Shared.AbfInfo info = Shared.AbfInfo.FromAbf(abfPath);
            Console.WriteLine(info);
        }

        [Test]
        public void Test_AbfReader_ReadFile()
        {
            string abfPath = @"X:/Data/SD/ACh-preliminary/abfs/2019_01_18_DIC2__0006.abf";
            Shared.MinimalAbfReader abf = new(abfPath);
            Assert.That(abf.SweepCount, Is.EqualTo(146));
            Assert.That(abf.SweepLengthSec, Is.EqualTo(10));
            Assert.That(abf.Protocol, Is.EqualTo("0406 VC 10s MT-50"));
            Assert.That(abf.Comments, Is.EqualTo("+ 50 uM CCh 3 min @ 10.07 min"));
        }
    }
}
