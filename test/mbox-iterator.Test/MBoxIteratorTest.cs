using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using mbox_iterator.Data;
using System.Diagnostics;

namespace mbox_iterator.Test
{
    /// <summary>
    /// Description résumée pour MBoxIteratorTest
    /// </summary>
    [TestClass]
    public class MBoxIteratorTest
    {
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestMBoxIteratorFilePathEmpty()
        {
            var filePath = string.Empty;
            var iterator = new MBoxIterator(filePath);
            foreach(var message in iterator)
            {
                Console.WriteLine(message.Header.Fields[0]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestMBoxIteratorFileNotFound()
        {
            var filePath = @"C:\MboxFile.mbox";
            var iterator = new MBoxIterator(filePath);
            foreach (var message in iterator)
            {
                Console.WriteLine(message.Header.Fields[0]);
            }
        }

        [TestMethod]
        [DeploymentItem(@"DataTest\mbox2.mbox", "data")]
        public void TestMBoxIteratorOK()
        {
            Stopwatch sw = new Stopwatch();

            var filePath = @"data\mbox2.mbox";
            var iterator = new MBoxIterator(filePath);
            List<Message> messages = new List<Message>();
            sw.Start();

            foreach (var message in iterator)
            {
                messages.Add(message);
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            Console.WriteLine("MessageReader RunTime " + elapsedTime);

            Assert.IsNotNull(messages);
            Assert.AreEqual(5, messages.Count);

        }
    }
}
