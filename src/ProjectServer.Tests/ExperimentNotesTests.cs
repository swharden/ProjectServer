using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServer.Tests
{
    internal class ExperimentNotesTests
    {
        [Test]
        public void Test_ExperimentNotes_ToJson()
        {
            ExperimentNotes notes1 = new()
            {
                Title = "demo title",
                Description = "demo description",
                NotesTxt = "demo\ntext",
                NotesMd = "demo\nmarkdown",
            };

            string json = notes1.ToJson();
            ExperimentNotes notes2 = ExperimentNotes.FromJson(json);

            Assert.That(notes2.Title, Is.EqualTo(notes1.Title));
            Assert.That(notes2.Description, Is.EqualTo(notes1.Description));
            Assert.That(notes2.NotesTxt, Is.EqualTo(notes1.NotesTxt));
            Assert.That(notes2.NotesMd, Is.EqualTo(notes1.NotesMd));
        }
    }
}
