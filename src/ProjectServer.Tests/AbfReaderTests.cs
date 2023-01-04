using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServer.Tests
{
    internal class AbfReaderTests
    {
        [Test]
        public void Test_AbfInfo_ABF2()
        {
            string abfPath = Path.Combine(SampleData.Folder, "abf2-with-tags.abf");
            AbfReader abf = new(abfPath);

            Assert.That(abf.SweepCount, Is.EqualTo(187));
            Assert.That(abf.SweepLengthSec, Is.EqualTo(2.0));
            Assert.That(abf.Protocol, Is.EqualTo("0402 VC 2s MT-50"));
            Assert.That(abf.Comments, Is.EqualTo("+TGOT @ 2.9 min, -TGOT @ 4.91 min"));
        }

        [Test]
        public void Test_AbfInfo_ABF1()
        {
            // TODO: support ABF1
            string abfPath = Path.Combine(SampleData.Folder, "abf1-with-tags.abf");
            AbfReader abf = new(abfPath);
            Assert.That(abf.Comments, Is.EqualTo("details are unavailable for ABF1 files"));
        }
    }
}
