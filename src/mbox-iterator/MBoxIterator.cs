using mbox_iterator.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbox_iterator
{
    public class MBoxIterator : IEnumerable<Message>
    {


        #region Properties

        /// <summary>
        /// FilePath of the MboxFile
        /// </summary>
        public string FilePath { get; set; }

        #endregion

        #region Constructor

        public MBoxIterator() { }

        public MBoxIterator(string filePath)
        {
            FilePath = filePath;
        }

        #endregion

        #region Methods

        public IEnumerator<Message> GetEnumerator()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new Exception("THe FilePath property must not be null or empty.");

            if (!File.Exists(FilePath))
                throw new FileNotFoundException(string.Format("The file {0} doesn't exist.", FilePath));

            StringBuilder stringBuilderMessageBuffered = new StringBuilder();
            IList<Message> messages = new List<Message>();
            string line;

            using (FileStream fileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamReader streamReader = new StreamReader(bufferedStream))
                    {
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (line.StartsWith(Message.START_MESSAGE_STRING))
                            {
                                // Manage message in string builder.
                                var newMessage = Message.FromString(stringBuilderMessageBuffered.ToString());
                                if (newMessage != null)
                                    yield return newMessage;

                                // Start new message
                                stringBuilderMessageBuffered.Clear();
                                stringBuilderMessageBuffered.AppendLine(line);
                            }
                            else
                            {
                                stringBuilderMessageBuffered.AppendLine(line);
                            }
                        }

                        var lastMessage = Message.FromString(stringBuilderMessageBuffered.ToString());
                        yield return lastMessage;
                    }
                }
            }

            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
