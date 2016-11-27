using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mbox_iterator.Data;
using System.IO;

namespace mbox_iterator.Test
{
    
    [TestClass]
    public class MessageTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetMessagesFromEmptyFilePath()
        {
            string filePath = string.Empty;
            var result = Message.GetMessages(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestGetMessagesFromEmptyFileNotFound()
        {
            string filePath = @"Z:\blabla";
            var result = Message.GetMessages(filePath);
        }
    }
}
