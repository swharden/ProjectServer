using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServer.Tests
{
    internal class AbfParentTests
    {
        [Test]
        public void Test_AbfFolder_Scan()
        {
            string testFolder = @"X:\Data\SD\DSI\PFC\abfs";
            Shared.AbfFolder f = Shared.AbfFolder.Scan(testFolder);
            Console.WriteLine(f);
            foreach (var p in f.AbfParents)
                Console.WriteLine(p);
        }
    }
}
