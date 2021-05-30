using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParser.Services.EventParser;

namespace WebParser.Test
{
    [TestClass]
    public class EventParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var parser = new EventParserService();
            var content = "Name 1;Descrption 1;2020-05-02T12:03+02:00;2020-05-02T12:03+02:00;";
            var events = parser.LoadData(content);
            const string Actual = "Name 1";
            Assert.AreEqual(events[0].Name, actual: Actual);
        }
    }
}
