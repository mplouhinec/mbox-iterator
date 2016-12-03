﻿using System;
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

        [TestMethod]
        [DeploymentItem(@"DataTest\mbox1.mbox", "data")]
        public void TestMessageFromStringMBox1()
        {
            string mboxData = File.ReadAllText(@"data\mbox1.mbox");
            PrivateType privateType = new PrivateType(typeof(Message));
            Message message = (Message)privateType.InvokeStatic("fromString", new object[] { mboxData });

            Assert.IsNotNull(message);
            Assert.AreEqual(5, message.Header.Fields.Count);
            Assert.AreEqual("How's that mail system project coming along?", message.Body);
        }

        [TestMethod]
        [DeploymentItem(@"DataTest\mbox1.mbox", "data")]
        public void TestGetMessagesFromMBox1()
        {
            string mboxData = File.ReadAllText(@"data\mbox1.mbox");

            var messages = Message.GetMessages(@"data\mbox1.mbox");

            Assert.IsNotNull(messages);
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual("How's that mail system project coming along?", messages[0].Body.Trim());
        }

        [TestMethod]
        [DeploymentItem(@"DataTest\mbox2.mbox", "data")]
        public void TestGetMessagesFromMBox2()
        {
            string mboxData = File.ReadAllText(@"data\mbox2.mbox");

            var messages = Message.GetMessages(@"data\mbox2.mbox");

            Assert.IsNotNull(messages);
            Assert.AreEqual(5, messages.Count);
        }

    }
}
