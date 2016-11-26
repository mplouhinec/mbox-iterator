using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mbox_iterator.Database;
using System.Text;

namespace mbox_iterator.Test
{
    [TestClass]
    public class HeaderFieldTest
    {
        [TestMethod]
        public void TestGetHeaderFromEmptyLine()
        {
            // Test empty string
            string line = "";
            HeaderField result = HeaderField.GetHeaderFromLine(line);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetHeaderFromUncorrectLine()
        {
            // Test string without ':'
            string line = "from test@test.fr";
            HeaderField result = HeaderField.GetHeaderFromLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetHeaderFromTooLongLine()
        {
            // Test string 
            StringBuilder sb = new StringBuilder();
            sb.Append("abcdefghi");
            sb.Append(":");
            for(int i = 0; i < 100; i++)
            {
                sb.Append("abcdefghij");
            }

            HeaderField result = HeaderField.GetHeaderFromLine(sb.ToString());
        }

        [TestMethod]
        public void TestGetHeaderFromLine()
        {
            string line = "from : test@test.fr ";
            HeaderField result = HeaderField.GetHeaderFromLine(line);
            Assert.IsNotNull(result);
            Assert.AreEqual("from", result.FieldName);
            Assert.AreEqual("test@test.fr", result.Value);
        }
    }
}
